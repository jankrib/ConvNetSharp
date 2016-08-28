using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepRL.Model;

namespace DeepRL.Helpers
{
    public enum HitType
    {
        None,
        Wall,
        Fruit,
        Poison
    }

    public struct HitResult
    {
        public double Distance { get; }

        public Vector2 IntersectionPoint { get; }

        public HitType Type { get; }

        public Item Item { get; set; }

        public HitResult(double ua, Vector2 up, HitType type)
            :this(ua, up, type, null)
        {
            
        }

        public HitResult(double ua, Vector2 up, HitType type, Item item)
        {
            Distance = ua;
            IntersectionPoint = up;
            Type = type;
            Item = item;
        }

        public static HitResult None => new HitResult(double.NaN, Vector2.Zero, HitType.None);

        public static bool operator == (HitResult a, HitResult b)
        {
            if (double.IsNaN(a.Distance) && double.IsNaN(b.Distance))
                return true;

            return a.Type == b.Type && a.Distance == b.Distance && a.IntersectionPoint == b.IntersectionPoint;
        }

        public static bool operator !=(HitResult a, HitResult b)
        {
            return !(a == b);
        }

        public static HitResult Best(HitResult a, HitResult b)
        {
            if (b.Type != HitType.None && (a == HitResult.None || a.Distance > b.Distance))
            {
                return b;
            }

            return a; 
        }
    }
}
