using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GestionBornesCollecte.Helpers;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.ViewModels
{
    public class EboueurViewModel : BaseViewModel
    {
        private readonly BorneService _service = new BorneService();

        public ObservableCollection<Borne> Bornes { get; } = new ObservableCollection<Borne>();

        private Borne? _selectedBorne;
        public Borne? SelectedBorne
        {
            get => _selectedBorne;
            set
            {
                _selectedBorne = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AlerteVisible));
            }
        }

        public Visibility AlerteVisible =>
            _selectedBorne != null && _selectedBorne.NiveauRemplissage >= 85
            ? Visibility.Visible
            : Visibility.Collapsed;

        public ICommand Top10Command { get; }
        public ICommand ToutesCommand { get; }
        public ICommand MarquerVideeCommand { get; }

        public EboueurViewModel()
        {
            Top10Command = new RelayCommand(_ => AppliquerTop10());
            ToutesCommand = new RelayCommand(_ => Recharger());
            MarquerVideeCommand = new RelayCommand(_ => MarquerVidee());

            Recharger();
        }

        private void Recharger()
        {
            var liste = _service.GetBornesAsync().Result;
            Bornes.Clear();
            foreach (var b in liste)
                Bornes.Add(b);
            SelectedBorne = Bornes.FirstOrDefault();
        }

        private void AppliquerTop10()
        {
            var triees = Bornes
                .OrderByDescending(b => b.NiveauRemplissage)
                .ToList();
            Bornes.Clear();
            foreach (var b in triees)
                Bornes.Add(b);
            SelectedBorne = Bornes.FirstOrDefault();
        }

        private void MarquerVidee()
        {
            if (SelectedBorne == null) return;
            int idBorne = SelectedBorne.IdBorne;
            SelectedBorne.NiveauRemplissage = 0;
            var copie = Bornes.ToList();
            Bornes.Clear();
            foreach (var b in copie)
                Bornes.Add(b);
            SelectedBorne = Bornes.FirstOrDefault(b => b.IdBorne == idBorne);
            OnPropertyChanged(nameof(AlerteVisible));
        }
    }
}