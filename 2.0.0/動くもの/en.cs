using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
namespace _2._0._0
{
    public class en
    {
        public bool editing = false;
        public float fx, fy;//エディト用描画変数
        public static int mynumber;
        public int number = 0;
        protected float chouseikakudo = (float)Math.PI / 180;
        protected float tzang;
        protected float PI = kansuu.PI();
        protected float PI2 = kansuu.PI2();
        protected int shot_cnt = 0;
        public int cnte = 0;
        public bool seizon = true;
        public float life = 100;
        public bool inswitch = false;
        public int gw, gh;
        public float zx, zy;
        public float sx, sy;
        public int idoutime, num, waittime, ugokikata;
        public float tspeed, tamakakudo = 5;
        public float ang, zspeed;
        public bool hyouji = true;
        public int itemnum = 3;//デフォルト値。
        public int[] dasitem;
        public int dantime = 0;
        public int damage = 2;
        public int gaz;
        public int toujyou;
        public int tamaknd, tamacol;
        public float pluskaku;
        public float tamasokudo;
        public int ti = 0;
        public int defalutlife;
        public int size = 0;
        public bool kaitendraw=false;
        //  public int color,size;
     //   public List<allyuudou> ttama = new List<allyuudou>();
        public List<Tdan> tamas = new List<Tdan>();
        public StreamReader read;
        public en() { }
        public en(float X, float Y, int Ugoki, int Gaz, int Time, int Waittime, int Tamakankaku, int Tamakosuu, int Tamasyurui, int Tamaknd, int Tamacol,float Pluskaku,float Tamaspd,int Hp, params int[] item)
        {
            mynumber = (++mynumber % int.MaxValue);
            number = mynumber;
            zx = X;
            zy = Y;
            ugokikata = Ugoki;
            itemnum = 3;
            waittime = Waittime;
            dasitem = item;
            size = Gaz;
            gaz = gazo.zakoGraphs[Gaz];
            toujyou = Time;
            gw = gazo.zakosizew[Gaz];
            gh = gazo.zakosizeh[Gaz];
            kankaku = Tamakankaku;
            tamakosuu = Tamakosuu; 
            tamasyurui = Tamasyurui;
            tamaknd = Tamaknd;
            tamacol = Tamacol;
            pluskaku = Pluskaku;
            tamasokudo = Tamaspd;
            itemnum = item.Length;
            life = Hp;
            fx = X; fy = Y;
            defalutlife=Hp;
         //   life = 50;
        }



