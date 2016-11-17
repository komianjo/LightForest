namespace LightForest.Models
{
   public interface ISketch
   {
      #region Properties

      Cloud Cloud { get; }

      Camera Camera { get; }

      #endregion

      #region Methods

      void AddObserver(ISketchObserver observer);

      void RemoveObserver(ISketchObserver observer);

      void Zoom(double distance);

      #endregion
   }
}
