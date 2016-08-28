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
        private int _actionIndex = 0;
        public WheelRotationSpeed Wheels 
        {
            get
            {
                return _actions[_actionIndex];
            }  
        } 

        public Eye[] Eyes { get; set; }

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
            Eyes = new Eye[9];
            for (var k = 0; k < 9; k++)
            {
                Eyes[k] = new Eye((k - 3) * 0.25);
            }

            var numInputs = 27; // 9 eyes, each sees 3 numbers (wall, green, red thing proximity)
            var numActions = _actions.Length; // possible angles agent can turn
            
            _brain = new Brain(numInputs, numActions)
            {
                Learning = true
            };

            
        }

        public void Forward()
        {
            double[] inputs = new double[27];
            var num_eyes = Eyes.Length;

            for (int i = 0; i < num_eyes; i++)
            {
                var eye = Eyes[i];
                var prox = eye.SensedProximity/eye.MaxRange;

                inputs[i * 3 + 0] = eye.HitType == HitType.Wall ? prox : 1.0;
                inputs[i * 3 + 1] = eye.HitType == HitType.Fruit ? prox : 1.0;
                inputs[i * 3 + 2] = eye.HitType == HitType.Poison ? prox : 1.0;
            }

            int actionIndex = _brain.Forward(inputs);

            _actionIndex = actionIndex;
        }

        public void Backward()
        {
            var proximity_reward = 0.0;
            var num_eyes = Eyes.Length;
            for (var i = 0; i < num_eyes; i++)
            {
                var e = Eyes[i];
                // agents dont like to see walls, especially up close
                proximity_reward += e.HitType == HitType.Wall ? e.SensedProximity / e.MaxRange : 1.0;
            }
            proximity_reward = proximity_reward / num_eyes;
            proximity_reward = Math.Min(1.0, proximity_reward * 2);

            // agents like to go straight forward
            var forward_reward = 0.0;
            if (_actionIndex == 0 && proximity_reward > 0.75)
                forward_reward = 0.1 * proximity_reward;

            // agents like to eat good things
            //var digestion_reward = this.digestion_signal;
            //this.digestion_signal = 0.0;

            var reward = proximity_reward + forward_reward;// + digestion_reward;

            // pass to brain for learning
            _brain.Backward(reward);
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
