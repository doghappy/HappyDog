using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.Models.Results;
using HappyDog.WindowsUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        readonly UserService userService;

        public LoginViewModel()
        {
            userService = new UserService();
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

        public async Task<HttpBaseResult> LoginAsync()
        {
            return await userService.LoginAsync(UserName, Password);
        }
    }
}
