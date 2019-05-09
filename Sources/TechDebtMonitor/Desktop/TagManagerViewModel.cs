using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace Desktop
{
    public sealed class TagManagerViewModel : INotifyPropertyChanged
    {
        private string _tag;

        public TagManagerViewModel(string tag)
        {
            Tag       = tag;
            OkCommand = new DelegateCommand<TagManagerWindow>(OkClose, _ => true);
        }

        public DelegateCommand<TagManagerWindow> OkCommand { get; }

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void OkClose(TagManagerWindow window)
        {
            window.DialogResult = true;
            window.Close();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
