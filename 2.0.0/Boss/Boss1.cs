﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class Boss1 : Boss
    {

        public Boss1(float X, float Y, bool Tochu)
        {
            zx = X; zy = Y; gw = gazo.Bossgws[0]; gh = gazo.Bossghs[0]; nboss = Tochu;
            gaz = gazo.BossGraphs[0];
            set_life = new int[10];
            if (nboss)
            {
                danmakusu = 1;
            }
            else
            {
                danmakusu = 3;
            }
            for (int i = 0; i < set_life.Length; i++)
            {
                set_life[i] = 5000;
            }
            input_phypos(Program.scx / 2, 150.0f, 50);

        }
        public override void iroiro()
        {
            if (state == 2)
            {
                if (nboss)
                {
                    switch (knd)
                    {
                        case 0:

                            shot1();
                            shot6();
                            break;

                        default:
                            end = true;
                            break;
                    }
                }

                else
                {
                    switch (knd)
                    {
                        case 0:
                            shot13(500);
                            shot2();
                            break;
                        case 1:
                            shot14(800);
                            shot4();
                            shot6();
                            break;
                        case 2:
                            shot3(true);
                            shot14(650);
                            break;
                        default:
                            end = true;
                            break;
                    }
                }
                DX.DrawString(0, 0, "ライフ：" + life + "残り時間：" + endtime, DX.GetColor(255, 255, 255));
            }

        }


    }
}
