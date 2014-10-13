using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public static class option
    {
        public static int[] OPs = new int[] { 3, 3, 0, 0 };
        private static int opkosu = OPs.Length;
        private static int[] gazos = new int[4];
        private static int haikei;
        private static int font ;
        private static bool config=false;
        private static int Option = 0;
        public static void syokika()
        {
            gazos[0] = DX.LoadGraph("option\\Player.png");
            gazos[1] = DX.LoadGraph("option\\Bombs.png");
            gazos[2] = DX.LoadGraph("option\\KeyConfig.png");
            gazos[3] = DX.LoadGraph("option\\Quit.png");
            haikei = DX.LoadGraph("option\\haiop.png");
            font = DX.CreateFontToHandle("HGS行書体", 30, 4, DX.DX_FONTTYPE_ANTIALIASING);
        }
        public static void settei()
        {
            byouga();
            key();
        }
        private static void key()
        {
            if (Program.key[DX.KEY_INPUT_ESCAPE] != 0)
            {
                Program.gamemode = 2; Program.key[DX.KEY_INPUT_ESCAPE]++;
            }
            if ((Program.key[Program.ENTER] == 1 || Program.key[DX.KEY_INPUT_Z] == 1))
            {
                if (Option == 2) { config = true; }
                if (Option == 3) { Program.gamemode = 2; }
            }
            else if (Program.key[Program.UP] == 1) { Option = (--Option) % opkosu; }
            else if (Program.key[Program.DOWN] == 1) { Option = (++Option) % opkosu; }
            else if (Program.key[Program.RIGHT] == 1) { OPs[Option]++; }
            else if (Program.key[Program.LEFT] == 1) { OPs[Option]--; }
            if (OPs[0] < 1) { OPs[0] = 1; } if (OPs[0] > 8) { OPs[0] = 8; }
            if (OPs[1] < 1) { OPs[1] = 1; } if (OPs[1] > 5) { OPs[1] = 5; }
            if (Option < 0) { Option += opkosu; }
            Program.lifesyoki = OPs[0];
            Program.bomsyoki = OPs[1];
        }
        private static void byouga()
        {
            DX.DrawExtendGraph(0, 0, Program.realscx, Program.realscy, haikei, 0);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 50);
            for (int i = 0; i < opkosu; i++)
            {
                if (Option == i) { continue; }
                DX.DrawGraph(50, 130 + 70 * i, gazos[i], 1);
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            DX.DrawGraph(50, Option * 70 + 130, gazos[Option], 1);
            for (int i = 1; i < 9; i++)
            {
                bool tyuu = i == OPs[0];
                DX.DrawStringToHandle(240 + i * 25, tyuu ? 145 : 140, "" + i, tyuu ? DX.GetColor(255, 0, 0) : DX.GetColor(255, 255, 255),font);
                if (i >= 6) { continue; }
                tyuu = i == OPs[1];
                DX.DrawStringToHandle(240 + i * 25, tyuu ? 215 : 210, "" + i, tyuu ? DX.GetColor(255, 0, 0) : DX.GetColor(255, 255, 255), font);
            }
        }
    }
}
