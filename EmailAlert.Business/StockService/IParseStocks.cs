using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailAlert.Data.Interfaces
{
    public interface IStockService
    {
        Task<List<Stock>> CallStockApi(string url, StockUrl URL);
    }
}
