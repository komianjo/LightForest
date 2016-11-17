using System.Windows;
using System.Windows.Media.Imaging;

namespace LightForest.ViewModels
{
   public interface ISketchViewModel
   {
      #region Properties

      WriteableBitmap Sketch { get; set; }

      double Width { get; set; }

      double Height { get; set; }

      #endregion

      #region Methods

      void RegisterEvents(Window shellView);

      #endregion
   }
}
