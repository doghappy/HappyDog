using HappyDog.WindowsUI.Common;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace HappyDog.WindowsUI.ViewModels.User
{
    public class SignInViewModel : ViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public void Initialize()
        {
            var vault = new PasswordVault();
            if (vault.RetrieveAll().Any(v => v.Resource == DogHappy))
            {
                var credential = vault.FindAllByResource(DogHappy).FirstOrDefault();
                credential.RetrievePassword();
                UserName = credential.UserName;
                Password = credential.Password;
            }
        }

        public async Task SignInAsync(bool isAuto)
        {
            string url = BaseAddress + "/user/signIn";
            var dto = new
            {
                UserName,
                Password,
                RememberMe = true
            };
            string json = JsonConvert.SerializeObject(dto);
            var resMsg = await HttpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, ApplicationJson));
            if (resMsg.IsSuccessStatusCode)
            {
                var vault = new PasswordVault();
                if (vault.RetrieveAll().Any(v => v.Resource == DogHappy))
                {
                    var credentialList = vault.FindAllByResource(DogHappy);
                    foreach (var item in credentialList)
                    {
                        vault.Remove(item);
                    }
                }
                vault.Add(new PasswordCredential
                {
                    Resource = DogHappy,
                    UserName = UserName,
                    Password = Password
                });
                Configuration.IsAuthorized = true;
                Configuration.ClearCache();
                if (!isAuto)
                {
                    GoBack();
                }
            }
            else
            {
                await HandleErrorStatusCodeAsync(resMsg);
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
