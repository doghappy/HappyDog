using System.ComponentModel;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class WindowsViewModel : ArticleViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override string Url => "article/windows";

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
