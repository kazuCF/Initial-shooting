using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using System.IO;
using Microsoft.VisualBasic;
namespace _2._0._0
{
    public static class Finished
    {
        private static int[] haikeis = new int[3];
        private static int[] finish = new int[3];
        private static int[] cont = new int[2];
        private static List<int> high = new List<int>();
        private static string[] clearst = new string[] { "ゲームクリア！", "ボリューム少なくてスイマセン、来年にご期待ください。", "物足りない場合はオプションからボムや残機数の", "初期設定を変えて厳しくしてみては？", "エンターキーでタイトルに戻ります" };
        private static string[] overst = new string[] { "ゲームオーバー！", "どうしてもクリアできないときはオプションから", "ボムや残機数の初期設定を変えてみては？", "エンターキーでタイトルに戻ります" };
        private static int selecting = 0;
        private static int cnt = 0;
        private static int font;
        public static void syokika()
        {
            finish[0] = DX.LoadGraph("option\\Clear.png");
            finish[1] = DX.LoadGraph("option\\Over.png");
            cont[0] = DX.LoadGraph("option\\Continue.png");
            cont[1] = DX.LoadGraph("option\\Return.png");
            haikeis[0] = DX.LoadGraph("option\\haiclear.png");
            haikeis[1] = DX.LoadGraph("option\\haiover.png");
            haikeis[2] = DX.LoadGraph("option\\haicon.png");
            font = DX.CreateFontToHandle("祥南行書体", 20, 3, 3, DX.DX_FONTTYPE_EDGE);
        }
        public static void Fin(int how)
        {
            DX.DrawExtendGraph(0, 0, Program.realscx, Program.realscy, haikeis[how], 0);
            switch (how)
            {
                case 0:
                    clear(); key1();
                    break;
                case 1:
                    over(); key1();
                    break;
                case 2:
                    con();
                    break;
            }

        }
        private static void highscore(int score)
        {
            string inputText;
            StreamReader read;
            inputText = Interaction.InputBox("名前を入力してください", "ハイスコア更新！", "", 200, 100);
            high.Clear();
            read = new StreamReader("datafiles\\gamename.txt", Encoding.GetEncoding("Shift_JIS"));
            string line;
            for (int i = 0; (line = read.ReadLine()) != null; i++)
            {
               
            }
        }
        private static void clear()
        {
            DX.DrawGraph(70, 20, finish[0], 1);
            for (int i = 0; i < clearst.Length; i++)
            {
                DX.DrawStringToHandle(70, 200 + i * 20, clearst[i], DX.GetColor(255, 255, 255), font);
            }
          
           
        }
        private static void over()
        {
            DX.DrawGraph(70, 20, finish[1], 1);
            for (int i = 0; i < overst.Length; i++)
            {
                DX.DrawStringToHandle(70, 200 + i * 20, overst[i], DX.GetColor(255, 255, 255), font);
            }
        }
        private static void key1()
        {
            cnt++; if (Program.key[DX.KEY_INPUT_RETURN] == 1) { present.Loading2(); Program.gamemode = 2; cnt = 0; }
        }
        /***********************************************************/
        private static void con()
        {
            if (gamemode4.continues == 0) { Program.gamemode = 6; }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 50);
            for (int i = 0; i < 2; i++)
            {
                if (selecting == i) { continue; }
                DX.DrawGraph(50, 50 + 50 * i, cont[i], DX.TRUE);
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            DX.DrawGraph(50, selecting * 50 + 50, cont[selecting], DX.TRUE);
            DX.DrawStringToHandle(50, 150, "あと" + gamemode4.continues + "回コンティニューできます", DX.GetColor(255, 255, 255), font);
          key2();
        }
        private static void key2()
        {
            if (Program.key[DX.KEY_INPUT_UP] == 1) { selecting--; }
            if (Program.key[DX.KEY_INPUT_DOWN] == 1) { selecting++; }
            selecting = (selecting + 2) % 2;
            switch (selecting)
            {
                case 0:
                    if (Program.key[DX.KEY_INPUT_RETURN] == 1) { Program.zibun[0].boms = Program.bomsyoki; Program.zibun[0].life = Program.lifesyoki; gamemode4.continues--; Program.gamemode = 4; }
                    break;
                case 1:
                    if (Program.key[DX.KEY_INPUT_RETURN] == 1) { present.Loading2(); gamemode4.continues = 3; Program.gamemode = 6; }
                    break;
                default:
                    break;
            }

        }

    }
}
