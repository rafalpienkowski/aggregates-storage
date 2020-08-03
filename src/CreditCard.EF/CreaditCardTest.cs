using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CreditCard.EF
{
    public class CreditCardTest
    {
        [Fact]
        public void CreaditCard_LimitReasign_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            Action act = () => creditCard.AssignLimit(123);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Limit to the card can be assigned only once");
        }

        [Fact]
        public void CreditCard_WithdrawWithoutAssignedLimit_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .Build();

            Action act = () => creditCard.Withdraw(100);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Lack of funds");
        }

        [Fact]
        public void CreditCard_WithdrawOverLimit_ShouldBeImposible()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            Action act = () => creditCard.Withdraw(150);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Lack of funds");
        }

        [Fact]
        public void CreditCard_WithdrawInLimit_ShouldDecreaseLimit()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Withdraw(50);

            creditCard.AvaliableLimit.Should().Be(50);
        }

        [Fact]
        public void CreditCard_Repay_ShouldIncreaseLimit()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Repay(50);

            creditCard.AvaliableLimit.Should().Be(150);
        }

        [Fact]
        public async Task CreditCard_Should_BeSavedAndRestored()
        {
            var owner = new AccountOwnerBuilder().Build();

            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .WithOwnerId(owner.Id)
                .Build();

            using(var context = new CreditCardContext())
            {
                var accountOwnerRepository = new AccountOwnerRepository(context);
                var creditCardRepository = new CreditCardRepository(context);

                await accountOwnerRepository.Add(owner);
                await creditCardRepository.Add(creditCard);
                await context.SaveChangesAsync();

                var storedCreditCard = await creditCardRepository.Get(creditCard.Id);

                storedCreditCard.Should().NotBeNull();
                storedCreditCard.AvaliableLimit.Should().Be(creditCard.AvaliableLimit);
                storedCreditCard.OwnerId.Should().Be(creditCard.OwnerId);
            }
        }
    }
}