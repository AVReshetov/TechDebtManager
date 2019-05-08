using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

using BL;

using Core;

namespace Desktop
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private string _repository;
        private string _tag;

        public MainViewModel()
        {
            Tag        = ConfigurationManager.AppSettings["tag"] ?? String.Empty;
            Repository = ConfigurationManager.AppSettings["repository"] ?? String.Empty;

            if (!String.IsNullOrEmpty(Repository) && !String.IsNullOrEmpty(Tag))
            {
                var techDebts = new RepositoryProcessor(Repository, Tag).GetTechDebts();
                TechDebts = new ObservableCollection<TechDebt>(techDebts);
            }
            else
            {
                TechDebts = new ObservableCollection<TechDebt>();
            }
        }

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged();
            }
        }

        public string Repository
        {
            get => _repository;
            set
            {
                _repository = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TechDebt> TechDebts { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
