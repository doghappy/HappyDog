﻿using HappyDog.WindowsUI.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class DetailViewModel : ViewModel, INotifyPropertyChanged
    {
        public DetailViewModel(int articleId)
        {
            this.articleId = articleId;
        }

        readonly int articleId;

        public event PropertyChangedEventHandler PropertyChanged;

        private Article article;
        public Article Article
        {
            get => article;
            set
            {
                article = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Article)));
            }
        }

        public bool IsLogged { get; private set; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }


        public async Task InitializeAsync()
        {
            IsLoading = true;
            string url = $"{BaseAddress}/article/{articleId}";
            var resMsg = await HttpClient.GetAsync(url);
            if (resMsg.IsSuccessStatusCode)
            {
                string json = await resMsg.Content.ReadAsStringAsync();
                Article = JsonConvert.DeserializeObject<Article>(json);
            }
            else
            {
                await ParseStatusCodeAsync(resMsg);
            }
            IsLoading = false;
        }
    }
}
