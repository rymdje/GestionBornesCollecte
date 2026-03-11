using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GestionBornesCollecte.Helpers;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.ViewModels
{
    public class HabitantViewModel : BaseViewModel
    {
        private readonly BorneService _service = new BorneService();
        private List<Borne> _toutesLesBornes = new List<Borne>();

        public ObservableCollection<Borne> Bornes { get; } = new ObservableCollection<Borne>();
        public ObservableCollection<Borne> Favoris { get; } = new ObservableCollection<Borne>();

        private Borne? _selectedBorne;
        public Borne? SelectedBorne
        {
            get => _selectedBorne;
            set
            {
                _selectedBorne = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AlerteVisible));
                OnPropertyChanged(nameof(TexteBoutonFavori));
            }
        }

        private string _recherche = "";
        public string Recherche
        {
            get => _recherche;
            set
            {
                _recherche = value;
                OnPropertyChanged();
                FiltrerBornes();
            }
        }

        public Visibility AlerteVisible =>
            _selectedBorne != null && _selectedBorne.NiveauRemplissage >= 85
            ? Visibility.Visible
            : Visibility.Collapsed;

        public string TexteBoutonFavori =>
            _selectedBorne != null && Favoris.Any(f => f.IdBorne == _selectedBorne.IdBorne)
            ? "★ Retirer des favoris"
            : "☆ Ajouter aux favoris";

        public ICommand FavoriCommand { get; }
        public ICommand VoirFavorisCommand { get; }
        public ICommand ToutesCommand { get; }

        public HabitantViewModel()
        {
            FavoriCommand = new RelayCommand(_ => ToggleFavori());
            VoirFavorisCommand = new RelayCommand(_ => AfficherFavoris());
            ToutesCommand = new RelayCommand(_ => AfficherToutes());

            Recharger();
        }

        private void Recharger()
        {
            var liste = _service.GetBornesAsync().Result;
            _toutesLesBornes = liste;
            Bornes.Clear();
            foreach (var b in liste)
                Bornes.Add(b);
            SelectedBorne = Bornes.FirstOrDefault();
        }

        private void FiltrerBornes()
        {
            var filtre = _toutesLesBornes
                .Where(b => b.Nom.ToLower().Contains(_recherche.ToLower())
                         || b.Adresse.ToLower().Contains(_recherche.ToLower()))
                .ToList();
            Bornes.Clear();
            foreach (var b in filtre)
                Bornes.Add(b);
        }

        private void ToggleFavori()
        {
            if (SelectedBorne == null) return;
            var existant = Favoris.FirstOrDefault(f => f.IdBorne == SelectedBorne.IdBorne);
            if (existant != null)
                Favoris.Remove(existant);
            else
                Favoris.Add(SelectedBorne);
            OnPropertyChanged(nameof(TexteBoutonFavori));
        }

        private void AfficherFavoris()
        {
            Bornes.Clear();
            foreach (var b in Favoris)
                Bornes.Add(b);
        }

        private void AfficherToutes()
        {
            Bornes.Clear();
            foreach (var b in _toutesLesBornes)
                Bornes.Add(b);
        }
    }
}