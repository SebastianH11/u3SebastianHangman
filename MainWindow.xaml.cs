/* Sebastian Horton
 * April 22nd 2018
 * u3HangmanSebastian
 * program allows the user to play a game of hangman
 * */
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
using System.IO;
using System.Net;
using System.Collections;

namespace u3HangmanSebastian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] incorrectGuessed = new string[5];
        string[] word = new string[10];
        string[] correctGuessed = new string[10];
        int RNG = 0;
        int counter = 5;
        int winnerCheck = 0;
        string wordUsed = null;
        string wordrecreated = null;
        string guessedLetter = null;
        string lblWrong = null;
        string line = null;

        Random random = new Random();
        System.IO.StreamReader streamReader = new System.IO.StreamReader("words.txt");
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush thisTownAintBigEnoughForTheTwoOfUs = new ImageBrush();
            thisTownAintBigEnoughForTheTwoOfUs.ImageSource = new BitmapImage(new Uri
               ("http://moziru.com/images/wild-west-clipart-background-1.jpg"));
            myCanvas.Background = thisTownAintBigEnoughForTheTwoOfUs;
        }
        //label method
        private void CreateLabel(int i, string content)
        {
            Label wordLabel = new Label();
            wordLabel.Content = content;
            Canvas.SetTop(wordLabel, 80);
            Canvas.SetLeft(wordLabel, (223 + (i * 10)));
            myCanvas2.Children.Add(wordLabel);
        }
        private void StartProgram(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Welcome to hangman! A random word will be selected and you must find it. You can guess with LOWER CASE LETTERS or WORDS. If you guess more than 5 wrong letters or words you lose! good luck.");
            RNG = random.Next(0, 9);//each word is attached to a number
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                //checking which word is being used
                if (line.Contains(RNG.ToString()))
                {
                    this.wordUsed = line.Substring(line.IndexOf(RNG.ToString()) + 2, line.Length - 2);//removing the number
                }

            }
            //loop that splits the word into an array of letters
            for (int i = 0; i < wordUsed.Length; i++)
            {
                word[i] = wordUsed.Substring(i, 1);

            }
            //loopp that create equally as many underscores
            for (int i = 0; i < wordUsed.Length; i++)
            {
                CreateLabel(i, "_");
            }

        }

        private void GuessLetters(object sender, RoutedEventArgs e)
        {
            guessedLetter = txtGuess.Text;
            for (int i = 0; i < wordUsed.Length; i++)
            {
                //check correct letters
                if (guessedLetter == word[i])
                {
                    correctGuessed[i] = guessedLetter;
                    CreateLabel(i, guessedLetter);
                }
                //check for whole word winner
                if (guessedLetter == wordUsed)
                {
                    MessageBox.Show("You win!");
                    i = wordUsed.Length;//ends loop early
                    winnerCheck = 1;//makes sure that there arent two "wins"
                }
                //check for incorrect letters
                if (!wordUsed.Contains(guessedLetter))
                {
                    counter--;
                    incorrectGuessed[i] = guessedLetter;
                    lblWrong = lblWrong + incorrectGuessed[i] + " ";
                    lblWrongLetters.Content = lblWrong;
                    i = wordUsed.Length;
                    if (counter != 0)
                    {
                        MessageBox.Show("Your guess of " +"\"" + txtGuess.Text + "\"" +  " was incorrect! You have " + counter.ToString() + " guesses remaining");
                    }
                    //check for loss
                    if (counter == 0)
                    {
                        MessageBox.Show("You lose! The word you were looking for was: " + wordUsed);
                    }
                }

            }
            //check for letter winner
            if (winnerCheck == 0)
            {
                wordrecreated = correctGuessed[0] + correctGuessed[1] + correctGuessed[2] + correctGuessed[3] + correctGuessed[4] + correctGuessed[5] + correctGuessed[6] + correctGuessed[7] + correctGuessed[8];
                if (wordrecreated == wordUsed)
                {
                    MessageBox.Show("You win!");
                }
            }




        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            myCanvas2.Children.Clear();
            for (int i = 0; i < 10; i++)
            {
                word[i] = null;
                line = null;
            }
        }
    }
}


