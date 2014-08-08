using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _2._0._0
{
   public static class atari
    {
       public static bool ReCollision(float mx, float my, float mgw, float mgh, float tx, float ty, float tgw, float tgh)
       {

           if (mx + mgw > tx && mx < tx + tgw && my + mgh > ty && my < ty + tgh) return true;
           else return false;
       }

       public static bool CirCollison(float scx, float scy, int r1, double scx2, double scy2, int r2)
       {
           if ((scx - scx2) * (scx - scx2) + (scy - scy2) * (scy - scy2) < (r1 + r2) * (r1 + r2))
           { return true; }

           else return false;
       }




    }
 
}
