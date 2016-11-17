using System.Windows;
using LightForest.ViewModels;

namespace LightForest.UI.Views
{
   /// <summary>
   /// Interaction logic for ShellView.xaml
   /// </summary>
   public partial class ShellView : Window
   {
      internal ShellView(ISketchViewModel viewModel)
      {
         InitializeComponent();
         viewModel.RegisterEvents(this);
         this.DataContext = viewModel;
      }
   }
}
