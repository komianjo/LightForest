using System;
using System.Collections.Generic;
using System.Linq;
using Math = LightForest.Models.Tools.Math;

namespace LightForest.Models
{
   public class Cloud
   {
      #region Fields

      private const string VertexKey = "v";
      private const int Command = 0;
      private const int X = 1;
      private const int Y = 2;
      private const int Z = 3;

      #endregion

      #region Constructors

      /// <summary>
      /// Opens an *.obj file and loads all of its vertices.
      /// </summary>
      /// <param name="path">Path to an *.obj file</param>
      public Cloud(string path)
      {
         this.Center = new double[] { 0.0, 0.0, 0.0 };
         this.Radius = 0.0;
         var pointList = new List<double>();
         var delimeters = new string[] { " ", "," };
         foreach (var line in System.IO.File.ReadLines(path))
         {
            var description = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            if (description.Length < 4 || description[Cloud.Command] != Cloud.VertexKey)
            {
               continue;
            }

            double x, y, z;
            if (!Double.TryParse(description[Cloud.X], out x) ||
                !Double.TryParse(description[Cloud.Y], out y) ||
                !Double.TryParse(description[Cloud.Z], out z))
            {
               continue;
            }

            pointList.Add(x);
            pointList.Add(y);
            pointList.Add(z);

            // Update Center
            this.Center[Math.X] += x;
            this.Center[Math.Y] += y;
            this.Center[Math.Z] += z;
         }

         this.Points = pointList.ToArray();

         // Update Center and Radius
         this.Center[Math.X] /= this.Points.Length;
         this.Center[Math.Y] /= this.Points.Length;
         this.Center[Math.Z] /= this.Points.Length;

         var points = this.Points.ToArray();
         for (var index = points.Length - 3; index >= 0; index -= 3)
         {
            Math.Minus(ref points, this.Center, index);
            var lengthSquared = Math.Dot(points, points, index, index);
            this.Radius = Math.Max(this.Radius, lengthSquared);
         }

         this.Radius = Math.Sqrt(this.Radius);
      }

      #endregion

      public double[] Points { get; private set; }

      /// <summary>
      /// Bounding sphere center
      /// </summary>
      public double[] Center { get; private set; }

      /// <summary>
      /// Bounding sphere radius
      /// </summary>
      public double Radius { get; private set; }
   }
}
