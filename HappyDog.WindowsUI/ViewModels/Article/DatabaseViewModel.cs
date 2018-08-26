using System.ComponentModel;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class DatabaseViewModel : ArticleViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override string Url => "article/db";

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
