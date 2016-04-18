using System.Net.Http;
using Microsoft.Owin.Testing;
using PactNet;
using Xunit;

namespace TeamB.Producer.Tests
{
    public class SomethingApiTests
    {
        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            //Arrange
            IPactVerifier pactVerifier = new PactVerifier(() => { }, () => { }); 

            pactVerifier
                .ProviderState("There is a something with id 'tester'",
                    setUp: AddTesterIfItDoesntExist); //NOTE: We also have tearDown

            //Act / Assert
            using (var testServer = TestServer.Create<Startup>()) 
            {
                pactVerifier
                    .ServiceProvider("TeamB Producer / Something API", testServer.HttpClient)
                    .HonoursPactWith("Consumer")
                    .PactUri("../../../TeamA.Consumer.Tests/pacts/teama.consumer-teamb.producer.json")
                    .Verify(); 
            }
        }

        private void AddTesterIfItDoesntExist()
        {
            //Logic to add the 'tester' something
        }
    }
}
