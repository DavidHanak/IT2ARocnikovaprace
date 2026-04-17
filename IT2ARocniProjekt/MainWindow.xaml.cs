using System.Windows;
using System.Windows.Input;

namespace IT2ARocniProjekt
{
    public partial class MainWindow : Window
    {
        private bool wokenUp = false;
        private bool hasPistol = false;

        public MainWindow()
        {
            InitializeComponent();
            ShowMessage("Probudil ses v posteli po divoké noci v saloonu, šerife.\nNajdi svou zbran.");
        }

        private void ShowMessage(string text)
        {
            message.Text = text;
        }

        private void Bed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!wokenUp)
            {
                wokenUp = true;
                bed.Opacity = 0.4;
                ShowMessage("Dobré ráno, šerife! \nTed najdi svou zbran...");
            }
        }

        private void Pistol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hasPistol)
            {
                hasPistol = true;
                invPistol.Text = "🔫";
                pistol.Opacity = 0.3;
                pistol.IsHitTestVisible = false;
                ShowMessage("Našel jsi svou starou věrnou pistoli!\n\n");
            }
        }
    }
}