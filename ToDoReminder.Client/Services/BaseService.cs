using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

        protected readonly HttpRestClient client;
        protected readonly string serviceName;

        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{serviceName}/Add";
            request.Parameter = entity;
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse<int>> DeleteAsync(int id)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.DELETE;
            request.Route = $"{serviceName}/Delete?id={id}";
            return await client.ExecuteAsync<int>(request);
        }

        public async Task<ApiResponse<PagedList<T>>> GetPagedListAsync(QueryParameter parameter)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{serviceName}/QueryList?pageIndex={parameter.IndexPage}" +
                $"&pageSize={parameter.SizePage}" +
                $"&search={parameter.Search}";
            return await client.ExecuteAsync<PagedList<T>>(request);
        }

        public async Task<ApiResponse<T>> GetSingleAsync(int id)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{serviceName}/Get?id={id}";
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T entity)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{serviceName}/Update";
            request.Parameter = entity;
            return await client.ExecuteAsync<T>(request);
        }
    }
}
