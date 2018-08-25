using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.Models.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.ViewModels
{
    public class LoginViewModel : ViewModel, INotifyPropertyChanged
    {
        public LoginViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public async Task SignInAsync()
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
                var credentialList = vault.FindAllByResource("doghappy");
                foreach (var item in credentialList)
                {
                    vault.Remove(item);
                }
                vault.Add(new PasswordCredential
                {
                    Resource = "doghappy",
                    UserName = UserName,
                    Password = Password
                });
            }
            else
            {
                await HandleErrorStatusCodeAsync(resMsg);
            }
        }
    }
}
