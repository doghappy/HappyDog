using System.ComponentModel;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class DotNetViewModel : ArticleViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override string Url => "article/net";

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
