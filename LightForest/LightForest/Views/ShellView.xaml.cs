using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LightForest.Models;
using LightForest.Models.Tools;
using Math = LightForest.Models.Tools.Math;

namespace LightForest
{
   /// <summary>
   /// Interaction logic for ShellView.xaml
   /// </summary>
   public partial class ShellView : Window
   {
      #region Fields

      public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(
         "View", typeof(WriteableBitmap), typeof(ShellView), new FrameworkPropertyMetadata());

      private Stopwatch stopwatch = new Stopwatch();
      private Dictionary<string, string> timeElapsed = new Dictionary<string, string>();

      #endregion

      #region Constructor

      public ShellView()
      {
         InitializeComponent();
         this.DataContext = this;
      }

      #endregion

      #region Properties

      public WriteableBitmap View
      {
         get { return (WriteableBitmap)this.GetValue(ShellView.ViewProperty); }
         set { this.SetValue(ShellView.ViewProperty, value); }
      }

      private Cloud Cloud { get; set; }

      private Camera Camera { get; set; }

      #endregion

      #region Methods

      private void Start()
      {
         this.stopwatch.Reset();
         this.stopwatch.Start();
      }

      private void Stop(string description)
      {
         this.stopwatch.Stop();
         this.timeElapsed[DateTime.Now.ToShortTimeString() + ": " + description] = this.stopwatch.Elapsed.ToString();
      }

      #endregion

      private void OnContentRendered(object sender, EventArgs e)
      {
         this.Start();
         this.Cloud = new Cloud("../../Resources/bunny.obj");
         this.Stop("Cloud");

         this.ResetCamera();
         this.ResetView();
      }

      private void OnSizeChanged(object sender, SizeChangedEventArgs e)
      {
         this.ResetView();
      }

      private void ResetView()
      {
         this.Start();
         this.View = DayDream.CreateBlank((int)this.ActualWidth, (int)this.ActualHeight, 0xFF, 0xFF, 0xFF);
         this.Stop("DayDream.CreateBlank");

         this.Paint();
      }

      private void ResetCamera()
      {
         if (this.Cloud == null)
         {
            return;
         }

         this.Start();
         var focalLength = 1.0;
         var dia = 1.25 * this.Cloud.Radius + focalLength;
         var center = this.Cloud.Center.ToArray();
         center[Math.Y] += 0.125 * this.Cloud.Radius;

         var up = new double[] { 0.0, -1.0, 0.0 };
         var location = new double[] { 0.0, -0.50 * dia, -dia };
         Math.Plus(ref location, center);

         this.Camera = new Camera(location, up, lookAt: center, width: dia, height: dia, focalLength: dia);
         this.Stop("Camera");
      }

      private void Paint()
      {
         if (this.Cloud == null || this.Camera == null)
         {
            return;
         }

         var view = this.View;

         this.Start();
         DayDream.Paint(ref view, this.Cloud, this.Camera);
         this.Stop("DayDream.Paint");
      }

      private void Clear()
      {
         var view = this.View;
         if (view == null)
         {
            return;
         }

         DayDream.Clear(ref view, 0xFF, 0xFF, 0xFF);
      }

      private void Zoom(double distance)
      {
         if (this.Camera == null)
         {
            return;
         }

         this.Camera.Zoom(distance);
         this.Clear();
         this.Paint();
      }

      private void RotateUp(int degrees)
      {
         if (this.Camera == null)
         {
            return;
         }

         this.Camera.RotateUp(degrees);
         this.Clear();
         this.Paint();
      }

      private void RotateRight(int degrees)
      {
         if (this.Camera == null)
         {
            return;
         }

         this.Camera.RotateRight(degrees);
         this.Clear();
         this.Paint();
      }

      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         if (this.Cloud == null)
         {
            return;
         }

         if (e.Key == Key.Up)
         {
            this.RotateUp(18);
         }
         else if (e.Key == Key.Down)
         {
            this.RotateUp(-18);
         }
         else if (e.Key == Key.Right)
         {
            this.RotateRight(18);
         }
         else if (e.Key == Key.Left)
         {
            this.RotateRight(-18);
         }
      }

      private void OnMouseWheel(object sender, MouseWheelEventArgs e)
      {
         if (e.Delta == 0 || this.Cloud == null)
         {
            return;
         }

         var distance = (double)e.Delta * this.Cloud.Radius / 120.0;
         this.Zoom(distance);
      }
   }
}