using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeepRL.Model;

namespace DeepRL
{
    /// <summary>
    /// Interaction logic for WorldView.xaml
    /// </summary>
    public partial class WorldView : UserControl
    {
        World _world = new World(700, 500);

        
        public static readonly DependencyProperty WallsProperty = DependencyProperty.Register(
            "Walls", typeof (List<Wall>), typeof (WorldView), new PropertyMetadata(default(List<Wall>)));

        public List<Wall> Walls
        {
            get { return (List<Wall>) GetValue(WallsProperty); }
            set { SetValue(WallsProperty, value); }
        }

        public static readonly DependencyProperty PoisonsProperty = DependencyProperty.Register(
            "Poisons", typeof (List<Poison>), typeof (WorldView), new PropertyMetadata(default(List<Poison>)));

        public List<Poison> Poisons
        {
            get { return (List<Poison>) GetValue(PoisonsProperty); }
            set { SetValue(PoisonsProperty, value); }
        }

        public static readonly DependencyProperty FruitsProperty = DependencyProperty.Register(
            "Fruits", typeof (List<Fruit>), typeof (WorldView), new PropertyMetadata(default(List<Fruit>)));

        public List<Fruit> Fruits
        {
            get { return (List<Fruit>) GetValue(FruitsProperty); }
            set { SetValue(FruitsProperty, value); }
        }

        public static readonly DependencyProperty AgentsProperty = DependencyProperty.Register(
            "Agents", typeof (List<Agent>), typeof (WorldView), new PropertyMetadata(default(List<Agent>)));

        public List<Agent> Agents
        {
            get { return (List<Agent>) GetValue(AgentsProperty); }
            set { SetValue(AgentsProperty, value); }
        }

        public WorldView()
        {
            InitializeComponent();

            RefreshUI();

            _world.AfterTick += WorldOnAfterTick;

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _world.Start();
                AppDomain.CurrentDomain.ProcessExit += (sender, args) => _world.Stop();
            }
        }

        private void WorldOnAfterTick(object sender, EventArgs eventArgs)
        {
            Dispatcher.Invoke(RefreshUI);
        }

        private void RefreshUI()
        {

            Fruits = new List<Fruit>(_world.Fruits);
            Poisons = new List<Poison>(_world.Poisons);
            Agents = new List<Agent>(_world.Agents);
            Walls = new List<Wall>(_world.Walls);

        }
    }
}
