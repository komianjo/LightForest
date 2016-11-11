namespace LightForest.Models.Tools
{
   public static class Math
   {
      public const int X = 0;
      public const int Y = 1;
      public const int Z = 2;

      public static double Dot(double[] u, double[] v, int offset = 0, int offsetV = 0)
      {
         return u[X + offset] * v[X + offsetV] + u[Y + offset] * v[Y + offsetV] + u[Z + offset] * v[Z + offsetV];
      }

      public static void Cross(double[] u, double[] v, ref double[] w)
      {
         w[X] = u[Y] * v[Z] - u[Z] * v[Y];
         w[Y] = u[Z] * v[X] - u[X] * v[Z];
         w[Z] = u[X] * v[Y] - u[Y] * v[X];
      }

      public static void Normalize(ref double[] u)
      {
         var length = Sqrt(Dot(u, u));
         length = length == 0.0 ? 1.0 : length;
         u[X] /= length;
         u[Y] /= length;
         u[Z] /= length;
      }

      public static double Sqrt(double u)
      {
         return System.Math.Sqrt(u);
      }

      public static void Minus(ref double[] u, double[] v, int offset = 0)
      {
         u[X + offset] -= v[X];
         u[Y + offset] -= v[Y];
         u[Z + offset] -= v[Z];
      }

      public static void Plus(ref double[] u, double[] v, int offset = 0)
      {
         u[X + offset] += v[X];
         u[Y + offset] += v[Y];
         u[Z + offset] += v[Z];
      }

      public static double Max(double u, double v)
      {
         return System.Math.Max(u, v);
      }
   }
}
