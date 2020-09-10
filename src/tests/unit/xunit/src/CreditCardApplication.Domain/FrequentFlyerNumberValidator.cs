namespace CreditCardApplication.Domain
{
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        public IServiceInformation ServiceInformation => throw new System.NotImplementedException();

        public ValidationMode ValidationMode
        {
            get => throw new System.NotImplementedException("For demo purposes");
            set => throw new System.NotImplementedException("For demo purposes");
        }

        // public string LicenseKey => throw new System.NotImplementedException("For demo purposes");

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new System.NotImplementedException("Simulate this real dependency being hard to use");
        }

        public void IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new System.NotImplementedException("Simulate this real dependency being hard to use");
        }
    }
}
