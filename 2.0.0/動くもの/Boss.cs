using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
namespace _2._0._0
{
    public  class Boss : en
    {
#region 宣言
        public bool nboss=false;
        public bool fend=false;
        public bool end = false;
        protected float BOSS_POS_X = Program.fmx / 2;
        protected float BOSS_POS_Y = 150.0f, v0x = 0, v0y = 0, ax, ay;
        protected float prex, prey;
        protected int set_t;
        protected bool saisyo = true;
        protected bool dansaisyo = true;
        protected bool phyflag = true, shotflag = false;
        public  int knd, state, endtime;
        protected int phycnt, wtime;
        protected int[] set_life = new int[100];
        protected int  life_max;   
      protected List<Tdan> dan = new List<Tdan>();
      //  protected int shot_cnt = 0;
        protected float angle;
        protected float dx, dy;
        protected int fucount = 0;
        protected static int count = -1;
        protected  int danmakusu = 1;
        protected float[] shot_base_angle = new float[10];
#endregion
        public void syokika()
        {
            effecttime = 255;
            count = -1;
            fucount = 0;
            dan.Clear();
            BOSS_POS_Y = 150.0f; v0x = 0;v0y = 0;
            BOSS_POS_X = Program.fmx / 2;
        }
        protected void input_phy(int t)//tは移動所要時間
        {
            if (t == 0) { t = 1; }
            float ymaxx, ymaxy;
            phyflag = true;
            dantime = 0;
            set_t = t;
            phycnt = 0;
            ymaxx = zx - BOSS_POS_X;
            

            v0x = 2 * ymaxx / t;
            ax = 2 * ymaxx / (t * t);
            prex = zx;

            ymaxy = zy - BOSS_POS_Y;
            v0y = 2 * ymaxy / t;
            ay = 2 * ymaxy / (t * t);
            prey = zy;
        }
        protected void calc()
        {
            float t = phycnt;
            zx = (float)prex - ((v0x * t) - 0.5f * ax * t * t);//現在いるべきx座標計算
            zy = (float)prey - ((v0y * t) - 0.5f * ay * t * t);//現在いるべきy座標計算

            phycnt++;
            if (phycnt >= set_t)
            {//移動にかける時間分になったら
                phyflag = false;
            }
        }


        protected void input_phypos(float x, float y, int t)//tは移動にかける時間
        {
            float ymax_x; float ymax_y;
            if (t == 0) { t = 1; }
            phyflag = true; phycnt = 0; set_t = t;
            ymax_x = zx - x; ymax_y = zy - y;
            v0x = 2 * ymax_x / t; v0y = 2 * ymax_y / t;
            ax = 2 * ymax_x / (t * t); ay = 2 * ymax_y / (t * t);
            prex = zx; prey = zy;
        }


        protected int move_boss_pos(float x1, float y1, float x2, float y2, float dist, int t)
        {
            float x, y, anglet;
            for (int i = 0; i < 1000; i++)
            {
                x = zx; y = zy;
                anglet = kansuu.rang(PI);
                x += kansuu.Cos(anglet) * dist; y += kansuu.Sin(anglet) * dist;
                if (x1 <= x && x <= x2 && y1 <= y && y <= y2)
                {
                    input_phypos(x, y, t);
                    return 0;
                }

            }
            return -1;
        }

        protected void enter_boss(int num)
        {
            if (num == 0)
            {
                gamemode4.bossnum++;
                gamemode4.bosschuu = true;
                knd = -1;
            }
            endtime = 9900;
            dan.Clear();
            state = 1;
            dantime = 0;
            knd++;
            if (state == 1 && knd == danmakusu) { end = true; }
            wtime = 0;
            dansaisyo = true;
            shot_cnt = 0;
            danhenka();
        }
        protected void enter_boss_shot()
        {
           wtime = 0; state = 2; life = set_life[knd]; life_max = (int)life;
            shotflag = true;
        }
        protected void wait_enter()
        {
            int t = 140;
            wtime++; if (wtime > t) { enter_boss_shot(); }
        }
        protected void calcboss()
        {
            dx = zx; dy = zy + kansuu.Sin(PI2 / 130 * (fucount % 130)) * 10;
        }

