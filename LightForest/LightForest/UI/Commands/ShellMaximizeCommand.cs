using System;
using System.Windows;
using System.Windows.Input;

namespace LightForest.UI.Commands
{
   public class ShellMaximizeCommand : ICommand
   {
      #region Events

      public event EventHandler CanExecuteChanged;

      #endregion

      #region Properties

      public static string MaximizeToolTip => "Maximize";

      public static string RestoreToolTip => "Restore";

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

         var state = shell.WindowState;
         shell.WindowState = (state == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
      }

      #endregion
   }
}
