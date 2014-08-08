using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
namespace _2._0._0
{
    public static class zakoudou
    {
       
        public static void enemy_pattern0(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time > 30 + wait) { sy = -0.5f - 0.1f * num; return; }
            else if (ey < Program.scy / 2) { sx = 0; sy = 1 + 0.1f * num; time = 0; return; }
            else { sy = 0; }
        }
        public static void enemy_pattern1(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time > 30 + wait) { sy = 1; sx = 1; return; }
            else if (ey < Program.scy / 2) { sy = 1 + 0.1f * num; time = 0; sx = 0; return; }
            else { sy = 0; sx = 0; }
        }
        public static void enemy_pattern2(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time > 30 + wait) { sy = 1; sx = -1; return; }
            else if (ey < Program.scy / 2) { sy = 1 + 0.1f * num; time = 0; sx = 0; }
            else { sy = 0; sx = 0; }

        }
        public static void enemy_pattern3(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0) { sy = 3; sx = 0; }
            sx -= 5 / 100.0f;//左向き加速
            sy -= 5 / 100.0f;//減速

        }
        public static void enemy_pattern4(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0) { sy = 3; sx = 0; }
            sx += 5 / 100.0f;//左向き加速
            sy -= 5 / 100.0f;//減速

        }
        public static void enemy_pattern5(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {

                sx += 1;
                sy = 2;
            }

        }
        public static void enemy_pattern6(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {

                sx -= 1;
                sy = 2;
            }

        }
        public static void enemy_pattern7(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {
                sx = 0; sy = 0;
            }
            else if (time == wait)
            {

                sx += 0.7f;
                sy -= 0.3f;
            }

        }
        public static void enemy_pattern8(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {
                sx = 0; sy = 0;
            }
            else if (time == wait)
            {

                sx -= 0.7f;
                sy -= 0.3f;
            }

        }
        public static void enemy_pattern9(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {
                sx = 0; sy = 0;
            }
            else if (time == wait)
            {

                sy = -1;
            }

        }

        public static void enemy_pattern10(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait, ref float ang, ref float speed)
        {
            if (time == 0)
            {
                sx = 0; sy = 4;
            }
            else if (time == 40)
            {

                sy = 0;
            }
            else if (time >= 400)
            {
                sy -= 0.05f;
            }
            else if (time >= 40)
            {
                if (time % 80 == 0)
                {
                    int r = Math.Cos(ang) < 0 ? 0 : 1;
                    speed = 6 + kansuu.rang(2);
                    ang = kansuu.rang(Math.PI / 4) + kansuu.PI() * r;


                }
                speed *= 0.95f;
            }


        }

        public static void enemy_pattern11(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {
                sx = 0; sy = 3;
            }
            else if (time == wait)
            {

                sy = -1;
            }

        }
        public static void enemy_pattern12(float ex, float ey, ref float sx, ref float sy, ref int time, int num, int wait)
        {
            if (time == 0)
            {
                sx = 0; sy = 3;
            }
            else if (time == wait)
            {

                sy = -1;
            }

        }
        public static bool enemyfile(ref float sx, ref float sy, ref int time, int num,ref int wait,int cnt,ref StreamReader read, string filename)
        {
            if (cnt == 0)
            {
               read = new StreamReader("ugokikata\\" + filename + ".csv", false);
           
            }
            if (time == 0)
            {
                var line = read.ReadLine();
                if (line.Contains("#")) 
                {
                    return true;
                }
                var values = line.Split(',');
                sx = float.Parse(values[0]);
                sy = float.Parse(values[1]);
                float kyori = (float)Math.Sqrt(sx * sx + sy * sy);
                if (sx == 0 && sy == 0)
                {
                    wait = int.Parse(values[2]);
                }
                else
                {
                    wait = (int)kyori / int.Parse(values[2]);
                    if (wait == 0) wait++;
                    sx = sx / wait;
                    sy = sy / wait;
                }
            }
            if (time == wait)
            {
                if (read.EndOfStream)
                { 
                    read.Close();
                    return true;
                   
                }
                else
                {
                    time = -1;
                }
            }
            return false;

        }
    }

}

