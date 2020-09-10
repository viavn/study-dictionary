using CreditCardApplication.Domain;
using Moq;
using System;
using Xunit;

namespace CreditCardApplication.Tests
{
    public class CreditCardApplicationEvaluatorTests
    {
        private Mock<IFrequentFlyerNumberValidator> mockValidator;
        private CreditCardApplicationEvaluator sut;

        public CreditCardApplicationEvaluatorTests()
        {
            mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.SetupAllProperties();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            sut = new CreditCardApplicationEvaluator(mockValidator.Object);
        }

        [Fact(DisplayName = "Should accept high income applications")]
        public void Should_Accept_High_Income_Applications()
        {
            // Arrange
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
            mockValidator.DefaultValue = DefaultValue.Mock;
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

        [Fact(DisplayName = "Should refer invalid frequent flyer applications")]
        public void Should_Refer_Invalid_Frequent_Flyer_Applications()
        {
            // Arrange
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            var application = new Domain.CreditCardApplication();

            // Act
            var decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        // [Fact]
        // public void Should_Decline_Low_Income_Applications_OutDemo()
        // {
        //     // Arrange
        //     var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

        //     bool isValid = true;
        //     mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

        //     var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
        //     var application = new Domain.CreditCardApplication
        //     {
        //         GrossAnnualIncome = 19_999,
        //         Age = 42
        //     };

        //     // Act
        //     var decision = sut.EvaluateUsingOut(application);

        //     // Assert
        //     Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        // }

        [Fact(DisplayName = "Should refer when license key expired")]
        public void Should_Refer_When_LicenseKey_Expired()
        {
            // Arrange
            // var mockLicenseData = new Mock<ILicenseData>();
            // mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");

            // var mockServiceInfo = new Mock<IServiceInformation>();
            // mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);

            // var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            // mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);
            // mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns(GetLicenseKeyExpiryString);
            var application = new Domain.CreditCardApplication { Age = 42 };

            // Act
            var decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact(DisplayName = "Should use detailed lookup for older applications")]
        public void Should_Use_Detailed_Lookup_For_Older_Applications()
        {
            // Arrange
            var application = new Domain.CreditCardApplication { Age = 30 };

            // Act
            sut.Evaluate(application);

            // Assert
            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        [Fact(DisplayName = "Should validate frequent flyer number for low income applications")]
        public void Should_Validate_Frequent_Flyer_Number_For_Low_Income_Applications()
        {
            // Arrange
            var application = new Domain.CreditCardApplication { FrequentFlyerNumber = "q" };

            // Act
            sut.Evaluate(application);

            // Assert
            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Should not validate frequent flyer number for high income applications")]
        public void Should_Not_Validate_Frequent_Flyer_Number_For_High_Income_Applications()
        {
            // Arrange
            var application = new Domain.CreditCardApplication { GrossAnnualIncome = 100_000 };

            // Act
            sut.Evaluate(application);

            // Assert
            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Should check license key for low income applications")]
        public void Should_Check_License_Key_For_Low_Income_Applications()
        {
            // Arrange
            var application = new Domain.CreditCardApplication { GrossAnnualIncome = 99_000 };

            // Act
            sut.Evaluate(application);

            // Assert
            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
        }

        [Fact(DisplayName = "Should detailed lookup for older applications")]
        public void Should_Detailed_Lookup_For_Older_Applications()
        {
            // Arrange
            var application = new Domain.CreditCardApplication { Age = 30 };

            // Act
            sut.Evaluate(application);

            // Assert
            mockValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed, Times.Once);
            //mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(), Times.Once);

            //mockValidator.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Should refer when frequent flyer validation error")]
        public void Should_Refer_When_Frequent_Flyer_Validation_Error()
        {
            // Arrange
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>()))
                         .Throws(new Exception("Custom message"));

            var application = new Domain.CreditCardApplication { Age = 42 };

            // Act
            var decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void LinqToMocks()
        {
            // Arrange
            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var mockValidator = Mock.Of<IFrequentFlyerNumberValidator>
                (
                    validator =>
                        validator.ServiceInformation.License.LicenseKey == "OK" &&
                        validator.IsValid(It.IsAny<string>()) == true
                );

            var application = new Domain.CreditCardApplication { Age = 25 };

            //Act
            var decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }


        string GetLicenseKeyExpiryString()
        {
            return "EXPIRED";
        }
    }
}
;