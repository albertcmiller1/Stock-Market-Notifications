using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EmailAlert.Domain;
using EmailAlert.Data.Interfaces;

namespace EmailAlert.Business.StockService
{
    public class StockService : IStockService
    {
        public async Task<List<Stock>> CallStockApi(string url, StockUrl URL)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                var BigDic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(result);

                List<Stock> AllStocks = new List<Stock>();

                foreach (var item in BigDic)
                {
                    if (item.Key == "Meta Data")
                    {
                        MetaData MyMetaData = new MetaData();

                        foreach (var MetaDataData in item.Value)
                        {
                            switch (MetaDataData.Key)
                            {
                                case "1. Information":
                                    MyMetaData.Information = MetaDataData.Value.ToString();
                                    break;

                                case "2. Symbol":
                                    MyMetaData.Ticker = MetaDataData.Value.ToString();
                                    break;

                                case "3. Last Refreshed":
                                    MyMetaData.LastRefreshed = DateTime.Parse(MetaDataData.Value.ToString());
                                    break;

                                case "4. Interval":
                                    MyMetaData.Interval = MetaDataData.Value.ToString();
                                    break;

                                case "5. Output Size":
                                    MyMetaData.OutputSize = MetaDataData.Value.ToString();
                                    break;

                                case "6. Time Zone":
                                    MyMetaData.TimeZone = MetaDataData.Value.ToString();
                                    break;
                            }
                        }
                    }

                    else if (item.Key == URL.Description)
                    {
                        foreach (var SmolDic in item.Value)
                        {
                            Stock Info = new Stock();

                            DateTime CurrentDate = DateTime.Parse(SmolDic.Key);
                            Info.date = CurrentDate;

                            var stockJObj = SmolDic.Value;

                            var stock = stockJObj as JObject;
                            foreach (var stockData in stock.Children())
                            {
                                var Jprop = stockData as JProperty;
                                switch (Jprop.Name)
                                {
                                    case "1. open":
                                        Info.open = double.Parse(Jprop.Value.ToString());
                                        break;

                                    case "2. high":
                                        Info.high = double.Parse(Jprop.Value.ToString());
                                        break;

                                    case "3. low":
                                        Info.low = double.Parse(Jprop.Value.ToString());
                                        break;

                                    case "4. close":
                                        Info.close = double.Parse(Jprop.Value.ToString());
                                        break;

                                    case "5. volume":
                                        Info.volume = int.Parse(Jprop.Value.ToString());
                                        break;
                                }
                            }
                            AllStocks.Add(Info);
                        }
                    }
                    else
                    {
                        //throw(new) new exception
                    }
                }
                return AllStocks;
            }
        }
    }
}
