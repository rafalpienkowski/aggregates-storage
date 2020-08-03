using System.Threading.Tasks;
using CreditCard.ES.Events.CreditCard;
using CreditCard.ES.Projections;
using FluentAssertions;
using Xunit;

namespace CreditCard.ES
{
    public class CreditCardTest
    {
        [Fact]
        public void CreaditCard_LimitReasign_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.AssignLimit(123);

            creditCard.Changes.Should().BeEmpty();
        }

        [Fact]
        public void CreditCard_WithdrawWithoutAssignedLimit_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .Build();

            creditCard.Withdraw(100);

            creditCard.Changes.Should().BeEmpty();
        }

        [Fact]
        public void CreditCard_WithdrawOverLimit_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Withdraw(150);

            creditCard.Changes.Should().BeEmpty();
        }

        [Fact]
        public void CreditCard_WithdrawInLimit_ShouldDecreaseLimit()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Withdraw(60);

            creditCard.Changes.Should().ContainEquivalentOf(new CreditCardWithdrawed(creditCard.Id, 60));
        }

        [Fact]
        public void CreditCard_Repay_ShouldIncreaseLimit()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Repay(70);

            creditCard.Changes.Should().ContainEquivalentOf(new CreditCardRepaid(creditCard.Id, 70));
        }

        [Fact]
        public void CreditCard_Should_BeSavedAndRestored()
        {
            var owner = new AccountOwnerBuilder().Build();

            var store = new MartenEventStore();

            using (var session = store.CreateSession())
            {
                var ownerRepository = new AccountOwnerRepository(session);
                ownerRepository.Add(owner);

                var creditCard = new CreditCardBuilder()
                    .WithLimit(100)
                    .WithOwnerId(owner.Id)
                    .DontFlushEvents()
                    .Build();

                var repository = new CreditCardRepository(session);
                repository.Add(creditCard);

                creditCard.Withdraw(60);
                creditCard.Repay(150);
                creditCard.Withdraw(55);
                repository.Save(creditCard);

                var creditCardLimit = session.Load<CreditCardLimit>(creditCard.Id);
                creditCardLimit.Limit.Should().Be(135);
                var accountOwnerName = session.Load<AccountOwnerName>(creditCardLimit.AccountOwnerId);
                accountOwnerName.Name.Should().Be("Dick the Quick");

                owner.Rename("Dick's new name");
                ownerRepository.Save(owner);
                accountOwnerName.Name.Should().Be("Dick's new name");

                var owner2 = MartenEventStore.GetStream<AccountOwner>(session, owner.Id, 1);
            }
        }
    }
}