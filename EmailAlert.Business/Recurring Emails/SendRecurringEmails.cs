using EmailAlert.Data;
using EmailAlert.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmailAlert.Business
{
    public class SendRecurringEmails
    {
        private EmailAPI EmailAPI { get; }
        private AnalyzeStocks _analyzeStocks { get; }

        public SendRecurringEmails(EmailAPI emailAPI, AnalyzeStocks Analyze)
        {
            EmailAPI = emailAPI;
            _analyzeStocks = Analyze;
        }


        public async void CheckAllStocks()
        {
            var UpFiveStocks = await _analyzeStocks.CheckUpFive();
            var DownFiveStocks = await _analyzeStocks.CheckDownFive();
            var MovingAvgStocks = await _analyzeStocks.CheckMovingAverage();


            if (UpFiveStocks.Count > 0)
            {
                foreach (var ticker in UpFiveStocks)
                {
                    this.SendUpFiveEmail(ticker);
                }
            }

            else if (DownFiveStocks.Count > 0)
            {
                foreach (var ticker in DownFiveStocks)
                {
                    this.SendDownFiveEmail(ticker);
                }
            }

            else if (MovingAvgStocks.Count > 0)
            {
                foreach (var ticker in DownFiveStocks)
                {
                    this.SendMovingAvgEmail(ticker);
                }
            }
        }


        public async void SendUpFiveEmail(string ticker)
        {
            Email UpFiveEmail = new Email
            {
                Subject = ticker + " Is Up 5%!",
                Content = "Good news! " + ticker + " went up five percent today! Go check it out at Market Watch.",
                From = "albertcmiller1@gmail.com",
                UpFive = true,
                DownFive = false,
                Admin = false,
                MovingAvg = false
            };
            await EmailAPI.Send(UpFiveEmail, ticker);
        }

        public async void SendDownFiveEmail(string ticker)
        {
            Email DownFiveEmail = new Email
            {
                Subject = "Your Stock Went Down :(",
                Content = "Sorry! " + ticker + " went down five percent today. Head over to Market Watch to check it out.",
                From = "albertcmiller1@gmail.com",
                UpFive = false,
                DownFive = true,
                Admin = false,
                MovingAvg = false
            };
            await EmailAPI.Send(DownFiveEmail, ticker);
        }

        public async void SendMovingAvgEmail(string ticker)
        {
            Email MovingAvgEmail = new Email
            {
                Subject = ticker + "'s" + " Moving Average Crossed!",
                Content = "Big news! " + ticker + "'s moving average crossed its market value! Head over to market watch to take a closer look!",
                From = "albertcmiller1@gmail.com",
                UpFive = false,
                DownFive = false,
                Admin = false,
                MovingAvg = true
            };
            await EmailAPI.Send(MovingAvgEmail, ticker);
        }
    }
}
