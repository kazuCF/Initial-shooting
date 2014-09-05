using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DxLibDLL;
namespace _2._0._0
{
    public static class gazo
    {
        /*自機関係*/
        public static int sinziki;
        public static int[] zakoGraphs = new int[100];
        public static int[] zakosizew = new int[100];
        public static int[] zakosizeh = new int[100];
        public static int zikiGraph;
        public static int zgw;
        public static int zgh;
        /*ボス関係*/
        public static int[] BossGraphs = new int[Program.bosskosuu];
        public static int[] Bossgws = new int[Program.bosskosuu];
        public static int[] Bossghs = new int[Program.bosskosuu];
        public static int[,] itemimg = new int[6, 2];

        //多弾専用
        public static int[,] otama = new int[25, 64];
        public static int[] otamagw = new int[25];
        public static int[] otamagh = new int[25];
        //ボムに関するエフェクト画像
        public static int[] imgefbom = new int[5];
        public static int bomw, bomh;
        /*その他*/
        public static int haikeig;
        public static int[] titles = new int[10];
        public static int bombs;
        public static int players;
        public static int star;
        public static void syokika()
        {
            zakoLoad();
            bombLoad();
            tamaload();
            charaload();
            itemload();
            sideload();
            sizeget();
        }
        private static void sizeget()
        {
            DX.GetGraphSize(zikiGraph, out zgw, out zgh);
            DX.GetGraphSize(imgefbom[3], out bomw, out bomh);
            for (int i = 0; i < Program.bosskosuu; i++)
            {
                DX.GetGraphSize(BossGraphs[i], out Bossgws[i], out Bossghs[i]);
            }
        }
        private static void zakoLoad()
        {
            zakoGraphs[0] = DX.LoadGraph("キャラクタ\\zako.png");
            zakoGraphs[1] = DX.LoadGraph("キャラクタ\\zako2.png");
            zakoGraphs[2] = DX.LoadGraph("キャラクタ\\smallzako.png");
            for (int i = 0; i < zakoGraphs.Length; i++)
            {
                if (zakoGraphs[i] == 0) break;
                DX.GetGraphSize(zakoGraphs[i], out zakosizew[i], out zakosizeh[i]);
            }
        }
        private static void bombLoad()
        {
            imgefbom[0] = DX.LoadGraph("弾\\bom0.png");
            imgefbom[1] = DX.LoadGraph("弾\\bom1.png");
            //  imgefbom[2] = DX.LoadGraph("chara.png");//キャラ
            imgefbom[3] = DX.LoadGraph("弾\\Bomtitle.png");
        }
        private static void tamaload()
        {
            DX.LoadDivGraph("弾\\1616.png", 32, 32, 1, 16, 16, out otama[0, 0]);
            otamagw[0] = 16;
            otamagh[0] = 16;
            for (int i = 2; i < 13; i++)
            {
                DX.LoadDivGraph("弾\\1616(" + i + ").png", 32, 32, 1, 16, 16, out otama[i - 1, 0]);
                otamagw[i] = 16;
                otamagh[i] = 16;
            }
            //ここで12まで
            DX.LoadDivGraph("弾\\1630.png", 32, 32, 1, 16, 30, out otama[12, 0]);
            otamagw[12] = 16;
            otamagh[12] = 30;
            DX.LoadDivGraph("弾\\8864.png", 64, 64, 1, 8, 8, out otama[13, 0]);
            otamagw[13] = otamagh[13] = 8;
            DX.LoadDivGraph("弾\\64648.png", 8, 8, 1, 64, 64, out otama[14, 0]);
            otamagw[14] = otamagh[14] = 64;
            DX.LoadDivGraph("弾\\322716.png", 16, 16, 1, 32, 27, out otama[15, 0]);
            otamagw[15] = 32; otamagh[15] = 28;
            DX.LoadDivGraph("弾\\322815.png", 15, 15, 1, 32, 28, out otama[16, 0]);
            otamagw[16] = 32; otamagh[16] = 28;
            DX.LoadDivGraph("弾\\322816 (2).png", 16, 16, 1, 32, 30, out otama[17, 0]);
            otamagw[17] = 32; otamagh[17] = 30;
            otamagw[18] = 32; otamagh[18] = 30;
            otamagw[19] = 32; otamagh[19] = 30;
            otamagw[20] = 32; otamagh[20] = 30;
            otamagw[21] = 32; otamagh[21] = 30;
            DX.LoadDivGraph("弾\\323016.png", 16, 16, 1, 32, 30, out otama[18, 0]);
            DX.LoadDivGraph("弾\\323016(2).png", 16, 16, 1, 32, 30, out otama[19, 0]);
            DX.LoadDivGraph("弾\\323016(3).png", 16, 16, 1, 32, 30, out otama[20, 0]);
            DX.LoadDivGraph("弾\\323016(4).png", 16, 16, 1, 32, 30, out otama[21, 0]);
            otamagw[22] = 32; otamagh[22] = 32;
            DX.LoadDivGraph("弾\\323216.png", 16, 16, 1, 32, 32, out otama[22, 0]);
            otamagw[23] = 32; otamagh[23] = 42;

            DX.LoadDivGraph("弾\\324216.png", 16, 16, 1, 32, 42, out otama[23, 0]);
        }
        private static void charaload()
        {
            sinziki = DX.LoadGraph("キャラクタ\\ziki.png");
            zikiGraph = DX.LoadGraph("キャラクタ\\atari1.png");
            BossGraphs[0] = DX.LoadGraph("キャラクタ\\Boss1.png");
            BossGraphs[1] = DX.LoadGraph("キャラクタ\\Boss2.png");
        }
        private static void sideload()
        {
            titles[0] = DX.LoadGraph("Title\\C1.png");
            titles[1] = DX.LoadGraph("Title\\C2.png");
            haikeig = DX.LoadGraph("option\\sg2.bmp");
            players = DX.LoadGraph("option\\players.png");
            bombs = DX.LoadGraph("option\\bomb.png");
            star = DX.LoadGraph("option\\star.png");
        }
        private static void itemload()
        {

            DX.LoadDivGraph("items\\p0.png", 2, 2, 1, 35, 35, out itemimg[0, 0]);
            DX.LoadDivGraph("items\\p1.png", 2, 2, 1, 35, 35, out itemimg[1, 0]);
            DX.LoadDivGraph("items\\p2.png", 2, 2, 1, 15, 15, out itemimg[2, 0]);
            DX.LoadDivGraph("items\\p3.png", 2, 2, 1, 35, 35, out itemimg[3, 0]);
            DX.LoadDivGraph("items\\p4.png", 2, 2, 1, 35, 35, out itemimg[4, 0]);
            DX.LoadDivGraph("items\\p5.png", 2, 2, 1, 35, 35, out itemimg[5, 0]);
        }
    }
}
