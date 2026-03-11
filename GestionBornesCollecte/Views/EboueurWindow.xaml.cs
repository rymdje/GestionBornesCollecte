using System.Windows;

namespace GestionBornesCollecte.Views
{
    public partial class EboueurWindow : Window
    {
        public EboueurWindow()
        {
            InitializeComponent();
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}