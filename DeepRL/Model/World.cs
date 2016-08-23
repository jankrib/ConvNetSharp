using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using DeepRL.Helpers;

namespace DeepRL.Model
{
    class World
    {
        public event EventHandler AfterTick;

        const double MARGIN = 10;

        private Random _random = new Random(0);

        public double Width { get; set; }
        public double Height { get; set; }

        public List<Wall> Walls { get; set; } = new List<Wall>();

        public ObservableCollection<Fruit> Fruits { get; set; } = new ObservableCollection<Fruit>();
        public ObservableCollection<Poison> Poisons { get; set; } = new ObservableCollection<Poison>();

        public List<Agent> Agents { get; set; } = new List<Agent>();

        public World(double width, double height)
        {
            Width = width;
            Height = height;

            Walls.Add(new Wall(new Vector2(MARGIN, MARGIN), new Vector2(Width- MARGIN, MARGIN)));
            Walls.Add(new Wall(new Vector2(Width - MARGIN, MARGIN), new Vector2(Width - MARGIN, Height - MARGIN)));
            Walls.Add(new Wall(new Vector2(Width - MARGIN, Height - MARGIN), new Vector2(MARGIN, Height - MARGIN)));
            Walls.Add(new Wall(new Vector2(MARGIN, Height - MARGIN), new Vector2(MARGIN, MARGIN)));

            AddFruit();
            AddFruit();
            AddFruit();
            AddFruit();
            AddFruit();

            AddPoison();
            AddPoison();
            AddPoison();
            AddPoison();

            AddAgent();
        }

        public void Update(double dt)
        {
            foreach (var agent in Agents)
            {
                var oldPosition = agent.Position;
                var oldAngle = agent.Angle;

                var axel = new Vector2(0, agent.Radius/2);
                axel = axel.Rotate(agent.Angle + Math.PI / 2);
                var w1p = axel;
                var w2p = new Vector2(0,0) - axel;
                w1p = w1p.Rotate(-agent.Wheels.Left);
                w2p = w2p.Rotate(agent.Wheels.Right);
                var toMove = (w1p + w2p) * 0.5 * dt;

                agent.Angle -= agent.Wheels.Left * dt;
                agent.Angle += agent.Wheels.Right * dt;
                
                var hit = Collide(agent.Position, toMove, true, true);

                if (hit.Type == HitType.None)
                    agent.Position += toMove;
                else
                {
                    
                }
            }

            OnAfterTick();
        }

        private HitResult Collide(Vector2 position, Vector2 ray, bool checkWalls, bool checkItems)
        {
            var minres = HitResult.None;

            if(checkWalls)
            {
                foreach (var wall in Walls)
                {
                    var res = Raycaster.LineIntersect(position, position + ray, wall.P1, wall.P2, HitType.Wall);

                    if (res.Type != HitType.None && (minres == HitResult.None || minres.Distance > res.Distance))
                    {
                        minres = res;
                    }
                }
            }

            return minres;
        }

        private bool _running = false;

        public void Start()
        {
            _running = true;

            Task.Run(() =>
            {
                Run();
            });
        }

        private void Run()
        {
            while (_running)
            {
                //TODO calculate dt
                Update(0.1);
                Thread.Sleep(50);
            }
        }

        public void Stop()
        {
            _running = false;
        }

        public Poison AddPoison()
        {
            Vector2 position = GetRandomPosition();
            var poison = new Poison(position);
            Poisons.Add(poison);

            return poison;
        }
        public Fruit AddFruit()
        {
            Vector2 position = GetRandomPosition();
            var fruit = new Fruit(position);
            Fruits.Add(fruit);

            return fruit;
        }
        public Agent AddAgent()
        {
            Vector2 position = GetRandomPosition();
            var agent = new Agent(position);
            Agents.Add(agent);

            return agent;
        }

        private Vector2 GetRandomPosition()
        {
            double x = MARGIN + _random.NextDouble() * (Width - MARGIN * 2);
            double y = MARGIN + _random.NextDouble() * (Height - MARGIN * 2);

            return new Vector2(x, y);
        }

        protected virtual void OnAfterTick()
        {
            AfterTick?.Invoke(this, EventArgs.Empty);
        }
    }
}
