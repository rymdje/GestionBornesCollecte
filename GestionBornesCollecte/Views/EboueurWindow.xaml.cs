using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.Views
{
    public partial class EboueurWindow : Window
    {
        // Liste de toutes les bornes
        private List<Borne> _bornes = new List<Borne>();

        public EboueurWindow()
        {
            InitializeComponent();
            ChargerBornes();
        }

        // Charge les bornes depuis le service et les affiche
        private void ChargerBornes()
        {
            BorneService service = new BorneService();
            _bornes = service.GetBornesAsync().Result;
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;
        }

        // Quand on clique sur une borne dans la liste
        private void lstBornes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
                return;

            // On affiche les détails à droite
            txtNom.Text = borne.Nom;
            txtAdresse.Text = borne.Adresse;
            txtNiveau.Text = "Remplissage : " + borne.NiveauRemplissage + " %";

            // On affiche l'alerte si la borne est >= 85%
            if (borne.NiveauRemplissage >= 85)
                txtAlerte.Visibility = Visibility.Visible;
            else
                txtAlerte.Visibility = Visibility.Collapsed;
        }

        // Bouton Top 10 : trie par niveau décroissant
        private void BtnTop10_Click(object sender, RoutedEventArgs e)
        {
            List<Borne> top10 = _bornes
                .OrderByDescending(b => b.NiveauRemplissage)
                .Take(10)
                .ToList();

            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = top10;
        }

        // Bouton Toutes : recharge toutes les bornes
        private void BtnToutes_Click(object sender, RoutedEventArgs e)
        {
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;
        }

        // Bouton Marquer comme vidée : remet le niveau à 0
        private void BtnMarquerVidee_Click(object sender, RoutedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
            {
                MessageBox.Show("Sélectionne une borne dans la liste.");
                return;
            }

            borne.NiveauRemplissage = 0;

            // On rafraîchit la liste pour voir la couleur changer
            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;

            txtNiveau.Text = "Remplissage : 0 %";
            txtAlerte.Visibility = Visibility.Collapsed;
        }

        // Bouton Retour : revient au login
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}