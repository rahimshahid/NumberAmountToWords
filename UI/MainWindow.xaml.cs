using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient httpClient;
        private readonly string? serverAddress;
        private readonly string? apiName;

        public MainWindow()
        {
            httpClient = new HttpClient();
            serverAddress = ConfigurationManager.AppSettings["ServerAddress"];
            apiName = ConfigurationManager.AppSettings["ApiName"];

            if(serverAddress == null)
                MessageBox.Show("No server address found in App.config", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (apiName == null)
                MessageBox.Show("No api name found in App.config", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnConvert.IsEnabled = false;
                Mouse.OverrideCursor = Cursors.Wait;

                string amount = txtAmount.Text.Trim();

                if (String.IsNullOrEmpty(amount)) 
                {
                    MessageBox.Show($"Please specify an amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string apiAddress = $"{serverAddress}/{apiName}?amount={amount}";

                HttpResponseMessage response = await httpClient.GetAsync(apiAddress);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                txtResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally 
            {
                btnConvert.IsEnabled = true;
                Mouse.OverrideCursor = null; // Revert back to the default cursor
            }
        }
    }
}
