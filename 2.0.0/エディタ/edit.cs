using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
namespace _2._0._0
{

    /// <summary>
    /// //全ての描画にfx,fyを足す！
    /// </summary>
   public static class edit
    {
        #region 宣言とか
       public static List<en> tekis = new List<en>();
       public static int jizokujikan;
        public const int tekisyuruisu = 4;
        public static int mode ;
        public static int sentakun ;
        public static bool enter = false, back = false;
        public static int susumu;
        public static int gtime;
        public static int mode2kaishi;
        public static int pat ;
        public const int screenx = 510;
        public const int screeny = 640;
        public static string[] ugokinamae;
        public static string[] plusmessage;
        public static string[] plusmessage2;
       /// <summary>
       /// /////////
       /// </summary>
 
        public static int[] tekiimg = new int[100];
        public struct Mouse_t
        {
            public int x, y;
            static public uint[] Button = new uint[8];
            static public int WheelRotVol;
        }
        public static int GetHitKeyStateAll_2(int[] GetHitKeyStateAll_InputKey)
        {
            byte[] GetHitKeyStateAll_Key = new byte[256];
            DX.GetHitKeyStateAll(out GetHitKeyStateAll_Key[0]);
            for (int i = 0; i < 256; i++)
            {
                if (GetHitKeyStateAll_Key[i] != 0) { key[i]++; }
                else { key[i] = 0; }
            }
            return 0;

            //     return 0;
        }
        public static int GetHitMouseStateAll_2()
        {
            //  int ax, ay;
            if (DX.GetMousePoint(out Mouse.x, out Mouse.y) == -1)
            { //マウスの位置取得
                return -1;
            }
            int MouseInput = DX.GetMouseInput();    //マウスの押した状態取得
            for (int i = 0; i < 8; i++)
            {            //マウスのキーは最大8個まで確認出来る
                if ((MouseInput & 1 << i) != 0) Mouse_t.Button[i]++;   //押されていたらカウントアップ
                else Mouse_t.Button[i] = 0; //押されてなかったら0
            }
            Mouse_t.WheelRotVol = DX.GetMouseWheelRotVol();    //ホイール回転量取得
            return 0;
        }
        public struct fPt_t
        {
            public float x, y;
        }
        public struct ugoki
        {
            public int Num;
            public ugoki_t[] ug;
        }
        public struct ugoki_t
        {
            public int num;
            public ugoki_tt[] Ug;
        }
        public struct ugoki_tt
        {
            public float x;
            public float y;
            public float ex;
            public float ey;
            public float sx;
            public float sy;
            public int jizoku;
        }
        public struct Bl_t
        {
            public int time;
            public int hp;
            public int idoutime;
            public int waittime;
            public float sx;
            public float sy;
            public float fx;//画面表示用の座標
            public float fy;
            public float ang;//移動用の向き
            public float zspeed;
            public int Knd;
            public int Col;
            public float Angle;//未使用。
            public float x, y;//本当の座標
            public float pluskaku;//nway弾用、弾の角度の開き方
            public int tamakosuu;
            public int tamacol;
            public int tamaknd;
            public int tamaugoki;
            public int tamakankaku;
            public float tamaspd;
            public bool hyouji;
            public bool seizon;
            public List<Tdan>tamas;
            public int cnt;
            public StreamReader read;

        }
        public struct Blpoint_t
        {
            public int Num;
            public Bl_t[] Bl;//= new Bl_t[1000];

        }
       
    //    public static Blpoint_t Blpoint = new Blpoint_t();
        public static ugoki ugokin = new ugoki();
        public struct Operate_t
        {
            public int State;
            public int Knd;
            public int Col;
            public int Space;
            public float Angle;
            public int renzoku;
            public int renzokukaisu;
            public int waittime;
            public bool hanten;
            public int flag;
            public fPt_t fPt1;
            public fPt_t fPt2;
            public int hp;
        }
        public static int[] key = new int[256];
        public static int Red, White, Blue;
        public static int[,] ImgBullet = new int[24, 64];

