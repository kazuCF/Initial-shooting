using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace _2._0._0
{
    class zako : en
    {
        public zako(float X, float Y, int Plus, int Ugoki, int Gaz, int Waittime,int Tamakankaku,int Tamakosuu)
        {
            zx = X;
            zy = Y;
            ugokikata = Ugoki;
            itemnum = 3;
            waittime = Waittime;
            dasitem = new int[] { 0, 0, 0 };
            gaz = gazo.zakoGraphs[Gaz];
            gw = gazo.zakosizew[Gaz];
            gh = gazo.zakosizeh[Gaz];
            kankaku = Tamakankaku;
            tamakosuu = Tamakosuu;
        }
        public void ido()
        {
        }
    }
}
