using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
namespace _2._0._0
{
    public class ene : en
    {
    
        public ene(float X, float Y, int Knd, int Col, float Angle, int Time, int Waittime, int Hp) :
            base(X, Y, Col, Knd, Time, Waittime, 20, 1, 0, 2, 0, 5, 3, Hp, 0)
        {
            
        }
    }
}
