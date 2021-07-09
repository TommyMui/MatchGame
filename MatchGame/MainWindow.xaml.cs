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
        int tenthsOfSecondsElapsed;                     //Store the how many second past
        int matchesFound;                               //Store the No. of match

        public MainWindow()
        {
            InitializeComponent();                      //

            timer.Interval = TimeSpan.FromSeconds(.1);  //Setting up the interval of the timer
            timer.Tick += Timer_Tick;                   //Create a Timer_Tick method, when 

            SetUpGame();                                //Game start
        }

        private void Timer_Tick(object sender, EventArgs e)                         //Occurs when the timer interval is exceeded
        {
            tenthsOfSecondsElapsed++;                                               //Counter up the tenthsOfSecondsElapsed, tenth of a second
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");   //display the actual time was past
            if (matchesFound == 8)                                                  //When 8 pair picture were match
            {
                timer.Stop();                                                       //Timer stop counting
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";         //Display the counted time and ask "Play again"
            }
            //throw new NotImplementedException();
        }

        private void SetUpGame()                            //
        {
            List<string> animalEmoji = new List<string>()   //Record Down the animal picture
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

            Random random = new Random();                           //Create a "random" OBJECT

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())  //Use Foreach loop to operate every TextBlock
            {
                if (textBlock.Name !="timeTextBlock")               //if the textBlock Nameis not "timeTextBlock",then start below codes
                {
                    textBlock.Visibility = Visibility.Visible;      //Display all the picture again becasue of the game restart
                    int index = random.Next(animalEmoji.Count);     //Pick a random number between 0 to the number of Emoji in the list
                                                                    //And assign to index
                    string nextEmoji = animalEmoji[index];          //Put index + animalEmoji together and assign to nextEmoji
                    textBlock.Text = nextEmoji;                     //Update the Emoji in the textBlock
                    animalEmoji.RemoveAt(index);                    //Remove the random Emoji from the list
                }
                
            }
            timer.Start();                                          //Timer start counting up
            tenthsOfSecondsElapsed = 0;                             //Resst "tenthsOFSecondsElapsed"
            matchesFound = 0;                                       //Reset "matchsFound"
        }
        TextBlock lastTextBlockClicked;                             //Create a variable store "lastTextBlockClicked"
        bool findingMatch = false;                                  //Create a variable store "findingMatch" or not

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;              //??
            if (findingMatch == false)                              //Check 1st click or 2nd click
            {
                textBlock.Visibility = Visibility.Hidden;           //textBlock change to Hidden
                lastTextBlockClicked = textBlock;                   //Record down the textBlock
                findingMatch = true;                                //Change the findingMatch condition to true, then next Click will 
                                                                    //go the another loop to check up textBlock is match or not
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)   //When 2nd Click happen, check up textBlock is match to 1st textBlock or not
            {
                matchesFound++;                                     //Add 1 to "matchesFound"
                textBlock.Visibility = Visibility.Hidden;           //if 2nd and 1st textBlock are match, then 2nd textBlock change to Hidden
                findingMatch = false;                               //Reset the "findingMatch"
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;   //if 2nd and 1st textBlock are not match,then 1st textBlock change back Visible
                findingMatch = false;                                   //Reset the loop back to otiginal again.
            }
        }

        private void TimerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)                                      //When TimeTextBlock click, and "matchesFound" equal to 8 pairs, 
            {
                SetUpGame();                                            //Game start again
            }
        }
    }
}
