using System;
using PactNet;
using PactNet.Mocks.MockHttpService;


namespace TeamA.Consumer.Tests
{
    public class ConsumerMyApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 1234; } }
        public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

        public ConsumerMyApiPact()
        {
            PactBuilder = new PactBuilder();
            PactBuilder
                .ServiceConsumer("TeamA.Consumer")
                .HasPactWith("TeamB.Producer");
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}
