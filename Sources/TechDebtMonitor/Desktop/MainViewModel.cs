using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

using BL;

using Core;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace Desktop
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private string _repository;
        private string _tag;

        public MainViewModel()
        {
            TechDebts = new ObservableCollection<TechDebt>();
            Tag        = ConfigurationManager.AppSettings["tag"] ?? String.Empty;
            Repository = ConfigurationManager.AppSettings["repository"] ?? String.Empty;

            RefreshCommand       = new DelegateCommand(ReprocessRepository, () => true);
            SetTagCommand        = new DelegateCommand(SetTag, () => true);
            SetRepositoryCommand = new DelegateCommand(SetRepository, () => true);
        }

        public DelegateCommand RefreshCommand       { get; }
        public DelegateCommand SetTagCommand        { get; }
        public DelegateCommand SetRepositoryCommand { get; }

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged();
                ReprocessRepository();
            }
        }

        public string Repository
        {
            get => _repository;
            set
            {
                _repository = value;
                OnPropertyChanged();
                ReprocessRepository();
            }
        }

        public ObservableCollection<TechDebt> TechDebts { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetTag()
        {
            var tagManagerWindow = new TagManagerWindow(Tag);
            if (tagManagerWindow.ShowDialog() != true)
            {
                return;
            }

            Tag = tagManagerWindow.ViewModel.Tag;
        }

        private void SetRepository()
        {
            var dialog = new CommonOpenFileDialog
                         {
                             IsFolderPicker = true,
                             Multiselect    = false
                         };
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            Repository = dialog.FileNames.FirstOrDefault() ?? String.Empty;
        }

        private void ReprocessRepository()
        {
            TechDebts.Clear();
            if (!String.IsNullOrEmpty(Repository) && !String.IsNullOrEmpty(Tag))
            {
                var techDebts = new RepositoryProcessor(Repository, Tag).GetTechDebts();
                foreach (var techDebt in techDebts)
                {
                    TechDebts.Add(techDebt);
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
