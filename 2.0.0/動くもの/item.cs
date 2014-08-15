using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace _2._0._0
{
    public class item
    {
        public bool seizon = true;
        public float x, y;
        public float sx = 0, sy = -10;
        public int syurui;
        public int state;
        public float speed;
        public float range = 100;
        public double[] r = new double[] { 0.6, 0.6, 1.0, 0.6, 1.0, 1.0 };//dat/img/itemの画像の拡大率
        public float v;
        public item(float X, float Y, int Syurui, float V)
        {
            x = X; y = Y; syurui = Syurui; speed = V;
        }
        public void main()
        {
            if (state == 1) { v = 15; } else { v = 3; }
            double kyorix = Program.syux - x, kyoriy = Program.syuy - y;

            if (state == 0)
            {
                if (speed < 2.5f) { speed += 0.06f; } y += speed;

                if (Program.border) { state = 1; }
                if (DX.CheckHitKey(DX.KEY_INPUT_LSHIFT) != 0 && kyorix * kyorix + kyoriy * kyoriy < range * range)
                {
                    x += (float)Math.Cos(kansuu.zikiangle(x, y)) * v;
                    y += (float)Math.Sin(kansuu.zikiangle(x, y)) * v;
                }
            }

            else if (state == 1)
            {

                if (!Program.border) { state = 0; }

                x += (float)Math.Cos(kansuu.zikiangle(x, y)) * v;
                y += (float)Math.Sin(kansuu.zikiangle(x, y)) * v;

            }
            kansuu.DrawRotaGraphfk(x, y, r[syurui], Math.PI * 2 * (Program.count % 120) / 120, gazo.itemimg[syurui, 1], DX.TRUE, true);
            kansuu.DrawRotaGraphfk(x, y, r[syurui] * 0.8, -Math.PI * 2 * (Program.count % 120) / 120, gazo.itemimg[syurui, 1], DX.TRUE, true);
            kansuu.DrawRotaGraphfk(x, y, r[syurui], 0, gazo.itemimg[syurui, 0], DX.TRUE, true);
            if (speed >= 0 || state == 1)
            {

                if (kyorix * kyorix + kyoriy * kyoriy < 25 * 25)
                {
                    switch (syurui)
                    {
                        case 0: Program.power += 3; break;
                        case 1: Program.point += 3; break;
                        case 2: Program.score += 3; break;
                        case 3: break;
                        case 4: Program.power += 30; break;
                        //アイテム　0:小パワー 1:小点 2:弾点 3:小金 4:大パワー 5:大金

                    }


                    seizon = false;
                }
            }

            if (syurui == 2) { if (kansuu.sotoRota(false, x, y, 15, 15)) { seizon = false; } }
            else
            {
                if (kansuu.sotoRota(false, x, y, 35, 35))
                {
                    seizon = false;
                }
            }
        }
    }
}
