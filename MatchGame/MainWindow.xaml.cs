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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() // Special method called a "Constructor"
        {
            InitializeComponent();
            SetUpGame();
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
                int index = random.Next(animalEmoji.Count); // array.Count를 이용해서 random의 max integer를 8로 설정
                    // random의 next는 다음 숫자를 의미하는 것이 아니라 단순히 random number를 return 시키는 method
                    // Next() returns a non-negative random integer
                    // Next(Int32) returns a non-negative number integer that is less than the specified maximum
                    // Next(Int32, Int32) returns a random integer that is within a specified range
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

    }
}
