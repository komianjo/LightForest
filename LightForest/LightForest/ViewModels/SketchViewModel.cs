using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LightForest.Models;
using LightForest.Models.Tools;

namespace LightForest.ViewModels
{
   public class SketchViewModel : DependencyObject, ISketchViewModel, ISketchObserver
   {
      #region Fields

      public static readonly DependencyProperty SketchProperty = DependencyProperty.Register(
         "Sketch", typeof(WriteableBitmap), typeof(SketchViewModel), new FrameworkPropertyMetadata());

      private byte red;
      private byte green;
      private byte blue;
      private ISketch model;

      #endregion

      #region Constructors

      public SketchViewModel(ISketch model, Color foreground)
      {
         this.red = foreground.R;
         this.green = foreground.G;
         this.blue = foreground.B;

         this.model = model;
         this.model.AddObserver(this);
      }

      #endregion

      #region Properties

      public WriteableBitmap Sketch
      {
         get { return (WriteableBitmap)this.GetValue(SketchViewModel.SketchProperty); }
         set { this.SetValue(SketchViewModel.SketchProperty, value); }
      }

      public double Width { get; set; }

      public double Height { get; set; }

      #endregion

      #region Methods

      public void RegisterEvents(Window shellView)
      {
         shellView.ContentRendered += this.OnContentRendered;
         shellView.SizeChanged += this.OnSizeChanged;
         shellView.KeyDown += this.OnKeyDown;
         shellView.MouseWheel += this.OnMouseWheel;
      }

      public void OnSketchChanged(ISketch sender)
      {
         this.ClearSketch();
         this.Paint();
      }

      private void OnContentRendered(object sender, EventArgs e)
      {
         this.ResetSketch();
      }

      private void OnSizeChanged(object sender, SizeChangedEventArgs e)
      {
         this.Width = e.NewSize.Width;
         this.Height = e.NewSize.Height;
         this.ResetSketch();
      }

      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         // TODO
      }

      private void OnMouseWheel(object sender, MouseWheelEventArgs e)
      {
         if (e.Delta == 0 || this.model == null)
         {
            return;
         }

         var distance = (double)e.Delta / 120.0;
         this.model.Zoom(distance);
      }

      private void ResetSketch()
      {
         this.Sketch = DayDream.CreateBlank((int)this.Width, (int)this.Height, this.red, this.green, this.blue);
         this.Paint();
      }

      private void ClearSketch()
      {
         var sketch = this.Sketch;
         if (sketch == null)
         {
            return;
         }

         DayDream.Clear(ref sketch, this.red, this.green, this.blue);
      }

      private void Paint()
      {
         if (this.model == null || this.model.Cloud == null || this.model.Camera == null)
         {
            return;
         }

         var sketch = this.Sketch;
         DayDream.Paint(ref sketch, this.model.Cloud, this.model.Camera);
      }

      #endregion
   }
}
