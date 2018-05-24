using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
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
            int start = source.IndexOf("<style>") + 7;
            int end = source.LastIndexOf("</style>");
            if (start != -1 && end != -1)
            {
                string style = source.Substring(start, end - start);
                style = Regex.Replace(style, "\\s+", string.Empty);
                Regex styleGroupReg = new Regex(@"(\.[\w,\.]+){([\w#:;-]+)}");
                var matchces = styleGroupReg.Matches(style);
                foreach (Match match in matchces)
                {
                    string[] classes = match.Groups[1].Value.Split(',');
                    string value = match.Groups[2].Value.TrimEnd(';').Replace(':', '=');
                    int eqStart = value.IndexOf('=');
                    value = value.Insert(eqStart + 1, "\"");
                    value += "\"";
                    foreach (var cls in classes)
                    {
                        source = source.Replace($"class=\"{cls.TrimStart('.')}\"", value + $" class=\"{cls.TrimStart('.')}\"");
                    }
                }
                source = Regex.Replace(source, "\\sclass=\"[\\w-]+\"", string.Empty);
                start = source.IndexOf("<style>");
                end = source.LastIndexOf("</style>") + 8;
                return source.Remove(start, end - start);
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

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dataPackage = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy
            };
            Output.Document.GetText(TextGetOptions.None, out string text);
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeChoices.Add("", new List<string> { ".svg" });
            var file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                Output.Document.GetText(TextGetOptions.None, out string text);
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, text);
            }
        }
    }
}
