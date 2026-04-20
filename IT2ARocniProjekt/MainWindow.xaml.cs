using System.Windows;
using System.Windows.Input;

namespace IT2ARocniProjekt
{
    public partial class MainWindow : Window
    {
        private bool wokenUp = false;
        private bool hasPistol = false;
        private int bulletsFound = 0;        

        public MainWindow()
        {
            InitializeComponent();
            UkaZpravu("Probudil ses v posteli po divoké noci v saloonu, šerife.\nNajdi pistoli a všechny tři nábojky.");
        }

        private void UkaZpravu(string text)
        {
            message.Text = text;
        }

        private void Probuzeni(object sender, MouseButtonEventArgs e)
        {
            if (!wokenUp)
            {
                wokenUp = true;
                bed.Opacity = 0.4;
                UkaZpravu("Dobré ráno, šerife!\nTeď můžeš hledat pistoli a tři nábojky.");

                // Aktivujeme nábojky až po probuzení
                bullet1.Opacity = 1;
                bullet1.IsHitTestVisible = true;

                bullet2.Opacity = 1;
                bullet2.IsHitTestVisible = true;

                bullet3.Opacity = 1;
                bullet3.IsHitTestVisible = true;
            }
        }

        private void PistoTouch(object sender, MouseButtonEventArgs e)
        {
            if (!hasPistol)
            {
                hasPistol = true;
                invSlot1.Text = "🔫";                    // pistole jde do 1. slotu
                pistol.Opacity = 0;
                pistol.IsHitTestVisible = false;
                UkaZpravu("Našel jsi svou starou věrnou pistoli! Natáhl ses pro ni z postele.");
                CheckIfEverythingFound();
            }
        }

        private void Bullet1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bullet1.Opacity == 1)
            {
                bulletsFound++;
                bullet1.Opacity = 0;                    // ← zmizí
                bullet1.IsHitTestVisible = false;
                UpdateBulletsInventory();
                UkaZpravu("Našel jsi první nábojku!");
                CheckIfEverythingFound();
            }
        }

        private void Bullet2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bullet2.Opacity == 1)
            {
                bulletsFound++;
                bullet2.Opacity = 0;                    // ← zmizí
                bullet2.IsHitTestVisible = false;
                UpdateBulletsInventory();
                UkaZpravu("Našel jsi druhou nábojku!");
                CheckIfEverythingFound();
            }
        }

        private void Bullet3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bullet3.Opacity == 1)
            {
                bulletsFound++;
                bullet3.Opacity = 0;                    // ← zmizí
                bullet3.IsHitTestVisible = false;
                UpdateBulletsInventory();
                UkaZpravu("Našel jsi třetí nábojku!");
                CheckIfEverythingFound();
            }
        }

        private void UpdateBulletsInventory()
        {
            // Nábojky se ukládají postupně do slotů 2, 3, 4
            if (bulletsFound >= 1) invSlot2.Text = "🔴";
            if (bulletsFound >= 2) invSlot3.Text = "🔴";
            if (bulletsFound >= 3) invSlot4.Text = "🔴";
        }

        private void CheckIfEverythingFound()
        {
            if (hasPistol && bulletsFound == 3)
            {
                UkaZpravu(" Výborně, šerife!\nMáš pistoli i všechny tři nábojky.\nTeď jsi připravený na divoký západ!");
            }
        }
    }
}