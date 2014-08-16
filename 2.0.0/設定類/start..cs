using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace _2._0._0
{
    public static class start
    {
        private static int myoutyou;
        public static int size = 5;
        private static int ssize = 1;
        private static int startgameGraph;
        private static int exitGraph ;
        private static int howto;
        private static int sentaku2 ;
        // private static int zkettei = DX.LoadGraph("z.bmp");

        private static bool exit ;
        private static bool start1;
        private static bool how;
        private static bool up;
        private static bool down ;
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
   
            size = 5; ssize = 1;
            startgameGraph = DX.LoadGraph("startgame.bmp");
            exitGraph = DX.LoadGraph("exit.bmp");
            howto = DX.LoadGraph("howto.bmp");
            sentaku2 = DX.LoadGraph("sentaku2.bmp");
            exit = false; start1 = true; how = false; down = false; r = g = b = wave = 0;
            time = stime = 1;
            selecting = 0;
        }
        public static void gamen()
        {
            present.time = 0;
            draw();
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

         //   DX.DrawGraph(150, 283, howto, DX.TRUE);
            DX.DrawString(150, 183, "Start Game", (selecting==0?Program.red:Program.white));
            DX.DrawString(150, 283, "How to Play", (selecting == 1 ? Program.red : Program.white));
            DX.DrawString(150, 383, "Exit", (selecting == 2 ? Program.red : Program.white));
         //   DX.DrawGraph(150, 183, startgameGraph, DX.TRUE);
          //  DX.DrawGraph(150, 383, exitGraph, DX.TRUE);
            //    DX.DrawGraph(300, 453, zkettei, DX.TRUE);
            if (Program.key[DX.KEY_INPUT_DOWN] == 1) { selecting=(++selecting)%koumokusu; }
            else if (Program.key[DX.KEY_INPUT_UP] == 1) { selecting = (--selecting) % koumokusu; }
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
                        Program.gamemode = 3; DX.SetFontSize(15);
                        time = 0;
                        size = 5;
                        r = 0; g = 0; b = 0;
                        Program.enter = true;
                        break;
                    case 2:
                        Program.gamemode = 7;
                        break;
                }
            }
            if (!Program.enter && DX.CheckHitKey(DX.KEY_INPUT_RETURN) != 0)
            {
                if (start1)
                {
                    Program.gamemode = 4; DX.SetFontSize(15);
                    time = 0;
                    size = 5;
                    r = 0; g = 0; b = 0;
                    Program.enter = true;
                }
                else if (exit)
                {
                    Program.gamemode = 7;
                }
                else if (how)
                {
                    Program.gamemode = 3; DX.SetFontSize(15);
                    time = 0;
                    size = 5;
                    r = 0; g = 0; b = 0;
                    Program.enter = true;
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
