using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
namespace _2._0._0
{
    public static class gamemode4
    {
        public static float[,] tekis = new float[100, 20];
        //   public static List<string> tekis = new List<string>();
        public static string[] lines = new string[100];
        public static string[] values;
        public static bool fileend = false;
        public static bool readin = false;
        public static int tekiyomi = 0;
        public static int htime = 0;
        public static int bossnum = -1;
        public static int chapter = 1;
        public static int time = 0;
        private static int effetime = 0;
        public static bool kaishi = true;
        public static bool bosschuu = false;
        public static Boss[] bosss = new Boss[2];
        public static List<item> im = new List<item>();
        public static List<effect> ef = new List<effect>();
        public static List<en> teki = new List<en>();
        public static void syokika()
        {
            tekiyomi = 4;
            im.Clear();
            ef.Clear();
            teki.Clear();
            chapter = 1;
            bossnum = -1;
            effetime = 0;
            time = 0;
            bosss[1] = new Boss1(Program.scx / 2, 30);
            bosss[0] = new NBoss1(Program.scx / 2, -30);

        }
        public static void removeall()
        {
            teki.Clear();
        }


        public static void bunki()
        {
            kansuu.setarea1();
            switch (chapter)
            {
                case 1:
                    chapter1();
                    break;
                case 2:
                    chapter2();
                    break;
            }
            im.RemoveAll(c => !c.seizon);
            ef.RemoveAll(c => !c.seizon);
            teki.RemoveAll(c => !c.seizon);

            foreach (var i in im) { i.main(); } Program.enter_func("アイテム計算", 0);
            foreach (var i in ef) { i.main(); } Program.enter_func("エフェクト計算", 0);
            foreach (var i in teki) { i.main(); if (ziki.bommcool == 60) { i.life -= 5; } } Program.enter_func("敵計算", 0);
            kansuu.setareaend();
            DX.DrawGraph(Program.hamix + 225, Program.scy - 200, gazo.haikeig, DX.TRUE);
            DX.DrawBox(0, 0, Program.fx, Program.scy, DX.GetColor(0, 0, 255), DX.TRUE);
            DX.DrawBox(0, 0, Program.scx, Program.fy, DX.GetColor(0, 0, 255), DX.TRUE);
            DX.DrawBox(0, Program.scy, Program.scx, Program.scy - Program.fy, DX.GetColor(0, 0, 255), DX.TRUE);
            for (int i = 0; i < Program.zibun[0].life; i++)
            {
                DX.DrawGraph(Program.hamix + 250 + (i * 30), 60, gazo.heart, DX.TRUE);
            }
            time += 1;
            DX.DrawString(Program.scx, 0, "" + Program.power, DX.GetColor(255, 0, 0));
        
        }



