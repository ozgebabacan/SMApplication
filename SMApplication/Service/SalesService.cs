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
using NLog.StructuredLogging.Json;
using SMApplication.Models.Enum;

namespace SMApplication.Service
{
    /// <summary>
    /// Servis istekleri yaparak modellerin doldurulduğu servis classı
    /// </summary>
    public class SalesService : BaseService
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public SalesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>
        /// request modeli doldurularak Response modeli alınır.
        /// </summary>
        /// <returns>Response</returns>
        private Response GetSalesHelp()
        {
            var tokenResult = GetToken();

            var serviceResult = GetSales(new SaleDetailRequest()
            {
                ApiCode = tokenResult.ApiCode,
                StoreId = 37814,
                OrderStatus = "Completed",
                InvoiceStatus = 0
            });

            return serviceResult;
        }

        /// <summary>
        /// GetSales servisine istek yapılarak Response modelinin doldurulması sağlanır.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Response GetSales(SaleDetailRequest request)
        {
            var result = new Response();

            //var result2 = JsonConvert.DeserializeObject<SaleDetailServiceResponse>(System.IO.File.ReadAllText("order.json"));

            //result.Orders = result2.Response.Orders;

            try
            {
                RestClient client = new RestClient(ServiceSettings.BaseUrl);
                RestRequest restRequest = new RestRequest(ServiceSettings.UrlSales, Method.POST);

                restRequest.AddJsonBody(request);
                var serviceResult = client.Execute<SaleDetailServiceResponse>(restRequest).Data;

                logger.ExtendedInfo("Sale Details", new { response = JsonConvert.SerializeObject(serviceResult) });

                if (!serviceResult.Result)
                {
                    throw new Exception("Servis isteği sırasında hata alındı.");
                }

                result.Orders = serviceResult.Response.Orders;

            }
            catch (Exception ex)
            {
                logger.Error(ex);

                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Detay sayfası için Order list filtrelenir. Detay modeli doldurulur.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public SaleDetailResponse GetOrderDetail(int orderId)
        {
            var result = new SaleDetailResponse();

            try
            {
                var orderList = GetSalesHelp();

                var order = orderList.Orders.Find(x => x.OrderId == orderId);

                result.Order = new OrderDTO()
                {
                    Address = order.Address,
                    City = order.City,
                    District = order.District,
                    Fullname = order.Fullname,
                    IntegrationOrderCode = order.IntegrationOrderCode,
                    OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
                    OrderId = order.OrderId,
                    OrderStatus = order.OrderStatus,
                    Telephone = order.Telephone,
                    PersonalIdentification = order.PersonalIdentification,
                    PostalCode = order.PostalCode,
                    TaxAuthority = order.TaxAuthority,
                    TaxNumber = order.TaxNumber
                };

                result.OrderDetail = order.OrderDetails.Select(x => new OrderDetailDTO()
                {
                    Price = x.Price,
                    CargoCompany = x.CargoCompany,
                    CargoDate = x.CargoDate.ToString("dd/MM/yyyy"),
                    ProductName = x.ProductName,
                    Quantity = x.Quantity
                }).ToList();

            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Message = $"İşlem sırasında hata alındı. Detayları : {ex.Message}";

                logger.ExtendedError("TokenError", new { response = JsonConvert.SerializeObject(ex) });
            }
            return result;
        }
        
        /// <summary>
        /// Liste sayfası için tüm order listesi dto olarak doldurulur.
        /// </summary>
        /// <returns></returns>
        public SaleResponse GetOrder()
        {
            var result = new SaleResponse();

            try
            {
                var serviceResult = GetSalesHelp();

                result.Orders = serviceResult.Orders.Select(x => new OrderDTO()
                {
                    Address = x.Address,
                    City = x.City,
                    District = x.District,
                    Fullname = x.Fullname,
                    TaxNumber = x.TaxNumber,
                    IntegrationOrderCode = x.IntegrationOrderCode,
                    OrderDate = x.OrderDate.ToString("dd/MM/yyyy"),
                    OrderId = x.OrderId,
                    OrderStatus = x.OrderStatus,
                    Telephone = x.Telephone,
                    PersonalIdentification = x.PersonalIdentification,
                    PostalCode = x.PostalCode,
                    TaxAuthority = x.TaxAuthority
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Message = $"İşlem sırasında hata alındı. Detayları : {ex.Message}";

                logger.ExtendedError("TokenError", new { response = JsonConvert.SerializeObject(ex) });
            }

            return result;
        }

        /// <summary>
        /// GetSales servis isteği için gerekli olan apicode alanı doldurulur.
        /// </summary>
        /// <returns></returns>
        private TokenResponse GetToken()
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
                
                logger.ExtendedInfo("Token", new {response = JsonConvert.SerializeObject(serviceResult) } );

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

                logger.ExtendedError("TokenError", new { response = JsonConvert.SerializeObject(ex) });
            }

            return result;
        }
    }
}
