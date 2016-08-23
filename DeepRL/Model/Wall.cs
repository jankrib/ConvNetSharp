using DeepRL.Helpers;

namespace DeepRL.Model
{
    public class Wall
    {
        public Vector2 P1 { get; set; }
        public Vector2 P2 { get; set; }

        public Wall(Vector2 p1, Vector2 p2)
        {
            P1 = p1;
            P2 = p2;
        }
    }
}
