﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OkexTrader.Trade;
using System.Threading;
using OkexTrader.MarketData;
using OkexTrader.FutureTrade;

namespace OkexTrader
{    

    class Program
    {
        static void output(String content)
        {
            Console.WriteLine(content);
            System.Diagnostics.Debug.WriteLine(content);
        }

        static void Main(string[] args)
        {
            OkexFutureTrader ft = new OkexFutureTrader();
            //String str = "----";

            //ft.getMarketData(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter);

            //OkexFutureDepthData dd = ft.getMarketDepthData(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter);

            //ft.getTradesInfo(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter);

            //ft.getFutureIndex(OkexFutureInstrumentType.FI_LTC);

            //ft.getExchangeRate();

            //ft.getEstimatePrice(OkexFutureInstrumentType.FI_LTC);

            //List<OkexKLineData> kl = ft.getKLineData(OkexFutureInstrumentType.FI_BTC, OkexFutureContractType.FC_NextWeek, OkexKLineType.KL_1Min);

            //ft.getHoldAmount(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter);

            //Dictionary<OkexFutureInstrumentType, OkexAccountInfo> info;
            //bool ret = ft.getUserInfo(out info);

            //List<OkexPositionInfo> info;
            //ft.getFuturePosition(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter, out info);

            //long orderID = ft.trade(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter, 3000.0, 1, OkexContractTradeType.TT_OpenSell, 20, false);
            //ft.cancel(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter, orderID);

            //AccountInfo ai = new AccountInfo();
            //ai.init();

            //List<OkexContractInfo> info = ai.getContractsByType(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter);
            //if(info != null)
            //{
            //    int n = info.Count;
            //    output(n.ToString());
            //}
            //long amt = ft.getHoldPositionAmount(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter, OkexFutureTradeDirectionType.FTD_Sell);

            //List<OkexPositionInfo> pi;
            //bool ret = ft.getFuturePosition(OkexFutureInstrumentType.FI_LTC, OkexFutureContractType.FC_Quarter, out pi);
            //if (ret)
            //{
            //    amt = pi[0].sell_available;
            //}
            //StrategyMgr.Instance.init();

            //while (true)
            //{
            //    StrategyMgr.Instance.update();
            //}

            Thread stThread = new Thread(strategyThread);
            Thread mdThread = new Thread(marketDataThread);
            Thread tdThread = new Thread(tradeThread);

            mdThread.Start();
            tdThread.Start();
            stThread.Start();
        }

        //static int mdThreadLastTick = 0;
        //static int tdThreadLastTick = 0;
        //static int stThreadLastTick = 0;

        const int MD_FREQ = 100;
        const int TD_FREQ = 10;
        const int ST_FREQ = 30;

        static void strategyThread()
        {
            while (true)
            {
                int beginTick = System.Environment.TickCount;
                StrategyMgr.Instance.update();
                int deltaTick = System.Environment.TickCount - beginTick;
                if(deltaTick < ST_FREQ)
                {
                    Thread.Sleep(ST_FREQ - deltaTick);
                }
            }
        }

        static void marketDataThread()
        {
            while (true)
            {
                int beginTick = System.Environment.TickCount;
                MarketDataMgr.Instance.update();
                int deltaTick = System.Environment.TickCount - beginTick;
                if (deltaTick < MD_FREQ)
                {
                    Thread.Sleep(MD_FREQ - deltaTick);
                }
            }
        }

        static void tradeThread()
        {
            while (true)
            {
                int beginTick = System.Environment.TickCount;
                FutureTradeMgr.Instance.update();
                int deltaTick = System.Environment.TickCount - beginTick;
                if (deltaTick < TD_FREQ)
                {
                    Thread.Sleep(TD_FREQ - deltaTick);
                }
            }
        }


    }
}
