using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class Boss1 : Boss
    {
       
        public Boss1(float X, float Y)
        {
            zx = X; zy = Y; gw = gazo.Boss1gw; gh = gazo.Boss1gh;
            gaz = gazo.Boss1Graph;
            set_life = new int[10] ;
            damage = 5;
            danmakusu = 4;
            for (int i = 0; i < set_life.Length; i++)
            {
                set_life[i] = 5000;
            }
            input_phypos(Program.scx/2,150.0f,50);
        }
        public override void iroiro()
        {
           
       
 
            if (state == 2)
            {
                switch (knd)
                {
                    case 0:
                     //   shot1();
                        shot6();
                        break;
                    case 1:
                           shot3();
                        shot6(); 
                        break;
                    case 2:
                         shot7("漢字");  shot8("sample龍");
                  
                        break;
                    case 3:
                   shot5();
                        break;
                    default:
                        end = true;
                        break;

                }
                DX.DrawString(0, 0, "ライフ：" + life+"残り時間："+endtime, DX.GetColor(255, 255, 255));
           //     DX.DrawBox(10, 10, 10 + life , 20, DX.GetColor(255, 255, 255), DX.TRUE);
            }
        //    kansuu.DrawRotaGraphfk(dx, dy, 1, 0, gaz, DX.TRUE,true);
            hantei();
            graph_bossefe();
        }

        public override void danhenka()
        { 
        
        }

    }
}
