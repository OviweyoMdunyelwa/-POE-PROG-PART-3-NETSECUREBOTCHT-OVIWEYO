using System;
using System.Windows;
using System.Windows.Controls;

namespace Net_Bot_App
{
    public partial class Chat : Window
    {
        public Chat()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredName = ChatInput.Text.Trim();

            if (!string.IsNullOrEmpty(enteredName))
            {
                // Open chatbot window with the name
                ChatBotWindow botWindow = new ChatBotWindow(enteredName);
                botWindow.Show();

                // Close this middle page
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter your name before continuing.",
                                "Missing Name",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: live validation
        }
    }
}
