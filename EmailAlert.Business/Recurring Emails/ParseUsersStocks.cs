using EmailAlert.Data;
using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Business
{
    public class ParseUsersStocks
    {
        public IRegisterDbAccess _registration;

        public ParseUsersStocks(IRegisterDbAccess Registration)
        {
            _registration = Registration;
        }


        public List<string> getUpFiveStocks()
        {
            var AllUsers = _registration.ReturnAll();
            List<string> StocksToCheckAgainstUpFive = new List<string> { };

            foreach (var user in AllUsers)
            {
                if (user.UpFive)
                {
                    if (user.Stocks == null) continue;

                    foreach (var stock in user.Stocks)
                    {
                        StocksToCheckAgainstUpFive.Add(stock.stock);
                    }
                }
            }
            return StocksToCheckAgainstUpFive;
        }


        public List<string> getDownFiveStocks()
        {
            var AllUsers = _registration.ReturnAll();
            List<string> StocksToCheckAgainstDownFive = new List<string> { };

            foreach (var user in AllUsers)
            {
                if (user.DownFive)
                {
                    foreach (var stock in user.Stocks)
                    {
                        StocksToCheckAgainstDownFive.Add(stock.stock);
                    }
                }
            }
            return StocksToCheckAgainstDownFive;
        }


        public List<string> getMovingAvgStocks()
        {
            var AllUsers = _registration.ReturnAll();
            List<string> StocksToCheckAgainstMovingAvg = new List<string> { };

            foreach (var user in AllUsers)
            {
                if (user.MovingAvg)
                {
                    foreach (var stock in user.Stocks)
                    {
                        StocksToCheckAgainstMovingAvg.Add(stock.stock);
                    }
                }
            }
            return StocksToCheckAgainstMovingAvg;
        }
    }
}
