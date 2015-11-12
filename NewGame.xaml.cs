using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using ProjectPhoenix.Model;
using Windows.Storage;
using System.Threading.Tasks;
using System.Data.Common;

namespace ProjectPhoenix
{
  
    public sealed partial class NewGame : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        int ScoreA, ScoreB, Rounds, TotalScoreA, TotalScoreB;
       
        public NewGame()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Game>();
            var add = conn.Insert(new Game());
            Debug.WriteLine(path);

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void EnterNameButton_Click(object sender, RoutedEventArgs e)
        {
            NameATextBlock.Text = NameATextBox.Text;
            NameBTextBlock.Text = NameBTextBox.Text;
            NameAStackPanel.Children.Remove(NameATextBox);
            NameBStackPanel.Children.Remove(NameBTextBox);
            ButtonStackPanel.Children.Remove(EnterNameButton);
            
           
           

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(NewGameListBoxItem.IsSelected)
            {
                ResetScore();
            }
        }

        private void EnterScoreButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(String.IsNullOrEmpty(ScoreATextBox.Text) || String.IsNullOrEmpty(ScoreBTextBox.Text)))
            {
                ScoreA = Convert.ToInt32(ScoreATextBox.Text);
                ScoreA = int.Parse(ScoreATextBox.Text);
                ScoreB = Convert.ToInt32(ScoreBTextBox.Text);
                ScoreB = int.Parse(ScoreBTextBox.Text);
                if (((ScoreA + ScoreB) == 0 || (ScoreA + ScoreB) == 100 || (ScoreA + ScoreB) == 200 || (ScoreA + ScoreB) == 300 || (ScoreA + ScoreB) == 400) && (ScoreA%5 == 0) && (ScoreB%5 == 0))
                {
                    TextBlock _newScoreA = new TextBlock();
                    _newScoreA.Text = ScoreA.ToString();
                    _newScoreA.FontSize = 20;
                    _newScoreA.HorizontalAlignment = HorizontalAlignment.Center;
                    ScoreAStackPanel.Children.Add(_newScoreA);
                    TextBlock _newScoreB = new TextBlock();
                    _newScoreB.Text = ScoreB.ToString();
                    _newScoreB.FontSize = 20;
                    _newScoreB.HorizontalAlignment = HorizontalAlignment.Center;
                    ScoreBStackPanel.Children.Add(_newScoreB);

                    TotalScoreA += ScoreA;
                    TotalScoreB += ScoreB;
                    TotalAScoreTextBlock.Text = TotalScoreA.ToString();
                    TotalBScoreTextBlock.Text = TotalScoreB.ToString();
                    ScoreATextBox.Text = String.Empty;
                    ScoreBTextBox.Text = String.Empty;

                    Rounds++;
                    TextBlock _rounds = new TextBlock();
                    _rounds.Text = Rounds.ToString();
                    _rounds.HorizontalAlignment = HorizontalAlignment.Center;
                    _rounds.FontSize = 20;
                    RoundsStackPanel.Children.Add(_rounds);
                }
                else
                {
                    ScoreATextBox.Text = String.Empty;
                    ScoreBTextBox.Text = String.Empty;
                }
            }

            ScoreATextBox.TextChanging += ScoreATextBox_TextChanging;
            ScoreBTextBox.TextChanging += ScoreBTextBox_TextChanging;


        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetScore();
        }

        private void ScoreATextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

            try {
                ScoreBTextBox.TextChanging -= ScoreBTextBox_TextChanging;

                if (ScoreATextBox.Text.Length != 0)
                {
                    if (ScoreATextBox.Text == "-")
                    {
                        ScoreB = 0;
                        ScoreBTextBox.Text = "0";
                        ScoreATextBox.Text.TrimStart('-');
                        string NegativeScore = ScoreATextBox.Text;
                        NegativeScore += "0";
                        ScoreA = Convert.ToInt32(NegativeScore);
                        ScoreA = int.Parse(NegativeScore);
                        ScoreA = -1 * ScoreB;
                    }
                    else
                    {
                        ScoreB = 0;
                        ScoreBTextBox.Text = "0";
                        ScoreA = Convert.ToInt32(ScoreATextBox.Text);
                        ScoreA = int.Parse(ScoreATextBox.Text);
                        if ((ScoreA % 5) == 0 && ScoreA != 0)
                        {
                            if ((ScoreA > 100 && ScoreA <= 200) || (ScoreA >= -200 && ScoreA < -100))
                            {
                                ScoreB = 200 - ScoreA;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            else if ((ScoreA > 200 && ScoreA <= 300) || (ScoreA >= -300 && ScoreA < -200))
                            {
                                ScoreB = 300 - ScoreA;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            else if (ScoreA == 400)
                            {
                                ScoreB = 0;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            else if (((ScoreA > 0) && (ScoreA <= 100)))
                            {
                                ScoreB = 100 -  ScoreA;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            else if  ((ScoreA >= -100) && (ScoreA < 0)) {
                                ScoreB = -1 * ScoreA;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            

                        }

                    }

                }
            }
            catch
                {
                //ScoreATextBox.Text.TrimEnd('.');
                //ScoreATextBox.Text.TrimEnd(',');
                ScoreATextBox.Text = String.Empty;
            }
        }

        private void ScoreBTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            try {
                ScoreATextBox.TextChanging -= ScoreATextBox_TextChanging;

                if (ScoreBTextBox.Text.Length != 0)
                {
                    if (ScoreBTextBox.Text == "-")
                    {
                        ScoreA = 0;
                        ScoreATextBox.Text = "0";
                        ScoreBTextBox.Text.TrimStart('-');
                        string NegativeScore = ScoreBTextBox.Text;
                        NegativeScore += "0";
                        ScoreB = Convert.ToInt32(NegativeScore);
                        ScoreB = int.Parse(NegativeScore);
                        ScoreB = -1 * ScoreA;
                    }
                    else
                    {
                        ScoreA = 0;
                        ScoreATextBox.Text = "0";
                        ScoreB = Convert.ToInt32(ScoreBTextBox.Text);
                        ScoreB = int.Parse(ScoreBTextBox.Text);
                        if ((ScoreB % 5) == 0 && ScoreB != 0)
                        {
                            if ((ScoreB > 100 && ScoreB <= 200) || (ScoreB >= -200 && ScoreB < -100))
                            {
                                ScoreA = 200 - ScoreB;
                                ScoreATextBox.Text = ScoreA.ToString();
                            }
                            else if ((ScoreB > 200 && ScoreB <= 300) || (ScoreB >= -300 && ScoreB < -200))
                            {
                                ScoreA = 300 - ScoreB;
                                ScoreATextBox.Text = ScoreA.ToString();
                            }
                            else if (ScoreB == 400)
                            {
                                ScoreA = 0;
                                ScoreATextBox.Text = ScoreA.ToString();
                            }
                            else if (((ScoreB > 0) && (ScoreB <= 100)))
                            {
                                ScoreA = 100 - ScoreB;
                                ScoreBTextBox.Text = ScoreB.ToString();
                            }
                            else if ((ScoreB >= -100) && (ScoreB < 0))
                            {
                                ScoreA = -1 * ScoreB;
                                ScoreATextBox.Text = ScoreA.ToString();
                            }
                           

                        }
                    }
                }
            }
            catch
            {
                ScoreBTextBox.Text = String.Empty;
            }
        }

        private void ResetScore()
        {
            Frame.Navigate(typeof(NewGame));
        }


    }
}
