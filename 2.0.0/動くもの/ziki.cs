using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class ziki
    {
        public const int kijyunpower = 15;
        public float Rpower = kijyunpower;
        public int chara;
        public float pluskaku = 5;
        public int[] cshotnum = new int[] { 2, 4 };
        public int[] cshotx = new int[] { -10, 10, -30, 30 };
        public int[] cshoty = new int[] { -30, -30,-10,-10 };
       /***********************************/
        public List<effect> efs  =new List<effect>();
        public List<Tdan> tamas = new List<Tdan>();
        public static float[] opx = new float[2], opy = new float[2];
        public static int opc = 0;
        public static int bommcool;
        public int t = 0;
        public const int optionx = 25, optiony = 35;
        public static float PI = kansuu.PI();
        public static float[] optionang = new float[] { -PI / 2, -PI / 2, -PI / 2 - PI / 4, -PI / 2 + PI / 4 };
        public const int T = 150;
        public const int W = 300;
        public bool muteki = false;
        public int mutekijikan = 0;
        public int _type;
        public int zikix;
        public int zikiy;
        public int life = 6;
        public int boms = 3;
        public int zikitime = 0;
        public int tamakosuu = 3;
        public int kakudojyougen = 6;
        
        public static int sx, sy;

        public void syokika()
        {
            efs = new List<effect>();
            tamas = new List<Tdan>();
            t = 0; bommcool = 0;
            muteki = false;
            mutekijikan = 0;
            life = Program.lifesyoki;
            zikitime = 0;
            tamakosuu = 3;
            kakudojyougen = 6;
            opc = 0;
               
        }
        public ziki(int type, int X, int Y,int Chara)
        {
            _type = type;
            zikix = X;
            zikiy = Y;
            if (type != 1) { life = 2; }
            chara = Chara;
        }
        public void Ziki()
        {
            pluskakuding();
            calchoming();
            zikiido(); Program.enter_func("自機計算", 0);
            if (_type == 1)
            {
                bomcalc();

                graphefect(1);
                graphefect(2);
                graphefect(3);

                Program.dn_calc();
            } Program.enter_func("ボム系の計算", 0);
            tamahassya(); Program.enter_func("自機の弾計算", 0);
            hantei(); Program.enter_func("自機の描画", 0);
        }
        private void zikiido()
        {
            opc++;
            if (Program.key[DX.KEY_INPUT_LSHIFT] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    opx[i] = zikix + optionx * (i == 0 ? -1 : 1);
                    opy[i] = zikiy + optiony+kansuu.Sin(PI*2/150*opc)*20;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    opx[i] = zikix + optionx/2 * (i == 0 ? -1 : 1);
                    opy[i] = zikiy + optiony/2 ;
                }
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_LSHIFT) != 0)
            {
                sx = 1;
                sy = 1; kakudojyougen = 3;
            }
            else { sx = 4; sy = 4; kakudojyougen = 6; }
            if (DX.CheckHitKey(DX.KEY_INPUT_UP) != 0)
            {
                zikiy -= sy;
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) != 0)
            {
                zikiy += sy;
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_RIGHT) != 0)
            {
                zikix += sx;
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_LEFT) != 0)
            {
                zikix -= sx;
            }
            kansuu.sotoRotaz(ref zikix, ref zikiy, gazo.zgw, gazo.zgh);
            Program.border = (zikiy < 100);
    
            if (t < T)
            {
                t++;
            }


        }
        private void tamahassya()
        {
            zikitime += 1;
            if (zikitime > Program.ztamakankaku && DX.CheckHitKey(DX.KEY_INPUT_Z) != 0)
            {
                if (chara == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float jissai = 0;
                        float plusangle = 0;
                        if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2; }
                        else { plusangle = -pluskaku * i / 2; }
                        jissai = (plusangle - 90) * Program.kakudokihon;
                        tamas.Add(new Tdan(zikix, zikiy, 0, 4, jissai, 20, 11, 0, Rpower));
                    }
                }
                if (Program.power >= 100)
                {
                   
                        for (int i = 0; i < (Program.power < 300 ? 2 : 4); i++)
                        {
                            tamas.Add(new Tdan(opx[i % 2], opy[i % 2], 0, 3, optionang[i], 20, 8, 1, 10 - 7 * (i / 2)));
                        }
                    
                   
                }
                zikitime = 0;

            }
            foreach (var i in tamas)
            {
                i.x += kansuu.Cos(i.angle) * i.speed;
                i.y += kansuu.Sin(i.angle) * i.speed;
          //      if (Program.brt != 255) { DX.SetDrawBright(Program.brt, Program.brt, Program.brt); }
                kansuu.DrawRotaGraphfk(i.x, i.y, 1, 0, gazo.otama[i.size, i.col], DX.TRUE, false);
           //     if (Program.brt != 255) { DX.SetDrawBright(255, 255, 255); }
                if (kansuu.sotoRota(i.x, i.y, gazo.otamagw[i.size], gazo.otamagh[i.size]))
                { i.seizon = false; }
               
            }
            tamas.RemoveAll(c => !c.seizon);

        }
        private void hantei()
        {

            kansuu.DrawRotaGraphfk(zikix, zikiy, 1, 0, gazo.sinziki, 1, false);
            if (muteki)
            {
                mutekijikan -= 1; if (Program.count % 20 < 10) { DX.SetDrawBright(255, 0, 0); } else { DX.SetDrawBright(100, 0, 0); }
                kansuu.DrawRotaGraphfk(zikix, zikiy, 2, 0, gazo.zikiGraph, DX.TRUE, false);
                //   DX.DrawRotaGraph(zikix, zikiy, 1, 1, gazo.zikiGraph, DX.TRUE);
                DX.SetDrawBright(255, 255, 255);
                if (mutekijikan <= 0) { muteki = false; }
            }
            else
            {
                //DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD,50);
                //   kansuu.DrawRotaGraphfk(zikix, zikiy, 1, 1, gazo.zikiGraph, DX.TRUE);
                kansuu.DrawRotaGraphfk(zikix, zikiy, 2, 0, gazo.zikiGraph, DX.TRUE, false);
                // DX.DrawRotaGraph(zikix, zikiy, 1, 1, gazo.zikiGraph, DX.TRUE);
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
            }
        }
        public void itemdrop()
        {
            life -= 1;
            boms = Program.bomsyoki;
            muteki = true; mutekijikan = 100;
            for (int i = 0; i < 3; i++)
            {
                if (i > 0) { gamemode4.im.Add(new item(zikix + kansuu.rang(40), zikiy + kansuu.rang(40), 4, -5.5f)); }
                else { gamemode4.im.Add(new item(zikix, zikiy, 4, -5.5f)); }
            }
            // zikix = 50; zikiy = Program.scy - 50;
            Program.power -= 150;
        }
        private void bomming()
        {
            
                efs.Add(new effect(70, 300, 1.5f, 1, PI / 2, -PI / 2, 0, 2, gazo.imgefbom[3], 3, -1));
                efs.Add(new effect(100, 450, 1.5f, 1, 0,0, 0, 2, gazo.imgefbom[3], 3, -1));
                efs.Add(new effect(260, 300, 1, 0.7f,0, -PI / 2, 0, 2, gazo.imgefbom[2], 2, -1));
                boms--;
        }
        private void bomcalc()
        {
            int n = 0;
            float[] angles = new float[] {0,PI,PI/2,PI*1.5f };
            if (Program.key[DX.KEY_INPUT_X] != 0 && bommcool == 0&&boms>0)
            {
                bomming();
                bommcool++;
                Program.isbom = true;
            }
            if(bommcool>0)
            {
                if (bommcool % 10 == 0)
                {
                    n = bommcool / 10;
                    if (n < 4)
                    {
                        efs.Add(new effect(zikix,zikiy,0.5f,13+kansuu.rang(2),kansuu.rang(PI),angles[n]-PI/4,0,2,gazo.imgefbom[n/3],1,-1));
                    }
                }
                bommcool++;
                if (bommcool < 40) { Program.brt = 255 - bommcool * 5; }
                else if (bommcool > 90) { Program.brt = 255 - 40 * 5 + (bommcool - 90) * 5; }
                if (bommcool > 130) { Program.isbom = false; bommcool = 0; Program.brt = 255; }
            }
        }
        private void graphefect(int knd)
        {
            foreach (var i in efs.Where(c => c.knd == knd))
            {
                i.main();
                if (i.eff == 1) { DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, (int)i.brt); }
                else if (i.eff == 2) { DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, (int)i.brt); }
                //i.er = 1;
                kansuu.DrawRotaGraphfk(i.x, i.y, i.er, i.ang, i.img, DX.TRUE, false);
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
            }
            efs.RemoveAll(c=>!c.seizon);
        }
        private void calchoming()
        {
            foreach (var i in tamas.Where(c => c.state == 1))
            {
                int num = -1, min = -1, d;
                double x, y;
                if (!gamemode4.bosschuu)
                {
                    foreach (var k in gamemode4.teki.Where(c=>c.inswitch&&c.hyouji))
                    {
                        
                            x = k.zx - i.x; y = k.zy - i.y;
                            d = (int)(x * x + y * y);
                            if (d < min || min == -1)
                            {
                                num = k.number;
                                //num = l;
                                min = d;
                            }
                        
                     //   l++;
                    }
                }
                if ((num != -1) || (gamemode4.bosschuu && gamemode4.boss[0].state == 2))
                {
                    if (gamemode4.bosschuu) { x = gamemode4.boss[0].zx; y = gamemode4.boss[0].zy; }
                    else { x = gamemode4.teki.FirstOrDefault(c => c.number == num).zx; y = gamemode4.teki.FirstOrDefault(c => c.number == num).zy; }
                    i.angle = kansuu.angling(i.x, i.y, x, y);
                }
            }
        }
        private void pluskakuding()
        {
            switch (chara)
            {
                case 0:
                    if (Program.power > 300)
                    {
                        if (Program.key[DX.KEY_INPUT_LSHIFT] > 0) { pluskaku = 1.5f; }
                        else { pluskaku = 3; }
                        tamakosuu = 5; Rpower = kijyunpower * 0.72f; //pluskaku = 3;
                    }
                    else if(Program.power>100)
                    {
                        if (Program.key[DX.KEY_INPUT_LSHIFT] > 0) { pluskaku = 3; }
                        else { pluskaku = 5; }
                        tamakosuu = 3; Rpower = kijyunpower;
                    }
                    else if (Program.power > 100)
                    {
                        if (Program.key[DX.KEY_INPUT_LSHIFT] > 0) { pluskaku = 3; }
                        else { pluskaku = 5; }
                        tamakosuu = 1; Rpower = kijyunpower * 2;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

     