        public static Mouse_t Mouse = new Mouse_t();
        public static Operate_t Operate = new Operate_t();
        #endregion
        public static void syokika()
        { 
         mode = 0;
         sentakun = 0;
         mode2kaishi = 0;
         enter = false;
         back = false;
         susumu = 0;
         gtime = 0;
         jizokujikan = 5;
         pat = 11;
     //    Blpoint.Num = 0;
         ugokin.Num = 0;
         
         ugokinamae = new string[] { "nway","速度変化nway","円形","ばらまき","減速ばらまき","角度記憶な時期狙い","自機追い弾","自機狙い→円形","回転"};
         plusmessage = new string[] { "nway弾の角度", "nway弾の角度", "直撃なら0以上,ずらすなら0未満","ランダムの度合い","ランダムの度合い","方向弾のnway角度","旋回の限界値","直撃なら0以上,ずらすなら0未満でnway角度","半径"};
         plusmessage2 = new string[] { "なし","なし"};      
        }
        public static void ini()
        {
            Operate.Knd = 0;
            Operate.Space = 50; Operate.flag = 1;
            Operate.waittime = 20;
            Operate.renzoku = 10;
            Operate.renzokukaisu = 1;
            Operate.hanten = false;
            Operate.hp = 50;
        }
        public static void load()
        {
            White = DX.GetColor(255, 255, 255);
            Red = DX.GetColor(255, 0, 0);
            Blue = DX.GetColor(0, 255, 255);

            tekiimg[0] = DX.LoadGraph("キャラクタ\\zako.png");
            tekiimg[1] = DX.LoadGraph("キャラクタ\\zako2.png");
            tekiimg[2] = DX.LoadGraph("zako2.png");
            tekiimg[3] = DX.LoadGraph("BossGraph.bmp");
        }
        public static void InputBlData(float x, float y, int Knd, int Col, float Angle, int time, int waittime,int hp)
        {
            tekis.Add(new en(x, y, Col, Knd, time, waittime, 20, 1, 0, 2, 0, 5, 3, hp, 0));
          /*  Blpoint.Bl[Blpoint.Num].x = x;
            Blpoint.Bl[Blpoint.Num].y = y;
            Blpoint.Bl[Blpoint.Num].Knd = Knd;
            Blpoint.Bl[Blpoint.Num].fx = x;
            Blpoint.Bl[Blpoint.Num].fy = y;
            Blpoint.Bl[Blpoint.Num].Col = Col;
            Blpoint.Bl[Blpoint.Num].Angle = Angle;
            Blpoint.Bl[Blpoint.Num].time = time;
            Blpoint.Bl[Blpoint.Num].sx = 0;
            Blpoint.Bl[Blpoint.Num].sy = 0;
            Blpoint.Bl[Blpoint.Num].waittime = waittime;
            Blpoint.Bl[Blpoint.Num].pluskaku = 5;
            Blpoint.Bl[Blpoint.Num].tamakosuu = 1;
            Blpoint.Bl[Blpoint.Num].tamacol = 0;
            Blpoint.Bl[Blpoint.Num].tamaknd = 2;
            Blpoint.Bl[Blpoint.Num].tamaugoki = 0;
            Blpoint.Bl[Blpoint.Num].tamakankaku = 20;
            Blpoint.Bl[Blpoint.Num].tamaspd = 3;
            Blpoint.Bl[Blpoint.Num].cnt = 0;
            Blpoint.Bl[Blpoint.Num].tamas = new List<Tdan>();
            Blpoint.Bl[Blpoint.Num].seizon = true;
            Blpoint.Bl[Blpoint.Num].hyouji = true;
            Blpoint.Bl[Blpoint.Num].hp = hp;
            Blpoint.Num++;*/
        }
        public static void InputugData(float x,float y,float ex,float ey,float sx, float sy,int jizoku)
        {
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].x = x;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].y = y;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].ex = ex;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].ey = ey;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].sx = sx;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].sy = sy;
            ugokin.ug[ugokin.Num].Ug[ugokin.ug[ugokin.Num].num].jizoku = jizoku;
            ugokin.ug[ugokin.Num].num++;
        }
        public static void CalcBullet()
        { 
                float x = Operate.fPt1.x - Program.fx, y = Operate.fPt1.y - Program.fy;//最初の場所
                //最初クリックした場所と最後クリックした場所との角度
                float X = Operate.fPt2.x - Program.fx, Y = Operate.fPt2.y - Program.fy;    
                float Angle = (float)Math.Atan2(Operate.fPt2.y - Program.fy - Operate.fPt1.y + Program.fy, Operate.fPt2.x - Program.fx - Operate.fPt1.x + Program.fx);
                float xlen = Operate.fPt2.x - Operate.fPt1.x;//xの距離
                float ylen = Operate.fPt2.y - Operate.fPt1.y;//yの距離
                float Length = (float)Math.Sqrt(xlen * xlen + ylen * ylen);//点と点との距離
                float Proceeded = 0;//今現在進んだ距離

            if (mode == 0)
            {
               
                //今現在進んだ距離が進むべき距離以内の間、かつ登録出来る個数の間ループ
                while (tekis.Count < 100000)
                {
                    for (int i = 0; i < Operate.renzokukaisu; i++)
                    {
                        InputBlData(x, y, Operate.Knd, Operate.Col, Operate.Angle, gtime + i * Operate.renzoku, Operate.waittime,Operate.hp);
                        if (Operate.hanten)
                        {
                            InputBlData(screenx - x-Program.fx, y, Operate.Knd, Operate.Col, Operate.Angle, gtime + i * Operate.renzoku, Operate.waittime,Operate.hp);
                        
                        }
                    }
                    x += (float)Math.Cos(Angle) * Operate.Space;
                    y += (float)Math.Sin(Angle) * Operate.Space;
                    Proceeded += Operate.Space;
                    if (Length < Proceeded) break;
                }
            }

            else if (mode == 3)
            {
                InputugData(x,y,X,Y,xlen, ylen, jizokujikan);
            }
        }
        public static void CalcMouse()
        {
            if (Mouse_t.Button[0] == 1)
            {
                switch (Operate.State)
                {
                    case 0:
                        Operate.fPt1.x = (float)Mouse.x;
                        Operate.fPt1.y = (float)Mouse.y;
                        Operate.State = 1;
                        break;
                    case 1:
                        Operate.fPt2.x = (float)Mouse.x;
                        Operate.fPt2.y = (float)Mouse.y;
                        Operate.State = 0;
                        CalcBullet();
                        break;
                }
            }
        }
        public static void CalcOperate()
        {
            int[] col = new int[24];//{ 5, 6, 10, 5, 10, 3, 3, 10, 10, 3, 8, 8, 10, 10 };
            int[] tehonkankaku = new int[] { 20, 20, 10, 4, 4, 20, 20, 20, -1, 1, 1, 1 };
            float[] tehonpluskaku = new float[] { 5, 5, 0, 1.5f, 2, 5, 15, 5, 70, 1, 1 };
            float[] tehonspeed = new float[]    { 3, 5, 4, -1, -1, 3, 5, 3, 0.2f, 0.2f };
            for (int i = 0; i < col.Length; i++)
            {
                col[i] = pat;
            }
            
            if (key[DX.KEY_INPUT_TAB] == 1)
            {
                if (mode <= 1)
                {
                    mode = (++mode) % 2;
                    gtime = 0;
                    susumu = 0;
                    /*for (int i = 0; i < Blpoint.Num; i++)
                    {
                        Blpoint.Bl[i].fx = Blpoint.Bl[i].x;
                        Blpoint.Bl[i].fy = Blpoint.Bl[i].y;
                        Blpoint.Bl[i].idoutime = 0;
                        Blpoint.Bl[i].cnt = 0;
                        Blpoint.Bl[i].tamas.Clear();
                        Blpoint.Bl[i].seizon = true;
                        Blpoint.Bl[i].hyouji = true;
                    }*/
                    foreach (var i in tekis)
                    {
                        i.zx = i.fx;
                        i.zy = i.fy;
                        i.idoutime = 0; i.ti = 0;
                        i.cnte = 0; i.tamas.Clear();
                        i.seizon = true;
                        i.hyouji = true;
                        i.life = i.defalutlife;
                    }
                }
                else if (mode == 2)
                {
                    mode = 0;
                    susumu = 0;
                    gtime = 0;
                    mode2kaishi = 0;
                }
            }
           


            gtime += susumu;
            #region mode0
            if (mode == 0)
            {
                if (key[DX.KEY_INPUT_I] == 1) { Operate.hanten = !Operate.hanten; }
                if (key[DX.KEY_INPUT_P] == 1)
                {
                    mode = 3;
                }
                if (key[DX.KEY_INPUT_RETURN] == 1)
                {
                    if (susumu == -1) { susumu = 0; }
                    else if (susumu > 0) { susumu = 0; }
                    else { susumu = 1; }

                }
                if (key[DX.KEY_INPUT_BACK] == 1)
                {
                    gtime = 0;
                   /* for (int i = 0; i < Blpoint.Num; i++)
                    {
                        Blpoint.Bl[i].fx = Blpoint.Bl[i].x;
                        Blpoint.Bl[i].fy = Blpoint.Bl[i].y;
                        Blpoint.Bl[i].idoutime = 0;
                        Blpoint.Bl[i].cnt = 0;
                        Blpoint.Bl[i].tamas.Clear();
                        Blpoint.Bl[i].seizon = true;
                        Blpoint.Bl[i].hyouji = true;
                        if (Blpoint.Bl[i].Col >= 12&&Blpoint.Bl[i].read!=null)
                        {
                            Blpoint.Bl[i].read.Close();
                        }
                    }*/
                    foreach (var i in tekis)
                    {
                        i.zx = i.fx; i.zy = i.fy;
                        i.idoutime = 0; i.cnte = 0; i.ti = 0;
                        i.tamas.Clear(); i.seizon = true;
                        i.hyouji = true;
                        if (i.ugokikata >= 12 && i.read != null)
                        { i.read.Close(); }
                        i.life = i.defalutlife;
                    }
                    mode2kaishi = gtime;
                    susumu = 0;
                }
                if (key[DX.KEY_INPUT_LCONTROL] != 0)
                {
                    if (key[DX.KEY_INPUT_Z] == 1 || key[DX.KEY_INPUT_Z] > 30)
                    {
                   /*     if (Blpoint.Num > 0)
                        {
                            Blpoint.Num--;
                        }*/
                        if (tekis.Any())
                        {
                            tekis.RemoveAt(tekis.Count - 1);
                        }

                    }
                    if (key[DX.KEY_INPUT_Y] == 1 || key[DX.KEY_INPUT_Y] > 30)
                    {
                      /*  if (Blpoint.Num < 99999 & !(Blpoint.Bl[Blpoint.Num].x == 0 && Blpoint.Bl[Blpoint.Num].y == 0))
                        {
                            Blpoint.Num++;
                        }*/

                    }
                }
                if (key[DX.KEY_INPUT_S] > 0)
                {
                    //左キー押されたら
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        //スペース減らす
                        if (Operate.Space > 2)
                        {
                            Operate.Space--;
                        }
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        //スペース増やす
                        if (Operate.Space < 300)
                        {
                            Operate.Space++;
                        }
                    }
                }
                else if (key[DX.KEY_INPUT_T] > 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        //スペース減らす
                        gtime -= 1;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        //スペース増やす
                        gtime += 1;
                    }
                }
                else if (key[DX.KEY_INPUT_R] > 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        Operate.renzoku -= 1;
                    }
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        Operate.renzoku += 1;
                    }
                }
                else if (key[DX.KEY_INPUT_F] > 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        Operate.renzokukaisu -= 1;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        Operate.renzokukaisu += 1;
                    }
                }
                if (key[DX.KEY_INPUT_C] > 0)//動き方
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        Operate.Col -= 1;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        Operate.Col += 1;
                    }
                }
                if (key[DX.KEY_INPUT_H] > 0)//動き方
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        Operate.hp -= 1;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        Operate.hp += 1;
                    }
                }
                  //  Operate.Col = (2 + Operate.Col) % (col[Operate.Knd] + 1) - 1;
                if (key[DX.KEY_INPUT_SPACE] == 1)//表示フラグ
                    Operate.flag *= -1;
                if (key[DX.KEY_INPUT_K] == 1)
                {//種類
                    Operate.Knd = (++Operate.Knd) % tekisyuruisu;

                }
                if (key[DX.KEY_INPUT_W] == 1)
                {
                    Operate.waittime = ++Operate.waittime;

                }
                if (key[DX.KEY_INPUT_Q] == 1)
                {
                    Operate.waittime = --Operate.waittime;

                }
            }
            #endregion
            #region mode1
            if (mode == 1)
            {
               // gtime = Blpoint.Bl[sentakun].time;
                gtime = tekis[sentakun].toujyou;
                if (key[DX.KEY_INPUT_SPACE] == 1)//表示フラグ
                    Operate.flag *= -1; 
                if (key[DX.KEY_INPUT_RETURN] == 1)
                {
                    mode = 2;
                    susumu = 1;
                    mode2kaishi = gtime;
                }
                if (key[DX.KEY_INPUT_N] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        sentakun--;
                        if (sentakun < 0) { sentakun = 0; }
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                        sentakun++;
                      //  if (sentakun >= Blpoint.Bl.Count()) { sentakun = Blpoint.Bl.Count() - 1; }
                        if (sentakun >= tekis.Count) { sentakun = tekis.Count - 1; }
                    }
                    else if (key[DX.KEY_INPUT_N] == 1)
                    {
                        sentakun++;
                        //if (sentakun >= Blpoint.Bl.Count()) { sentakun = Blpoint.Bl.Count() - 1; }
                        if (sentakun >= tekis.Count) { sentakun = tekis.Count - 1; }
                    }
                }
                else if (key[DX.KEY_INPUT_R] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        tekis[sentakun].tamakosuu--;
                     //   Blpoint.Bl[sentakun].tamakosuu--;
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                        tekis[sentakun].tamakosuu++;
                  //      Blpoint.Bl[sentakun].tamakosuu++;
                    }
                }
                else if (key[DX.KEY_INPUT_P] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        tekis[sentakun].tamacol--;
             //           Blpoint.Bl[sentakun].tamacol--;
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                        tekis[sentakun].tamacol++;
                //        Blpoint.Bl[sentakun].tamacol++;
                    }
                }
                else if (key[DX.KEY_INPUT_O] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        tekis[sentakun].tamaknd--;
                 //       Blpoint.Bl[sentakun].tamaknd--;
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                  //      Blpoint.Bl[sentakun].tamaknd++;
                        tekis[sentakun].tamaknd++;
                    }
                }
                else if (key[DX.KEY_INPUT_I] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                      /*  Blpoint.Bl[sentakun].tamaugoki--;
                        if (Blpoint.Bl[sentakun].tamaugoki < 0) { Blpoint.Bl[sentakun].tamaugoki = 0; }
                        Blpoint.Bl[sentakun].tamakankaku = tehonkankaku[Blpoint.Bl[sentakun].tamaugoki];
                        Blpoint.Bl[sentakun].pluskaku = tehonpluskaku[Blpoint.Bl[sentakun].tamaugoki];
                        Blpoint.Bl[sentakun].tamaspd = tehonspeed[Blpoint.Bl[sentakun].tamaugoki];
*/
                        tekis[sentakun].tamasyurui--;
                        if (tekis[sentakun].tamasyurui < 0) { tekis[sentakun].tamasyurui = 0; }
                        tekis[sentakun].kankaku = tehonkankaku[tekis[sentakun].tamasyurui];
                        tekis[sentakun].pluskaku = tehonpluskaku[tekis[sentakun].tamasyurui];
                        tekis[sentakun].tamasokudo = tehonspeed[tekis[sentakun].tamasyurui];

                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                       /* Blpoint.Bl[sentakun].tamaugoki++;
                        Blpoint.Bl[sentakun].tamakankaku = tehonkankaku[Blpoint.Bl[sentakun].tamaugoki];
                        Blpoint.Bl[sentakun].pluskaku = tehonpluskaku[Blpoint.Bl[sentakun].tamaugoki];
                        Blpoint.Bl[sentakun].tamaspd = tehonspeed[Blpoint.Bl[sentakun].tamaugoki];
                        tekis[sentakun].tamasyurui++;*/
                        tekis[sentakun].kankaku = tehonkankaku[tekis[sentakun].tamasyurui];
                        tekis[sentakun].pluskaku = tehonpluskaku[tekis[sentakun].tamasyurui];
                        tekis[sentakun].tamasokudo = tehonspeed[tekis[sentakun].tamasyurui];

                    }
                }
                else if (key[DX.KEY_INPUT_U] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] % 5 == 1)
                    {
                      /*  if (Blpoint.Bl[sentakun].tamaugoki == 3 || Blpoint.Bl[sentakun].tamaugoki == 4)
                        {
                            Blpoint.Bl[sentakun].pluskaku -= 0.5f;
                        }
                        else
                        {
                            Blpoint.Bl[sentakun].pluskaku--;
                        }
                        */
                        if (tekis[sentakun].tamasyurui == 3 || tekis[sentakun].tamasyurui == 4)
                        {
                            tekis[sentakun].pluskaku -= 0.5f;
                        }
                        else
                        {
                            tekis[sentakun].pluskaku--;
                        }
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] % 5 == 1)
                    {/*
                        if (Blpoint.Bl[sentakun].tamaugoki == 3 || Blpoint.Bl[sentakun].tamaugoki == 4)
                        {
                            Blpoint.Bl[sentakun].pluskaku += 0.5f;
                        }
                        else
                        {
                            Blpoint.Bl[sentakun].pluskaku++;
                        }*/
                        if (tekis[sentakun].tamasyurui == 3 || tekis[sentakun].tamasyurui == 4)
                        {
                            tekis[sentakun].pluskaku += 0.5f;
                        }
                        else
                        {
                            tekis[sentakun].pluskaku++;
                        }
                    }
                }
                else if (key[DX.KEY_INPUT_Y] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] % 5 == 1)
                    {
                      //  Blpoint.Bl[sentakun].tamakankaku--;
                    //    if (Blpoint.Bl[sentakun].tamakankaku < 1) { Blpoint.Bl[sentakun].tamakankaku = 1; }
                        tekis[sentakun].kankaku--;
                        if (tekis[sentakun].kankaku < 1) { tekis[sentakun].kankaku = 1; }
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] % 5 == 1)
                    {
                       // Blpoint.Bl[sentakun].tamakankaku++;
                        tekis[sentakun].kankaku++;
                    }
                }
                else if (key[DX.KEY_INPUT_T] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] > 0)
                    {
                   //     Blpoint.Bl[sentakun].time--;
                        tekis[sentakun].toujyou--;
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] > 0)
                    {
                        //Blpoint.Bl[sentakun].time++;
                        tekis[sentakun].toujyou++;
                    }
                }
                else if (key[DX.KEY_INPUT_H] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] > 0)
                    {
                   //     Blpoint.Bl[sentakun].hp--;
                        tekis[sentakun].life--;
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] > 0)
                    {
                       // Blpoint.Bl[sentakun].hp++;
                        tekis[sentakun].life++;
                    }
                }
                else if (key[DX.KEY_INPUT_F] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                  /*      if (Blpoint.Bl[sentakun].tamaugoki == 8)
                        {
                            Blpoint.Bl[sentakun].tamaspd -= 0.1f;
                        }
                        else
                        {
                            Blpoint.Bl[sentakun].tamaspd--;
                        }*/
                        if (tekis[sentakun].tamasyurui == 8)
                        {
                            tekis[sentakun].tamasokudo -= 0.1f;
                        }
                        else
                        {
                            tekis[sentakun].tamasokudo--;
                        }
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                       /* if (Blpoint.Bl[sentakun].tamaugoki == 8)
                        {
                            Blpoint.Bl[sentakun].tamaspd += 0.1f;
                        }
                        else
                        {
                            Blpoint.Bl[sentakun].tamaspd++;
                        }*/
                        if (tekis[sentakun].tamasyurui == 8)
                        {
                            tekis[sentakun].tamasokudo += 0.1f;
                        }
                        else
                        {
                            tekis[sentakun].tamasokudo++;
                        }
                    }
                }
                if (key[DX.KEY_INPUT_LSHIFT] != 0)
                {
                    if (key[DX.KEY_INPUT_W] == 1)
                    {
                     //   Blpoint.Bl[sentakun].waittime--;
                       // if (Blpoint.Bl[sentakun].waittime < 0) { Blpoint.Bl[sentakun].waittime = 0; }
                        tekis[sentakun].waittime--;
                        if (tekis[sentakun].waittime < 0) { tekis[sentakun].waittime = 0; }
                    }
                    if (key[DX.KEY_INPUT_S] == 1)
                    {
                       // Blpoint.Bl[sentakun].waittime++;
                        tekis[sentakun].waittime++;
                    }
                }
                else
                {
                    if (key[DX.KEY_INPUT_W] > 0)
                    {
                   //     Blpoint.Bl[sentakun].y--;
                        tekis[sentakun].fy--;
                    }
                    if (key[DX.KEY_INPUT_S] > 0)
                    {
                     //   Blpoint.Bl[sentakun].y++;
                        tekis[sentakun].fy++;
                    }
                }
                if (key[DX.KEY_INPUT_A] > 0)
                {
                   // Blpoint.Bl[sentakun].x--;
                    tekis[sentakun].fx--;
                }
                if (key[DX.KEY_INPUT_D] > 0)
                {
                    //Blpoint.Bl[sentakun].x++;
                    tekis[sentakun].fx++;
                }
                if (key[DX.KEY_INPUT_K] == 1)
                {
                    //Blpoint.Bl[sentakun].Knd = (++Blpoint.Bl[sentakun].Knd) % tekisyuruisu;
                    tekis[sentakun].gaz = (++tekis[sentakun].gaz) % tekisyuruisu;
                }
                if (key[DX.KEY_INPUT_C] != 0)//動き方
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
               //         Blpoint.Bl[sentakun].Col -= 1;
                        tekis[sentakun].ugokikata--;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                 //       Blpoint.Bl[sentakun].Col += 1;
                        tekis[sentakun].ugokikata++;
                    }
                }
                if (key[DX.KEY_INPUT_DELETE] == 1)
                {
                  //  Blpoint.Bl[sentakun].x = -99;
                   // Blpoint.Bl[sentakun].tamakankaku = 0;
                    tekis[sentakun].zx = -99;
                    tekis[sentakun].kankaku = 0;
                }
            }
            #endregion
            #region mode2
            if (mode == 2)
            {
                if (key[DX.KEY_INPUT_N] != 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        sentakun--;
                        if (sentakun < 0) { sentakun = 0; }
                    }
                    else if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                        sentakun++;
                     //   if (sentakun >= Blpoint.Bl.Count()) { sentakun = Blpoint.Bl.Count() - 1; }
                        if (sentakun >= tekis.Count) { sentakun = tekis.Count - 1; }
                    }
                }
                if (key[DX.KEY_INPUT_RETURN] == 1)
                {
                    if (susumu > 0) { susumu = 0; }
                    else { susumu = 1; }
                }
                if (key[DX.KEY_INPUT_SPACE] == 1)
                {
                    susumu = 0;
                 //   gtime = Blpoint.Bl[sentakun].time;
                    gtime = tekis[sentakun].toujyou;
                    mode2kaishi = 0;
                    mode = 1;
                }
                if (key[DX.KEY_INPUT_BACK] == 1)
                {
                    gtime = 0;
                    mode2kaishi = gtime;
                /*    for (int i = 0; i < Blpoint.Num; i++)
                    {
                        Blpoint.Bl[i].fx = Blpoint.Bl[i].x;
                        Blpoint.Bl[i].fy = Blpoint.Bl[i].y;
                        Blpoint.Bl[i].idoutime = 0;
                        Blpoint.Bl[i].cnt = 0;
                        Blpoint.Bl[i].tamas.Clear();
                        Blpoint.Bl[i].seizon = true;
                        Blpoint.Bl[i].hyouji = true;
                        if (Blpoint.Bl[i].Col >= 12)
                        {
                            Blpoint.Bl[i].read.Close();
                        }
                       
                    }*/
                    foreach (var i in tekis)
                        {
                            i.zx = i.fx;
                            i.zy = i.fy;
                            i.idoutime = 0;
                            i.cnte = 0; i.ti = 0;
                            i.tamas.Clear();
                            i.seizon = true;
                            i.hyouji = true;
                            i.life = i.defalutlife;
                            if (i.ugokikata >= 12)
                            {
                                i.read.Close();
                            }

                        }
                    susumu = 0;
                }
            }
            #endregion
            if (mode == 3)
            {
                if (key[DX.KEY_INPUT_RETURN] == 1)
                {
                    StreamWriter writer = new StreamWriter("ugokikata\\ugokiting" + ugokin.Num + ".csv", false);
                    foreach (var k in ugokin.ug[ugokin.Num].Ug.Where(c => c.jizoku != 0))
                    {
                        writer.WriteLine(k.sx + "," + k.sy + "," + k.jizoku);
                    }
                    writer.Close();

                    ugokin.Num++;
                    ugokin.ug[ugokin.Num].Ug = new ugoki_tt[1000];
                    ugokin.ug[ugokin.Num].num = 0;

                }
                else if (key[DX.KEY_INPUT_F] == 1)
                {
                    mode = 0;
                }
                else if (key[DX.KEY_INPUT_LSHIFT] > 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1 || key[DX.KEY_INPUT_LEFT] > 30)
                    {
                        jizokujikan--;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1 || key[DX.KEY_INPUT_RIGHT] > 30)
                    {
                        jizokujikan++;
                    }
                }
                else if (key[DX.KEY_INPUT_Q] > 0)
                {
                    if (key[DX.KEY_INPUT_LEFT] == 1)
                    {
                        ugokin.Num--;
                        ugokin.ug[ugokin.Num].Ug = new ugoki_tt[1000];
                        ugokin.ug[ugokin.Num].num = 0;
                    }
                    //右キー
                    if (key[DX.KEY_INPUT_RIGHT] == 1)
                    {
                        ugokin.Num++;
                        ugokin.ug[ugokin.Num].Ug = new ugoki_tt[1000];
                        ugokin.ug[ugokin.Num].num = 0;
                    }
                }
            }
            
        }

        public static void Show()
        {
#warning リストと配列の境目
            foreach (var i in tekis)
            {
                if (mode == 0 || mode == 2)
                {
                    if (gtime >= i.toujyou && mode2kaishi <= i.toujyou && susumu == 0)
                    {
                        kansuu.DrawRotaGraphfk(i.zx, i.zy, 1, 0, i.gaz, DX.TRUE, false);
                    }
                }
                else if (mode == 1)
                {
                    kansuu.DrawRotaGraphfk(i.fx, i.fy, 1, 0, i.gaz, DX.TRUE, false);
                  
                }
            }
         /*   for (int i = 0; i < Blpoint.Num; i++)
            {
                if (mode == 0 || mode == 2)
                {
                    if (gtime >= Blpoint.Bl[i].time&&mode2kaishi<=Blpoint.Bl[i].time )
                    {
                       // if (mode == 2&&mode2kaishi<) { }

  //kansuu.DrawRotaGraphfk(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 1.0, Blpoint.Bl[i].Angle,
    //                       tekiimg[Blpoint.Bl[i].Knd], DX.TRUE, false);
                
                       
                    }
                }
                else if (mode == 1)
                {
                    if (gtime == Blpoint.Bl[i].time)
                    {
            //            kansuu.DrawRotaGraphfk(Blpoint.Bl[i].x, Blpoint.Bl[i].y, 1.0, Blpoint.Bl[i].Angle,
            //                  tekiimg[Blpoint.Bl[i].Knd], DX.TRUE, false);
                     }
                }
            
                
            }*/
            if (mode == 3)
            {
                foreach (var k in ugokin.ug[ugokin.Num].Ug)
                {
                    kansuu.DrawRotaGraphfk(k.x, k.y, 1, 0, tekiimg[0], 1, false);
                    kansuu.DrawRotaGraphfk(k.ex, k.ey, 1, 0, tekiimg[2], 1, false);
                }
            }
            if (mode == 0||mode ==3)
            {
                if (Operate.State == 1)
                {
                    DX.DrawLine((int)Operate.fPt1.x, (int)Operate.fPt1.y, Mouse.x, Mouse.y, Red);
                }
                //マウスポインタ部に弾を表示する
                DX.DrawRotaGraph(Mouse.x, Mouse.y, 1.0, Operate.Angle,gazo.zakoGraphs[Operate.Knd], DX.TRUE);
                //弾の上に、今設定されているスペースがどれ位か表示する
                DX.DrawLine(Mouse.x, Mouse.y, Mouse.x + Operate.Space, Mouse.y, Blue);
                //表示フラグがオンなら現在の操作設定内容を表示
            }
            else if (mode == 1)
            {
               // DX.DrawLine((int)Blpoint.Bl[sentakun].x, (int)Blpoint.Bl[sentakun].y, (int)Blpoint.Bl[sentakun].x + 50, (int)Blpoint.Bl[sentakun].y, Blue);
           //     kansuu.DrawLine((int)Blpoint.Bl[sentakun].x, (int)Blpoint.Bl[sentakun].y, (int)Blpoint.Bl[sentakun].x + 50, (int)Blpoint.Bl[sentakun].y, Blue);
                kansuu.DrawLine((int)tekis[sentakun].zx,(int)tekis[sentakun].zy,(int)tekis[sentakun].zx+50,(int)tekis[sentakun].zy,Blue);
            }
            else if (mode == 2)
            {
              //  DX.DrawLine((int)Blpoint.Bl[sentakun].fx, (int)Blpoint.Bl[sentakun].fy, (int)Blpoint.Bl[sentakun].fx + 50, (int)Blpoint.Bl[sentakun].fy, Blue);
             //   kansuu.DrawLine((int)Blpoint.Bl[sentakun].fx, (int)Blpoint.Bl[sentakun].fy, (int)Blpoint.Bl[sentakun].fx + 50, (int)Blpoint.Bl[sentakun].fy, Blue);
                kansuu.DrawLine((int)tekis[sentakun].fx, (int)tekis[sentakun].fy, (int)tekis[sentakun].fx + 50, (int)tekis[sentakun].fy, Blue);
            }
            if (Operate.flag == 1)
            {
                if (mode == 0)
                {
                    kansuu.DrawString(0, 0, "座標[%3d,%3d]" + Mouse.x + ":" + Mouse.y, White);
                    kansuu.DrawString(0, 20, "種類     [%2d] : Kキー" + Operate.Knd, White);
                    kansuu.DrawString(0, 40, "移動方法　     [%2d] : Cキー" + Operate.Col, White);
                    kansuu.DrawString(0, 60, "空白    [%3d] : S+←→キー", Operate.Space, White);
                    kansuu.DrawString(0, 80, "エンターキーで時間を進める／止める", White);
                    kansuu.DrawString(0, 100, "シフト押しながらクリックで連続描画:"+Operate.renzoku+"時間単位   :R+←→キー", White);
                    kansuu.DrawString(0, 120, "連続描画の回数(合計)は" + Operate.renzokukaisu + "回   :F+←→キー", White);
                    kansuu.DrawString(0, 140, "左右対称描画："+Operate.hanten+":Iキー", White);
                    kansuu.DrawString(0, 160, "バックスペースキーで時間を0に戻す", White);
                    kansuu.DrawString(0, 180, "waittime     :w/s" + Operate.waittime, White);
                    kansuu.DrawString(0, 200, "体力     :H+←→キー" + Operate.hp, White);
                    kansuu.DrawString(0, 220, "スペースキーで非表示", White);
                    kansuu.DrawString(0, 240, "時間     " + gtime, White);
                }
                else if (mode == 1)
                {
                    kansuu.DrawString(0, 0, "座標[%3d,%3d]:W,S,A,Dキー" + tekis[sentakun].zx + ":" + tekis[sentakun].zy, White);
                    kansuu.DrawString(0, 20, "種類     [%2d] : Kキー" + tekis[sentakun].size, White);
                    kansuu.DrawString(0, 40, "移動方法　     [%2d] : Cキー" + tekis[sentakun].ugokikata, White);
                    kansuu.DrawString(0, 60, "弾の個数    [%3d] : R+←→キー" + tekis[sentakun].tamakosuu, White);
                    kansuu.DrawString(0, 80, "弾の色    [%3d] : P+←→キー" + tekis[sentakun].tamacol, White);
                    kansuu.DrawString(0, 100, "弾の見た目    [%3d] : O+←→キー" + tekis[sentakun].tamaknd, White);
                    kansuu.DrawString(0, 120, ugokinamae[tekis[sentakun].tamaknd] + "     [%3d] : I+←→キー", White);
                    kansuu.DrawString(0, 140, plusmessage[tekis[sentakun].tamaknd]+"    [%3d] : U+←→キー" + tekis[sentakun].pluskaku, White);
                    kansuu.DrawString(0, 160, "弾の発射間隔    [%3d] : Y+←→キー" + tekis[sentakun].kankaku, White);
                    kansuu.DrawString(0, 180, "登場までの時間    [%3d] : T+←→キー" + tekis[sentakun].toujyou, White);
                    kansuu.DrawString(0, 200, "弾の速度     :F+←→キー" + tekis[sentakun].tamasokudo, White);
                    kansuu.DrawString(0, 220, "waittime     :shift+(w/s)" + tekis[sentakun].waittime, White);
                    kansuu.DrawString(0, 240, "体力     :H+←→キー" + tekis[sentakun].life, White);
                    kansuu.DrawString(0, 260, "選択     :n" + sentakun, White);
                    kansuu.DrawString(0, 280, "削除     :delete", White);
                    kansuu.DrawString(0, 300, "モード切替     :tab" + mode, White);
                    kansuu.DrawString(0, 320, "現在時刻     " + gtime, White);
                    kansuu.DrawString(0, 340, "スペースキーで非表示", White);
                  
                }
                else if (mode == 2)
                {
                    kansuu.DrawString(0, 0, "選択     :n" + sentakun, White);
                    kansuu.DrawString(0, 20, "敵追加モードへ     :tab" + mode, White);
                    kansuu.DrawString(0, 40, "敵情報変更モードへ     :space", White);
                    kansuu.DrawString(0, 60, "時間の一時停止/解除    :enter" + gtime, White);
                    kansuu.DrawString(0, 80, "時間を0に戻す          :backspace", White);
                }
                else if (mode == 3)
                {
                    kansuu.DrawString(0, 0, "座標[%3d,%3d]" + Mouse.x + ":" + Mouse.y, White);
                    kansuu.DrawString(0, 20, "動き方を"+ugokin.Num+"番目に保存:enter", White);
                    kansuu.DrawString(0, 40, jizokujikan + "速度で移動:shift+←→キー", White);
                    kansuu.DrawString(0, 60, "保存ファイル変更:Q+←→キー", White);
                    if (File.Exists("ugokikata\\ugokiting" +ugokin.Num+".csv"))
                    {
                        kansuu.DrawString(0, 80, "既に保存されているファイル有(上書き保存されます)", Red);
                    }
               
                }
            }

        }
        public static bool sotohan(float x,float y)
        {
            return (x < 0 || x > screenx || y < 0 || y > screeny);
        }
        public static void zakoudouing()
        {
            foreach (var i in tekis.Where(c=>gtime>=c.toujyou))
            {
                
                i.main();
            }
         /*   for (int i = 0; i < Blpoint.Num; i++)
            {
                Blpoint.Bl[i].hyouji = !sotohan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy);
                if (Blpoint.Bl[i].tamas.Count > 0) { Blpoint.Bl[i].seizon = true; }
                    if (!(gtime >= Blpoint.Bl[i].time)) { continue; }

                    switch (Blpoint.Bl[i].Col)
                    {
                        case 0:
                            zakoudou.enemy_pattern0(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 1:
                            zakoudou.enemy_pattern1(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 2:
                            zakoudou.enemy_pattern2(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 3:
                            zakoudou.enemy_pattern3(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 4:
                            zakoudou.enemy_pattern4(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 5:
                            zakoudou.enemy_pattern5(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 6:
                            zakoudou.enemy_pattern6(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 7:
                            zakoudou.enemy_pattern7(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 8:
                            zakoudou.enemy_pattern8(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 9:
                            zakoudou.enemy_pattern9(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        case 10:
                            zakoudou.enemy_pattern10(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime, ref Blpoint.Bl[i].ang, ref Blpoint.Bl[i].zspeed);
                            break;
                        case 11:
                            zakoudou.enemy_pattern11(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, Blpoint.Bl[i].waittime);
                            break;
                        default:
                            zakoudou.enemyfile(ref Blpoint.Bl[i].sx, ref Blpoint.Bl[i].sy, ref Blpoint.Bl[i].idoutime, Blpoint.Bl[i].Col, ref Blpoint.Bl[i].waittime,Blpoint.Bl[i].cnt,ref Blpoint.Bl[i].read, "ugokiting" + (Blpoint.Bl[i].Col-12));
                            break;

                    }
                    Blpoint.Bl[i].fx += (float)Math.Cos(Blpoint.Bl[i].ang) * Blpoint.Bl[i].zspeed;
                    Blpoint.Bl[i].fy += (float)Math.Sin(Blpoint.Bl[i].ang) * Blpoint.Bl[i].zspeed;

                    Blpoint.Bl[i].fx += Blpoint.Bl[i].sx;
                    Blpoint.Bl[i].fy += Blpoint.Bl[i].sy;
                   
                    #region 弾の動き方
               if (Blpoint.Bl[i].seizon)
                {
                    
                        int t = Blpoint.Bl[i].cnt;
                        int t2 = t % Blpoint.Bl[i].tamakankaku;
                        float tzang = kansuu.zikiangle(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy);
                        float tamasokudo = Blpoint.Bl[i].tamaspd;    
                   switch (Blpoint.Bl[i].tamaugoki)
                        {
                            case 0:
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            float plusangle = 0;
                                            float jissai = 0;
                                            if (k % 2 == 1) { plusangle = 5 * (k + 1) / 2; }
                                            else { plusangle = -5 * k / 2; }
                                            jissai = (plusangle) * Blpoint.Bl[i].pluskaku + kansuu.zikiangle(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy);
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, jissai, tamasokudo, Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                break;

                            case 1:
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            float plusangle = 0;
                                            float jissai = 0;
                                            if (k % 2 == 1) { plusangle = 5 * (k + 1) / 2; }
                                            else { plusangle = -5 * k / 2; }
                                            jissai = (plusangle) * Blpoint.Bl[i].pluskaku + kansuu.zikiangle(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy);
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, jissai, (tamasokudo / 1000) * t, Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            float angle = kansuu.zikiangle(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy);
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, angle + kansuu.PI2() / Blpoint.Bl[i].tamakosuu * i, tamasokudo, Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, tzang + kansuu.rang(kansuu.PI() / 4), tamasokudo + kansuu.rang(1.5), Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                break;
                            case 4:
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, tzang + kansuu.rang(kansuu.PI() / 4), tamasokudo + kansuu.rang(1.5), Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                foreach (var l in Blpoint.Bl[i].tamas)
                                {
                                    if (l.speed > 1.5)
                                    {
                                        l.speed -= 0.04f;
                                    }
                                }
                                break;
                            case 5:
                                float baseangle = 0;
                                
                                if (Blpoint.Bl[i].hyouji)
                                {
                                    if (t2 == 0)
                                    {
                                        if (t == 0)
                                        {
                                            baseangle = tzang;
                                        }
                                        for (int k = 0; k < Blpoint.Bl[i].tamakosuu; k++)
                                        {
                                            float plusangle = 0;
                                            float jissai = 0;
                                            if (k % 2 == 1) { plusangle = 5 * (k + 1) / 2; }
                                            else { plusangle = -5 * k / 2; }
                                            jissai = (plusangle) * Blpoint.Bl[i].pluskaku + baseangle;
                                            Blpoint.Bl[i].tamas.Add(new Tdan(Blpoint.Bl[i].fx, Blpoint.Bl[i].fy, 0, Blpoint.Bl[i].tamacol, jissai,tamasokudo, Blpoint.Bl[i].tamaknd));
                                        }
                                    }
                                }
                                break;
                            default:
                                break;

                        }
                    }
                    #endregion
               
                    foreach (Tdan tama in Blpoint.Bl[i].tamas)
                    {
                        tama.x += kansuu.Cos(tama.angle) * tama.speed;
                        tama.y += kansuu.Sin(tama.angle) * tama.speed;
                        if (tama.inswitch)
                        {
                            if (kansuu.sotoRota(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagh[tama.size]))
                            {
                                tama.seizon = false;
                            }
                            if (kansuu.haniatariz(tama.x, tama.y, tama.atarihani, tama.speed, tama.angle))
                            {
                                tama.seizon = false;
                            }
                        }

                        else { if (kansuu.naka(tama.x, tama.y, gazo.otamagw[tama.size], gazo.otamagw[tama.size])) { tama.inswitch = true; }; }
                   tama.dispangle = tama.angle + kansuu.PI() / 2; 
                        kansuu.DrawRotaGraphfk(tama.x, tama.y, 1, tama.dispangle, gazo.otama[tama.size, tama.col], DX.TRUE, false);
                        tama.cnt++;
                        
                    }
                    if (mode2kaishi > Blpoint.Bl[i].time) { Blpoint.Bl[i].seizon = false; }
                    Blpoint.Bl[i].tamas.RemoveAll(x => !x.seizon);
                  Blpoint.Bl[i].idoutime += 1;
                  Blpoint.Bl[i].cnt += 1;
                
            }*/
        }
        public static void Output()
        {
            StreamWriter writer;
            if (mode == 0 || mode == 1 || mode == 2)
            {
                /* for (int k = 0; k < 1000; k++)
                 {
                     if (File.Exists("enemy\\output" + k + ".csv")) { continue; }
               
                  writer = new StreamWriter("enemy\\output"+k+".csv", false);
                 writer.WriteLine("x座標,y座標,動き方,種類,登場時間,待ち時間,弾間隔,弾個数,弾種類,弾knd,弾col,弾角度,弾速度,体力,アイテム");
                 foreach (var i in Blpoint.Bl.Where(c => c.x != -99 && c.tamakankaku != 0))
                 {
                     writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", i.x, i.y, i.Col, i.Knd, i.time, i.waittime, i.tamakankaku, i.tamakosuu, i.tamaugoki, i.tamaknd, i.tamacol, i.pluskaku, i.tamaspd,i.hp, 0, 0, 0);
                 }
                 writer.Close();
                 break;
                 }*/
                for (int k = 0; k < 1000; k++)
                {
                    if (File.Exists("enemy\\output" + k + ".csv")) { continue; }
                    writer = new StreamWriter("enemy\\output" + k + ".csv", false);
                    writer.WriteLine("x座標,y座標,動き方,種類,登場時間,待ち時間,弾間隔,弾個数,弾種類,弾knd,弾col,弾角度,弾速度,体力,アイテム");
                    foreach (var i in tekis.Where(c=>c.fx!=99&&c.kankaku!=0))
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", i.fx, i.fy, i.ugokikata, i.size, i.toujyou, i.waittime, i.kankaku, i.tamakosuu, i.tamasyurui, i.tamaknd, i.tamacol, i.pluskaku, i.tamasokudo, i.life, 0);
                
                    }
                    writer.Close();
                    break;
                }

            }
           
            int x = 0;
            int y = 0;
            int sx = 2;
            int sy = 2;
            while (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 0 && DX.ProcessMessage() == 0)
            {
                DX.ClearDrawScreen();
                string str = "正常に書き込みが終了しました。エンターキーで終了します。";
                DX.SetFontSize(30);
                DX.DrawString(x, y, str, DX.GetColor(255, 255, 255));
                DX.SetFontSize(15);
                x += sx;
                y += sy;
                if (x < 0 || x > screenx - 450) { sx *= -1; }
                if (y < 0 || y > screeny - 15) { sy *= -1; }
                DX.ScreenFlip();
            }
        }

       public static void main()
        {
            DX.SetWindowText("敵配置ツール");
            if (DX.DxLib_Init() < 0) { return; }
         
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.SetMouseDispFlag(DX.TRUE);
            syokika();
            ini();
            load();
            //Blpoint.Bl = new Bl_t[10000];
            ugokin.ug = new ugoki_t[1000];
            ugokin.ug[0].Ug = new ugoki_tt[1000];
            ugokin.ug[0].num = 0;
           while (GetHitKeyStateAll_2(key) == 0 && DX.ProcessMessage() == 0 && key[DX.KEY_INPUT_ESCAPE] == 0)
            {
                DX.ClearDrawScreen();
                DX.DrawBox(screenx, 0, screenx + 300, screeny, DX.GetColor(255, 255, 255), DX.TRUE);
                  
                for (int i = 0; i < Program.zibun.Count; i++)
                {
                    Program.zibun[i].Ziki();
                    Program.syux = Program.zibun[0].zikix;
                    Program.syuy = Program.zibun[0].zikiy;
                }
                if (mode == 0 || mode == 3)
                {
                    GetHitMouseStateAll_2();
                    CalcMouse();
                    CalcOperate();
                    Show();
                }
                else if (mode == 1 || mode == 2)
                {
                    Show();
                    CalcOperate();
                }

                for (int i = 0; i < Math.Abs(susumu); i++)
                {
                    zakoudouing();
                }
                DX.DrawBox(0, 0, Program.fx, Program.scy, DX.GetColor(0, 0, 255), DX.FALSE);
                DX.DrawBox(0, 0, Program.scx, Program.fy, DX.GetColor(0, 0, 255), DX.FALSE);
                DX.DrawBox(0, Program.scy, Program.scx, Program.scy - Program.fy, DX.GetColor(0, 0, 255), DX.FALSE);
            
                DX.ScreenFlip();
            }
           if (DX.ProcessMessage() == 0) { Output(); }
            Program.gamemode = 1;
        }
    }
}
