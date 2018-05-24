﻿using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class HomeViewModel : ArticleViewModel
    {
        public async Task InitializeAsync()
        {
            await LoadArticleAsync();
        }
    }
}