        public void boss_shot_main()
        {
            if (saisyo) { enter_boss(0); saisyo = false; }
            calcboss(); Program.enter_func("ボス計算", 0);
            if (phyflag) { calc(); }
            if (state == 2 && (life <= 0 || endtime <= 0)) { enter_boss(1); }
            else if (state == 1) { wait_enter(); }
            iroiro();
            hantei();
            graph_bossefe();
            Program.enter_func("ボスメイン", 0);
            if (state == 2)
            {
                int hyoujirai = Program.scx * (int)life / life_max;

                shot_calc(); Program.enter_func("ボスショット", 0);
                kansuu.DrawBox(0, 0, hyoujirai, 20, DX.GetColor(255, 255, 255), DX.TRUE);
            }
            if (end)
            {
                hyouji = false;
                DX.SetDrawBright(255, 0, 0); fend = dieefe(1); DX.SetDrawBright(255, 255, 255);
            }
            else
            {
                if (state == 2)
                {
     //               DX.SetDrawBlendMode(DX.DX_BLENDMODE_SUB, 255);
                }
                kansuu.DrawRotaGraphfk(dx, dy, 1, 0, gaz, DX.TRUE, false);
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            } fucount += 1;
        }
        
        override public void shot_calc()
        {
            dan.RemoveAll(c => Program.isbom);
            if (ziki.bommcool == 60)
            {
                life -= 300;
            }
            else if (ziki.bommcool == 130)
            {
                shot_cnt = 0;
            }
            endtime--; if (endtime < 0) { life = 0; }
            foreach (Tdan tama in dan.Where(c => c.effect == 0))
            {
                dodan(tama);
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, 255);

            foreach (Tdan tama in dan.Where(c => c.effect == 1))
            {
                dodan(tama);
            }
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            shot_cnt++;

            angle = this.zAtan2();
            dan.RemoveAll(x => !x.seizon);

        }

        public virtual void iroiro()
        { }
        public virtual void danhenka()
        { }
        public virtual void graph_bossefe()
        {

        }
        protected void dodan(Tdan tama)
        {
            if (tama.inswitch)
            {
                if (kansuu.sotoRota(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagh[tama.size]) || kansuu.haniatariz(tama.x, tama.y, tama.atarihani, tama.speed, tama.angle))
                {
                    tama.seizon = false; return;
                }
                if (tama.speed != 0) { tama.dispangle = tama.angle + PI / 2; }
                else { tama.dispangle = kansuu.angling(0, 0, tama.sx, tama.sy) + PI / 2; }
                kansuu.DrawRotaGraphfk(tama.x, tama.y, 1, tama.dispangle, gazo.otama[tama.size, tama.col], DX.TRUE, true);

            }
            else { if (kansuu.naka(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagw[tama.size])) { tama.inswitch = true; }; }
            tama.x += kansuu.Cos(tama.angle) * tama.speed;
            tama.y += kansuu.Sin(tama.angle) * tama.speed;
            tama.cnt++;
        }
        //ここから弾幕 
          int cnum = 0; float zangle = 0;
        int tcnt = 0; int cnt = 0;
        int ki = 0;
        #region shot0
        protected void shot0()
        {          int t2 = -1;
        if (dansaisyo)
        {
            count = 0;
            t2 = 0;
            dansaisyo = false;
        }
            int t = shot_cnt % 240;
            if (t == 0)
            {
                ki += 1;
            }
            if (t < 100 && t % 5 == 0)
            {

                for (int i = 0; i < 30; i++)
                {
                    dan.Add(new Tdan(zx + kansuu.Sin(kansuu.PI() * 2 / 30 * count) * 20, zy, 0, DX.GetRand(15), angle + PI2 / 30 * i + kansuu.Sin(kansuu.PI() * 2 / 30 * count) * 5, 10.0f, ki % 24));
                }

            }
            else if (t < 220 && t > 120 && t % 5 == 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    dan.Add(new Tdan(zx + kansuu.Sin(kansuu.PI() * 2 / 30 * count) * 20, zy, 0, /*5*/DX.GetRand(15), PI2 / 30 * i + kansuu.Sin(kansuu.PI() * 2 / 60 * count) * 5, 10.0f, ki % 24));
                }
            }
                t2 += 1;
            
