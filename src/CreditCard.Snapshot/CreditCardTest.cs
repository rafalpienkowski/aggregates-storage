using System;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CreditCard.Snapshot
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

            var snapshot = creditCard.GetSnapshot();
            snapshot.AvaliableLimit.Should().Be(50);
        }

        [Fact]
        public void CreditCard_Repay_ShouldIncreaseLimit()
        {
            var creditCard = new CreditCardBuilder()
                .WithLimit(100)
                .Build();

            creditCard.Repay(50);

            var snapshot = creditCard.GetSnapshot();
            snapshot.AvaliableLimit.Should().Be(150);
        }

        [Fact]
        public async Task CreditCard_Should_BeSavedAndRestoredWithGivenLimit()
        {
            var ownerId = Guid.NewGuid();
            var owner = new AccountOwnerBuilder()
                .WithId(ownerId)
                .Build();

            var expectedLimit = 100;
            var creditCardId = Guid.NewGuid();
            var creditCard = new CreditCardBuilder()
                .WithId(creditCardId)
                .WithLimit(expectedLimit)
                .WithOwnerId(ownerId)
                .Build();

            using(var context = new CreditCardContext())
            {
                var accountOwnerRepository = new AccountOwnerRepository(context);
                var creditCardRepository = new CreditCardRepository(context);

                await accountOwnerRepository.Add(owner);
                await creditCardRepository.Add(creditCard);
                await context.SaveChangesAsync();

                var storedCreditCard = await creditCardRepository.Get(creditCardId);

                storedCreditCard.Should().NotBeNull();
                var storedSnapshot = storedCreditCard.GetSnapshot();
                storedSnapshot.AvaliableLimit.Should().Be(expectedLimit);
            }
        }

        [Fact]
        public async Task CreditCardReport_Should_ShowValidData()
        {
            var ownerId = Guid.NewGuid();
            var ownerName = $"Name for {ownerId}";
            var owner = new AccountOwnerBuilder()
                .WithId(ownerId)
                .WithName(ownerName)
                .Build();


            var expectedLimit = 512.11m;
            var creditCardId = Guid.NewGuid();
            var creditCard = new CreditCardBuilder()
                .WithId(creditCardId)
                .WithLimit(expectedLimit)
                .WithOwnerId(ownerId)
                .Build();

            using(var context = new CreditCardContext())
            {
                var accountOwnerRepository = new AccountOwnerRepository(context);
                var creditCardRepository = new CreditCardRepository(context);

                await accountOwnerRepository.Add(owner);
                await creditCardRepository.Add(creditCard);
                await context.SaveChangesAsync();

                var reports = new CreditCardReports();

                var reportResult = await reports.Generate();

                var expectedEntry = reportResult.FirstOrDefault(rr => rr.Owner == ownerName);

                expectedEntry.Should().NotBeNull();
                expectedEntry.Limit.Should().Be(expectedLimit);
            }
        }
    }
}
