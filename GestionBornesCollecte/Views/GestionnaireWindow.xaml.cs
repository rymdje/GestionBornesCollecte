using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestionBornesCollecte.Models;
using GestionBornesCollecte.Services;

namespace GestionBornesCollecte.Views
{
    public partial class GestionnaireWindow : Window
    {
        // Liste de toutes les bornes
        private List<Borne> _bornes = new List<Borne>();

        public GestionnaireWindow()
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

        // Quand on clique sur une borne : remplit le formulaire
        private void lstBornes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
                return;

            txtNom.Text = borne.Nom;
            txtAdresse.Text = borne.Adresse;
            txtNiveau.Text = borne.NiveauRemplissage.ToString();
        }

        // Bouton Ajouter
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                txtMessage.Text = "⚠ Le nom est obligatoire.";
                return;
            }

            int niveau = 0;
            int.TryParse(txtNiveau.Text, out niveau);

            Borne nouvelle = new Borne();
            nouvelle.IdBorne = _bornes.Count > 0 ? _bornes.Max(b => b.IdBorne) + 1 : 1;
            nouvelle.Nom = txtNom.Text;
            nouvelle.Adresse = txtAdresse.Text;
            nouvelle.NiveauRemplissage = niveau;

            _bornes.Add(nouvelle);

            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;

            txtMessage.Text = "✔ Borne \"" + nouvelle.Nom + "\" ajoutée.";
            BtnVider_Click(sender, e);
        }

        // Bouton Modifier
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
            {
                txtMessage.Text = "⚠ Sélectionne une borne dans la liste.";
                return;
            }

            int niveau = 0;
            int.TryParse(txtNiveau.Text, out niveau);

            borne.Nom = txtNom.Text;
            borne.Adresse = txtAdresse.Text;
            borne.NiveauRemplissage = niveau;

            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;

            txtMessage.Text = "✔ Borne modifiée.";
        }

        // Bouton Supprimer
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Borne borne = (Borne)lstBornes.SelectedItem;

            if (borne == null)
            {
                txtMessage.Text = "⚠ Sélectionne une borne dans la liste.";
                return;
            }

            string nom = borne.Nom;
            _bornes.Remove(borne);

            lstBornes.ItemsSource = null;
            lstBornes.ItemsSource = _bornes;

            txtMessage.Text = "✔ Borne \"" + nom + "\" supprimée.";
            BtnVider_Click(sender, e);
        }

        // Bouton Vider le formulaire
        private void BtnVider_Click(object sender, RoutedEventArgs e)
        {
            txtNom.Text = "";
            txtAdresse.Text = "";
            txtNiveau.Text = "0";
            lstBornes.SelectedItem = null;
            txtMessage.Text = "";
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