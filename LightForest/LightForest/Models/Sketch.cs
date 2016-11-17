using System.Collections.Generic;
using System.Linq;
using LightForest.Models.Tools;

namespace LightForest.Models
{
   public class Sketch : ISketch
   {
      #region Fields

      private readonly List<ISketchObserver> observers = new List<ISketchObserver>();

      #endregion

      #region Constructors

      public Sketch()
      {
         this.Cloud = new Cloud("../../Resources/bunny.obj");
         this.InitializeCamera();
      }

      #endregion

      #region Properties

      public Cloud Cloud { get; set; }

      public Camera Camera { get; set; }

      #endregion

      #region Methods

      public void AddObserver(ISketchObserver observer)
      {
         if (this.observers.Contains(observer))
         {
            return;
         }

         this.observers.Add(observer);
      }

      public void RemoveObserver(ISketchObserver observer)
      {
         this.observers.RemoveAll(o => o.Equals(observer));
      }

      public void Zoom(double distance)
      {
         if (this.Cloud == null || this.Camera == null)
         {
            return;
         }

         var zoom = distance * this.Cloud.Radius;
         this.Camera.Zoom(zoom);
         this.NotifySketchChanged();
      }

      private void InitializeCamera()
      {
         if (this.Cloud == null)
         {
            return;
         }

         var focalLength = 1.0;
         var dia = 1.25 * this.Cloud.Radius + focalLength;
         var center = this.Cloud.Center.ToArray();
         center[Math.Y] += 0.125 * this.Cloud.Radius;

         var up = new double[] { 0.0, -1.0, 0.0 };
         var location = new double[] { 0.0, -0.50 * dia, -dia };
         Math.Plus(ref location, center);

         this.Camera = new Camera(location, up, lookAt: center, width: dia, height: dia, focalLength: dia);
      }

      private void NotifySketchChanged()
      {
         foreach (var observer in this.observers)
         {
            observer.OnSketchChanged(this);
         }
      }

      #endregion
   }
}
