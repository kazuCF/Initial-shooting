using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
  public  class allyuudou
    {
      public bool seizon = true;
      public float x, y;
      public float hyoutekix;
      public float hyoutekiy;
      public float speed=0;
      public float hitotunokakudo;
      public float zurasukakudo=0;
      public float jissaikakudo;
      public int syurui;
      public int kosuu;
      public float chokukaku;
      public float tekigw, tekigh;
      public int tamasyurui;
      public bool t;
      public int size, color;
      public int pattern;
      public float atarihani;
      public float dispangle,angle;
      public int gw, gh;
   //自機狙い
      public allyuudou( float X, float Y,float Hyoutekix,float Hyoutekiy,float Tekigw,float Tekigh, float Speed,float Hitotunokakudo,int Syurui,int Size,int Color,int Kosuu,bool T)
    :this(0,X,Y,Hyoutekix,Hyoutekiy,Tekigw,Tekigh,Speed,Hitotunokakudo,Syurui,Size,Color,Kosuu,T)
      {
        pattern = 0;


    }

      //角度記憶
      public allyuudou(float X, float Y, float Hyoutekix, float Hyoutekiy, float Tekigw, float Tekigh, float Speed, bool saisyo, int Size, int Color, ref float kakudo, bool T)
      {
          x = X; y = Y; hyoutekix = Hyoutekix; hyoutekiy = Hyoutekiy; speed = Speed; t = T;
          tekigh = Tekigh; tekigw = Tekigw;
          size = Size; color = Color;
          pattern = 1;
          if (saisyo) { kakudo = (float)(Math.Atan2(y - hyoutekiy, x - hyoutekix) / Program.kakudokihon); }
          jissaikakudo = kakudo;

      }

      //速度変化型自機狙い
      public float henka = 0;
      public int time = 0;
      public allyuudou(float Henka, float X, float Y, float Hyoutekix, float Hyoutekiy, float Tekigw, float Tekigh, float Speed, float Hitotunokakudo, int Syurui, int Size, int Color, int Kosuu, bool T)
      {
          pattern = 2; henka = Henka;
          x = X; y = Y; hyoutekix = Hyoutekix; hyoutekiy = Hyoutekiy; speed = Speed; hitotunokakudo = Hitotunokakudo; syurui = Syurui; kosuu = Kosuu;
          tekigh = Tekigh; tekigw = Tekigw; t = T;
          size = Size; color = Color;
          chokukaku = (float)(Math.Atan2(y - hyoutekiy, x - hyoutekix) / Program.kakudokihon);

          if (syurui % 2 == 0) { zurasukakudo = (syurui / 2) * hitotunokakudo; }
          else { zurasukakudo = (syurui / 2) * -hitotunokakudo; }
          jissaikakudo = chokukaku + zurasukakudo;

      }

    
      //バラマキショット(定速)
      public allyuudou(float Motospeed, float X, float Y, float Hyoutekix, float Hyoutekiy, float Tekigw, float Tekigh, int Size, int Color, bool T)
    :this(0,Motospeed,X,Y,Hyoutekix,Hyoutekiy,Tekigw,Tekigh, Size,Color,T)
      {
          pattern = 3;
      }
    //バラマキショット(減速)
      public float saitei=0;
      public allyuudou(float Saitei, float Motospeed, float X, float Y, float Hyoutekix, float Hyoutekiy, float Tekigw, float Tekigh, int Size, int Color, bool T)
        
      {
          pattern = 4; saitei = Saitei; size = Size; color = Color;
         x = X; y = Y; hyoutekix = Hyoutekix; hyoutekiy = Hyoutekiy; tekigh = Tekigh; tekigw = Tekigw; t = T;
          jissaikakudo = (float)((Math.Atan2(y - hyoutekiy, x - hyoutekix)+kansuu.rang((float)Math.PI/4) )/ Program.kakudokihon);
          speed = Motospeed + kansuu.rang(2);
      }

      //BOSS1-1

       public void ido()
      {
          angle = jissaikakudo * Program.kakudokihon;
          dispangle = angle + kansuu.PI() / 2;
          time += 1;
          if (pattern == 4) 
          {
              if (speed >saitei ) { speed -= 0.04f; }
          }
          x -= (float)Math.Cos(angle) * speed + (5.0f / 100 * time * henka);
          y -= (float)Math.Sin(angle) * speed + (5.0f / 100 * time * henka); 
           //        kansuu.DrawRotaGraphfk(x, y, 1, 1, gazo.zako1tGraph, DX.TRUE);
          switch (size)
          {
              case 0:
              case 1:
              case 2:
              case 3:
              case 4:
              case 5:
              case 6:
              case 7:
              case 8:
              case 9:
              case 10:
              case 11: atarihani = 4.5f; break;
              case 12: atarihani = 4.0f; break;
              case 13: atarihani = 2.25f; break;
              case 14: atarihani = 18.0f; break;
              case 15:
              case 16:
              case 17:
              case 18:
              case 19:
              case 20:
              case 21:
              case 22: atarihani = 9.0f; break;
              case 23: atarihani = 2.5f; break;
              default:
                  break;
          }
          gw = gazo.otamagw[size];
          gh = gazo.otamagh[size];
          kansuu.DrawRotaGraphfk(x, y, 1, dispangle, gazo.otama[size,color], DX.TRUE, true);
        
             //      kansuu.DrawRotaGraphfk(x,y,1,0,gazo.tama[color],DX.TRUE,true);
                  if (!t)
                   {
                       //if (kansuu.enatariz(x, y, 16)) { seizon = false; }  
                     //  if (kansuu.sotoRota(x, y, 16,16)) { seizon = false; }
                      if(kansuu.haniatariz(x,y,atarihani,speed,jissaikakudo * Program.kakudokihon)){seizon=false;}
                      if (kansuu.sotoRota(x, y, gw, gh)) { seizon = false; }

                   }
            
           
           //変数patternやColorを使って弾ごとに画像を変化も可能。
       }

   
    }
}
