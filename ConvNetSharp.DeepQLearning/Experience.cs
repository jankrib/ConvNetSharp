namespace ConvNetSharp.DeepQLearning
{
    public class Experience
    {
        public double[] State0 { get; set; }
        public int Action0 { get; set; }
        public double Reward0 { get; set; }
        public double[] State1 { get; set; }
    }
}