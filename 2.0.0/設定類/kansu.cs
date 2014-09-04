using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public static class kansuu
    {

        private static int fx = Program.fx, fy = Program.fy, fmx = Program.fmx, fmy = Program.fmy, scx = Program.scx, scy = Program.scy;
        static Random rnd = new Random();

        public static float zikix() { return Program.zibun[0].zikix; }
        public static float zikiy() { return Program.zibun[0].zikiy; }
        public static float Cos(double x) { return (float)Math.Cos(x); }
        public static float Sin(double x) { return (float)Math.Sin(x); }
        public static float PI() { return (float)Math.PI; }//円周率
        public static float PI2() { return (float)Math.PI * 2; }//円周率の二倍
        public static float rang() { return rnd.Next(0, 1); }//0～1の乱数
        public static int rang(int a, int b) { return rnd.Next(a, b); }//a～bの乱数
        public static int rang(int a) { return rnd.Next(0, a); }
        public static float rang(float a, float b) { return (a + b * 2 * rnd.Next(10000) / 10000.0f); }//上のfloat版
        public static float rang(float ang) { return (-ang + ang * 2 * rnd.Next(10000) / 10000.0f); }//-a～aの乱数
        public static float rang(double ang) { return rang((float)ang); }//上のdouble版
        public static float Abs(double abs) { return (float)Math.Abs(abs); }
        public static float Abs(double A,double B) { return (float)Math.Abs(A-B); }//二つの数字の差の絶対値を求める
        public static float angling(double x, double y,double X,double Y) { return (float)Math.Atan2(Y - y, X - x); }//任意の二つの座標から角度を求める
        public static float zikiangle(double x, double y) { return (float)Math.Atan2(Program.syuy - y, Program.syux - x); }//自機との角度を求める
        public static bool sotoRota(double x, double y, int gw, int gh)//画面外にいるか否か
        {
            return !naka(x,y,gw,gh);
        }
        public static bool sotoRota(bool ue, double x, double y, int gw, int gh)//上(または下)以外の画面外にいるか否か
        {
            if (x-fx < -gw *2) { return true; } if (x-fx > fmx + gw / 2) { return true; }
            if (ue) { if (y-fy < -gh / 2) { return true; } }
            else { if (y-fy > fmy + gh *2) { return true; } }
            return false;
        }
        public static void sotoRotaz(ref int x, ref int y, int gw, int gh)//画面外にいるなら補正を
        {

            if (x < gw / 2) { x = gw / 2; } if (x > fmx - gw / 2) { x = fmx - gw / 2; }
            if (y < gh / 2) { y = gh / 2; } if (y > fmy - gh / 2 - fy) { y = fmy - gh / 2 - fy; }

        }
        public static void sotoRotaz(ref float x, ref float y, int gw, int gh)//上のfloatバージョン
        {

            if (x < gw / 2) { x = gw / 2; } if (x > fmx - gw / 2) { x = fmx - gw / 2; }
            if (y < gh / 2) { y = gh / 2; } if (y > fmy - gh / 2 - fy) { y = fmy - gh / 2 - fy; }

        }
        public static bool naka(double x, double y, int gw, int gh)//画面内にいるか
        {
            //return (x-fx >= -gw*2 && x-fx < fmx + gw && y-fy >= -gw && y-fy < fmy + gh*2);
            return (x >= -gw / 2 && x <= scx + gw / 2 && y >= -gh / 2 && y <= scy + gh / 2);
        }
        public static bool enatariz(double x, double y, double gw)//円を用いた自機との当たり判定
        {
            bool yes = false;
            foreach (var item in Program.zibun)
            {
                if (!item.muteki)
                {
                    if (atari.CirCollison(item.zikix, item.zikiy, gazo.zgw, x, y, gazo.zako1tgw / 2))
                    {
                        yes = true; 
                        if (item.kurai == 0)
                        {
                            item.kurai = 1;
                        }
                    }
                }
            }
            return yes;
        }


        public static float Crange = 3.3f;
        public static bool haniatariz(double x, double y, double atarihani, double speed, double angle)//当たり範囲を用いた自機との当たり判定
        {
            foreach (var zi in Program.zibun)
            {

                if (!zi.muteki)
                {
                    double r = atarihani + Crange;
                    if (speed > r)
                    {
                        double prex = x + Cos(angle + PI()) * speed;
                        double prey = y + Sin(angle + PI()) * speed;
                        double px, py;
                        for (int i = 0; i < speed / r; i++)
                        {
                            px = prex - zi.zikix;
                            py = prey - zi.zikiy;
                            if (px * px + py * py < r * r)
                            {
                                if (zi.kurai == 0) { zi.kurai = 1; }   // zi.itemdrop();
                                return true;

                            }
                            prex += Cos(angle) * r;
                            prey += Sin(angle) * r;
                        }
                    }
                    else
                    {
                        double kx = x - zi.zikix; double ky = y - zi.zikiy;

                        if (kx * kx + ky * ky < r * r)
                        {
                        //    zi.itemdrop();
                            if (zi.kurai == 0) { zi.kurai = 1; } 
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        public static void DrawLine(double x,double y,double x2,double y2,int Color)
        {
               DrawLine(x, y, x2, y2, Color, 1,false);
        }
        public static void DrawLine(double x, double y, double x2, double y2, int Color,int Thickness)
        {
            DrawLine(x, y, x2, y2, Color, Thickness, false);  
        }
        public static void DrawLine(double x, double y, double x2, double y2, int Color, int Thickness,bool Add)
        {
            if (Add) { DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, 255); }
             DX.DrawLine((int)x + fx + Program.dnx, (int)y + fy + Program.dny, (int)x2 + fx + Program.dnx, (int)y2 + fy + Program.dny, Color, Thickness);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
        }
        public static void DrawString(double x,double y,string Str,int Color)//ゲーム画面内での文字列描画
        {
            DX.DrawString((int)x + fx + Program.dnx, (int)y + fy + Program.dny, Str, Color);
        }
        public static void DrawString(double x, double y, string Str, int Color,int EdgeColor)//その2
        {
            DX.DrawString((int)x + fx + Program.dnx, (int)y + fy + Program.dny, Str, Color, EdgeColor);
        }
        public static void DrawBox(double x, double y, double x2, double y2, int Color, int FillFlag)//ゲーム画面内のDrawBox
        {
            DX.DrawBox((int)x + fx+Program.dnx, (int)y + fy+Program.dny, (int)x2+Program.dnx+fx,(int)y2+fy+Program.dny, Color, FillFlag);
        }
        public static void DrawPixel(int x, int y, int Color)
        {
            DX.DrawPixel(x + fx + Program.dnx, y + fy + Program.dny, Color);
        }
        public static void setarea1()//描画範囲のセット
        { 
            DX.SetDrawArea(fx + Program.dnx, fy + Program.dny, scx + Program.dnx, scy + Program.dny - fy);
        }
        public static void setareaend()//描画範囲の補正解除
        {
            DX.SetDrawArea(0, 0, scx + Program.hamix, scy);
        }

        


        public static void DrawRotaGraphfk(float x, float y, double Exrate, double angle, int grHandle, int TransFlag, bool Bil)//描画
        {
            if (Bil)
            {
                DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
            }
            DX.DrawRotaGraphF(x + fx+Program.dnx, y + fy+Program.dny, Exrate, angle, grHandle, TransFlag);

            DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
        }
    }
}
