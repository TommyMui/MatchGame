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
    using System.Windows.Threading;                     //Using Windows Threading for timer
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();  //Create a timer object from DispatchTimer
        int tenthsOfSecondsElapsed;                     //
        int matchesFound;                               //Store the No. of match

        public MainWindow()
        {
            InitializeComponent();                      //

            timer.Interval = TimeSpan.FromSeconds(.1);  //
            timer.Tick += Timer_Tick;                   //Create a Timer_Tick method

            SetUpGame();                                //
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;                   //
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");   //
            if (matchesFound == 8)                      //
            {
                timer.Stop();                           //
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";         //
            }
            //throw new NotImplementedException();
        }

        private void SetUpGame()                            //
        {
            List<string> animalEmoji = new List<string>()   //
            {
                "❤","❤",
                "😊","😊",
                "💋","💋",
                "🐱‍🚀","🐱‍🚀",
                "✔","✔",
                "👀","👀",
                "🙈","🙈",
                "🦊","🦊",
            };

            Random random = new Random();                       //Create a "random" OBJECT

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())  //Use Foreach loop to operate every TextBlock
            {
                if (textBlock.Name !="timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);     //Pick a random number between 0 to the number of Emoji in the list
                                                                    //And assign to index
                    string nextEmoji = animalEmoji[index];          //Put index + animalEmoji together and assign to nextEmoji
                    textBlock.Text = nextEmoji;                     //Update the Emoji in the textBlock
                    animalEmoji.RemoveAt(index);                    //Remove the random Emoji from the list
                }
                
            }
            timer.Start();                                          //
            tenthsOfSecondsElapsed = 0;                             //
            matchesFound = 0;                                       //
        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;              //
            if (findingMatch == false)                              //
            {
                textBlock.Visibility = Visibility.Hidden;           //textBlock change to Hidden
                lastTextBlockClicked = textBlock;                   //Record down the textBlock
                findingMatch = true;                                //Change the findingMatch condition to true, then next Click will 
                                                                    //go the another loop to check up textBlock is match or not
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)   //When 2nd Click happen, check up textBlock is match to 1st textBlockor not
            {
                matchesFound++;                                     //
                textBlock.Visibility = Visibility.Hidden;           //if 2nd and 1st textBlock are match, then 2nd textBlock change to Hidden
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;   //if 2nd and 1st textBlock are not match,then 1st textBlock change back to Hidden
                findingMatch = false;                                   //Reset the loop back to otiginal again.
            }
        }

        private void TimerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
