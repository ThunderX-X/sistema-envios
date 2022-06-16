using Microsoft.Extensions.Options;
using Shipments.Service.Models;
using System.Net;

namespace Shipments.Service.Proxies
{
    public class DeliveryPointsProxy
    {
        string apiUrl;
        HttpClient httpClient;
        ILogger<DeliveryPointsProxy> logger;

        public DeliveryPointsProxy(
            IOptions<DeliveryPointsUrl> apiUrl,
            HttpClient httpClient,
            ILogger<DeliveryPointsProxy> logger)
        {
            this.apiUrl = apiUrl.Value.Url;
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<bool> ExistDeliveryPointAsync(string id)
        {
            
            string getByIdUrl = $"{apiUrl}/deliveryPoints/{id}";
            logger.LogInformation($"Trying get {getByIdUrl}");
            try
            {
                HttpResponseMessage? response = await httpClient.GetAsync(getByIdUrl);

                logger.LogInformation(
                    $"Delivery Points service returned status: ${response?.StatusCode}" +
                    $" with body: {response?.Content?.ToString()}");
                return response?.StatusCode == HttpStatusCode.OK;

            }
            catch (Exception e)
            {
                logger.LogCritical(
                    $"Critical error when getting delivery point with url: {getByIdUrl}"
                    );
                logger.LogCritical(e.Message);

                return false;
            }
        }
    }
}
