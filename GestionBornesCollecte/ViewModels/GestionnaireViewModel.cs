using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GestionBornesCollecte.Helpers;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.ViewModels
{
    public class GestionnaireViewModel : BaseViewModel
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
                if (_selectedBorne != null)
                    RemplirFormulaire(_selectedBorne);
            }
        }

        private string _nomFormulaire = "";
        public string NomFormulaire
        {
            get => _nomFormulaire;
            set { _nomFormulaire = value; OnPropertyChanged(); }
        }

        private string _adresseFormulaire = "";
        public string AdresseFormulaire
        {
            get => _adresseFormulaire;
            set { _adresseFormulaire = value; OnPropertyChanged(); }
        }

        private string _niveauFormulaire = "0";
        public string NiveauFormulaire
        {
            get => _niveauFormulaire;
            set { _niveauFormulaire = value; OnPropertyChanged(); }
        }

        private string _message = "";
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand AjouterCommand { get; }
        public ICommand ModifierCommand { get; }
        public ICommand SupprimerCommand { get; }
        public ICommand ViderCommand { get; }

        public GestionnaireViewModel()
        {
            AjouterCommand = new RelayCommand(_ => AjouterBorne());
            ModifierCommand = new RelayCommand(_ => ModifierBorne());
            SupprimerCommand = new RelayCommand(_ => SupprimerBorne());
            ViderCommand = new RelayCommand(_ => ViderFormulaire());

            Recharger();
        }

        private void Recharger()
        {
            var liste = _service.GetBornesAsync().Result;
            Bornes.Clear();
            foreach (var b in liste)
                Bornes.Add(b);
        }

        private void RemplirFormulaire(Borne b)
        {
            NomFormulaire = b.Nom;
            AdresseFormulaire = b.Adresse;
            NiveauFormulaire = b.NiveauRemplissage.ToString();
        }

        private void ViderFormulaire()
        {
            NomFormulaire = "";
            AdresseFormulaire = "";
            NiveauFormulaire = "0";
            SelectedBorne = null;
            Message = "";
        }

        private void AjouterBorne()
        {
            if (string.IsNullOrWhiteSpace(NomFormulaire))
            {
                Message = "⚠ Le nom est obligatoire.";
                return;
            }

            int niveau = int.TryParse(NiveauFormulaire, out int n) ? n : 0;

            var nouvelle = new Borne
            {
                IdBorne = Bornes.Count > 0 ? Bornes.Max(b => b.IdBorne) + 1 : 1,
                Nom = NomFormulaire,
                Adresse = AdresseFormulaire,
                NiveauRemplissage = niveau
            };

            Bornes.Add(nouvelle);
            Message = "✔ Borne \"" + nouvelle.Nom + "\" ajoutée.";
            ViderFormulaire();
        }

        private void ModifierBorne()
        {
            if (SelectedBorne == null)
            {
                Message = "⚠ Sélectionne une borne dans la liste.";
                return;
            }

            int niveau = int.TryParse(NiveauFormulaire, out int n) ? n : 0;
            int id = SelectedBorne.IdBorne;

            SelectedBorne.Nom = NomFormulaire;
            SelectedBorne.Adresse = AdresseFormulaire;
            SelectedBorne.NiveauRemplissage = niveau;

            var index = Bornes.IndexOf(SelectedBorne);
            var copie = SelectedBorne;
            Bornes.RemoveAt(index);
            Bornes.Insert(index, copie);

            Message = "✔ Borne modifiée.";
            SelectedBorne = Bornes.FirstOrDefault(b => b.IdBorne == id);
        }

        private void SupprimerBorne()
        {
            if (SelectedBorne == null)
            {
                Message = "⚠ Sélectionne une borne dans la liste.";
                return;
            } 

            string nom = SelectedBorne.Nom;
            Bornes.Remove(SelectedBorne);
            Message = "✔ Borne \"" + nom + "\" supprimée.";
            ViderFormulaire();
        }
    }
}