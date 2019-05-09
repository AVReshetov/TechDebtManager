using System.Windows;

namespace Desktop
{
    /// <summary>
    ///     Interaction logic for TagManagerWindow.xaml
    /// </summary>
    public partial class TagManagerWindow : Window
    {
        public TagManagerWindow(string tag)
        {
            ViewModel = new TagManagerViewModel(tag);
            InitializeComponent();
            DataContext = ViewModel; //Todo: implement Locator pattern
        }

        public TagManagerViewModel ViewModel { get; }
    }
}
