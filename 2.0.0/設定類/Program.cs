using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using DxLibDLL;
namespace _2._0._0
{
    class Program
    {//510-64=446
        //640-32=608
        public const int bosskosuu = 3;
        public static int[] Fonts = new int[100];
        public static int dnx, dny, dns, dnc, dnflg, dnt;
        public static int brt;
        public static int white, black, red, green, blue;
        public static int count = 0;
        public const int fx = 32, fy = 16, fmx = 510 - 32, fmy = 640 - 16;
        public const int scx = 510;
        public const int scy = 640;
        public static int gamemode = 0;
        public static int power = 0, point = 0, score = 0;
        public const int hamix = 300;
        public const int realscx = scx + hamix, realscy = scy;

        public static int syux, syuy;
        public static int ztamakankaku = 10;
        public const float kakudokihon = (float)Math.PI / 180;
        public static List<ziki> zibun = new List<ziki>();
        public static bool enter = false, esc = false;
        public static bool border;
        public static bool isbom;
        public static int bomsyoki = 3;
        public static int lifesyoki = 3;
        public static void syokika()
        {
            white = DX.GetColor(255, 255, 255);
            black = DX.GetColor(0, 0, 0);
            red = DX.GetColor(255, 0, 0);
            blue = DX.GetColor(0, 0, 255);
            green = DX.GetColor(0, 255, 0);
            isbom = false;
            brt = 255;
            dnx = 0; dny = 0; dns = 0; dnc = 0; dnflg = 0; dnt = 0;
            zibun.Clear();
            zibun.Add(new ziki(1, 320, 300, 0));
            power = 100; point = 0; score = 0; gamemode = 0; count = 0;
            Fonts[0] = DX.CreateFontToHandle("Lindsey", 50, 10, DX.DX_CHARSET_DEFAULT); 
            zibun[0].syokika();
            zibun[0].life = lifesyoki;
            zibun[0].boms = bomsyoki;
            DX.SetDrawBright(255, 255, 255);
          
        }
        static void Main(string[] args)
        {
            DX.SetWindowText("しゅーてぃんぐげぇむつー");
            DX.ChangeWindowMode(1);
            DX.SetGraphMode(scx + hamix, scy, 32);

            if (DX.DxLib_Init() < 0) { return; }
            int thickness = 100;
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            while (GetHitKeyStateAll_2(key) == 0 && DX.ProcessMessage() == 0)
            {
                DX.ClearDrawScreen();
                if (power > 500) power = 500;
                else if (power < 0) power = 0;
                if (point > 9999) point = 9999;
                if (score > 999999999) score = 999999999;
                count += 1;

                switch (gamemode)
                {
                    case 0:
                        //ローディング
                        if (present.Loading() & present.Loading2()) { gamemode = 1; }
                        break;
                    case 1:
                        //KCLC
                        present.hajimari();
                        break;
                    case 2:
                        //スタート画面
                        start.gamen();
                        DX.SetFontSize(15);
                        break;
                    case 3:
                        //設定
                        option.settei();
                        zibun[0].life = lifesyoki;
                        zibun[0].boms = bomsyoki;
                        break;
                    case 4:
                        //ゲーム本体
                        enter_func("最初", 0);
                        if (brt != 255) { DX.SetDrawBright(brt, brt, brt); }
                        DX.SetDrawArea(0, 0, scx + hamix, scy);
                        haikeidraw();
                        DX.DrawBox(scx, 0, scx + 300, scy, DX.GetColor(255, 255, 255), 1);
                        DX.SetDrawArea(fx, fy, scx, scy - fy);
                        for (int i = 0; i < zibun.Count; i++)
                        {
                            zibun[i].Ziki();
                            syux = zibun[0].zikix;
                            syuy = zibun[0].zikiy;
                        }
                        DX.SetDrawArea(0, 0, scx + hamix, scy);
                        gamemode4.bunki();

                        if (brt != 255) { DX.SetDrawBright(255, 255, 255); }

                        dn_calc(); enter_func("描画", 0);
                        break;
                    case 5:
                        Finished.Fin(0);
                        //ゲームクリア
                        break;
                    case 6:
                        Finished.Fin(1);
                        //ゲームオーバー
                        break;
                    case 7:
                        //ゲーム終了
                        goto exit;
                    case 8:
                        //エディター
                        edit.main();
                        DX.SetGraphMode(scx + hamix, scy, 32);
                        break;
                    case 9:
                        //コンティニュー
                        Finished.Fin(2);
                        break;
                }

                /*
     　　　　　　ゲームモード対応表
                  
                 0：初期化
                 1：KCLC
                 2：スタート画面
                 3：設定
                 4：実際のゲーム
                 5：ゲームクリア
                 6：ゲームオーバー
                 7：EXIT
                 8:エディタ
                 9:コンティニュー
                 */
               
                //         DX.DrawLine(0, 0, 300, 300, DX.GetColor(255, 255, 255), thickness);
                if (thickness > 0) { thickness -= 1; }
               DX.DrawString(0, scy - 20, "" + Math.Sqrt(1000 / fpsave), DX.GetColor(255, 0, 0));

                fpsing(); 
                //enter_func("待機した時間", 1);
            //   drawfunc(scx, 250);
                DX.ScreenFlip();
            }

        exit:
            DX.DxLib_End();
            return;
        }

