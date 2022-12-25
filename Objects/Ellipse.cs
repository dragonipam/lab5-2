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
        public Ellipse(float x, float y) : base(x, y)
        {
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.LightGreen), -30, -30, 50, 50);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-30, -30, 50, 50);
            return path;
        }
    }
}