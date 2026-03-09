using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GestionBornesCollecte.Helpers;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly BorneService _service = new BorneService();

        // La liste affichée dans l'interface
        public ObservableCollection<Borne> Bornes { get; } = new ObservableCollection<Borne>();

        // La borne sélectionnée dans la liste
        private Borne? _selectedBorne;
        public Borne? SelectedBorne
        {
            get => _selectedBorne;
            set
            {
                _selectedBorne = value;
                OnPropertyChanged();
            }
        }

        // Les boutons
        public ICommand RefreshCommand { get; }
        public ICommand Top10Command { get; }

        public MainViewModel()
        {
            RefreshCommand = new RelayCommand(async _ => await LoadAsync());
            Top10Command = new RelayCommand(_ => AppliquerTop10());

            // Chargement automatique au démarrage
            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            Bornes.Clear();
            var liste = await _service.GetBornesAsync();
            foreach (var b in liste)
                Bornes.Add(b);

            SelectedBorne = Bornes.FirstOrDefault();
        }

        private void AppliquerTop10()
        {
            var top = Bornes
                .OrderByDescending(b => b.NiveauRemplissage)
                .Take(10)
                .ToList();

            Bornes.Clear();
            foreach (var b in top)
                Bornes.Add(b);

            SelectedBorne = Bornes.FirstOrDefault();
        }
    }
}