using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionBornesCollecte.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // MainWindow lance LoginWindow au démarrage
            LoginWindow login = new LoginWindow();
            login.Show();

            // On cache MainWindow, elle sert juste de point de départ
            this.Hide();
        }
    }
}