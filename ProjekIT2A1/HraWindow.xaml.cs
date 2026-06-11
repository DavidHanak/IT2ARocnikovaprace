using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjektIT2A1
{
    public partial class HraWindow : Window
    {
        private bool wokenUp = false;
        private bool hasPistol = false;
        private int bulletsFound = 0;
        private bool outside = false;
        private bool horseSpooked = false;
        private bool horseDead = false;
        private bool hasBanditWeapon = false;
        private string currentEndType = "";
        private int continueStep = 0;

        private Random random = new Random();

        public HraWindow()
        {
            InitializeComponent();
            ResetGameToInitialState();
        }

        private void ShowMessage(string text)
        {
            message.Text = text;
        }

        private void ResetGameToInitialState()
        {
            wokenUp = false;
            hasPistol = false;
            bulletsFound = 0;
            outside = false;
            horseSpooked = false;
            horseDead = false;
            hasBanditWeapon = false;
            currentEndType = "";
            continueStep = 0;

            ShowMessage("");
            RefreshInventory();
            invSlot1.Text = "⬜";

            bed.Visibility = Visibility.Visible;
            bed.Opacity = 1;
            pistol.Visibility = Visibility.Visible;
            pistol.IsHitTestVisible = true;

            bullet1.Visibility = Visibility.Visible;
            bullet1.Opacity = 0.3;
            bullet1.IsHitTestVisible = false;
            bullet2.Visibility = Visibility.Visible;
            bullet2.Opacity = 0.3;
            bullet2.IsHitTestVisible = false;
            bullet3.Visibility = Visibility.Visible;
            bullet3.Opacity = 0.3;
            bullet3.IsHitTestVisible = false;

            tableTop.Visibility = Visibility.Visible;
            tableLeg1.Visibility = Visibility.Visible;
            tableLeg2.Visibility = Visibility.Visible;
            insideCactus.Visibility = Visibility.Visible;
            hat.Visibility = Visibility.Visible;
            door.Visibility = Visibility.Visible;

            sun.Visibility = Visibility.Hidden;
            horse.Visibility = Visibility.Hidden;
            horse.IsHitTestVisible = true;
            outsideCactus.Visibility = Visibility.Hidden;
            magickeDvere.Visibility = Visibility.Hidden;
            bandit.Visibility = Visibility.Hidden;

            barCounter.Visibility = Visibility.Collapsed;
            whiskeyGlass.Visibility = Visibility.Collapsed;
            sandStormOverlay.Visibility = Visibility.Collapsed;
            btnContinue.Visibility = Visibility.Hidden;

            scene.Background = new SolidColorBrush(Color.FromRgb(92, 64, 51));
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Collapsed;
            ResetGameToInitialState();
            ShowMessage("Probudil ses v posteli po divoké noci v saloonu, šerife.\nNajdi pistoli a všechny tři náboje.");
        }

        private void BtnGameDetails_Click(object sender, RoutedEventArgs e)
        {
            menuButtonsPanel.Visibility = Visibility.Collapsed;
            detailsPanel.Visibility = Visibility.Visible;
        }

        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            detailsPanel.Visibility = Visibility.Collapsed;
            menuButtonsPanel.Visibility = Visibility.Visible;
        }

        private void Bed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!wokenUp)
            {
                wokenUp = true;
                bed.Opacity = 0.4;
                ShowMessage("Dobré ráno, šerife! Teď můžeš hledat náboje.");

                bullet1.Opacity = 1; bullet1.IsHitTestVisible = true;
                bullet2.Opacity = 1; bullet2.IsHitTestVisible = true;
                bullet3.Opacity = 1; bullet3.IsHitTestVisible = true;
            }
        }

        private void Pistol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hasPistol)
            {
                hasPistol = true;
                invSlot1.Text = "🔫";
                pistol.Visibility = Visibility.Hidden;
                pistol.IsHitTestVisible = false;

                if (!wokenUp) ShowMessage("Natáhl ses z postele pro svou starou věrnou pistoli.");
                else ShowMessage("Vzals svou insignii spravedlnosti – pistoli.");

                CheckWin();
            }
        }

        private void Bullet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock bullet = sender as TextBlock;
            if (!wokenUp)
            {
                ShowMessage("Nejdřív se probuď.");
                return;
            }

            if (bullet != null && bullet.Opacity == 1)
            {
                bulletsFound++;
                bullet.Visibility = Visibility.Hidden;
                bullet.IsHitTestVisible = false;
                RefreshInventory();

                if (bulletsFound == 1) ShowMessage("Našel jsi první náboj!");
                if (bulletsFound == 2) ShowMessage("Našel jsi druhý náboj!");
                if (bulletsFound == 3) ShowMessage("Našel jsi třetí náboj!");

                CheckWin();
            }
        }

        private void RefreshInventory()
        {
            if (hasBanditWeapon)
            {
                invSlot2.Text = "🔴"; invSlot3.Text = "🔴"; invSlot4.Text = "🔴"; invSlot5.Text = "🔫⭐";
                return;
            }
            invSlot2.Text = bulletsFound >= 1 ? "🔴" : "⬜";
            invSlot3.Text = bulletsFound >= 2 ? "🔴" : "⬜";
            invSlot4.Text = bulletsFound >= 3 ? "🔴" : "⬜";
            invSlot5.Text = "⬜";
        }

        private void CheckWin()
        {
            if (hasPistol && bulletsFound == 3)
            {
                ShowMessage("🔥 Výborně, šerife! Teď můžeš odejít dveřmi ven.");
            }
        }

        private void Door_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hasPistol || bulletsFound < 3)
            {
                ShowMessage("Nejdřív potřebuješ pistoli a všechny náboje.");
                return;
            }

            if (!outside)
            {
                outside = true;
                scene.Background = new SolidColorBrush(Color.FromRgb(220, 190, 90));

                bed.Visibility = Visibility.Hidden;
                pistol.Visibility = Visibility.Hidden;
                bullet1.Visibility = Visibility.Hidden;
                bullet2.Visibility = Visibility.Hidden;
                bullet3.Visibility = Visibility.Hidden;
                tableTop.Visibility = Visibility.Hidden;
                tableLeg1.Visibility = Visibility.Hidden;
                tableLeg2.Visibility = Visibility.Hidden;
                insideCactus.Visibility = Visibility.Hidden;
                hat.Visibility = Visibility.Hidden;
                door.Visibility = Visibility.Hidden;

                sun.Visibility = Visibility.Visible;
                horse.Visibility = Visibility.Visible;
                outsideCactus.Visibility = Visibility.Visible;
                magickeDvere.Visibility = Visibility.Visible;

                ShowMessage("🌵 Vyšel jsi ven do rozpálené pouště.");
            }
        }

        private void Horse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bandit.Visibility == Visibility.Visible || btnContinue.Visibility == Visibility.Visible) return;

            btnShootHorse.Visibility = Visibility.Visible;
            btnTameHorse.Visibility = Visibility.Visible;
            btnScareHorse.Visibility = Visibility.Visible;

            ShowMessage("🐎 Co chceš s koněm udělat?");
        }

        private void HideHorseButtons()
        {
            btnShootHorse.Visibility = Visibility.Hidden;
            btnTameHorse.Visibility = Visibility.Hidden;
            btnScareHorse.Visibility = Visibility.Hidden;
        }

        private void BtnShootHorse_Click(object sender, RoutedEventArgs e)
        {
            if (bulletsFound < 1) { ShowMessage("Nemáš žádný náboj."); return; }
            bulletsFound--;
            RefreshInventory();

            horse.Visibility = Visibility.Hidden;
            horse.IsHitTestVisible = false;
            horseDead = true;

            ShowMessage("🔫 Zastřelil jsi koně.");
            HideHorseButtons();
            TriggerBanditAttack();
        }

        private void BtnTameHorse_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("🤝 Trpělivě jsi koně ochočil. Vypadá přátelsky.");
            HideHorseButtons();
            TriggerBanditAttack();
        }

        private void BtnScareHorse_Click(object sender, RoutedEventArgs e)
        {
            if (bulletsFound < 2) { ShowMessage("Potřebuješ alespoň dva náboje."); return; }
            bulletsFound -= 2;
            RefreshInventory();

            Canvas.SetLeft(horse, 1500);
            horseSpooked = true;

            ShowMessage("💥 Výstřely vyděsily koně a ten odběhl daleko do pouště.");
            HideHorseButtons();
            TriggerBanditAttack();
        }

        private void TriggerBanditAttack()
        {
            bandit.Visibility = Visibility.Visible;
            ShowMessage("🦹 Náhle se zpoza duny vyřítil obávaný bandita a vytasil na tebe zbraň! 'Ruce vzhůru, šerife!'");

            btnRunAway.Visibility = Visibility.Visible;
            btnRideHorse.Visibility = Visibility.Visible;
            btnShootBandit.Visibility = Visibility.Visible;
            btnBargain.Visibility = Visibility.Visible;
        }

        private void HideBanditButtons()
        {
            btnRunAway.Visibility = Visibility.Hidden;
            btnRideHorse.Visibility = Visibility.Hidden;
            btnShootBandit.Visibility = Visibility.Hidden;
            btnBargain.Visibility = Visibility.Hidden;
            btnBargainEmotions.Visibility = Visibility.Hidden;
            btnBargainAggressive.Visibility = Visibility.Hidden;
        }

        private void BtnRunAway_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("🏃 Pokusil ses utéct po svých. Bandita se ti hlasitě vysmál: 'Haha, ty jsi ale zbabělý šerif!' a za běhu tě chladnokrevně střelil do zad.");
            HideBanditButtons();
            SetupGameOverImmediately();
        }

        private void BtnRideHorse_Click(object sender, RoutedEventArgs e)
        {
            if (horseDead)
            {
                ShowMessage("❌ Nemůžeš ujet na koni, protože jsi ho zastřelil!");
            }
            else if (horseSpooked)
            {
                if (random.Next(0, 3) == 0)
                {
                    ShowMessage("🏃💨 Běžel jsi ze všech sil! Bandita sice střílel, ale ty jsi stihl naskočit na splašeného koně a v oblaku prachu mu zmizet!");
                    HideBanditButtons();
                    SetupAlternativeEnd("HorseEscape");
                }
                else
                {
                    ShowMessage("🏃 Pokusil ses běžet k zahnanému koni... Bandita tě ale bez potíží zaměřil a chladnokrevně zastřelil.");
                    HideBanditButtons();
                    SetupGameOverImmediately();
                }
            }
            else
            {
                ShowMessage("🐎 Vyskočil jsi na koně, kterého jsi předtím ochočil, a bleskově banditovi ujel do bezpečí!");
                HideBanditButtons();
                SetupAlternativeEnd("HorseEscape");
            }
        }

        private async void BtnShootBandit_Click(object sender, RoutedEventArgs e)
        {
            if (!hasBanditWeapon && bulletsFound < 1)
            {
                ShowMessage("❌ Nemáš žádné náboje! Bandita tě chladnokrevně zastřelil.");
                HideBanditButtons();
                SetupGameOverImmediately();
                return;
            }

            HideBanditButtons();
            if (!hasBanditWeapon) { bulletsFound--; RefreshInventory(); }

            ShowMessage(hasBanditWeapon ? "🔫 Tasíš prémiovou pušku a střílíš..." : "🔫 Tasíš svou zbraň a střílíš...");
            await Task.Delay(1500);

            if (random.Next(0, 2) == 0)
            {
                bandit.Visibility = Visibility.Hidden;
                hasBanditWeapon = true;
                bulletsFound = 999;
                RefreshInventory();

                ShowMessage("💥 TREFA! Bandita padl k zemi. Sebral jsi jeho zbraň s nekonečnem nábojů (🔫⭐). Město je zachráněno!");

                if (horseDead) SetupAlternativeEnd("NoHorseStorm");
                else SetupAlternativeEnd("HorseEscape");
                return;
            }

            ShowMessage("💨 VEDLE! Bandita bleskově opětuje palbu...");
            await Task.Delay(1500);

            if (random.Next(0, 2) == 0)
            {
                ShowMessage("💥 ZÁSAH! Bandita tě střelil přímo do hrudi. Padáš do písku...");
                SetupGameOverImmediately();
                return;
            }

            ShowMessage("💨 ŠTĚSTÍ! Bandita tě minul!");
            await Task.Delay(1500);

            if (hasBanditWeapon || bulletsFound >= 1)
            {
                ShowMessage($"🔄 Pořád žiješ! Můžeš akci opakovat.");
                btnRunAway.Visibility = Visibility.Visible;
                btnRideHorse.Visibility = Visibility.Visible;
                btnShootBandit.Visibility = Visibility.Visible;
                btnBargain.Visibility = Visibility.Visible;
            }
            else
            {
                ShowMessage("❌ Bandita tě minul, ale tobě zcela došly náboje! V dalším tahu tě dostal.");
                SetupGameOverImmediately();
            }
        }

        private void BtnBargain_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("💬 Rozhodl ses mluvit. Jakým způsobem se ho pokusíš přesvědčit?");
            HideBanditButtons();
            btnBargainEmotions.Visibility = Visibility.Visible;
            btnBargainAggressive.Visibility = Visibility.Visible;
        }

        private void BtnBargainEmotions_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("😭 Začal jsi žadonit. Bandita se jen zamračil: 'Zbabělce já nešetřím!' a bez milosti zmáčkl spoušť.");
            HideBanditButtons();
            SetupGameOverImmediately();
        }

        private void BtnBargainAggressive_Click(object sender, RoutedEventArgs e)
        {
            bandit.Visibility = Visibility.Hidden;
            HideBanditButtons();

            ShowMessage("😠 Ostře jsi na něj vyjel o maršálech! Bandita znervózněl, schoval zbraň a začal ustupovat hlouběji do pouště.");

            SetupAlternativeEnd("FollowBandit");
        }

        private void SetupGameOverImmediately()
        {
            currentEndType = "GameOver";
            btnContinue.Visibility = Visibility.Visible;
        }

        private void SetupAlternativeEnd(string type)
        {
            currentEndType = type;
            continueStep = 1;
            btnContinue.Visibility = Visibility.Visible;
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            btnContinue.Visibility = Visibility.Hidden;

            if (currentEndType == "GameOver")
            {
                mainMenu.Visibility = Visibility.Visible;
                ResetGameToInitialState();
                return;
            }

            if (currentEndType == "NoHorseStorm")
            {
                if (continueStep == 1)
                {
                    sun.Visibility = Visibility.Hidden;
                    outsideCactus.Visibility = Visibility.Hidden;
                    sandStormOverlay.Visibility = Visibility.Visible;

                    scene.Background = new SolidColorBrush(Color.FromRgb(210, 180, 140));
                    ShowMessage("🌪️ Šerif se vydal pěšky k nejbližšímu městu. Náhle ho však uprostřed dun zastihla gigantická písečná vichřice!");

                    continueStep = 2;
                    btnContinue.Visibility = Visibility.Visible;
                }
                else if (continueStep == 2)
                {
                    ShowMessage("💀 Hustý písek oslepil tvé oči a zaplnil tvé plíce. Šerif Adam se na následky vdechnutí písku udusil. Hra končí.");
                    currentEndType = "GameOver";
                    btnContinue.Visibility = Visibility.Visible;
                }
            }

            if (currentEndType == "HorseEscape")
            {
                if (continueStep == 1)
                {
                    sun.Visibility = Visibility.Hidden;
                    outsideCactus.Visibility = Visibility.Hidden;
                    horse.Visibility = Visibility.Hidden;
                    bandit.Visibility = Visibility.Hidden;

                    scene.Background = new SolidColorBrush(Color.FromRgb(61, 43, 31));
                    barCounter.Visibility = Visibility.Visible;
                    whiskeyGlass.Visibility = Visibility.Visible;

                    ShowMessage("🍻 Dorazil jsi bezpečně na svém koni do nejbližšího města! Vešel jsi do místního baru si odpočinout.\n\n👉 KLIKNI NA PANÁKA WHISKEY na pultu!");
                }
            }

            if (currentEndType == "FollowBandit")
            {
                if (continueStep == 1)
                {
                    bandit.Visibility = Visibility.Visible;
                    Canvas.SetLeft(bandit, 200);
                    horse.Visibility = Visibility.Hidden;

                    ShowMessage("👣 Rozhodl ses odcházejícího banditu tajně následovat hluboko do neprobádaných dun divoké pouště...");
                    continueStep = 2;
                    btnContinue.Visibility = Visibility.Visible;
                }
                else if (continueStep == 2)
                {
                    bandit.Visibility = Visibility.Hidden;
                    ShowMessage("💀 Spalující žár a totální dezorientace si vybraly svou daň. Bandita padl vyčerpáním a šerif Adam zemřel v písku hned vedle něj. Poušť nikoho nešetří. Hra končí.");
                    currentEndType = "GameOver";
                    btnContinue.Visibility = Visibility.Visible;
                }
            }
        }

        private void WhiskeyGlass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            whiskeyGlass.Visibility = Visibility.Collapsed;
            ShowMessage("🥃 Exnul jsi luxusní chladnou 'visky'. Příjemné teplo tě zaplavilo a ty víš, že spravedlnost opět zvítězila. \n\n🎉 VYHRÁL JSI A PŘEŽIL! Gratulujeme!");

            currentEndType = "GameOver";
            btnContinue.Visibility = Visibility.Visible;
        }
    }
}