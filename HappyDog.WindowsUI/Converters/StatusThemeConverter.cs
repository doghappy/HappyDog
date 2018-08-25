using HappyDog.WindowsUI.Enums;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HappyDog.WindowsUI.Converters
{
    public class StatusThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is BaseStatus status)
            {
                if (status == BaseStatus.Enable)
                {
                    return Enum.Parse<ElementTheme>(App.RootTheme.ToString());
                }
                else if (status == BaseStatus.Disable)
                {
                    if (Application.Current.RequestedTheme == ApplicationTheme.Light)
                    {
                        return ElementTheme.Dark;
                    }
                    else if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    {
                        return ElementTheme.Light;
                    }
                }
            }
            return ElementTheme.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
