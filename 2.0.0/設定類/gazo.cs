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
       public static int sinziki;
       public static int[] zakoGraphs = new int[100];
       public static int[] zakosizew = new int[100];
       public static int[] zakosizeh = new int[100];

       public static int zikiGraph ;//= DX.LoadGraph("atari1.png");
        public static int zgw;
        public static int zgh;
        public static int zikitamaGraph;// = DX.LoadGraph("zikitama.bmp");
        public static int zikitamaGraph2; //= DX.LoadGraph("zikitama2.bmp");
        public static int zikitamaGraph3; //= DX.LoadGraph("zikitama3.bmp");
        public static int heart; //= DX.LoadGraph("heart.png");
        public static int haikeig;// = DX.LoadGraph("sg2.bmp");
       public static int ztamagw;
        public static int ztamagh;
        public static int ztamagw3;
        public static int ztamagh3;
     //   public static int zako1Graph = DX.LoadGraph("zakoteki.png");
        public static int[] zako1Graphs=new int [5];
        public static int zako1gw, zako1gh;
        public static int zako1tGraph;// = DX.LoadGraph("zakot.png");
        public static int zako1tgw, zako1tgh;
        public static int syougekiGraph; //= DX.LoadGraph("syougekiha.png");
        public static int haikei;// = DX.LoadGraph("back0.png");
        public static int syougeki;// = DX.LoadGraph("syougekiha.png");
        public static int Boss1n1Graph;// = DX.LoadGraph("Boss1-1.png");
        public static int B1gw, B1gh;
        public static int Boss1Graph;// = DX.LoadGraph("BossGraph.bmp");
        public static int Boss1gw, Boss1gh;
        public static int[] tama = new int[10];
        public static int tamakosu = 3;
       public static int[,] itemimg=new int[6,2];
       public static int[,] btamab = new int[2, 10];
       public static int[] btamabgw = new int[2] { 16, 24 };
       public static int[] titles = new int[10];
       //多弾専用
       public static int[,] otama = new int[25, 64];
       public static int[] otamagw = new int[25];
       public static int[] otamagh = new int[25];
       //ボムに関するエフェクト画像
       public static int[] imgefbom = new int[5];
       public static void zakoLoad()
       {
           zakoGraphs[0] = DX.LoadGraph("キャラクタ\\zako.png");
           zakoGraphs[1] = DX.LoadGraph("キャラクタ\\zako2.png");
           zakoGraphs[2] = DX.LoadGraph("キャラクタ\\smallzako.png");
           for (int i = 0; i <zakoGraphs.Length; i++)
           {
               if (zakoGraphs[i] == 0) break;
               DX.GetGraphSize(zakoGraphs[i],out zakosizew[i],out zakosizeh[i]);
           }
       }
       public static void bombLoad()
       {
           imgefbom[0] = DX.LoadGraph("弾\\bom0.png");
           imgefbom[1] = DX.LoadGraph("弾\\bom1.png");
           imgefbom[2] = DX.LoadGraph("chara.png");//キャラ
           imgefbom[3] = DX.LoadGraph("弾\\Bomtitle.png");
       }
       public static void syokika()
       {
           zakoLoad();
           bombLoad();
           titles[0] = DX.LoadGraph("Title\\C1.png");
           titles[1] = DX.LoadGraph("Title\\C2.png");
           sinziki = DX.LoadGraph("キャラクタ\\ziki.png");
           zikiGraph = DX.LoadGraph("atari1.png");

           zikitamaGraph = DX.LoadGraph("zikitama.bmp");
           zikitamaGraph2 = DX.LoadGraph("zikitama2.bmp");
           zikitamaGraph3 = DX.LoadGraph("zikitama3.bmp");
           heart = DX.LoadGraph("heart.png");
           haikeig = DX.LoadGraph("sg2.bmp");

           zako1tGraph = DX.LoadGraph("zakot.png");

           syougekiGraph = DX.LoadGraph("syougekiha.png");
           haikei = DX.LoadGraph("back0.png");
           syougeki = DX.LoadGraph("syougekiha.png");
           Boss1n1Graph = DX.LoadGraph("Boss1-1.png");

        //   Boss1Graph = DX.LoadGraph("BossGraph.bmp");
           Boss1Graph = DX.LoadGraph("キャラクタ\\Boss1.png");

           ////////////////////////////////////////////////
          // zako1Graphs[0] = DX.LoadGraph("zakoteki.png");
           zako1Graphs[0] = DX.LoadGraph("zakoteki.png");
           zako1Graphs[1] = DX.LoadGraph("zako2.png");
           zako1Graphs[2] = DX.LoadGraph("zako3.png");
           DX.LoadDivGraph("p0.png", 2, 2, 1, 35, 35, out itemimg[0, 0]);
           DX.LoadDivGraph("p1.png", 2, 2, 1, 35, 35, out itemimg[1, 0]);
           DX.LoadDivGraph("p2.png", 2, 2, 1, 15, 15, out itemimg[2, 0]);
           DX.LoadDivGraph("p3.png", 2, 2, 1, 35, 35, out itemimg[3, 0]);
           DX.LoadDivGraph("p4.png", 2, 2, 1, 35, 35, out itemimg[4, 0]);
           DX.LoadDivGraph("p5.png", 2, 2, 1, 35, 35, out itemimg[5, 0]);
           DX.LoadDivGraph("tekitamam.png", tamakosu, tamakosu, 1, 16, 16, out tama[0]);
           DX.LoadDivGraph("Bosst1.png", 10, 5, 2, 16, 16, out btamab[0, 0]);
           DX.LoadDivGraph("Bosst2.png", 10, 10, 1, 25, 25, out btamab[1, 0]);
           DX.LoadDivGraph("b8.png", 10, 10, 1, 12, 18, out btamab[1, 0]);//要変更
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
           //  DX.LoadDivGraph("弾\\322816.png", 16, 16,1, 32,28, out otama[17, 0]);
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
           sizeget();
       }
        public static void sizeget()
        {
            DX.GetGraphSize(zikiGraph, out zgw, out zgh);
            DX.GetGraphSize(zikitamaGraph, out ztamagw, out ztamagh);
            DX.GetGraphSize(zikitamaGraph, out ztamagw, out ztamagh);
            DX.GetGraphSize(zako1Graphs[0], out zako1gw, out zako1gh);
            DX.GetGraphSize(zako1tGraph, out zako1tgw, out zako1tgh);
            DX.GetGraphSize(Boss1n1Graph, out B1gw, out B1gh);
            DX.GetGraphSize(Boss1Graph, out Boss1gw, out Boss1gh);
        }
    }
}
