using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class beje
    {
        #region パスカルの三角形
        public static int[][] nPasTgl =
        {
                      new int[]{},
                     new int[] {1},
                    new int[] {1,2,1},
                   new int[]{1,3,3,1},
                  new int[]{1,4,6,4,1},
                 new int[]{1,5,10,10,5,1},
                new int[] {1,6,15,20,15,6,1},
               new int[] {1,7,21,35,35,21,7,1},
              new int[] {1,8,28,56,70,56,28,8,1},
             new int[]{1,9,36,84,126,126,84,36,9,1},
            new int[]{1,10,45,120,210,252,210,120,45,10,1},
           new int[]{1,11,55,165,330,464,464,330,165,55,11,1},
          new int[]{ 1,12,66,220,495,792,924,792,495,220,66,12,1},						
         new int[]{1,13,78,286,715,1287,1716,1716,1287,715,286,78,13,1},						
        new int[]{1,14,91,364,1001,2002,3003,3432,3003,2002,1001,364,91,14,1},					
       new int[]{1,15,105,455,1365,3003,5005,6435,6435,5005,3003,1365,455,105,15,1}			

        };
        #endregion

        public static float Get(float y1, float y2, float t, int n)
        { 
        float b = t > 1 ? 1 : (t < 0 ? 0 : t);
                float a = 1 - b;
                float ay=0;
                float[] y=new float[4]{ 0, y1, y2, 1 };
                int m;
                for( int i=0; i<=n; i++ ){
                        m = i==0 ? 0 : ( i==n ? 3 : ( i <= n/2 ? 1 : 2 ));//yの添え字決定
                       
                            ay += nPasTgl[n][i] * (float)Math.Pow(a, n - i) * (float)Math.Pow(b, i) * y[m];//※1
                        
                }
                return ay;
        }
        public  enum ePrm_t
        {           // Prm/ Prm2
           
            eSlow_Lv7,
            eSlow_Lv6,
            eSlow_Lv5,      // 11強　ゆっくり動き始める / ゆっくり動き終える
            eSlow_Lv4,      // ↑　ゆっくり動き始める / ゆっくり動き終える
            eSlow_Lv3,      // 　　ゆっくり動き始める / ゆっくり動き終える
            eSlow_Lv2,      // ↓　ゆっくり動き始める / ゆっくり動き終える
            eSlow_Lv1,      // 弱　ゆっくり動き始める / ゆっくり動き終える
            eNoAccel,       // 　　直線的な動きをする
            eRapid_Lv1,     // 弱　急に動き始める      / 急に動き終える
            eRapid_Lv2,     // ↑　急に動き始める      / 急に動き終える
            eRapid_Lv3,     // 　　急に動き始める      / 急に動き終える
            eRapid_Lv4,     // ↓　急に動き始める      / 急に動き終える
            eRapid_Lv5,     // 強　急に動き始める      / 急に動き終える
            eRapid_Lv6,
            eRapid_Lv7,
           
        };
     
       public static float Get(ePrm_t ePrm1, ePrm_t ePrm2, float fRate)
        {
            int n = 3;                //n次元指定
            float y1 = 0, y2 = 0;
            switch (ePrm1)
            {
            
                case ePrm_t.eSlow_Lv7: y1 = 0; n = 15; break;//15次元
                case ePrm_t.eSlow_Lv6: y1 = 0; n = 13; break;//13次元
                case ePrm_t.eSlow_Lv5: y1 = 0; n = 11; break;//11次元
                case ePrm_t.eSlow_Lv4: y1 = 0; n = 9; break;//9次元
                case ePrm_t.eSlow_Lv3: y1 = 0; n = 7; break;//7次元
                case ePrm_t.eSlow_Lv2: y1 = 0; n = 5; break;//5次元
                case ePrm_t.eSlow_Lv1: y1 = 0; n = 3; break;//3次元
                case ePrm_t.eNoAccel: y1 = 0.333333f; n = 3; break;//直線の場合は3次元中1/3の点
                case ePrm_t.eRapid_Lv1: y1 = 1; n = 3; break;//3次元
                case ePrm_t.eRapid_Lv2: y1 = 1; n = 5; break;//5次元
                case ePrm_t.eRapid_Lv3: y1 = 1; n = 7; break;//7次元
                case ePrm_t.eRapid_Lv4: y1 = 1; n = 9; break;//9次元
                case ePrm_t.eRapid_Lv5: y1 = 1; n = 11; break;//11次元
                case ePrm_t.eRapid_Lv6: y1 = 1; n = 13; break;//3次元
                case ePrm_t.eRapid_Lv7: y1 = 1; n = 15; break;//5次元
                }
            switch (ePrm2)
            {
                case ePrm_t.eSlow_Lv7: y2 = 1; n = 15; break;//15次元
                case ePrm_t.eSlow_Lv6: y2 = 1; n = 13; break;//13次元
                case ePrm_t.eSlow_Lv5: y2 = 1; n = 11; break;//11次元
                case ePrm_t.eSlow_Lv4: y2 = 1; n = 9; break;//9次元
                case ePrm_t.eSlow_Lv3: y2 = 1; n = 7; break;//7次元
                case ePrm_t.eSlow_Lv2: y2 = 1; n = 5; break;//5次元
                case ePrm_t.eSlow_Lv1: y2 = 1; n = 3; break;//3次元
                case ePrm_t.eNoAccel: y2 = 0.6666667f; n = 3; break;//直線の場合は3次元中2/3の点
                case ePrm_t.eRapid_Lv1: y2 = 0; n = 3; break;//3次元
                case ePrm_t.eRapid_Lv2: y2 = 0; n = 5; break;//5次元
                case ePrm_t.eRapid_Lv3: y2 = 0; n = 7; break;//7次元
                case ePrm_t.eRapid_Lv4: y2 = 0; n = 9; break;//9次元
                case ePrm_t.eRapid_Lv5: y2 = 0; n = 11; break;//11次元
                case ePrm_t.eRapid_Lv6: y2 = 0; n = 13; break;//13次元
                case ePrm_t.eRapid_Lv7: y2 = 0; n = 15; break;//15次元
            }
            return Get(y1, y2, fRate, n);
        }


    }
}
