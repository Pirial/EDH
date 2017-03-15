
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Efficient_developer_helper.ViewModels
{
    public class Content
    {
        public Content(string name, ContentControl content, ContentControl settings, BitmapImage icon = null)
        {
            Name = name;
            Icon = icon;
            Main = content;
            Settings = settings;
        }

        public string Name { get; private set; }

        public BitmapImage Icon { get; private set; }

        public ContentControl Main { get; private set; }

        public ContentControl Settings { get; private set; }
    }
}
