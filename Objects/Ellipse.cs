using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2.Objects
{
    class Ellipse : BaseObject
    {
        public float radius;
        public int timer;

        public Ellipse(float x, float y, float angle, float radius, int timer) : base(x, y, angle)
        {
            this.radius = radius;
            this.timer = timer;
        }
    }
}
