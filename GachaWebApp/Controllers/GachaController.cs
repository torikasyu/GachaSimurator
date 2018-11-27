using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GachaWebApp.Models;

namespace GachaWebApp.Controllers
{
    public class GachaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PlayOnce()
        {
            return View(new GachaModel());
        }
        
        /*
        [HttpPost]
        public IActionResult PlayOnce(GachaModel gachaModel)
        {
            return RedirectToAction("PlayOnceResult", gachaModel);
        }
        */

        Dictionary<int, Conf> confDict;
        int tryCount = 0;

        [HttpPost]
        public IActionResult PlayOnce(GachaModel gachaModel)
        {
            confDict = new Dictionary<int, Conf>();
            confDict.Add(5, new Conf(gachaModel.rate5, gachaModel.num5));
            confDict.Add(4, new Conf(gachaModel.rate4, gachaModel.num4));
            confDict.Add(3, new Conf(gachaModel.rate3, gachaModel.num3));
            confDict.Add(2, new Conf(gachaModel.rate2, gachaModel.num2));
            confDict.Add(1, new Conf(gachaModel.rate1, gachaModel.num1));
            tryCount = gachaModel.tryCount;

            Dictionary<string, int> result;
            gachaModel.playCount = PlayGacha(out result);
            gachaModel.result = result;


            return View("PlayOnceResult", gachaModel);
        }

        class Conf
        {
            public Conf(float _rate, int _num)
            {
                rate = _rate;
                num = _num;
            }
            public float rate;
            public int num;
        }

        int PlayGacha(out Dictionary<string,int> result)
        {
            //コンプすべき全種類数
            int totalNum = confDict.Sum(e => e.Value.num);

            Random rnd = new Random();
            double rndNumRarerity;
            int rndNumSameRarerity;

            Dictionary<string, int> resultDict = new Dictionary<string, int>();

            int totalGet = 0;   //獲得した種別
            int loop = 0;

            bool loopFlg = true;

            do
            {
                rndNumRarerity = rnd.NextDouble();

                int rarelity = 0;
                if (rndNumRarerity <= confDict[5].rate)
                {
                    rarelity = 5;
                }
                else if (rndNumRarerity <= confDict[5].rate + confDict[4].rate)
                {
                    rarelity = 4;
                }
                else if (rndNumRarerity <= confDict[5].rate + confDict[4].rate + confDict[3].rate)
                {
                    rarelity = 3;
                }
                else if (rndNumRarerity <= confDict[5].rate + confDict[4].rate + confDict[3].rate + confDict[2].rate)
                {
                    rarelity = 2;
                }
                else
                {
                    rarelity = 1;
                }

                string id = "";
                if (confDict[rarelity].num > 0)
                {
                    //同一レアリティ内での抽選
                    rndNumSameRarerity = rnd.Next(0, confDict[rarelity].num) + 1;

                    id = rarelity.ToString() + "_" + rndNumSameRarerity.ToString("000");


                }
                else
                {
                    id = rarelity.ToString() + "_対象カードなし";
                }

                if (resultDict.ContainsKey(id))
                {
                    resultDict[id] += 1;
                }
                else
                {
                    resultDict.Add(id, 1);
                }

                loop++;

                totalGet = resultDict.Count;

                if (tryCount > 0)   //試行回数が1以上を指定された
                {
                    if (tryCount <= loop)   //試行回数を達成した
                    {
                        loopFlg = false;
                    }
                }
                else //試行回数0（コンプモード）
                {
                    if (totalGet >= totalNum)   //カードの種類が設定のトータル枚数になった（コンプ）
                    {
                        loopFlg = false;
                    }
                }

            } while (loopFlg);

            var orderDict = resultDict.OrderByDescending(e => e.Key);

            /*
            foreach (KeyValuePair<string, int> p in orderDict)
            {
                System.Diagnostics.Debug.WriteLine(p.Key + ":" + p.Value.ToString());
            }
            */

            //System.Diagnostics.Debug.WriteLine(loop);

            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach (var p in orderDict)
            {
                temp.Add(p.Key, p.Value);
            };

            result = temp;
            return loop;
        }
    }
}