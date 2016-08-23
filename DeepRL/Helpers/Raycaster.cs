using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepRL.Helpers
{
    public static class Raycaster
    {
        public static HitResult LinePointIntersect(Vector2 p1, Vector2 p2, Vector2 p0, double rad, HitType type)
        {
            var v = new Vector2(p2.Y - p1.Y, -(p2.X - p1.X)); // perpendicular vector
            var d = Math.Abs((p2.X - p1.X) * (p1.Y - p0.Y) - (p1.X - p0.X) * (p2.Y - p1.Y));
            d = d/v.Length;
            if (d > rad) { return HitResult.None; }

            v.Normalize();
            v.Scale(d);
            var up = p0 + v;
            double ua;
            if (Math.Abs(p2.X - p1.X) > Math.Abs(p2.Y - p1.Y))
            {
                ua = (up.X - p1.X) / (p2.X - p1.X);
            }
            else {
                ua = (up.Y - p1.Y) / (p2.Y - p1.Y);
            }
            if (ua > 0.0 && ua < 1.0)
            {
                return new HitResult(ua, up, type);
            }
            return HitResult.None;
        }

        public static HitResult LineIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, HitType type)
        {
            var denom = (p4.Y - p3.Y)*(p2.X - p1.X) - (p4.X - p3.X)*(p2.Y - p1.Y);
            if (denom == 0.0)
            {
                return HitResult.None;
            } // parallel lines

            var ua = ((p4.X - p3.X)*(p1.Y - p3.Y) - (p4.Y - p3.Y)*(p1.X - p3.X))/denom;
            var ub = ((p2.X - p1.X)*(p1.Y - p3.Y) - (p2.Y - p1.Y)*(p1.X - p3.X))/denom;
            if (ua > 0.0 && ua < 1.0 && ub > 0.0 && ub < 1.0)
            {
                var up = new Vector2(p1.X + ua*(p2.X - p1.X), p1.Y + ua*(p2.Y - p1.Y));

                return new HitResult(ua, up, type); // up is intersection point
            }

            return HitResult.None;
        }
    }
}
