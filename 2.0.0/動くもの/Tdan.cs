using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace _2._0._0
{
    //Boss1用の弾幕(4種類ぐらい)
    public class Tdan
    {
        public static long mynumber;
        public long number;
        public int effect = 0;
        public int efvalue = 0;
        public float x { get; set; }
        public float y { get; set; }
        public int cnt { get; set; }
        public int col { get; set; }
        public bool inswitch { get; set; }
        public bool seizon { get; set; }
        public float angle { get; set; }
        public float speed = 1.5f;
        public int size { get; set; }
        public int state { get; set; }
        public float bangle { get; set; }
        public float dispangle { get; set; }
        public float power = 3;
        public bool kaiten = false;
        public int gw, gh, iro = 10;
        public int pluskaku = 5;
        public int hankei = 0;
        public float atarihani;
        public bool draw = true;
        public bool mitizure = false;
        public float sx = 0, sy = 0;
        public float[] baseangle = new float[10];
        public List<Tdan> tamas = new List<Tdan>();
        public Tdan(float X, float Y, int Cnt, int Col, float Angle, float Speed, int Size, int State)
        {
            x = X; y = Y; cnt = Cnt; inswitch = true; seizon = true; angle = Angle; speed = Speed; size = Size;
            col = Col; state = State; bangle = angle;
            mynumber = (++mynumber) % long.MaxValue;
            number = mynumber;
            switch (size)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6: atarihani = 4.5f; break;
                case 7: atarihani = 4.0f; break;
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
                case 23: atarihani = 2.25f; break;
                default:
                    break;
            }
            gw = gazo.otamagw[size];
            gh = gazo.otamagh[size];
        }
        #region コンストラクタ達


        public Tdan(int Col, float X, float Y, int Size, float Angle, float Speed)
            : this(X, Y, 0, Col, Angle, Speed, Size, -9999)
        { }
        public Tdan(int Col, float X, float Y, int Size, float Angle, float Speed, bool Kaiten)
            : this(X, Y, 0, Col, Angle, Speed, Size, -9999)
        { kaiten = Kaiten; }
        public Tdan(int Col, float X, float Y, int Size, float Angle, float Speed, int State, bool Kaiten)
            : this(X, Y, 0, Col, Angle, Speed, Size, State)
        { kaiten = Kaiten; }
        public Tdan(int Col, float X, float Y, int Size, float Angle, int Pluskaku)
            : this(X, Y, 0, Col, Angle, 1.5f, Size)
        {
            pluskaku = Pluskaku;
        }
        public Tdan(float X, float Y, int Cnt, int Col, float Angle, float Speed, int Size)
            : this(X, Y, Cnt, Col, Angle, Speed, Size, -9999)
        { }
        public Tdan(float X, float Y, int Cnt, int Col, float Angle, float Speed, int Size, int State, float Power)
            : this(X, Y, Cnt, Col, Angle, Speed, Size, State)
        { power = Power; }
        #endregion

        //弾のステート一覧
        //-2特になし　主に役目を終えた弾に使う
        //-1敵からのうち返し
        //0,1→Boss弾幕2で用いる回転
        //2,3,4Boss弾幕4で用いる
        //5一般攻撃の自機狙い弾×円形弾(回転弾)の表示しない自機狙い
        //6回転弾
        //7低速バラマキショット
        //8敵からの回転
        //9,10Boss弾幕9で用いる
        //11,12Boss弾幕12で用いる
        //13,14Boss弾幕13,14で用いる
    }
}
