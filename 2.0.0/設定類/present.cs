using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DxLibDLL;
namespace _2._0._0
{
    class present
    {
        private static int K;
        private static int C ;
        private static int L ;
        private static int C2 ;
        private static int KCLC2013;
        public static int time = 0;
        private static int jyougen = 40;
        private static int kgw, kgh, cgw, cgh, lgw, lgh, c2gw, c2gh, k2gw, k2gh;
        public static void syokika()
        {
            K = DX.LoadGraph("k.bmp"); C = DX.LoadGraph("kc.bmp"); L = DX.LoadGraph("kcl.bmp"); C2 = DX.LoadGraph("kclc.bmp"); KCLC2013 = DX.LoadGraph("kclc2013.png");
        }
        public static void Loading()
        {
         
            
    //         ThreadStart action = () =>
   // {
        syokika(); start.syokika(); Program.syokika(); gazo.syokika(); gamemode4.syokika();

 /*   }; 
            Thread thread = new Thread(action);
       
      
            thread.Start();
            DX.DrawString(Program.realscx / 2, Program.realscy / 2, "Now Loading", DX.GetColor(255, 255, 255));
           
            thread.Join();*/
                Program.gamemode = 1;
        }
        public static void hajimari()
        {

            DX.GetGraphSize(K, out kgw, out kgh);
            DX.GetGraphSize(C, out cgw, out cgh);
            DX.GetGraphSize(L, out lgw, out lgh);
            DX.GetGraphSize(C2, out c2gw, out c2gh);
            DX.GetGraphSize(KCLC2013, out k2gw, out k2gh);
            time += 1;
            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) != 0)
            {
                time = 0; Program.gamemode = 2; Program.enter = true;
            }
            if (time <= 255)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(time, time, time), DX.TRUE);
            }
            else
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(255, 255, 255), DX.TRUE);

            }

            if (time < jyougen)
            {

                DX.DrawGraph((Program.scx + 300 - kgw) / 2, (Program.scy - kgh) / 2, K, DX.TRUE);

            }
            else if (time < jyougen * 2)
            {

                DX.DrawGraph((Program.scx + 300 - cgw) / 2, (Program.scy - cgh) / 2, C, DX.TRUE);

            }

            else if (time < jyougen * 3)
            {
                DX.DrawGraph((Program.scx + 300 - lgw) / 2, (Program.scy - lgh) / 2, L, DX.TRUE);

            }
            else if (time < jyougen * 5)
            {

                DX.DrawGraph((Program.scx + 300 - c2gw) / 2, (Program.scy - c2gh) / 2, C2, DX.TRUE);

            }
            else if (time > jyougen * 5 && time < jyougen * 10)
            {
                DX.DrawGraph((Program.scx + 300 - k2gw) / 2, (Program.scy - c2gh) / 2, KCLC2013, DX.TRUE);
            }
          else if (time > jyougen * 10)
            {
                Program.gamemode =2;
                time = 0;
            }

        }
        
    }
}
