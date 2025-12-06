using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Media;
using System.IO;
using System;

namespace LegyenOnIsMilliomosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard myStoryboard = new Storyboard();
        Random rand = new Random();
        MediaPlayer sus = new MediaPlayer();
        MediaPlayer good = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();


        }


        private async Task MoneyLadder(int ind)
        {
            Ladder.Visibility = Visibility.Visible;
            TextBlock textBlock = (TextBlock)FindName("ML" + ind.ToString());
            Brush Back = textBlock.Background;
            Brush Fore = textBlock.Foreground;
            MessageBox.Show(textBlock.Name);

            if (ind > 1)
            {
                TextBlock textBlock2 = (TextBlock)FindName("ML" + (ind - 1).ToString());
                textBlock2.Background = Brushes.Green;
                textBlock2.Foreground = Back;
            }
            for (int i = 0; i < 10; i++)
            {
                if (i %2== 0)
                {
                    textBlock.Background = Brushes.Gold;
                    textBlock.Foreground = Brushes.Black;
                }
                else
                {
                    textBlock.Background = Back;
                    textBlock.Foreground = Fore;
                }
                await Task.Delay(500);
            }
            Progression();
            Ladder.Visibility = Visibility.Hidden;

        }

        private void Progression()
        {
            //kérdés és válaszok betöltése
        }

        private void BtnReset()
        {
            Button1.Background = Brushes.DarkBlue;
            Button1.Name = "Button1";
            Button2.Background = Brushes.DarkBlue;
            Button2.Name = "Button2";
            Button3.Background = Brushes.DarkBlue;
            Button3.Name = "Button3";
            Button4.Background = Brushes.DarkBlue;
            Button4.Name = "Button4";
        }


        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            Anim(btn);

            //ha nyer

        }


        //animáció készítése
        private async Task Anim(Button aws)
        {

            sus.Open(new Uri("C:\\Users\\berki\\source\\repos\\LegyenOnIsMilliomosWPF\\SFX\\Suspense.mp3"));
            sus.Play();
            Light1.Visibility = Visibility.Visible;
            Light2.Visibility = Visibility.Visible; 
            await Task.Delay(rand.Next(8000, 12000));
            sus.Stop();

            if (aws.Name == "GoodButton")
            {
                MessageBox.Show("Good Boi");
            }
            else
            {
                
                MediaPlayer Wong = new MediaPlayer();
                Wong.Open(new Uri("C:\\Users\\berki\\source\\repos\\LegyenOnIsMilliomosWPF\\SFX\\Wrong.mp3"));
                Wong.Play();
                Light1.Fill = Brushes.Red;
                Light2.Fill = Brushes.Red;
                kanva.Background = Brushes.DarkRed;

                foreach (var btn in new[] { Button1, Button2, Button3, Button4 })
                {
                    if (btn.Name == "GoodButton")
                    {
                        btn.Background = Brushes.Green;
                    }
                }

                aws.Background = Brushes.Red;
                
                await Task.Delay(2000);
                MessageBox.Show("Sajnálatos módon vesztettél (Womp Womp)\n");
                MoneyLadder(1);
                //App.Current.Shutdown();
            }

            Light1.Visibility = Visibility.Hidden;
            Light2.Visibility = Visibility.Hidden;

        }
    }
}