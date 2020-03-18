using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proj12.View.Controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public delegate void EmptyJoystickEventHandler(Joystick sender);
        public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);
        public event EmptyJoystickEventHandler Released;

        public event EmptyJoystickEventHandler Captured;
        private Point StartingPosition;
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;

        public Joystick()
        {
            InitializeComponent();


            Knob.MouseMove += KnobMoved;
            Knob.MouseLeftButtonUp += KnobLeft;
            Knob.MouseLeftButtonDown += KnobPressed;

            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }


        private void KnobPressed(object sender, MouseButtonEventArgs e)
        {
            StartingPosition = e.GetPosition(Base);
            canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            Captured?.Invoke(this);
            Knob.CaptureMouse();
            centerKnob.Stop();
        }


        private void KnobLeft(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
        }


        private void KnobMoved(object sender, MouseEventArgs e)
        {

            if (Knob.IsMouseCaptured == false)
            {
                return;
            }

            Point newPosition = e.GetPosition(Base);

            Point toMove = new Point(newPosition.X - StartingPosition.X, newPosition.Y - StartingPosition.Y);

            double distance = Math.Round(Math.Sqrt(toMove.X * toMove.X + toMove.Y * toMove.Y));
            if (distance >= (canvasHeight / 2) || distance >= (canvasWidth / 2))
            {
                return;
            }

            knobPosition.Y = toMove.Y;
            knobPosition.X = toMove.X;


        }



        private void centerKnob_Completed(object sender, EventArgs e)
        {

            Released?.Invoke(this);
        }
    }

    public class VirtualJoystickEventArgs
    {

        public double Aileron { get; set; }
        public double Elevator { get; set; }

    }
}