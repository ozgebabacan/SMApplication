using RestSharp;
using SMApplication.Models;
using SMApplication.Settings;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using SMApplication.Models.Enum;

namespace SMApplication.Service
{
    public class SalesService : BaseService
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public SalesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public SaleResponse GetSalesDetail(SaleDetailRequest request)
        {
            var result = new SaleResponse();

            var result2 = JsonConvert.DeserializeObject<SaleDetailServiceResponse>(System.IO.File.ReadAllText("order.json"));

            result.Orders = result2.Response.Orders;


            //try
            //{
            //    RestClient client = new RestClient(ServiceSettings.BaseUrl);
            //    RestRequest restRequest = new RestRequest(ServiceSettings.UrlSales, Method.POST);

            //    restRequest.AddJsonBody(request);
            //    var serviceResult = client.Execute<SaleDetailServiceResponse>(restRequest).Data;

            //    logger.Info(JsonConvert.SerializeObject(serviceResult));

            //    if (!serviceResult.Result)
            //    {
            //        throw new Exception("Servis isteği sırasında hata alındı.");
            //    }

            //    result.Orders = serviceResult.Response.Orders;

            //}
            //catch (Exception ex)
            //{
            //    result.Status = Status.Error;
            //    result.Message = $"İşlem sırasında hata alındı. Detayları : {ex.Message}";

            //    logger.Error(ex);
            //}

            return result;
        }

        public TokenResponse GetToken()
        {
            var result = new TokenResponse();

            try
            {
                RestClient client = new RestClient(ServiceSettings.BaseUrl);
                RestRequest restRequest = new RestRequest(ServiceSettings.UrlLogin, Method.POST);

                var user = new
                {
                    username = ServiceSettings.Username,
                    password = ServiceSettings.Password,
                };
                restRequest.AddJsonBody(user);
                var serviceResult = client.Execute<TokenServiceResponse>(restRequest).Data;

                logger.Info(JsonConvert.SerializeObject(serviceResult));

                if (!serviceResult.Result)
                {
                    throw new Exception("Servis isteği sırasında hata alındı.");
                }

                result = serviceResult.Response;
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Message = $"İşlem sırasında hata alındı. Detayları : {ex.Message}";

                logger.Error(ex);
            }

            return result;
        }
    }
}
