using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
//using Twitterizer;
//using LinqToTwitter;


namespace TweetParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        bool errorFlag; //for errors

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblResults.Content = pbLoader.Value + " Results displayed";
            
        }


        //validate a textbox function
        private void ValidateBox(TextBox userInput)
        {
            if (userInput.Text.Trim().Length == 0)
            {
                //display the error label
                lblErrorText.Visibility = Visibility.Visible;
                errorFlag = true;
            }
            else if (userInput.Text.Trim().Length >= 1)
            {
                errorFlag = false;
                lblErrorText.Visibility = Visibility.Hidden;
            }
        }


        //on user click of button1
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            pbLoader.Value = 0;
            pbLoader.Minimum = 0;
            pbLoader.Maximum = 100;
            //make sure our label is hidden
            lblErrorText.Visibility = Visibility.Hidden;
            //validate our box when user clicks
            ValidateBox(txtTwitterSearch);
            if (errorFlag == false)
            {
                string userKeyword;
                userKeyword = txtTwitterSearch.Text; //putting textbox as a var


                //try to get results from twitter and list
                //display our cursor working so the user doesn't think
                //that nothing is going on
                try
                {
                    Cursor = Cursors.Wait;
                    List<TweetInfo> searchResults = twitterScrape.GetSearchResults(userKeyword);
                    lblResults.Content = searchResults.Count + " of " + pbLoader.Maximum.ToString() + " Tweets Displayed";
                    pbLoader.Value = searchResults.Count;
                    //display tweets
                    lstTweets.ItemsSource = searchResults;
                }
                    //cursor is arrow
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
                //this could probably be executed differently, however
                //display error if no text entered
            else
            {
                lblErrorText.Visibility = Visibility.Visible;
                errorFlag = true;
            }
        } //btn click event

    } //Main window  
}
