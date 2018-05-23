using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.Tools.Views
{
    public sealed partial class SvgForWindows10Page : Page
    {
        public SvgForWindows10Page()
        {
            InitializeComponent();
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".svg");
            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string content = await FileIO.ReadTextAsync(file);
                string result = await SvgParse(content);
                Output.Document.SetText(TextSetOptions.None, result);
            }
        }

        private async Task<string> SvgParse(string source)
        {
            int start = source.IndexOf("<style>");
            int end = source.LastIndexOf("</style>");
            string style = source.Substring(start + 7, end);
            if (start != -1 && end != -1)
            {
                var builder = new StringBuilder();
                Regex styleGroupReg = new Regex(@"(\.[\w,\.]+){([\w:;-]+)}");
                var matchces = styleGroupReg.Matches(style);
                foreach (Match match in matchces)
                {
                    string[] classes = match.Groups[1].Value.Split(',');
                    string value = match.Groups[1].Value.TrimEnd(';').Replace(':', '=');
                    foreach (var cls in classes)
                    {
                        source.Replace($"class=\"{cls.TrimStart('.')}\"", value);
                    }
                }
                return source;
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "错误",
                    Content = "<style>信息错误，无法转换成行内样式",
                    PrimaryButtonText = "确定"
                };
                await dialog.ShowAsync();
            }
            return string.Empty;
        }
    }
}
