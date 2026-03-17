using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.Views
{
    public partial class HabitantWindow : Window
    {
        // Liste de toutes les bornes
        private List<Borne> _bornes = new List<Borne>();

        // Liste des bornes favorites
        private List<Borne> _favoris = new List<Borne>();

        public HabitantWindow()
        {
            InitializeComponent();
            ChargerBornes();
        }

        // Charge les bornes depuis le service
        private void ChargerBornes()
        {
            BorneService service = new BorneService();
            _bornes = service.GetBornesAsync().Result;
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;
        }

        // Quand on clique sur une borne : affiche les détails
        private void lstBornes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
                return;

            txtNom.Text = borne.Nom;
            txtAdresse.Text = borne.Adresse;
            txtNiveau.Text = "Remplissage : " + borne.NiveauRemplissage + " %";

            // Alerte si >= 85%
            if (borne.NiveauRemplissage >= 85)
                borderAlerte.Visibility = Visibility.Visible;
            else
                borderAlerte.Visibility = Visibility.Collapsed;

            // Change le texte du bouton favori selon l'état
            bool estFavori = _favoris.Any(f => f.IdBorne == borne.IdBorne);
            if (estFavori)
                btnFavori.Content = "★ Retirer des favoris";
            else
                btnFavori.Content = "☆ Ajouter aux favoris";
        }

        // Recherche en temps réel
        private void txtRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            string recherche = txtRecherche.Text.ToLower();

            List<Borne> resultats = _bornes
                .Where(b => b.Nom.ToLower().Contains(recherche)
                         || b.Adresse.ToLower().Contains(recherche))
                .ToList();

            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = resultats;
        }

        // Bouton Toutes les bornes
        private void BtnToutes_Click(object sender, RoutedEventArgs e)
        {
            txtRecherche.Text = "";
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;
        }

        // Bouton Mes favoris
        private void BtnFavoris_Click(object sender, RoutedEventArgs e)
        {
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _favoris;
        }

        // Bouton Ajouter/Retirer des favoris
        private void BtnToggleFavori_Click(object sender, RoutedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
                return;

            bool estFavori = _favoris.Any(f => f.IdBorne == borne.IdBorne);

            if (estFavori)
            {
                // On retire des favoris
                Borne aRetirer = _favoris.First(f => f.IdBorne == borne.IdBorne);
                _favoris.Remove(aRetirer);
                btnFavori.Content = "☆ Ajouter aux favoris";
            }
            else
            {
                // On ajoute aux favoris
                _favoris.Add(borne);
                btnFavori.Content = "★ Retirer des favoris";
            }
        }

        // Bouton Retour
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}