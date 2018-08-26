using HappyDog.WindowsUI.Common;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.ViewModels
{
    public class SignInViewModel : ViewModel, INotifyPropertyChanged
    {
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
    }
}
