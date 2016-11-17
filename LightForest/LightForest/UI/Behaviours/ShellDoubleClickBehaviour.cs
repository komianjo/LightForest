using System.Windows;
using System.Windows.Input;

namespace LightForest.UI.Behaviours
{
   public static class ShellDoubleClickBehaviour
   {
      #region Properties

      public static readonly DependencyProperty ExecuteCommandProperty = DependencyProperty.RegisterAttached("ExecuteCommand",
            typeof(ICommand), typeof(ShellDoubleClickBehaviour),
            new UIPropertyMetadata(null, ShellDoubleClickBehaviour.OnExecuteCommandChanged));

      public static readonly DependencyProperty ExecuteCommandParameterProperty = DependencyProperty.RegisterAttached("ExecuteCommandParameter",
            typeof(Window), typeof(ShellDoubleClickBehaviour));

      #endregion

      #region Methods

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static ICommand GetExecuteCommand(DependencyObject obj)
      {
         return (ICommand)obj.GetValue(ShellDoubleClickBehaviour.ExecuteCommandProperty);
      }

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="command"></param>
      public static void SetExecuteCommand(DependencyObject obj, ICommand command)
      {
         obj.SetValue(ShellDoubleClickBehaviour.ExecuteCommandProperty, command);
      }

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static Window GetExecuteCommandParameter(DependencyObject obj)
      {
         return (Window)obj.GetValue(ShellDoubleClickBehaviour.ExecuteCommandParameterProperty);
      }

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="command"></param>
      public static void SetExecuteCommandParameter(DependencyObject obj, Window command)
      {
         obj.SetValue(ShellDoubleClickBehaviour.ExecuteCommandParameterProperty, command);
      }

      private static void OnExecuteCommandChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         var element = sender as UIElement;
         if (element == null)
         {
            return;
         }

         element.MouseLeftButtonDown += ShellDoubleClickBehaviour.MouseDoubleClick;
      }

      private static void MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         var element = sender as UIElement;
         if (element == null)
         {
            return;
         }

         if (e.ClickCount < 2)
         {
            return;
         }

         var command = element.GetValue(ShellDoubleClickBehaviour.ExecuteCommandProperty) as ICommand;
         if (command == null || !command.CanExecute(e))
         {
            return;
         }

         var parameter = element.GetValue(ShellDoubleClickBehaviour.ExecuteCommandParameterProperty);
         command.Execute(parameter);
      }

      #endregion
   }
}
