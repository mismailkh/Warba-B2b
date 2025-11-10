using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using System.Net.Http;
using WB.Shared.Dtos;

namespace WB.Admin.Services
{
    public class ApiClientService
    {
        #region variables declaration
        private readonly IConfiguration _config;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructor

        public ApiClientService(IConfiguration config, ProtectedLocalStorage localStorage, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Get Client
        private HttpClient GetClient(string clientName)
        {
            return _httpClientFactory.CreateClient(clientName);
        }
        #endregion

        #region Send POST with Body
        public async Task<ApiCallResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest requestData, bool hasReturnData = true, bool isApiKeyAuth = false)
        {
            try
            {
                var client = GetClient("WB");
                var request = new HttpRequestMessage(method, endpoint)
                {
                    Content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                };
                if (isApiKeyAuth)
                {
                    request.Headers.Add("WBApiKey", _config.GetValue<string>("WBApiKey"));
                }
                else
                {
                    var token = await _localStorage.GetAsync<string>("Token");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                }

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    object resultData = null;
                    if (hasReturnData)
                    {
                        resultData = await response.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false };
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<BadRequestResponse>();
                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false, ResultData = error };
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccessStatusCode = false, ResultData = new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message } };
            }
        }
        #endregion

        #region Send GET with Params
        public async Task<ApiCallResponse> SendGetAsync<TResponse>(string endpointWithQuery, bool hasReturnData = true, bool isApiKeyAuth = false)
        {
            try
            {
                var client = GetClient("WB");
                var request = new HttpRequestMessage(HttpMethod.Get, endpointWithQuery);
                if (isApiKeyAuth)
                {
                    request.Headers.Add("WBApiKey", _config.GetValue<string>("WBApiKey"));
                }
                else
                {
                    var token = await _localStorage.GetAsync<string>("Token");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                }

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    object resultData = null;
                    if (hasReturnData)
                    {
                        resultData = await response.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false };
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<BadRequestResponse>();
                    return new ApiCallResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false, ResultData = error };
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccessStatusCode = false, ResultData = new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message } };
            }
        }
        #endregion
    }
}
