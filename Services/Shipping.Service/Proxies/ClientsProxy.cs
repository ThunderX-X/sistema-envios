using Microsoft.Extensions.Options;
using Shipments.Service.Models;
using System.Net;

namespace Shipments.Service.Proxies
{
    public class ClientsProxy
    {
        string apiUrl;
        HttpClient httpClient;
        ILogger<ClientsProxy> logger;

        public ClientsProxy(
            IOptions<ClientUrl> apiUrl,
            HttpClient httpClient,
            ILogger<ClientsProxy> logger)
        {
            this.apiUrl = apiUrl.Value.Url;
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<bool> ExistClientAsync(string id)
        {
            string getByIdUrl = $"{apiUrl}/clients/{id}";
            try { 
                HttpResponseMessage? response = await httpClient.GetAsync(getByIdUrl);
                
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                logger.LogCritical($"Critical error when getting client with url: {getByIdUrl}");
                logger.LogCritical(e.Message);
                
                return false;
            }
            
        }

    }
}
