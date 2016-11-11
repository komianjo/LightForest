using System.Windows;

namespace LightForest
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      public void OnStartUp(object sender, StartupEventArgs eventArgs)
      {
         // TODO: Process any command line arguments here.
         var shell = new ShellView();
         shell.Show();
      }
   }
}
