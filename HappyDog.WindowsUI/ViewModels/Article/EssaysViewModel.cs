using System.ComponentModel;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class EssaysViewModel : ArticleViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override string Url => "article/essays";

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
