using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HappyDog.WindowsUI.Converters
{
    class ThemeIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var theme = (ElementTheme)value;
            return theme.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //if ((bool)value)
            //{
            return Enum.Parse<ElementTheme>(parameter.ToString());
            //}
        }
    }
}
