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
using System.Windows.Shapes;
namespace Net_Bot_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Chat chatWindow = new Chat();
            chatWindow.Show();
            this.Close();
        }

        private void TaskAssistantButton_Click(object sender, RoutedEventArgs e)
        {
            TaskAssistant taskWindow = new TaskAssistant();
            taskWindow.ShowDialog();
        }
    }
}