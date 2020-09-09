using CreditCardApplication.Domain;
using Moq;
using Xunit;

namespace CreditCardApplication.Tests
{
    public class CreditCardApplicationEvaluatorTests
    {
        [Fact(DisplayName = "Should accept high income applications")]
        public void Should_Accept_High_Income_Applications()
        {
            // Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new Domain.CreditCardApplication { GrossAnnualIncome = 100_000 };

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact(DisplayName = "Should refer young applications")]
        public void Should_Refer_Young_Applications()
        {
            // Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new Domain.CreditCardApplication { Age = 19 };

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact(DisplayName = "Should decline low income applications")]
        public void Should_Decline_Low_Income_Applications()
        {
            // Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            //mockValidator.Setup(x => x.IsValid("x")).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(
            //                x => x.IsValid(It.Is<string>(number => number.StartsWith("y"))))
            //             .Returns(true);
            //mockValidator.Setup(
            //                x => x.IsValid(It.IsInRange<string>("a", "z", Moq.Range.Inclusive)))
            //             .Returns(true);
            //mockValidator.Setup(
            //                x => x.IsValid(It.IsIn("z", "y", "x")))
            //             .Returns(true);
            mockValidator.Setup(
                            x => x.IsValid(It.IsRegex("[a-z]")))
                         .Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new Domain.CreditCardApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "a"
            };

            // Act
            var decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }
    }
}
