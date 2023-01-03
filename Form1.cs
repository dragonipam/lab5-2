using lab5_2.Objects;

namespace lab5_2
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker? marker;
        Ellipse? dot1;
        Ellipse? dot2;
        ushort score = 0;

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            player.OnOverlap += (player, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            player.OnDotOverlap += (dot) =>
            {
                ResetDot(dot);
                score++;
                Score_label.Text = $"—чЄт: {score}";
            };

            Random random = new();
            dot1 = new(random.Next(20, 539), random.Next(20, 384), 0, random.Next(30, 50), random.Next(60, 150));
            dot2 = new(random.Next(20, 539), random.Next(20, 384), 0, random.Next(30, 50), random.Next(60, 150));

            objects.Add(player);
            objects.Add(dot1);
            objects.Add(dot2);
        }

        private void ResetDot(Ellipse dot)
        {
            Random random = new();
            dot.X = random.Next(20, 539);
            dot.Y = random.Next(20, 384);
            dot.radius = random.Next(30, 50);
            dot.timer = random.Next(60, 150);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
            foreach (var obj in objects.ToList())
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float lenght = MathF.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDot();

            pbMain.Invalidate();
        }

        private void UpdateDot()
        {
            dot1.timer--;
            dot2.timer--;

            float d = (float)1 / dot1.timer;
            dot1.radius -= dot1.radius * d;
            d = (float)1 / dot2.timer;
            dot2.radius -= dot2.radius * d;

            if (dot1.timer < 0)
                ResetDot(dot1);
            else if (dot2.timer < 0)
                ResetDot(dot2);
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}