        private static void haikeidraw()
        {
            for (int i = 0; i < 500; i++)
            {
                kansuu.DrawPixel(kansuu.rang(0, scx), kansuu.rang(0, scy), DX.GetColor(kansuu.rang(255), kansuu.rang(255), kansuu.rang(255)));
            }
        }
        public static int seMax = 100;
        public static int[] sound_se = new int[seMax];
        public static bool[] seFlag = new bool[seMax];
        public void musicini()
        {
            for (int i = 0; i < seMax; i++)
            {
                seFlag[i] = false;
            }
        }
        public void music_play()
        {
            for (int i = 0; i < seMax; i++)
            {
                if (seFlag[i]) { DX.PlaySoundMem(sound_se[i], DX.DX_PLAYTYPE_BACK); }
            }
        }

        public static void load_font_dat(char[] name)
        {

            StreamReader reader = new StreamReader("kan1.dat");
            string s = reader.ReadLine();
            reader.Close();
        }
        public static int fpscount, countot;
        public const int Flame = 60;
        static int[] f = new int[Flame];
        public static int t = 0;
        static double fpsave;
        public static int term, gnt;
        public static void fpsing()
        {
            if (fpscount == 0)
            {
                if (t == 0)
                {
                    term = 0;
                }
                else
                {
                    term = countot + 1000 - DX.GetNowCount();
                }
            }
            else { term = (int)(countot + fpscount * (1000.0 / Flame)) - DX.GetNowCount(); }
            if (term > 0) { Thread.Sleep(term); }
            gnt = DX.GetNowCount();
            if (fpscount == 0)
            {
                countot = gnt;
            }
            f[fpscount] = gnt - t;
            t = gnt;
            if (fpscount == Flame - 1)
            {
                fpsave = 0; for (int i = 0; i < Flame; i++)
                {
                    fpsave += f[i];
                    fpsave /= Flame;
                }
            }
            fpscount = (++fpscount) % Flame;
        }
        public static void dn_calc()
        {
            if (dnflg == 1)
            {
                dnx = (int)kansuu.rang(dns);
                dny = (int)kansuu.rang(dns);
                if (++dnc > dnt) { dnflg = 0; dnx = 0; dny = 0; }
            }
        }
        public const int STRMAX = 64;
        public const int FUNCMAX = 30;
        public static int funccount = 0;
        public static long lt;
        public struct functm
        {
            public int tm; public string str;
        }
        static functm[] funct = new functm[FUNCMAX];
        public static void enter_func(string st, int flag)
        {
            long nowtm;
            if (funccount >= FUNCMAX) { return; }
            nowtm = DX.GetNowHiPerformanceCount();
            if (nowtm - lt < int.MaxValue)
            {
                funct[funccount].tm = (int)(nowtm - lt);
                funct[funccount].str = st;

            }
            else { funct[funccount].tm = -1; }
            lt = nowtm;
            if (flag == 1)
            {
                funccount = 0;
            }
            else { funccount++; }

        }
        public static void drawfunc(int x, int y)
        {
            int total = 0;
            int i;
            for (i = 0; i < FUNCMAX; i++)
            {
                if (funct[i].str == null) { break; }
                DX.DrawString(x, y + 14 * i, funct[i].tm / 1000.0f + "" + funct[i].str, red);
                total += funct[i].tm;
            }
            DX.DrawString(x, y + 14 * i, "合計:" + total / 1000, red);
            DX.DrawString(x, y + 14 * (i + 1), "敵の数:" + gamemode4.teki.Count, red);
        }
        public static int[] key = new int[256];
        public static int GetHitKeyStateAll_2(int[] GetHitKeyStateAll_InputKey)
        {

            byte[] GetHitKeyStateAll_Key = new byte[256];
            DX.GetHitKeyStateAll(out GetHitKeyStateAll_Key[0]);
            for (int i = 0; i < 256; i++)
            {
                if (GetHitKeyStateAll_Key[i] != 0) { key[i]++; }
                else { key[i] = 0; }
            }
            return 0;
        }

    }
}
