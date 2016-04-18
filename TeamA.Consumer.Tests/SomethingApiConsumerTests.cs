using System.Collections.Generic;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace TeamA.Consumer.Tests
{
    
    public class SomethingApiConsumerTests : IClassFixture<ConsumerMyApiPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public SomethingApiConsumerTests(ConsumerMyApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions(); 
        }

        [Fact]
        public void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            //Arrange
            _mockProviderService
                .Given("There is a something with id 'tester'")
                .UponReceiving("A GET request to retrieve the something")
                .With(GetTesterSomethingRequest())
                .WillRespondWith(GetTesterSomethingResponse()); 

            var consumer = new SomethingApiClient(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.GetSomething("tester");

            //Assert
            Assert.Equal("tester", result.id);
            _mockProviderService.VerifyInteractions();
        }
     
        private static ProviderServiceRequest GetTesterSomethingRequest()
        {
            return new ProviderServiceRequest
            {
                Method = HttpVerb.Get,
                Path = "/somethings/tester",
                Headers = new Dictionary<string, string>
                {
                    { "Accept", "application/json" }
                }
            };
        }
        private static ProviderServiceResponse GetTesterSomethingResponse()
        {
            return new ProviderServiceResponse
            {
                Status = 200,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json; charset=utf-8" }
                },
                Body = new //NOTE: Note the case sensitivity here, the body will be serialised as per the casing defined
                {
                    id = "tester",
                    firstName = "Totally",
                    lastName = "Awesome",
                }
            };
        }

    }
}