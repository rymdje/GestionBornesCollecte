using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionBornesCollecte.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnHabitant_Click(object sender, RoutedEventArgs e)
        {
            var window = new HabitantWindow();
            window.Show();
            this.Close();
        }

        private void BtnEboueur_Click(object sender, RoutedEventArgs e)
        {
            var window = new EboueurWindow();
            window.Show();
            this.Close();
        }

        private void BtnGestionnaire_Click(object sender, RoutedEventArgs e)
        {
            var window = new GestionnaireWindow();
            window.Show();
            this.Close();
        }
    }
}
