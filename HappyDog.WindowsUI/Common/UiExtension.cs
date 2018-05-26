using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace HappyDog.WindowsUI.Common
{
    public static class UiExtension
    {
        public static T GetParent<T>(this FrameworkElement obj) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (true)
            {
                if (parent is T)
                {
                    return parent as T;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    if (parent == null)
                    {
                        return null;
                    }
                }
            }
        }

        public static T GetParent<T>(this FrameworkElement obj, string name) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (true)
            {
                if (parent is T && parent is FrameworkElement fe && fe.Name == name)
                {
                    return parent as T;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    if (parent == null)
                    {
                        return null;
                    }
                }
            }
        }

        public static List<T> GetChildren<T>(this FrameworkElement obj) where T : FrameworkElement
        {
            var children = new List<T>();
            var queue = new Queue<DependencyObject>();
            int count = VisualTreeHelper.GetChildrenCount(obj);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child is T)
                    {
                        children.Add(child as T);
                    }
                    queue.Enqueue(child);
                }
            }

            while (queue.Any())
            {
                var parent = queue.Dequeue();
                int count2 = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count2; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T)
                    {
                        children.Add(child as T);
                    }
                    queue.Enqueue(child);
                }
            }
            return children;
        }

        public static List<T> GetChildren<T>(this FrameworkElement obj, string name) where T : FrameworkElement
        {
            var children = new List<T>();
            var queue = new Queue<DependencyObject>();
            int count = VisualTreeHelper.GetChildrenCount(obj);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child is T fe && fe.Name == name)
                    {
                        children.Add(child as T);
                    }
                    queue.Enqueue(child);
                }
            }

            while (queue.Any())
            {
                var parent = queue.Dequeue();
                int count2 = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count2; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T fe && fe.Name == name)
                    {
                        children.Add(child as T);
                    }
                    queue.Enqueue(child);
                }
            }
            return children;
        }

        public static T GetChild<T>(this FrameworkElement obj, string name) where T : FrameworkElement
        {
            var children = obj.GetChildren<T>();
            return children.FirstOrDefault(e => e.Name == name);
        }
    }
}
