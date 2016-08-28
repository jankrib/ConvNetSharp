using System.Collections.Generic;

namespace ConvNetSharp.DeepQLearning
{
    public class Averager
    {
        private readonly int _maxSize;
        private readonly int _minSize;

        List<double> _numbers = new List<double>();
        private double _sum = 0;

        public Averager(int maxSize, int minSize)
        {
            _maxSize = maxSize;
            _minSize = minSize;
        }

        public void Add(double number)
        {
            _numbers.Add(number);
            _sum += number;

            if (_numbers.Count > _maxSize)
            {
                _sum -= _numbers[0];
                _numbers.RemoveAt(0);
            }
        }

        public double GetAverage()
        {
            if (_numbers.Count < _minSize)
                return -1;
            else
                return _sum/_numbers.Count;
        }

        public void Reset()
        {
            _numbers.Clear();
            _sum = 0;
        }
    }
}