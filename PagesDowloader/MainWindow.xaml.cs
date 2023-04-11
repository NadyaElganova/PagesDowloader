using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace PagesDowloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            statusLabel.Content = "";
            adressTextBox.Text = "";
            bodyTextBox.Text = "";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(adressTextBox.Text);
                bodyTextBox.Text = await client.GetStringAsync(adressTextBox.Text);
                HttpStatusCode status = response.StatusCode;
                statusLabel.Content = status.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string fileText = bodyTextBox.Text;
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, fileText);
            }
        }
    }
}
