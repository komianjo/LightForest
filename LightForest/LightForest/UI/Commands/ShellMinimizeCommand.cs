using System;
using System.Windows;
using System.Windows.Input;

namespace LightForest.UI.Commands
{
   public class ShellMinimizeCommand : ICommand
   {
      #region Events

      public event EventHandler CanExecuteChanged;

      #endregion

      #region Properties

      public static string ToolTip => "Minimize";

      #endregion

      #region Methods

      public bool CanExecute(object sender)
      {
         return true;
      }

      public void Execute(object sender)
      {
         var shell = sender as Window;
         if (shell == null)
         {
            return;
         }

         shell.WindowState = WindowState.Minimized;
      }

      #endregion
   }
}
