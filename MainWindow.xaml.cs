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
        int Count = 0;
        bool QuestionOn = true;
        List<Question> questions = new List<Question>();

        public MainWindow()
        {
            InitializeComponent();
            Button1.Name = "GoodButton";
            Button2.Name = "GoodButton";
            Button3.Name = "GoodButton";
            Button4.Name = "GoodButton";

            LoadQuestions("cigány vagy");
        }


        private async Task MoneyLadder()
        {
            Ladder.Visibility = Visibility.Visible;
            TextBlock textBlock = (TextBlock)FindName("ML" + (Count+1).ToString());
            Brush Back = textBlock.Background;
            Brush Fore = textBlock.Foreground;

            if (Count > 1)
            {
                TextBlock textBlock2 = (TextBlock)FindName("ML" + (Count).ToString());
                textBlock2.Background = Brushes.Green;
                textBlock2.Foreground = Back;
            }
            for (int i = 0; i < 10; i++)
            {
                if (i %2== 0)
                {
                    textBlock.Background = Back;
                    textBlock.Foreground = Fore;
                }
                else
                {
                    textBlock.Background = Brushes.Gold;
                    textBlock.Foreground = Brushes.Black;
                }
                await Task.Delay(500);
            }
            Ladder.Visibility = Visibility.Hidden;
            Progression();

        }

        private void LoadQuestions(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 6)
                    {
                        var question = new Question(parts[0], new List<string> { parts[1], parts[2], parts[3], parts[4] }, int.Parse(parts[5]));

                        questions.Add(question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}");
            }
        }

        private void Progression()
        {
            Count++;
            FieldClear();







            QuestionOn = true;
        }

        private void FieldClear()
        {
            //a fájhoz képest változhat
            Button1.Content = "A: ";
            Button2.Content = "B: ";
            Button3.Content = "C: ";
            Button4.Content = "D: ";

            

            Button1.Background = Brushes.DarkBlue;
            Button1.Name = "Button1";
            Button2.Background = Brushes.DarkBlue;
            Button2.Name = "Button2";
            Button3.Background = Brushes.DarkBlue;
            Button3.Name = "Button3";
            Button4.Background = Brushes.DarkBlue;
            Button4.Name = "Button4";
            TextBlock t = (TextBlock)FindName("ML" + Count.ToString());
            ScoreTB.Text = $"{Count}/15 kérdés {t.Text}-ért";
            QuestionTB.Text = "A következő kérdés:";


        }



        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            if (Count == 0)
            {
                QuestionOn = false;
                MoneyLadder();
            }
            else if (QuestionOn)
            {
                QuestionOn = false;
                Button btn = sender as Button;
                btn.Background = Brushes.Yellow;
                btn.Foreground = Brushes.Black;
                Anim(btn);
            }


        }


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
                aws.Background = Brushes.Green;
                good.Open(new Uri("C:\\Users\\berki\\source\\repos\\LegyenOnIsMilliomosWPF\\SFX\\Good.mp3"));
                Light1.Fill = Brushes.LightGreen;
                Light2.Fill = Brushes.LightGreen;
                good.Play();
                await Task.Delay(5000);

                Light1.Visibility = Visibility.Hidden;
                Light2.Visibility = Visibility.Hidden;
                Light1.Fill = Brushes.Yellow;
                Light2.Fill = Brushes.Yellow;
                aws.Foreground = Brushes.White;
                MoneyLadder();
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
                MessageBox.Show("Sajnálatos módon vesztettél\n");
                MoneyLadderL();
            }


        }

        private async Task MoneyLadderL()
        {
            Ladder.Visibility = Visibility.Visible;
            for (int i=1; i<16;i++)
            {
                TextBlock t = (TextBlock)FindName("ML" + i.ToString());
                if (i % 2 == 0)
                {
                    t.Background = Brushes.Red;
                }
                else
                {
                    t.Background = Brushes.DarkRed;
                }
                await Task.Delay(300);
            }
            MessageBox.Show("Womp Womp");
            App.Current.Shutdown();
        }

    }
}