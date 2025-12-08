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
using System.Data;
using System.CodeDom.Compiler;

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
        MediaPlayer Waiting = new MediaPlayer();
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

            LoadQuestions("kerdes.txt");
        }


        private async Task MoneyLadder()
        {
            Ladder.Visibility = Visibility.Visible;
            TextBlock textBlock = (TextBlock)FindName("ML" + (Count+1).ToString());
            Brush Back = textBlock.Background;
            Brush Fore = textBlock.Foreground;

            if (Count > 0)
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
                   
                        var question = new Question(int.Parse(parts[0]), parts[1], new List<string> { parts[2], parts[3], parts[4], parts[5] }, parts[6], parts[7]);
                        questions.Add(question);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}");
            }
        }

        private async void wictory()
        {
            MediaPlayer win = new MediaPlayer();
            win.Open(new Uri("C: /Users/berki/source/repos/LegyenOnIsMilliomosWPF/SFX/Ruder Buster.mp3"));
            MessageBox.Show("Gratulálok, megnyerted a játékot!", "Nyertél", MessageBoxButton.OK, MessageBoxImage.Information);
            Ladder.Visibility = Visibility.Visible;
            win.Play();
            while (true)
            {
                SpawnMoney();
                await Task.Delay(200);
            }
        }

        private async Task Progression()
        {
            Count++;
            if (Count > 15)
            {                
                wictory();
                return;
            }
            if (Count == 6 || Count == 11)
            {
                MessageBoxResult result =  MessageBox.Show("Gratulálok, elérted a biztos pontot!\nSzeretnél továbbhaladni? (igen)\nSzeretnél hazamenni? (nem)", "Biztos pont", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    // continue
                }
                else
                {
                    wictory();
                    return;
                }

            }
            FieldClear();
            Question q = null;
            int index = 0;
            int random = rand.Next(1,150);
            foreach (var quest in questions)
            {
                if (quest.QuestionNumber == Count)
                {
                    if (random == index)
                        q = quest;
                    
                    index++;
                }
            }
            if (q == null)
            {
                MessageBox.Show("Nem volt megfelelő kérdés, véletlenszerű kérdés kerül kiválasztásra.");
                q = questions[rand.Next(0, questions.Count())];
            }


            MessageBox.Show($"Következő kérdés száma: {q.QuestionNumber}\nKategória: {q.Category}\nKérdés: {q.QuestionText}\nA: {q.Answers[0]}\nB: {q.Answers[1]}\nC: {q.Answers[2]}\nD: {q.Answers[3]}\nHelyes válasz: {q.Solution}");
            await Task.Delay(1000);
            CategoryTB.Background = Brushes.White;
            CategoryTB.Text = "Kategória: " + q.Category;
            await Task.Delay(2000);
            CategoryTB.Background = Brushes.DarkOrange;
            QuestionTB.Text = q.QuestionText;
            await Task.Delay(4000);

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        Button1.Content = "A: " + q.Answers[i];
                        break;
                    case 1:
                        Button2.Content = "B: " + q.Answers[i];
                        break;
                    case 2:
                        Button3.Content = "C: " + q.Answers[i];
                        break;
                    case 3:
                        Button4.Content = "D: " + q.Answers[i];
                        break;
                }
                await Task.Delay(1000);
            }
            switch (q.Solution)
            {
                case "A":
                    Button1.Name = "GoodButton";
                    break;
                case "B":
                    Button2.Name = "GoodButton";
                    break;
                case "C":
                    Button3.Name = "GoodButton";
                    break;
                case "D":
                    Button4.Name = "GoodButton";
                    break;
            }




            QuestionOn = true;
            Waiting.Open(new Uri("C:\\Users\\berki\\source\\repos\\LegyenOnIsMilliomosWPF\\SFX\\It's Showtime!.mp3"));
            Waiting.Play();
        }



        private void FieldClear()
        {
            Button1.Content = "";
            Button2.Content = "";
            Button3.Content = "";
            Button4.Content = "";

            CategoryTB.Text = "Kategória: ";



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
            Waiting.Stop();
             if (Count == 0 && QuestionOn)
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
                MessageBox.Show("Sajnálatos módon vesztettél\n", "Vesztettél", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void SpawnMoney()
        {
            Image money = new Image();
            money.Source = new BitmapImage(new Uri("C:\\Users\\berki\\source\\repos\\LegyenOnIsMilliomosWPF\\Money.png"));
            money.Width = 50;

            // start at top, random X
            double x = rand.Next(0, 1800);

            Canvas.SetLeft(money, x);
            Canvas.SetTop(money, -50); 

            kanva.Children.Add(money);

            // falling animation
            DoubleAnimation anim = new DoubleAnimation()
            {
                From = -50,
                To = 1000,
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            anim.Completed += (s, e) =>
            {
                kanva.Children.Remove(money);
            };



            money.BeginAnimation(Canvas.TopProperty, anim);
        }


    }
}