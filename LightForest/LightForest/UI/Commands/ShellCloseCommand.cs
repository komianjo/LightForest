using System;
using System.Windows;
using System.Windows.Input;

namespace LightForest.UI.Commands
{
   public class ShellCloseCommand : ICommand
   {
      #region Events

      public event EventHandler CanExecuteChanged;

      #endregion

      #region Properties

      public static string ToolTip => "Close";

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

         shell.Close();
      }

      #endregion
   }
}
