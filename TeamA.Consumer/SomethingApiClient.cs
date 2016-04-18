using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace TeamA.Consumer
{
    public class SomethingApiClient
    {
        public string BaseUri { get; set; }

        public SomethingApiClient(string baseUri = null)
        {
            BaseUri = baseUri ?? "http://my-api";
        }

        public Something GetSomething(string id)
        {
            string reasonPhrase;

            using (var client = new HttpClient { BaseAddress = new Uri(BaseUri) })
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/somethings/" + id);
                request.Headers.Add("Accept", "application/json");

                var response = client.SendAsync(request);

                var content = response.Result.Content.ReadAsStringAsync().Result;
                var status = response.Result.StatusCode;

                reasonPhrase = response.Result.ReasonPhrase; //NOTE: any Pact mock provider errors will be returned here and in the response body

                request.Dispose();
                response.Dispose();

                if (status == HttpStatusCode.OK)
                {
                    return !String.IsNullOrEmpty(content) ?
                        JsonConvert.DeserializeObject<Something>(content)
                        : null;
                }
            }

            throw new Exception(reasonPhrase);
        }
    }
}
