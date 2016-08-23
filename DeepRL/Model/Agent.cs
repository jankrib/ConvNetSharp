using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConvNetSharp;
using DeepRL.Helpers;

namespace DeepRL.Model
{
    public class Agent:Item
    {
        public WheelRotationSpeed Wheels { get; set; }

        private Brain _brain;

        public double Angle
        {
            get { return _angle; }
            set
            {
                if (value < 0)
                    value += 2 * Math.PI;

                if (value > 2 * Math.PI)
                    value -= 2 * Math.PI;

                _angle = value;
            }
        }

        public double Speed { get; set; } = 5;

        private WheelRotationSpeed[] _actions = new[]
        {
            new WheelRotationSpeed(1, 1),
            new WheelRotationSpeed(0.8, 1),
            new WheelRotationSpeed(1, 0.8),
            new WheelRotationSpeed(0.5, 0),
            new WheelRotationSpeed(0, 0.5),
        };

        private double _angle;

        public Agent(Vector2 position) 
            : base(position)
        {
            var numInputs = 27; // 9 eyes, each sees 3 numbers (wall, green, red thing proximity)
            var numActions = _actions.Length; // possible angles agent can turn
            
            _brain = new Brain(numInputs, numActions);

            Wheels = _actions[1];
        }

        public struct WheelRotationSpeed
        {
            public double Left { get; }
            public double Right { get; }

            public WheelRotationSpeed(double left, double right)
            {
                Left = left;
                Right = right;
            }
        }
    }
}
