using DeepRL.Helpers;

namespace DeepRL.Model
{
    public abstract class Item
    {
        public Vector2 Position { get; set; }

        public Item(Vector2 position)
        {
            Position = position;
        }

        public double Radius { get; } = 10;
    }
}