        public void main()
        {
            cnte += 1;
          //  btama0();
            hassya();
            shot_calc();
            this.pattern();
            if (hyouji)
            {
               
                this.hantei();
                if (!editing) { this.draw(); }
                
                if (ziki.bommcool == 60)
                {
                    life -= 50;
                }
            }
           
       //     this.tamakanri();
            this.hihyouji();
           tamas.RemoveAll(c=>Program.isbom);
         
            
          
        }
        public void hihyouji()
        {
            if (life < 0)
            {
                if (hyouji)
                {
                    hyouji = false; this.enteritem();
                    if (size != 2)
                    {
                        btama_1();
                    }
                }
            }
            if (inswitch&&kansuu.sotoRota(zx, zy, gw, gh))
            {
                hyouji = false;
            }
            if (!hyouji&&inswitch)
            {
                if (tamas.Count == 0)
                {
                    seizon = false;
                }
            }
        }
        public void draw()
        {
            kansuu.setarea1();
            if (hyouji)
            {
                if (kaitendraw)
                {
                    kansuu.DrawRotaGraphfk(zx, zy, 1, ang - PI / 2, gaz, DX.TRUE, true);
                }
                else
                {
                    kansuu.DrawRotaGraphfk(zx, zy, 1, 0, gaz, DX.TRUE, true);
                }
            }
            kansuu.setareaend();
        }
        public void hantei()
        {
            dantime += 1;
            if (kansuu.naka(zx, zy, gw, gh))
            {
                inswitch = true;
            }
            if (inswitch)
            {
                foreach (var item in Program.zibun)
                {
                  /*  foreach (var tama in item.ztama)
                    {
                        if (atari.CirCollison(zx, zy, 2 * gazo.zgw / 3, tama.ztamax, tama.ztamay, 2 * gw / 3)) { life -= damage; tama.seizon = false; }
                       
                    }*/
                    
                    foreach (var tama in item.tamas)
                    {
                        if (atari.CirCollison(zx, zy, gazo.otamagw[tama.size], tama.x, tama.y, 2 * gw / 3))
                        {
                          //  if (tama.state == 0) { life -= damage; } else { life -= damage / 2; } 
                            life -= tama.power;
                            tama.seizon = false;
                        }
                  
                    }

                }


            }

        }
        public float zAtan2()
        {
            return (float)kansuu.zikiangle(zx, zy);
        }
        public void pattern()
        {
            switch (ugokikata)
            {
                case -2:
                    zakoudou.enemy_patternm2(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime, ref ang, ref zspeed);
                    kaitendraw = true;
                    break;
                case -1:
                    zakoudou.enemy_patternm1(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime, ref ang, ref zspeed);
                    kaitendraw = true;
                    break;
                case 0:
                    zakoudou.enemy_pattern0(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 1:
                    zakoudou.enemy_pattern1(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 2:
                    zakoudou.enemy_pattern2(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 3:
                    zakoudou.enemy_pattern3(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 4:
                    zakoudou.enemy_pattern4(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 5:
                    zakoudou.enemy_pattern5(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 6:
                    zakoudou.enemy_pattern6(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 7:
                    zakoudou.enemy_pattern7(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 8:
                    zakoudou.enemy_pattern8(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 9:
                    zakoudou.enemy_pattern9(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;
                case 10:
                    zakoudou.enemy_pattern10(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime, ref ang, ref zspeed);
                    kaitendraw = true;
                    break;
                case 11:
                    zakoudou.enemy_pattern11(zx, zy, ref sx, ref sy, ref idoutime, ugokikata, waittime);
                    break;

                default:
                    zakoudou.enemyfile(ref sx, ref sy, ref idoutime, ugokikata, ref waittime,ti,ref ang, ref read,"ugokiting" + (ugokikata - 12));
                    zspeed = 0; kaitendraw = true;
                    break;

            }
            zx += (float)Math.Cos(ang) * zspeed;
            zy += (float)Math.Sin(ang) * zspeed;
            idoutime += 1;
            ti++;
            zx += sx;
            zy += sy;
        }
        public int kankaku, tamakosuu;
        public int tamasyurui;
        //case1
        public bool hitotume;
        public float ztkakudo;
        //case2
        public float henka;

        //case4
        public float saitei4 = 1.5f, motospeed = 4;
        public void hassya()
        {
            switch (tamasyurui)
            {
                case 0:
                    btama0();
                    break;
                case 1:
                    btama1();
                    break;
                case 2:
                    btama2();
                    break;
                case 3:
                    btama3();
                    break;
                case 4:
                    btama4();
                    break;
                case 5:
                    btama5();
                    break;
                case 6:
                    btama6();
                    break;
                case 7:
                    btama7();
                    break;
                case 8:
                    btama8();
                    break;
                case 9:
                    btama9();
                    break;
                case 10:
                    btama10();
                    break;
                case 11:
                    btama11();
                    break;
            }
        }

        public void tamakanri()
        {
        //    for (int i = 0; i < ttama.Count; i++)
        //        if (ttama[i].seizon)
                {
        //            ttama[i].ido();
                }
       //         else
                {
           //         ttama.Remove(ttama[i]);
       //             i -= 1;
                }

            
        }
        public void enteritem()
        {
            if (kansuu.rang(10) != 1)
            {
                for (int i = 0; i < itemnum; i++)
                {
                    if (i > 0) { gamemode4.im.Add(new item(zx + kansuu.rang(40), zy + kansuu.rang(40), dasitem[i], -3.5f)); }
                    else { gamemode4.im.Add(new item(zx, zy, dasitem[i], -3.5f)); }
                }
            }
            else
            {
                gamemode4.im.Add(new item(zx, zy, 4, -3.5f));
            }
            gamemode4.ef.Add(new effect(zx, zy,-1,-1,-1,-1,255,-1,-1, 0, 255));
        }
        public virtual void shot_calc()
        {
            foreach (Tdan tama in tamas)
            {

                tama.x += kansuu.Cos(tama.angle) * tama.speed;
                tama.y += kansuu.Sin(tama.angle) * tama.speed;
                if (tama.inswitch)
                {
                    //     if (tama.x < -50 || tama.x > Program.fmx + 50 || tama.y < -50 || tama.y > Program.fmy + 50)
                    if (kansuu.sotoRota(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagh[tama.size]))
                    {
                        if (!tama.draw)
                        {
                            if (!tamas.Any(c => c.draw))
                            {
                                tama.seizon = false;
                            }
                        }
                            tama.seizon = false;
                        
                    }
                    if (tama.cnt > 0)
                    {
                        if (tama.draw)
                        {
                            if (kansuu.haniatariz(tama.x, tama.y, tama.atarihani, tama.speed, tama.angle))
                            {
                                tama.seizon = false;
                            }
                        }
                    }
                    
                }

                else { if (kansuu.naka(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagw[tama.size])) { tama.inswitch = true; }; }
                kansuu.setarea1();
                if (tama.kaiten)
                {
                    tama.dispangle = 2 * PI * (tama.cnt % 120) / 120;
                }
                else { tama.dispangle = tama.angle + PI / 2; }
                if (tama.draw)
                {
                    kansuu.DrawRotaGraphfk(tama.x, tama.y, 1, tama.dispangle, gazo.otama[tama.size, tama.col], DX.TRUE, true);
                }
                kansuu.setareaend();

                if (!hyouji && tama.mitizure) { tama.seizon = false; }

                tama.cnt++;
            }
           
            shot_cnt++;
            tamas.RemoveAll(x => !x.seizon);
            tzang = zAtan2();
        }
        public void btama_1()
        {
          if (kansuu.rang(4) > 2)
            {
                tamas.Add(new Tdan(zx, zy, 0, 18, zAtan2(), 15, 9, -1));
            }
        }
        public void btama0()//nway弾
        {
            int t = shot_cnt % kankaku;
            float jissai = 0;
            if (hyouji)
            {
                if (t == 0)
                {
                    if (tamakosuu % 2 != 0)
                    {
                        for (int i = 0; i < tamakosuu; i++)
                        {
                            float plusangle = 0;
                            if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2; }
                            else { plusangle = -pluskaku * i / 2; }
                            jissai = (plusangle) * chouseikakudo + tzang;
                            tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd));

                        }
                    }
                    else
                    {
                        for (int i = 0; i < tamakosuu; i++)
                        {
                            float plusangle = 0;
                            if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2+2.5f; }
                            else { plusangle = -pluskaku * i / 2-2.5f; }
                            jissai = (plusangle) * chouseikakudo + tzang;
                            tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd));

                        }
                    
                    }
                }
            }
        }
        public void btama1()//速度変化nway弾
        {
            int t = shot_cnt;
            float jissai = 0;
            if (hyouji)
            {
                if (tamakosuu % 2!= 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float plusangle = 0;
                        if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2; }
                        else { plusangle = -pluskaku * i / 2; }
                        jissai = (plusangle) * chouseikakudo + tzang;
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, (tamasokudo / 1000.0f) * t, tamaknd));

                    }
                }
                else
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float plusangle = 0;
                        if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2 + pluskaku/2.0f; }
                        else { plusangle = -pluskaku * i / 2 - pluskaku/2.0f; }
                        jissai = (plusangle) * chouseikakudo + tzang;
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, (tamasokudo / 1000.0f) * t, tamaknd));

                    }

                }
            }

        }
        public void btama2()//円形発射
        {
            int t = shot_cnt;
            if (hyouji)
            {
                if (t % kankaku == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float angle = tzang;
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, angle + kansuu.PI2() / tamakosuu * i, tamasokudo, tamaknd));
                    }
                }
            }
        }
        public void btama3()//ばらまきしょっと
        {
            int t = shot_cnt;
            if (hyouji)
            {
                if (t % kankaku == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, tzang + kansuu.rang(kansuu.PI() / 4), 3 + kansuu.rang((int)pluskaku), tamaknd));
                    }
                }
            }
        }
        public void btama4()//ばらまきしょっと(減速)
        {
            int t = shot_cnt;
            if (hyouji)
            {
                if (t % kankaku == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, tzang + kansuu.rang(kansuu.PI() / 4), 4 + kansuu.rang((int)pluskaku), tamaknd,7));
                    }
                }
            }
            foreach (var i in tamas.Where(c=>c.state==7))
            {
                if (i.speed > 1.5)
                {
                    i.speed -= 0.04f;
                }
            }
        }
        public float baseangle;
        public void btama5()//角度記憶な自機狙い
        {
            int t = shot_cnt;
            
            float jissai = 0;
            if (hyouji)
            {
                if (t % kankaku == 0)
                {
                    if (t == 0)
                    {
                        baseangle = tzang;
                    }
                    if (tamakosuu % 2 != 0)
                    {
                        for (int i = 0; i < tamakosuu; i++)
                        {
                            float plusangle = 0;
                            if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2; }
                            else { plusangle = -pluskaku * i / 2; }
                            jissai = (plusangle) * chouseikakudo + baseangle;
                            tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd));

                        }
                    }
                    else
                    {
                        for (int i = 0; i < tamakosuu; i++)
                        {
                            float plusangle = 0;
                            if (i % 2 == 1) { plusangle = pluskaku * (i + 1) / 2 + pluskaku/2.0f; }
                            else { plusangle = -pluskaku * i / 2 - pluskaku/2.0f; }
                            jissai = (plusangle) * chouseikakudo + baseangle;
                            tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd));

                        }

                    }
                }
            }
        }
        public void btama6()//自機を追いかける弾
        {
            int t = shot_cnt;
            float senkakigenkai = pluskaku;
            if (hyouji)
            {
                if (t % kankaku == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, tzang, tamasokudo, tamaknd));
                    }
                }
            }
            if (t % 10 == 0)
            {
                foreach (var i in tamas)
                {
                    float genzai = i.angle * 180 / kansuu.PI();
                    genzai %= 360;
                    float risou = kansuu.zikiangle(i.x,i.y) * 180 / kansuu.PI();
                    if ((risou-genzai)*(risou-genzai)<senkakigenkai*senkakigenkai)
                    {
                        i.angle = kansuu.zikiangle(i.x, i.y);
                    }
                    else if(risou>genzai)
                    {
                        float jissai = senkakigenkai * kansuu.PI() / 180 +i.angle;
                        i.angle = jissai;
                    }
                    else if (risou < genzai)
                    {
                        float jissai = -senkakigenkai * kansuu.PI() / 180 + i.angle;
                        i.angle = jissai;
                    }
                    
                }
            }
        }
        public void btama7()//自機狙い円形弾
        {
            int t = shot_cnt;
            float jissai = 0;
            float kaku = Math.Abs(pluskaku);
            if (t % kankaku == 0)
            {
                if (tamakosuu % 2 == 0)
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float plusangle = 0;
                        if (i % 2 == 1) { plusangle = kaku * (i + 1) / 2; }
                        else { plusangle = -kaku * i / 2; }
                        jissai = (plusangle) * chouseikakudo + tzang;
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd, 5));
                    }
                }
                else
                {
                    for (int i = 0; i < tamakosuu; i++)
                    {
                        float plusangle = 0;
                        if (i % 2 == 1) { plusangle = kaku * (i + 1) / 2 + kaku/2.0f; }
                        else { plusangle = -kaku * i / 2 - kaku/2.0f; }
                        jissai = (plusangle) * chouseikakudo + tzang;
                        tamas.Add(new Tdan(zx, zy, 0, tamacol, jissai, tamasokudo, tamaknd, 5));

                    }

                }
                for (int i = 0; i < tamas.Count; i++)
                {
                    if (tamas[i].state == 5)
                    {
                        if (tamas[i].cnt % 20 == 0)
                        {
                            tamas[i].draw = false;
                            for (int k = 0; k < 20; k++)
                            {
                                tamas.Add(new Tdan(tamas[i].x, tamas[i].y, 0, tamacol, kansuu.zikiangle(tamas[i].x, tamas[i].y) + kansuu.PI2() / 20 * k, 4, tamaknd));
                         
                            }
                            
                        }
                    }
                }
        
            }
        }
        public void btama8()//回転弾(敵から)
        {
            int t = shot_cnt;
            float omega = tamasokudo;
            if (t == 1)
            {
                for (int i = 0; i < tamakosuu; i++)
                {
                    float angle = tzang;
                    tamas.Add(new Tdan(zx, zy, 0, tamacol, angle + kansuu.PI2() / tamakosuu * i, 0, tamaknd,8));
                }
            }
            foreach (var i in tamas.Where(c=>c.seizon&&c.state==8))
            {
                i.mitizure = true;
                if (i.hankei < pluskaku) { i.hankei++; }
                i.angle += omega;
                i.x = zx + i.hankei * kansuu.Cos(i.angle);
                i.y = zy + i.hankei * kansuu.Sin(i.angle);
            }
        }
        public void btama9()
        {
            int t = shot_cnt;
            float omega = tamasokudo;
            if (t % kankaku == 0)
            {
                tamas.Add(new Tdan(zx, zy, 0, tamacol, tzang, 4, tamaknd,5));
            }
            for (int i = 0; i < tamas.Count; i++)
            {
                if (tamas[i].state == 5&&tamas[i].cnt==1)
                {
                    tamas[i].mitizure = true;
                    for (int k = 0;  k < tamakosuu;  k++)
                    {
                        tamas.Add(new Tdan(tamas[i].x,tamas[i].y,0,tamacol,tzang+ kansuu.PI2() / tamakosuu * k,0,tamaknd,6));
                    }
                }
            }
            foreach (var i in tamas.Where(c =>c.state == 5))
            {
                if (i.hankei < pluskaku) { i.hankei++; }
                i.angle += omega;
                i.x = zx + i.hankei * kansuu.Cos(i.angle);
                i.y = zy + i.hankei * kansuu.Sin(i.angle);
            }

        }
        public void btama10()//適当な方向弾
        {
            int t = shot_cnt;
            if (t % kankaku == 0)
            {
                tamas.Add(new Tdan(zx,zy,0,tamacol,pluskaku,tamasokudo,tamaknd));
            }
        }
        public void btama11()//直下撃ち
        {
            int t = shot_cnt;
            if (t % kankaku == 0)
            {
                tamas.Add(new Tdan(zx, zy, 0, tamacol, PI * 1 / 2, tamasokudo, tamaknd));
            }
        }
    }
}