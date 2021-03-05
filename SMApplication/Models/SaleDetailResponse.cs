using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMApplication.Models
{
    /// <summary>
    /// GetSales servisinden dönen veriler için tek bir order model DTO modeli
    /// </summary>
    public class SaleDetailResponse : BaseResponse
    {
        public OrderDTO Order { get; set; }
        public List<OrderDetailDTO> OrderDetail { get; set; }
    }

    /// <summary>
    /// Liste ekranı için kullanılan dto modeli
    /// </summary>
    public class SaleResponse : BaseResponse
    {
        public List<OrderDTO> Orders { get; set; }
    }

    /// <summary>
    /// GetSales servisinden dönen response modeli
    /// </summary>
    public class SaleDetailServiceResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public Response Response { get; set; }
    }

    /// <summary>
    /// GetSales servisinden dönen orders verileri için kullanılan ara model
    /// </summary>
    public class Response
    {
        public List<Order> Orders { get; set; }
    }

    /// <summary>
    /// Detay ekranı ve liste ekranı için kullanılan Order DTO modeli
    /// </summary>
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string PersonalIdentification { get; set; }
        public string OrderDate { get; set; }
        public string IntegrationOrderCode { get; set; }
        public string OrderStatus { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string TaxAuthority { get; set; }
        public string TaxNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
    }

    /// <summary>
    /// Detay ekranı için kullanılan detail dto modeli
    /// </summary>
    public class OrderDetailDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CargoCompany { get; set; }
        public string CargoDate { get; set; }
    }
}
