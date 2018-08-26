using HappyDog.WindowsUI.Common;
using System.ComponentModel;
using System.Linq;
using Windows.Security.Credentials;

namespace HappyDog.WindowsUI.ViewModels.User
{
    public class ProfileViewModel : ViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel()
        {
            vault = new PasswordVault();
            UserName = vault.FindAllByResource("doghappy").FirstOrDefault().UserName;
        }

        readonly PasswordVault vault;

        public string UserName { get; }

        public void SignOut()
        {
            if (vault.RetrieveAll().Any(v => v.Resource == DogHappy))
            {
                var credentialList = vault.FindAllByResource(DogHappy);
                foreach (var item in credentialList)
                {
                    vault.Remove(item);
                }
            }
            HttpClient.Dispose();
            HttpClient = null;
            Configuration.ClearCache();
            Configuration.IsAuthorized = false;
            GoBack();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
