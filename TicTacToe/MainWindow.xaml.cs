using System;
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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Random random { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }
        private async Task ComputerRound()
        {
            int row = random.Next(0, 3);
            int col = random.Next(0, 3);
            string buttonName = $"Button_{row}_{col}";

            Button selectedButton = (Button)this.FindName(buttonName);
            if (selectedButton != null && selectedButton.IsEnabled == true)
            {
                selectedButton.Foreground = new SolidColorBrush(Colors.Red);
                selectedButton.Content = "X";
                selectedButton.IsEnabled = false;
            }
            else
            {
                await CheckDraw();
                ComputerRound();
            }

        }

        private void TickInBox(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Content = "O";
                button.Foreground = new SolidColorBrush(Colors.Blue);
                button.IsEnabled = false;
                ComputerRound();
                CheckWin();
            }
        }

        private async Task CheckWin()
        {
            if (Button_0_0.Content == "O" && Button_0_1.Content == "O" && Button_0_2.Content == "O" ||
                Button_1_0.Content == "O" && Button_1_1.Content == "O" && Button_1_2.Content == "O" ||
                Button_2_0.Content == "O" && Button_2_1.Content == "O" && Button_2_2.Content == "O" ||
                Button_0_0.Content == "O" && Button_1_0.Content == "O" && Button_2_0.Content == "O" ||
                Button_0_1.Content == "O" && Button_1_1.Content == "O" && Button_2_1.Content == "O" ||
                Button_0_2.Content == "O" && Button_1_2.Content == "O" && Button_2_2.Content == "O" ||
                Button_0_0.Content == "O" && Button_1_1.Content == "O" && Button_2_2.Content == "O" ||
                Button_0_2.Content == "O" && Button_1_1.Content == "O" && Button_2_0.Content == "O")
            {
                UserScore++;
                Result.Foreground = new SolidColorBrush(Colors.Blue);
                Result.Text = "Du vann!";
                ScoreBoard.Text = $"Du: {UserScore} Datorn: {ComputerScore}";
                await Task.Delay(1000);
                ResetBoard(null, null);
            }

            if (Button_0_0.Content == "X" && Button_0_1.Content == "X" && Button_0_2.Content == "X" ||
                Button_1_0.Content == "X" && Button_1_1.Content == "X" && Button_1_2.Content == "X" ||
                Button_2_0.Content == "X" && Button_2_1.Content == "X" && Button_2_2.Content == "X" ||
                Button_0_0.Content == "X" && Button_1_0.Content == "X" && Button_2_0.Content == "X" ||
                Button_0_1.Content == "X" && Button_1_1.Content == "X" && Button_2_1.Content == "X" ||
                Button_0_2.Content == "X" && Button_1_2.Content == "X" && Button_2_2.Content == "X" ||
                Button_0_0.Content == "X" && Button_1_1.Content == "X" && Button_2_2.Content == "X" ||
                Button_0_2.Content == "X" && Button_1_1.Content == "X" && Button_2_0.Content == "X")
            {
                ComputerScore++;
                Result.Foreground = new SolidColorBrush(Colors.Red);
                Result.Text = "Du förlorade";
                ScoreBoard.Text = $"Du: {UserScore} Datorn: {ComputerScore}";
                await Task.Delay(1000);
                ResetBoard(null, null);
            }
            CheckDraw();
        }

        private async Task CheckDraw()
        {
            bool isDraw = true;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    string buttonName = $"Button_{row}_{col}";
                    Button button = (Button)this.FindName(buttonName);
                    if (button.Content == null || button.Content == "")
                    {
                        isDraw = false;
                        break;
                    }
                }
                if (isDraw == false)
                    break;
            }
            if (isDraw)
            {
                Result.Foreground = new SolidColorBrush(Colors.Black);
                Result.Text = "Oavgjort";
                await Task.Delay(1000);
                ResetBoard(null, null);

            }
        }

        private void ResetBoard(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    string buttonName = $"Button_{row}_{col}";
                    Button button = (Button)this.FindName(buttonName);
                    button.Content = "";
                    button.IsEnabled = true;
                }
            }
            Result.Text = "";
        }
    }
}