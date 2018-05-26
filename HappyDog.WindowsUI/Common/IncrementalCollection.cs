using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace HappyDog.WindowsUI.Common
{
    public class IncrementalCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        public IncrementalCollection(Func<Task<IEnumerable<T>>> loader)
        {
            this.loader = loader;
            HasMoreItems = true;
        }

        readonly Func<Task<IEnumerable<T>>> loader;
        public bool HasMoreItems { get; set; }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async c =>
            {
                var data = await loader();
                foreach (var item in data)
                {
                    Add(item);
                }
                return new LoadMoreItemsResult
                {
                    Count = (uint)data.Count()
                };
            });
        }
    }
}
