using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class effect
    {
        public int syurui;
        public int alpha;
        public float size;
        public int value;
        public float x, y,spd,mvang,er,ang,brt;
        public int eff, img;
        public int time = 0;
        public int hikari = kansuu.rang(0,4);
        public int r = kansuu.rang(0, 256);
        public int b = kansuu.rang(0, 256);
        public int g = kansuu.rang(0, 256);
        public bool seizon = true;
        public int knd;
       // public effect(float X,float Y,int Syurui,int Value,int Knd)
        //{
        //    x = X; y = Y; syurui = Syurui; value=Value;
        //    knd = Knd;
       // }
        public effect(float x, float y, float er, float spd, float ang,float mvang,float brt, int eff, int img, int knd,int value)
        {
            this.x = x; this.y = y; this.er = er; this.spd = spd; this.ang = ang; this.mvang = mvang; this.eff = eff; this.img = img; this.knd = knd; this.value = value; this.brt = brt;
        }
        public void main()
        {
            time += 1;
            value -= 1;
            switch (knd)
            {
                case 0:
                    ef0();
                    break;
                case 1:
                    ef1();
                    break;
                case 2:
                    ef2();
                    break;
                case 3:
                    ef3();
                    break;
                default:
                   
                    break;
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            DX.SetDrawBright(255, 255, 255);
            if (value == 0) { seizon = false; }
        }

        private void ef0()
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, value);
            DX.SetDrawBright(r, b, g);
            size = 0.1f + beje.Get(beje.ePrm_t.eRapid_Lv7, beje.ePrm_t.eSlow_Lv7, (float)time / (1504 * 1)) / 10;
            kansuu.DrawRotaGraphfk(x, y, size, 0, gazo.syougeki, DX.TRUE, true);
        }
        private void ef1()//ボムエフェクト
        {
            x += kansuu.Cos(mvang) * spd;
            y += kansuu.Sin(mvang) * spd;
            if (time < 60) { spd -= (0.2f + time * time / 3000.0f); }
            else if (time == 60) { spd = 0; Program.dnflg = 1; Program.dnc = 0; Program.dns = 11; Program.dnt = 20; }
            er += 0.015f;
            if (time < 51) { brt += 5; }
            else if (time >= 60) { er += 0.04f; brt -= 255 / 30.0f; }
            if (time >= 90) { seizon = false; }
        }
        private void ef2()//ボムエフェクト(キャラクタ)
        {
            x += kansuu.Cos(mvang) * spd;
            y += kansuu.Sin(mvang) * spd;
            if (time < 51) { brt += 4; }
            else if (time > 130 - 51) { brt -= 4; }
            if (time >= 130) { seizon = false; }
        }
        private void ef3()//ボムのエフェクト(ライン)
        {
            x += kansuu.Cos(mvang) * spd;
            y += kansuu.Sin(mvang) * spd;
            if (time < 51) { brt += 2; }
            else if (time > 130 - 51) { brt -= 2; }
            if (time >= 130) { seizon = false; }
        }
    }
}
