﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace _2._0._0
{
    public static class start
    {

        public static int size = 5;
        private static int ssize = 1;
        private static int startgameGraph;
        private static int exitGraph ;
        private static int howto;
        private static int sentaku2 ;
        private static int haikei;
        private static int[] options=new int[4];
        private static int selecting;
        public static int r ;
        public static int g;
        public static int b ;
        private static int time ;
        private static int stime ;
        private static int wave ;
        private const int koumokusu = 4;
        public static void syokika()
        {
   
            startgameGraph = DX.LoadGraph("startgame.bmp");
            exitGraph = DX.LoadGraph("exit.bmp");
            howto = DX.LoadGraph("howto.bmp");
            sentaku2 = DX.LoadGraph("sentaku2.bmp");
            options[0] = DX.LoadGraph("option\\Game Start.png");
            options[1] = DX.LoadGraph("option\\Extra Start.png");
       //     options[2] = DX.LoadGraph("option\\Practice.png");
            options[2] = DX.LoadGraph("option\\Option.png");
            options[3] = DX.LoadGraph("option\\Exit.png");
            haikei = DX.LoadGraph("option\\haistart.png");

           r = g = b = wave = 255;
           
        }
        public static void syokika2()
        {
            time = stime = 1;
            size = 5; ssize = 1;
            selecting = 0; r = g = b = wave = 255;
        }
        public static void gamen()
        {
 //           DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(g, b, r), DX.TRUE);
           DX.DrawExtendGraph(0, 0, Program.realscx, Program.realscy, haikei, 1);
            present.time = 0;
          //  draw();
            if (size <= 5)
            {
                ssize = 1;
            }
            if (size > 100)
            {
                ssize = 0;
            }

            size += ssize;

            if (size > 50)
            {
                DX.SetFontSize(50);
            }

            else
            {
                DX.SetFontSize(size);
            }

            //   */
            DX.DrawString(115, 30, "しゅーてぃんぐげぇむつぅ", DX.GetColor(255, 255, 0));

#warning エディットモード?
//Program.gamemode = 8;

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA,50);
            for (int i = 0; i < options.Length; i++)
            {
                if (selecting == i) { continue; }
                DX.DrawGraph(450+10*i, 150 + 50 * i,options[i],DX.TRUE);
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            DX.DrawGraph(450+10*selecting+30, selecting * 50 + 150, options[selecting], DX.TRUE);
            if (Program.key[DX.KEY_INPUT_DOWN] == 1) { selecting=(++selecting)%koumokusu; }
            else if (Program.key[DX.KEY_INPUT_UP] == 1) { selecting = (--selecting) % koumokusu; }
            if (selecting < 0) { selecting += koumokusu; }
            if (!Program.enter&&Program.key[DX.KEY_INPUT_RETURN] == 1)
            {
                switch (selecting)
                {
                    case 0:
                        Program.gamemode = 4; DX.SetFontSize(15);
                        time = 0;
                        size = 5;
                        r = 0; g = 0; b = 0;
                        Program.enter = true;
                        break;
                    case 1:
                        Program.gamemode = 4; DX.SetFontSize(15);
                        gamemode4.extra = true;
                        time = 0;
                        size = 5;
                        r = 0; g = 0; b = 0;
                        Program.enter = true;
                        break;
                    case 2:
                        Program.gamemode = 3;
                        break;
                    case 3:
                        Program.gamemode = 7;
                        break;
                }
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) != 0)
            {
                Program.gamemode = 1;
                DX.SetFontSize(15);
                size = 5;
                time = 0;
                r = 0; g = 0; b = 0;
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 0)
            {
                Program.enter = false;
            }
        }
        static void draw()
        {
            if (wave % 6 == 0)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(r, g, b), DX.TRUE);
            }
            else if (wave % 6 == 1)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(g, b, r), DX.TRUE);

            }
            else if (wave % 6 == 2)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(r, b, g), DX.TRUE);

            }
            else if (wave % 6 == 3)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(g, r, b), DX.TRUE);

            }
            else if (wave % 6 == 4)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(b, r, g), DX.TRUE);

            }
            else if (wave % 6 == 5)
            {
                DX.DrawBox(0, 0, Program.scx + 300, Program.scy, DX.GetColor(b, g, r), DX.TRUE);

            }
            if (time == 765)
            {
                wave += 1;
                stime *= -1;
            }
            if (time == 0)
            {
                wave += 1;
                stime *= -1;
            }
            if (time < 0)
            {
                time = 1;
                stime *= -1;
            }
            if (time > 765)
            {
                stime *= -1;
                time = 765;
            }
            time += stime;
            if (time <= 255)
            {
                r = time;
            }
            else
            {
                r = 255;
            }
            if (time > 255 && time <= 510)
            {
                g = time - 255;
            }
            else if (time <= 255)
            {
                g = 0;
            }
            else
            {
                g = 255;
            }

            if (time > 510 && time <= 765)
            {
                b = time - 510;
            }
            else
            {
                b = 0;
            }
        }
        //    DX.DrawString(100, 0, "a", DX.GetColor(100, 100, 100));


    }
}
