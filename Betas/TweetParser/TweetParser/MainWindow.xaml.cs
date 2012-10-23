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
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lblErrorText.Visibility = Visibility.Hidden;
            //validate our box when user clicks
            ValidateBox(txtTwitterSearch);
            if (errorFlag == false)
            {
                string userKeyword;
                userKeyword = txtTwitterSearch.Text;

                try
                {
                    Cursor = Cursors.Wait;
                    List<TweetInfo> searchResults = twitterScrape.GetSearchResults(userKeyword);
                    lstTweets.ItemsSource = searchResults;
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
            else
            {
                lblErrorText.Visibility = Visibility.Visible;
                errorFlag = true;
            }


        } //btn click event

    } //Main window  
}
