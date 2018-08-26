using HappyDog.WindowsUI.Common;
using System.Linq;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.ViewModels.User
{
    public class ProfileViewModel : ViewModel
    {
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
    }
}
