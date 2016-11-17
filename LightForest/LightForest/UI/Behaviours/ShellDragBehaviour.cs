using System.Windows;
using System.Windows.Input;

namespace LightForest.UI.Behaviours
{
   public static class ShellDragBehaviour
   {
      #region Properties

      public static readonly DependencyProperty LeftMouseButtonDragProperty = DependencyProperty.RegisterAttached("LeftMouseButtonDrag",
            typeof(Window), typeof(ShellDragBehaviour),
            new UIPropertyMetadata(null, ShellDragBehaviour.OnLeftMouseButtonDragChanged));

      #endregion

      #region Methods

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static Window GetLeftMouseButtonDrag(DependencyObject obj)
      {
         return (Window)obj.GetValue(ShellDragBehaviour.LeftMouseButtonDragProperty);
      }

      /// <summary>
      /// XAML binding uses this.
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="shell"></param>
      public static void SetLeftMouseButtonDrag(DependencyObject obj, Window shell)
      {
         obj.SetValue(ShellDragBehaviour.LeftMouseButtonDragProperty, shell);
      }

      private static void OnLeftMouseButtonDragChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         var element = sender as UIElement;
         if (element == null)
         {
            return;
         }

         element.MouseLeftButtonDown += ShellDragBehaviour.MouseLeftButtonDown;
      }

      private static void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         var element = sender as UIElement;
         if (element == null)
         {
            return;
         }

         var shell = element.GetValue(ShellDragBehaviour.LeftMouseButtonDragProperty) as Window;
         if (shell == null)
         {
            return;
         }

         shell.DragMove();
      }

      #endregion
   }
}
