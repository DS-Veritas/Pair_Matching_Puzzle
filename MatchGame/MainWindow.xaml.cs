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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow() // Special method called a "Constructor"
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                // This is not dictionary. It is just 16 emojis with 8 pairs. 
                "🍔","🍔",
                "🍕","🍕",
                "🌭","🌭",
                "🥩","🥩",
                "🥗","🥗",
                "🥨","🥨",
                "🍤","🍤",
                "🍰","🍰"
            }; // Closing ';' should come after the closing bracket

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
                     // Unlike python, C# needs to set its type before the name for iteration
                     // If Python, textBlock in mainGrid.Children.OfType<TextBlock>():
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);  // array.Count를 이용해서 random의 max integer를 8로 설정
                    // random의 next는 다음 숫자를 의미하는 것이 아니라 단순히 random number를 return 시키는 method
                    // Next() returns a non-negative random integer
                    // Next(Int32) returns a non-negative number integer that is less than the specified maximum
                    // Next(Int32, Int32) returns a random integer that is within a specified range
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        // Predefine variables that are to be used in the event (These are called "fields") 
        // They live inside the class but outside the methods, so all of the methods in the window can access them
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            // findingMatch가 false라 함은, 해당 textBlock이 처음으로 눌러지는 것이라는 의미 (즉, Pair Search X)
            // 따라서, visibility는 hidden처리하고, lastTextBlockClicked는 해당 textBlock으로 새로히 설정하고
            // 마지막으로 findingMatch는 true로 설정해서 다음 mouseClick은 pair를 찾는 두 번째 textblock임을 표시
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            // 이는 이번에 선택 된 textBlock이 바로 이전에 선택된
            // textBlock (=lastTextBlockClicked) 와 동일한
            // 즉, 올바른 pair 일 때 실행되는 method를 명시
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            // 최초 click도 아니고, 맞는 pair도 아니므로 결국에는 2번째 선택이나, 이전에 선택한 block과 pair가 아닐 때의 경우
            // 이전에 선택했던 lastTextBlockClicked는 다시 visible로 바꾸고, findingMatch도 다시 false로 돌려놓는다
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
                // Resets the game if all 8 matched pairs have been found
                // Otherwise, it does nothing because the game should be still running
            }
        }
    }
}
