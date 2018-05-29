using HappyDog.WindowsUI.Enums;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HappyDog.WindowsUI.Converters
{
    public class StateThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is BaseState state)
            {
                return state == BaseState.Enable ? ElementTheme.Light : ElementTheme.Dark;
            }
            return ElementTheme.Light;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
