using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using ToDoReminder.Share;

namespace ToDoReminder.Client.Services.Http
{
    public class HttpRestClient
    {
        private readonly string _url;
        private readonly RestClient _client;

        public HttpRestClient(string url)
        {
            _url = url;
            _client = new RestClient();
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            _client.BaseUrl  = new Uri($"{_url}{baseRequest.Route}");
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
            }
            else
            {
                return new ApiResponse(response.ErrorMessage);
            }
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            _client.BaseUrl  = new Uri($"{_url}{baseRequest.Route}");
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            }
            else
            {
                return new ApiResponse<T>(response.ErrorMessage);
            }
        }











    }
}
