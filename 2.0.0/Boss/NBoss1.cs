using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
  public class NBoss1:Boss
    {
      public NBoss1(float X, float Y)
      {
          zx = X; zy = Y; gw = gazo.Boss1gw; gh = gazo.Boss1gh;
          set_life = new int[10];
          damage = 5;
          for (int i = 0; i < set_life.Length; i++)
          {
              set_life[i] = 5000;
          }
          input_phypos(Program.scx-70,  50, 50);
          danmakusu = 2;
      }
      public override void iroiro()
      {
          if (state==2)
          {
              switch (knd)
              { 
                  case 0:
                     shot10();
                      break;
 
                  default:
                      end = true;
                      break;
              }
              DX.DrawString(0, 0, "ライフ：" + life + "残り時間：" + endtime, DX.GetColor(255, 255, 255));
              // DX.DrawBox(10, 10, 10 +hyoujirai, 20, DX.GetColor(255, 255, 255), DX.TRUE);
              }
          kansuu.DrawRotaGraphfk(dx, dy, 1, 0, gazo.Boss1Graph, DX.TRUE, true);
          hantei();
          graph_bossefe();
      }
    }
}