                foreach (var it in dan)
                {
                    it.speed += kansuu.Cos(count);
                }
    
            count += 1;
        }
        #endregion
        #region shot1
        protected void shot1()
        {
            int t2 = shot_cnt % 150;
            
            float rnd = 0;
            if (t2 >= 0 && t2 <= 110 && t2 % 10 == 0)
            {
     
                for (int i = 0; i < 20; i++)
                {
                    float x = zx + 7 + kansuu.Cos(kansuu.PI() / 2 + kansuu.PI() / 150 * t2) * 100;
                    float y = zy + 7 + kansuu.Sin(kansuu.PI() / 2 + kansuu.PI() / 150 * t2) * 100;
                    float ang = PI2 / 20 * i;
                    dan.Add(new Tdan(rnd + x, y, 0, 2, ang, 1.2f, 5));
                }
                for (int i = 0; i < 20; i++)
                {

                    float x = zx + 7 + kansuu.Cos(kansuu.PI() / 2 - kansuu.PI() / 150 * t2) * 100;
                    float y = zy + 7 + kansuu.Sin(kansuu.PI() / 2 - kansuu.PI() / 150 * t2) * 100;
                    float ang = PI2 / 20 * i;
                    dan.Add(new Tdan(rnd + x, y, 0, 4, ang, 1.2f, 5));

                }
            }
        }
        #endregion
        #region shot2
        protected void shot2()
        {
            int t = shot_cnt % 1170;
            if (t >= 0 && t < 1170 && t % 90 == 0)
            {
                float hangle = kansuu.rang(PI);
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        float ang = hangle + PI2 / 60 * i;
                        dan.Add(new Tdan(zx, zy, 0, 4, ang, 2, 1, j));
                    }

                }
            }
            foreach (var s2 in dan.Where(c=>c.state==0||c.state==1))
            {
                if (s2.seizon)
                {
                    int st = s2.state;
                    int cn = s2.cnt;
                    if (30 < cn && cn < 120)
                    {
                        s2.speed -= 1.2f / 120.0f;
                        s2.angle += ((kansuu.PI() / 2)) / 120.0f * (st == 1 ? -1 : 1);

                    }

                }
            }

        }
        #endregion
        #region shot3
        protected void shot3(bool smalls)
        {
            int tm = 60;
            int t = shot_cnt % tm, t2 = shot_cnt;
          
            if (t2 == 0) { cnum = 0; zangle = 0; }
            if (t == 0)
            {
                zangle = kansuu.zikiangle(zx, zy);
                if (cnum % 4 == 3) { move_boss_pos(40, 30, Program.fmx - 40, 120, 60, 60); }
            }
            if (t == tm / 2 - 1) { zangle += PI2 / 20 / 2; }
            if (t % (tm / 10) == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    dan.Add(new Tdan(zx, zy, 0,/* i % 16*/DX.GetRand(15), zangle + PI2 / 20 * i, 2.7f, 23, 10));

                }
            }

            if (smalls&&t % 4 == 0)
            {
                dan.Add(new Tdan(DX.GetRand(64-1), DX.GetRand(Program.fmx), DX.GetRand(200), 13, kansuu.PI() / 2, 1 + kansuu.rang(0.5)));
            }
            if (t == tm - 1)
            {
                cnum += 1;
            }
            foreach (var s2 in dan)
            {
                if (s2.seizon)
                {
                    int st = s2.state;
                    int cn = s2.cnt;
                    if (30 < cn && cn < 120&&st==10)
                    {
                        s2.speed -= 1.2f / 120.0f;
                        s2.angle += ((kansuu.PI() / 2)) / 120.0f * (-1);

                    }

                }
            }
        }
        #endregion
        #region shot4
        protected void shot4()
        {
            int tm = 650;
            int t = shot_cnt%tm;
            float angle4;
            if (t == 0 || t == 210)
            {
                move_boss_pos(40, 50, Program.fmx - 40, 150, 100, 80);
            }
            if (t < 180)
            {
                for (int i = 0; i < 2; i++)
                {
                    float ang4=kansuu.rang(PI2/20)+PI2/10*t;
                    dan.Add(new Tdan(DX.GetRand(6),zx,zy,12,ang4,3.2f+kansuu.rang(2.1),0,true));
                }
            }
            if (t > 210 && t < 270 && t % 3 == 0)
            {
                angle4 = kansuu.zikiangle(zx, zy);
                for (int i = 0; i < 8; i++)
                {
                    float ang4 = angle4 - PI / 2 * 0.8f + PI * 0.8f / 7 * i + kansuu.rang(PI / 180);
                    dan.Add(new Tdan(0,zx,zy,0,ang4,3.0f+kansuu.rang(0.3),2,true));
                }
            }

            foreach (var i in dan)
            {
                if (i.state == 0)
                {
                    if (t == 190)
                    {
                        i.kaiten = false;
                        i.speed = 0;
                        i.col = 9;
                        i.cnt = 0;
                        i.state = 1;
                    }
                }
                    if (i.state == 1)
                    {
                        if (i.cnt == 200)
                        {
                            i.angle = kansuu.rang(PI);
                            i.kaiten = true;
                        }
                        if (i.cnt > 200)
                        {
                            i.speed += 0.01f;
                        }
                    }
                
            }
        }
        #endregion
        #region shot5
        protected void shot5()
        {
            int tm = 600; int df = 20;
            int t = shot_cnt % tm; int t2 = shot_cnt;
            float angle5;
            if (t2 == 0) { input_phypos(Program.fmx / 2, Program.fmy / 2, 50); cnum = 0; }
            if (t == 0) { 
                shot_base_angle[0] = kansuu.zikiangle(zx, zy); tcnt = 2; cnt = 0;
            }
            if (t < 540 && t % 3!=0)
            {
                angle5 = kansuu.zikiangle(zx, zy);
                if (tcnt - 2 == cnt || tcnt - 1 == cnt)
                {
                    if (tcnt - 1 == cnt)
                    {
                        shot_base_angle[1] = shot_base_angle[0] + PI2 / df * cnt * (cnum != 0 ? -1 : 1) - PI2 / (df * 6) * 3;
                        tcnt += df - 2;
                    }
                }
           
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    float ang5 = shot_base_angle[0] + PI2 / df * cnt * (cnum != 0 ? -1 : 1) + PI2 / (df * 6) * i * (cnum != 0 ? -1 : 1);
                    dan.Add(new Tdan(cnum != 0 ? 1 : 4, zx, zy, 8, ang5, 2));
                }
            }
            cnt++;
            }
            if (40 < t && t < 540 && t % 30 == 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    angle5 = shot_base_angle[1] - PI2 / 36 * 4;
                    for (int i = 0; i < 27; i++)
                    {
                        dan.Add(new Tdan(cnum != 0 ? 6 : 0, zx, zy, 12, angle5, 4 - 1.6f / 3 * j));
                        angle5 -= PI2 / 36;
                    }
                }
            }

            if (t == tm - 1) { cnum++; }
        }
        #endregion
        #region shot6
        protected void shot6()
        {
            int t = shot_cnt%150;  

             
            if (t >= 0 && t < 110 && t % 10 == 0)
            {
                 for (int i = 0; i < 20; i++)
                {

                    float x = zx - kansuu.Cos(kansuu.PI() / 2 - kansuu.PI() / 150 * t) * 100;
                    float y = zy+50 - kansuu.Sin(kansuu.PI() / 2 - kansuu.PI() / 150 * t) * 100;
                    float ang = PI2 / 20 * i;
                    dan.Add(new Tdan(x, y, 0, 4, ang, 1.2f, 7));
                }
                for (int i = 0; i < 20; i++)
                {
                    float x = zx - kansuu.Cos(kansuu.PI() / 2 + kansuu.PI() / 150 * t) * 100;
                    float y = zy+50 - kansuu.Sin(kansuu.PI() / 2 + kansuu.PI() / 150 * t) * 100;
                    float ang = PI2 / 20 * i;
                    dan.Add(new Tdan(x, y, 0, 2, ang, 1.2f, 7));
                }
            }


        }
        #endregion
        protected  void shot7(string font)
        {
            int t = shot_cnt%800;
            int t2 = shot_cnt;
     //      int num;
            if(t2==0)
            {
                input_phypos(Program.fmx / 2, Program.fmy / 2, 50);
                num = 0;
                loadfont(font);
            }
            if (t == 50)
            {
                for (int i = 0; i < kx.Length; i++)
                {
                    if (kx[i] == 0 && ky[i] == 0)
                    {
                        DX.DrawString(50, 50, "漢字座標データの中に（0，0）のものがあります・・・", DX.GetColor(255, 0, 0));
                        return;
                    }
                    float angle=(float)Math.Atan2(ky[i],kx[i]);
                    float spd=(float)Math.Sqrt(kx[i]*kx[i]+ky[i]*ky[i]);
                    dan.Add(new Tdan(zx+kx[i],zy+ky[i],0,kcol[i],angle,spd,kknd[i]));
                }
            }
            foreach (var item in dan)
            {
                if (item.state == 0)
                {
                    item.speed *= 3.5f;
                }
            }
        }//漢字弾幕１
        protected void shot8(string font)
        {
            int t = shot_cnt % 800; int t2 = shot_cnt;
            if (t2 == 0)
            {
                input_phypos(Program.fmx / 2, Program.fmy / 2, 50);
                loadfont(font);
          
            }
            if (t == 50)
            {
                for (int i = 0; i < kx.Length; i++)
                {
                    if (kx[i] == 0 && ky[i] == 0)
                    {
                        DX.DrawString(50, 50, "漢字座標データの中に（0，0）のものがあります・・・", DX.GetColor(255, 0, 0));
                        return;
                    }
                    float angle = (float)Math.Atan2(ky[i], kx[i]);
                    float spd = (float)Math.Sqrt(kx[i] * kx[i] + ky[i] * ky[i]);
                    dan.Add(new Tdan(zx + kx[i]*200, zy + ky[i]*200, 0, kcol[i], kang[i], 0, kknd[i]));
                }
            }

            foreach (var da in dan)
            {
                if (true)
                {
                    int cnt = da.cnt;
                    if (60 < cnt && cnt <= 120)
                    {
                        da.x += 1;
                    }
                    if (120 < cnt && cnt <= 240)
                    {
                        da.x -= 1;
                    }
                    if (240 < cnt && cnt <= 300)
                    {
                        da.x += 1;
                    }
                    if (cnt==350)
                    {
                        da.speed=1;
                    }
                }
            }
        }
        protected int sst, bnum;
       protected float  sangle;
        protected void shot9()
        {
            int t = shot_cnt % 820; int t2 = shot_cnt;
            if (t == 0)
            {
                sst = 0; sx = zx; sy = zy - 100; sangle = PI / 5 / 2 + PI / 2;
                bnum = 0;
            }
                if (sst <= 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        sx += kansuu.Cos(sangle) * 4;
                        sy += kansuu.Sin(sangle) * 4;
                        if ((sx - zx) * (sx - zx) + (sy - zy) * (sy - zy) > 100.0 * 100.0)
                        {
                            sangle -= PI - PI / 5.0f;
                            sst++;
                            if (sst == 5) { break; }
                        }
                        for (int j = 0; j < 5; j++)
                        {
                              dan.Add(new Tdan(sx, sy, 0, j+3, -PI/2.0f+PI2/5.0f*j, 0, 7,9));
                              dan[dan.Count-1].sx = kansuu.Cos(sangle) * 1.4f * 1.2f;
                              dan[dan.Count - 1].sy = kansuu.Sin(sangle) * 1.4f;
                              dan[dan.Count - 1].baseangle[0] = sangle - PI + PI / 20 * bnum;
       
                        }
                    }
                    bnum++;
                }
            

            foreach (var i in dan)
            {
                int cnt=i.cnt;
                if (i.state == 9)
                {
                    if (t == 150)
                    {
                        i.speed = 4; i.cnt = 0;
                        i.state++;
                    }
                }
                else if (i.state == 10)
                {
                    if (cnt <= 80)
                    {
                        i.speed -= 0.05f;
                    }
                    else if (cnt == 100)
                    {
                        i.angle = i.baseangle[0];
                    }
                    else if (cnt >= 100 && cnt < 160)
                    {
                        i.speed += 0.015f;
                    }
                }
            }


        }
        protected int[,] idx = new int[4,2] { { 50, 150 }, { 40, 50 }, { Program.scx - 100, 170 }, { Program.scx - 70, 50 } };
        protected int qo = 0;
        protected void shot10()//自機狙いからランダムではずして・・・
        {
            int t = shot_cnt % 320;
            if (shot_cnt == 0)
            {
                idx = new int[4, 2] { { 50, 150 }, { 40, 50 }, { Program.scx - 100, 170 }, { Program.scx - 70, 50 } };
                input_phypos(idx[3, 0], idx[3, 1], 50);
            }
            if (t >= 0 && t < 180 && t % 5 == 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    dan.Add(new Tdan(zx, zy, 0, 25 + kansuu.rang(-5, 5), zAtan2() + kansuu.rang(-0.3f, 0.3f), 2, 6));
                }
            }
            if (t == 220) { input_phypos(idx[qo, 0], idx[qo, 1], 50); qo = (++qo) % 4; }

        }
        protected int gx, gy;
        protected void shot11()//レーザーの試作
        {
            int t = shot_cnt % 100;
            if (t == 0) { gx = Program.syux; gy = Program.syuy; }
            if (t < 50) { kansuu.DrawLine(zx+6, zy+6, gx, gy, DX.GetColor(255, 255, 0), t, true); }
            else { kansuu.DrawLine(zx + 6, zy + 6, gx, gy, DX.GetColor(255, 255, 0), 50, true); }
        } 
       int tm=0;
        protected void shot12()
        {
            int t = shot_cnt % 200, t2 = shot_cnt;
           
            double angle;
            if (t == 0) { tm = 190 + kansuu.rang(30); }
            angle = PI * 1.5f + PI / 6 * kansuu.Sin(PI2 / tm * t2);
            if (t2 % 4 == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    dan.Add(new Tdan(zx, zy, 0, 5, 0, 0, 19, 11));
                    dan[dan.Count - 1].sx = kansuu.Cos(angle - PI / 8 * 4 + PI / 8 * i + PI / 16) * 3;
                    dan[dan.Count - 1].sy = kansuu.Sin(angle - PI / 8 * 4 + PI / 8 * i + PI / 16) * 3;
                    dan[dan.Count - 1].effect = 1;
              
                }
            }
            if ( t2 > 80)
            {
                int num = 1;
                if (t % 2 == 1) { num = 3; }
                for (int i = 0; i < num; i++)
                {
                    angle = PI * 1.5f - PI / 2 + PI / 12 * (t2 % 13) + kansuu.rang(PI / 15);
                    dan.Add(new Tdan(zx, zy, 0, 4, 0, 0, 8, 12));
                    dan[dan.Count - 1].sx = kansuu.Cos(angle) * 1.4f*1.2f;
                    dan[dan.Count - 1].sy = kansuu.Sin(angle) * 1.4f;
                }
            }
            foreach (var i in dan)
            {
                if (i.state == 11)
                {
                    if (i.cnt < 150) { i.sy += 0.03f; }
                    i.x += i.sx;
                    i.y += i.sy;
                }
                else if (i.state == 12)
                {
                    if (i.cnt < 160) { i.sy += 0.03f; }
                    i.x += i.sx;
                    i.y += i.sy;
                    i.angle = kansuu.zikiangle(i.sx, i.sy);
                }
            }
                    
        }
        public static int kai = 0;
        protected void shot13(int kakudo)//中二のときの弾幕の再現
        {
            idx = new int[2, 2] { { 40, 50 }, { Program.scx - 70, 50 } };
            int sx = 6; sy = 3;
            int kosuu = 100;
            int t = shot_cnt %10;
            float px = kansuu.Abs(kansuu.Cos(shot_cnt));
            if (t == 0 && !(shot_cnt % 320 > 220 && shot_cnt % 320 < 280))
            {
                          int plusk = 0;
                if (kai == 0) { plusk = -5; }
                else { plusk = 5; }
                for (int i = 0; i < kosuu; i++)
                {
                    dan.Add(new Tdan(zx + px, zy, 0, 5, 0, 0, 7, 13));
                    dan.Last().sx = kansuu.Cos((PI / 180) * kakudo / kosuu * i);
                    dan.Last().sy = kansuu.Sin((PI / 180) * kakudo / kosuu * (i + plusk));
                }
                kai = (++kai) % 2;
            }
            foreach (var i in dan.Where(c=>c.state==13))
            {
                i.x += i.sx*sx;
                i.y += i.sy*sy;
          
            }
            if (shot_cnt == 0) { input_phypos(idx[1, 0], idx[1, 1], 50); }
            if (shot_cnt%320 == 220) { input_phypos(idx[qo, 0], idx[qo, 1], 50); qo = (++qo) % 2; }

        }
        protected void shot14(int kakudo)//中二のときの弾幕の再現
        {

            int t = shot_cnt % 10;
            if (t == 0)
            {
                int plusk = 0;
                if (kai == 0) { plusk = -5; }
                else { plusk = 5; }
                for (int i = 0; i < 25; i++)
                {
                    dan.Add(new Tdan(zx, zy, 0, 5, 0, 0, 7, 14));
                    dan.Last().sx = kansuu.Cos((PI / 180) * kakudo / 25 * i);
                    dan.Last().sy = kansuu.Sin((PI / 180) * kakudo / 25 * (i + plusk));
                }
                kai = (++kai) % 2;
            }
            foreach (var i in dan.Where(c => c.state == 14))
            {
                int sx = 6; sy = 3;
                i.x += i.sx * sx;
                i.y += i.sy * sy;
            }
        }
        protected void shot15()
        {
            int t = shot_cnt % 10;
            if (t == 0)
            {
                
            }

        }
        public static float[] kx;
        public static float[] ky;
        public static int[] kcol;
        public static float[] kang;
        public static int[] kknd;
        protected void loadfont(string aa)
        {
                  float[] kx2=new float[1000];
                  float[] ky2 = new float[1000];
                  int[] kcol2 = new int[1000];
                  float[] kang2 = new float[1000];
                  int[] kknd2 = new int[1000];
            StreamReader reader = new StreamReader("dat\\"+aa+".dat");
            int kaisu = 0;
            while (reader.Peek() >= 0)
            {
                // ファイルを 1 行ずつ読み込む
                float stBuffer =float.Parse(reader.ReadLine());
                // 読み込んだものを追加で格納する
                switch (kaisu%5)
                {
                    case 0:
                        kx2[kaisu / 5] = stBuffer;
                        break;
                    case 1:
                        ky2[kaisu / 5] = stBuffer;
                        break;
                    case 2:
                        kcol2[kaisu / 5] = (int)stBuffer;
                        break;
                    case 3:
                        kang2[kaisu / 5] = stBuffer;
                        break;
                    case 4:
                        kknd2[kaisu / 5] = (int)stBuffer;
                        break;
                }
                kaisu += 1;
            }
            kx = new float[kaisu / 5];
            ky = new float[kaisu / 5];
            kcol = new int[kaisu / 5];
            kang = new float[kaisu / 5];
            kknd = new int[kaisu / 5];
            for (int i = 0; i < kaisu/5; i++)
            {
                kx[i] = kx2[i];
                ky[i] = ky2[i];
                kang[i] = kang2[i];
                kknd[i] = kknd2[i];
                kcol[i] = kcol2[i];
            }
        }

    }

}
