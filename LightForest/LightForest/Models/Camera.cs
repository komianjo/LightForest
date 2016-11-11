using System.Linq;
using LightForest.Models.Tools;

namespace LightForest.Models
{
   /// <summary>
   /// TODO: Add code to allow the user to move the camera in 3D space.
   /// </summary>
   public class Camera
   {
      #region Constructors

      public Camera(double[] location, double[] up, double[] lookAt, double width, double height, double focalLength)
      {
         this.Width = width;
         this.Height = height;
         this.F = focalLength;
         this.O = new double[] { location[Math.X], location[Math.Y], location[Math.Z] };
         this.U = new double[] { up[Math.X], up[Math.Y], up[Math.Z] };
         this.N = new double[3];
         this.V = new double[3];
         this.LookAt(lookAt);
      }

      #endregion

      #region Properties

      /// <summary>
      /// Origin point
      /// </summary>
      public double[] O;

      /// <summary>
      /// Up vector
      /// </summary>
      public double[] U;

      /// <summary>
      /// Right vector
      /// </summary>
      public double[] V;

      /// <summary>
      /// Normal vector
      /// </summary>
      public double[] N;

      /// <summary>
      /// Focal length
      /// </summary>
      public double F;

      public double Width;

      public double Height;

      public double[] Attention;

      #endregion

      #region Methods

      public void LookAt(double[] lookAt)
      {
         this.Attention = lookAt.ToArray();
         this.N[Math.X] = lookAt[Math.X];
         this.N[Math.Y] = lookAt[Math.Y];
         this.N[Math.Z] = lookAt[Math.Z];

         // N = lookAt - O
         Math.Minus(ref this.N, this.O);
         Math.Normalize(ref this.N);

         // V = N x U
         Math.Cross(this.N, this.U, ref this.V);
         Math.Normalize(ref this.V);

         // U = V x N
         Math.Cross(this.V, this.N, ref this.U);
         Math.Normalize(ref this.U);
      }

      public void Translate(double[] t)
      {
         Math.Plus(ref this.O, t, 0);
      }

      public void Zoom(double distance)
      {
         var vector = this.N.ToArray();
         vector[Math.X] *= distance;
         vector[Math.Y] *= distance;
         vector[Math.Z] *= distance;
         this.Translate(vector);
      }

      public void RotateUp(int degrees)
      {
      }

      public void RotateRight(int degrees)
      {
      }

      #endregion
   }
}
