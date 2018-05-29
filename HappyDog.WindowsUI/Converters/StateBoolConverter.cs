using HappyDog.WindowsUI.Enums;
using System;
using Windows.UI.Xaml.Data;

namespace HappyDog.WindowsUI.Converters
{
    public class StateBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is BaseState state)
            {
                return state == BaseState.Enable;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return bool.Parse(value.ToString())
                ? BaseState.Enable
                : BaseState.Disable;
        }
    }
}
