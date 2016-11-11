using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LightForest.Models.Tools
{
   /// <summary>
   /// DayDream, given a Camera, paints a Cloud onto a WriteableBitmap.
   /// </summary>
   public static class DayDream
   {
      public static WriteableBitmap CreateBlank(int width, int height, byte red, byte green, byte blue)
      {
         var blank = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
         DayDream.Clear(ref blank, red, green, blue);

         return blank;
      }

      /// <summary>
      /// Cloud regions are painted by turning off their transparency.
      /// </summary>
      /// <param name="image"></param>
      /// <param name="cloud"></param>
      /// <param name="camera"></param>
      public static void Paint(ref WriteableBitmap image, Cloud cloud, Camera camera)
      {
         var width = image.PixelWidth;
         var height = image.PixelHeight;

         // Find the cloud points in 2D
         var cloudPoints = cloud.Points.ToArray();
         var points = DayDream.Project(ref cloudPoints, camera, (double)width, (double)height);

         // Paint the 2D points onto our image
         if (!image.TryLock(new Duration(new TimeSpan(0, 0, 0, 1))))
         {
            return;
         }

         var buffer = image.BackBuffer;
         var stride = image.BackBufferStride;

         unsafe
         {
            var data = (byte*)buffer.ToPointer();
            for (var index = points.Length - 2; index >= 0; index -= 2)
            {
               var location = points[index + Math.Y] * stride + points[index + Math.X] * 4 + 3;
               data[location] = 0xFF;
            }
         }

         image.AddDirtyRect(new Int32Rect(0, 0, width, height));
         image.Unlock();
      }

      /// <summary>
      /// Fills the image with the given color, then makes it transparent.
      /// </summary>
      /// <param name="image"></param>
      /// <param name="red"></param>
      /// <param name="green"></param>
      /// <param name="blue"></param>
      public static void Clear(ref WriteableBitmap image, byte red, byte green, byte blue)
      {
         var length = image.PixelWidth * image.PixelHeight * 4;
         var data = new byte[length];
         for (var index = length - 4; index >= 0; index -= 4)
         {
            data[index] = blue;
            data[index + 1] = green;
            data[index + 2] = red;
            data[index + 3] = 0x00;
         }

         image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), data, image.PixelWidth * 4, 0);
      }

      private static int[] Project(ref double[] p, Camera camera, double screenWidth, double screenHeight)
      {
         var widthOffset = screenWidth / 2.0;
         var heightOffset = screenHeight / 2.0;
         var scale = screenWidth / camera.Width;
         screenWidth -= 1.0;
         screenHeight -= 1.0;

         var points = new List<int>();
         for (var index = p.Length - 3; index >= 0; index -= 3)
         {
            Math.Minus(ref p, camera.O, index);

            var n = Math.Dot(p, camera.N, index);
            if (n <= 0)
            {
               continue;
            }

            var x = Math.Dot(p, camera.V, index) * scale * camera.F / n + widthOffset;
            if (x < 0 || x > screenWidth)
            {
               continue;
            }

            var y = Math.Dot(p, camera.U, index) * scale * camera.F / n + heightOffset;
            if (y < 0 || y > screenHeight)
            {
               continue;
            }

            points.Add((int)x);
            points.Add((int)y);
         }

         return points.ToArray();
      }
   }
}