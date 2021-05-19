using System.Collections.Generic;

namespace SunMarket.Services.Order
{
    public interface IOrderService
    {
        List<Data.Models.SalesOrder> GetAllOrders();
        ServiceResponse<bool> GenerateOpenOrder(Data.Models.SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}