        private static void chapter1()
        {
            switch (tekiyomi)
            {
                case -1:
                    if (startin(1)) { htime = time + 40; tekiyomi++; };
                   
                    break;
                case 0:
                    if (tekiyomikomi("1-1-1", 0)) { htime = time + 40; tekiyomi++; }
                    //if (tekiyomikomi("1-1-5", htime)) { tekiyomi++; htime = time + 40; }

                    break;
                case 1:
                    if (tekiyomikomi("1-1-2", htime)) { htime = time + 40; tekiyomi++; }
                    //if (tekiyomikomi("1-1-6", htime)) { tekiyomi++; htime = time + 40; }

                    break;
                case 2:
                    if (tekiyomikomi("1-1-3", htime)) { tekiyomi++; htime = time + 40; }
                    break;
                case 3:
                    if (tekiyomikomi("1-1-4", htime)) { tekiyomi++; htime = time + 150; }
                    break;
                case 4:

                    if (time > htime)
                    {
                        removeall();
                        bosss[0].boss_shot_main();
                        if (bosss[0].fend) { tekiyomi++; htime = time + 40; gamemode4.bosschuu = false; }
                    }
                    break;
                case 5:
                    if (tekiyomikomi("1-1-5", htime)) { tekiyomi++; htime = time + 40; }
                    break;
                case 6:
                    if (tekiyomikomi("1-1-6", htime)) { tekiyomi++; htime = time + 40; }
                    break;
                case 7:
                    if (tekiyomikomi("1-1-7", htime)) { tekiyomi++; htime = time + 240; }
                    break;
                case 8:
                    if (time > htime)
                    {
                        // removeall();
                        bosss[1].boss_shot_main();
                        if (bosss[1].fend) { tekiyomi++; htime = time + 40; gamemode4.bosschuu = false; }
                    }
                    break;
                default:
                    chapter++;
                    tekiyomi = -1;
                    break;
            }
            Program.enter_func("第1章", 0);
        }
        private static void chapter2()
        {
            switch (tekiyomi)
            {
                case -1:
                    if (startin(2)) { htime = time + 40; tekiyomi++; };
                    break;
                case 0:
                    if (tekiyomikomi("2-1-1", htime)) { tekiyomi++; htime = time + 40; }
                    break;
                case 1:
                    if (tekiyomikomi("2-1-2", htime)) { tekiyomi++; htime = time + 60; }
                    break;
                case 2:
                    if (tekiyomikomi("2-1-3", htime)) { tekiyomi++; htime = time + 40; }
                    break;
                case 3:
                    if (tekiyomikomi("2-1-4", htime)) { tekiyomi++; htime = time + 40; }
                    break;
            }
            Program.enter_func("第2章", 0);
        }
        
        private static bool tekiyomikomi(string filename)
        {
            return tekiyomikomi(filename, 0);
        }
        private static bool tekiyomikomi(string filename, int hajime)
        {
            var owari = true;
            int jyouhousu = 13;//アイテムを除いた情報の項目数
            if (!readin)
            {
                lines = new string[100];
                readin = true;
                var read = new StreamReader("enemy\\" + filename + ".csv", false);

                int k = 0;

                var dam = read.ReadLine();
                while (!read.EndOfStream)
                {
                    fileend = false;
                    lines[k] = read.ReadLine();
                    k++;
                    //
                    //0 x座標
                    //1 y座標
                    //2 動き方
                    //3 種類=画像
                    //4 登場時間
                    //5 待ち時間(waittime)
                    //6 弾間隔
                    //7 弾個数
                    //8 弾種類
                    //9 弾knd
                    //10弾col
                    //11特有の情報
                    //12弾の速度
                    //13敵の体力
                    //それ以降　出すアイテム
                }



            }
            foreach (var i in lines)
            {
                if (i == null) { fileend = true; break; }
                values = i.Split(',');
                int timing = int.Parse(values[4]);

                if (timing + hajime > time) { owari = false; continue; }
                else if (timing + hajime < time) { continue; }
                else
                {
                    float[] ivalues = new float[values.Length];
                    int[] intmonos = new int[values.Length];
                    for (int l = 0; l < values.Length; l++)
                    {
                        ivalues[l] = float.Parse(values[l]);
                        if (l > 1) { intmonos[l] = int.Parse(values[l]); }
                    }
                    var items = new int[values.Length - jyouhousu];
                    for (int l = jyouhousu + 1; l < values.Length; l++)
                    {
                        items[l - jyouhousu - 1] = intmonos[l];
                    }
                    teki.Add(new en(ivalues[0], ivalues[1], intmonos[2], intmonos[3], intmonos[4], intmonos[5], intmonos[6], intmonos[7], intmonos[8], intmonos[9], intmonos[10], ivalues[11], ivalues[12], intmonos[13], items));
                    owari = false;
                }
            }
            if (owari) { readin = false; }
            return owari;
        }
        private static bool startin(int chapter)
        {
            
           // kansuu.DrawRotaGraphfk(Program.scx/2, Program.scy/2, 1, 0, gazo.titles[0], DX.TRUE, false);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255-effetime);
            kansuu.DrawRotaGraphfk(Program.scx / 2, Program.scy / 2,effetime/100.0, 0, gazo.titles[chapter-1], DX.TRUE, false);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, effetime);
            if (++effetime >= 256) { effetime = 0; return true; }
            else { return false; }
            
        }
    }
}
