using Newtonsoft.Json;
using System;
using System.Text;

namespace GPTClientTest
{
    internal class Program
    {
        private const string _apiKey = "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";
        private const string _baseURL = "https://api.openai.com/v1/chat/completions";
        private const string _model = "gpt-3.5-turbo";

        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseURL);

            // Add the Authorization header with your API key
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            Console.WriteLine("Enter a prompt: ");
            do
            {
                string userprompt = Console.ReadLine()!;

                if (userprompt == "exit")
                {
                    break;
                }

                var requestBody = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = userprompt
                        }
                    }
                };
               
                string jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                StringContent content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                // Send the request
                // Send a POST request to the API
                HttpResponseMessage response = await client.PostAsync(_baseURL, content);

                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic responseObj = JsonConvert.DeserializeObject(responseBody)!;

                string contentStr = responseObj.choices[0].message.content;

                Console.WriteLine(contentStr);

            } while (true);
        }
    }
}