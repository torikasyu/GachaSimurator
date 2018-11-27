using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GachaWebApp.Models
{
    public class GachaModel
    {
        [DisplayName("RARE1排出確率")]
        public float rate1 { get; set; }
        [DisplayName("RARE2排出確率")]
        public float rate2 { get; set; }
        [DisplayName("RARE3排出確率")]
        public float rate3 { get; set; }
        [DisplayName("RARE4排出確率")]
        public float rate4 { get; set; }
        [DisplayName("RARE5排出確率")]
        public float rate5 { get; set; }

        [DisplayName("RARE1カード種類")]
        public int num1 { get; set; }
        [DisplayName("RARE2カード種類")]
        public int num2 { get; set; }
        [DisplayName("RARE3カード種類")]
        public int num3 { get; set; }
        [DisplayName("RARE4カード種類")]
        public int num4 { get; set; }
        [DisplayName("RARE5カード種類")]
        public int num5 { get; set; }

        [DisplayName("ガチャ実行結果回数")]
        [DefaultValue(0)]
        public int playCount { get; set; }

        [DisplayName("ガチャ試行回数")]
        [DefaultValue(0)]
        public int tryCount { get; set; }

        public Dictionary<string, int> result { get; set; }

        public GachaModel()
        {
            tryCount = 100;

            rate1 = 0.40f;
            rate2 = 0.20f;
            rate3 = 0.19f;
            rate4 = 0.18f;
            rate5 = 0.03f;

            num1 = 150;
            num2 = 130;
            num3 = 127;
            num4 = 84;
            num5 = 87;
        }
    }
}
