using System.Windows;
using System.Windows.Media;
using LightForest.Models;
using LightForest.UI.Views;
using LightForest.ViewModels;

namespace LightForest
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      public void OnStartUp(object sender, StartupEventArgs eventArgs)
      {
         var shellForeBrush = this.TryFindResource("ShellForeBrush") as SolidColorBrush;
         var foregroundColor = (shellForeBrush == null) ? Colors.White : shellForeBrush.Color;

         var sketch = new Sketch();
         var sketchViewModel = new SketchViewModel(sketch, foregroundColor);
         var shellView = new ShellView(sketchViewModel);
         shellView.Show();
      }
   }
}
