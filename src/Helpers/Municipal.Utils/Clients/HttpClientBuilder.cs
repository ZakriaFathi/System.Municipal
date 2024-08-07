using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Municipal.Utils.Exceptions;

namespace Municipal.Utils.Clients
{
    public class HttpClientBuilder
    {
        private readonly HttpClient _client;

        private readonly HttpRequestMessage _httpRequest;

        public HttpClientBuilder(HttpClient httpClient)
        {
            _client = httpClient;
            _httpRequest = new HttpRequestMessage();
        }

        public static HttpClientBuilder CreateClient(HttpClient httpClient) => new(httpClient);

        public HttpClientBuilder WithMethod(HttpMethodType methodType = HttpMethodType.Post)
        {
            _httpRequest.Method = methodType == HttpMethodType.Post ? new HttpMethod("POST") : new HttpMethod("GET");
            return this;
        }

        public HttpClientBuilder WithContent(string content)
        {

            _httpRequest.Content = string.IsNullOrEmpty(content)
                ? null
                : new StringContent(content, Encoding.UTF8, "application/json");
            return this;
        }

        public HttpClientBuilder WithFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content)
        {
            var encodedContent = new FormUrlEncodedContent(content);

            _httpRequest.Content = encodedContent;
            return this;
        }


        public HttpClientBuilder WithApiKey(string apiKey)
        {
            _httpRequest.Headers.Add("ApiKey", apiKey);
            return this;
        }

        public HttpClientBuilder AddToHeader(string key, string value)
        {
            _httpRequest.Headers.Add(key, value);
            return this;
        }

        public HttpClientBuilder WithHeader(params HeaderParams[] headerValues)
        {
            if (headerValues.Length == 0) return this;
            foreach (var header in headerValues) _httpRequest.Headers.Add(header.Key, header.Value);
            return this;
        }

        public HttpClientBuilder WithUrl(string baseUrl, string endPoint)
        {
            _httpRequest.RequestUri = new Uri(baseUrl.Trim() + endPoint.Trim());
            return this;
        }

        public record HeaderParams(string Key, string Value);

        public async Task<T> Send<T>()
        {
            var response = await _client.SendAsync(_httpRequest);

            var content = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> Send()
        {
            var response = await _client.SendAsync(_httpRequest);

            var content = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync();

            return content;
        }

        public async Task<T> SendAndValidate<T>() where T : Root
        {
            var response = await _client.SendAsync(_httpRequest);

            var content = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync();

            var deserializeObject = JsonConvert.DeserializeObject<T>(content);

            if (deserializeObject is not null && deserializeObject.Status.Any(x => x.Value != 1000) &&
                deserializeObject.Status.Any(x => x.Value != 2010))
                throw new AppException(deserializeObject.Status.FirstOrDefault()?.Value + "_" +
                                       deserializeObject.Status.FirstOrDefault()?.Message);

            return deserializeObject;
        }
    }

    public class Root
    {
        public List<Status> Status { get; set; }
    }

    public class Root<T> : Root
    {
        public T Data { get; set; }
    }

    public class Status
    {
        public int Value { get; set; }
        public string Message { get; set; }
    }
    public enum HttpMethodType
    {
        Post = 1,
        Get,
    }
}
