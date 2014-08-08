using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class tama
    {
        public bool seizon = true;
        public int _type;
        public int kakudotype;
        public float ztamax;
        public float ztamay;
        public float ztamamukix;
        public float ztamamukiy;
        public float zidouy;
        public float ugoki;
        public int syou2;
        private int zikitamakakudo;
        private int zure;
        private double speed = 20.0f;
        public tama(int type, int kakudo1, int X, int Y, int tamakosuu)
        {
            _type = type;
            ztamax = X;
            ztamay = Y;
            if (type % 2 == 0)
            {
                zure = (type / 2)*kakudo1;
            }
            else
            {
                zure = (-1 * type / 2)*kakudo1;
            }
            zikitamakakudo = 90 + zure;
            ztamamukix = (float)(Math.Cos(zikitamakakudo * Program.kakudokihon));
            ztamamukiy = (float)(Math.Sin(zikitamakakudo * Program.kakudokihon));

        }
        public void ido()
        {
          
            ztamax -= (float)(ztamamukix * speed);
            ztamay -= (float)(ztamamukiy * speed);
            if (kansuu.sotoRota(ztamax, ztamay, gazo.ztamagw, gazo.ztamagh)) { seizon = false; }
            if (Program.brt != 255) { DX.SetDrawBright(Program.brt, Program.brt, Program.brt); }
            kansuu.DrawRotaGraphfk(ztamax, ztamay, 1, 1, gazo.zikitamaGraph, DX.TRUE, true);
            if (Program.brt != 255) { DX.SetDrawBright(255, 255, 255); }
        }
    }
}
