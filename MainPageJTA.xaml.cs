using System;
using Microsoft.Maui.Controls;

namespace Torres_ExamenP2
{
    public partial class MainPageJTA : ContentPage
    {
        public MainPageJTA()
        {
            InitializeComponent();
            ConvertButton.Clicked += OnConvertButtonClicked;
            ClearButton.Clicked += OnClearButtonClicked;

            CurrencyFromPicker.ItemsSource = new string[] { "Euros", "Dólares", "Pesos Colombianos" };
            CurrencyToPicker.ItemsSource = new string[] { "Euros", "Dólares", "Pesos Colombianos" };
        }

        private void OnConvertButtonClicked(object sender, EventArgs e)
        {
            if (double.TryParse(AmountEntry.Text, out double amount) &&
                CurrencyFromPicker.SelectedItem != null &&
                CurrencyToPicker.SelectedItem != null)
            {
                string from = CurrencyFromPicker.SelectedItem.ToString();
                string to = CurrencyToPicker.SelectedItem.ToString();
                double result = ConvertCurrency(amount, from, to);

                ResultLabel.Text = $"Resultado: {result:F2} {to}";
            }
            else
            {
                DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            }
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            AmountEntry.Text = string.Empty;
            ResultLabel.Text = "Resultado:";
            CurrencyFromPicker.SelectedIndex = -1;
            CurrencyToPicker.SelectedIndex = -1;
        }

        private double ConvertCurrency(double amount, string from, string to)
        {
            double rate = from switch
            {
                "Euros" when to == "Dólares" => 1,
                "Euros" when to == "Pesos Colombianos" => 46623,
                "Dólares" when to == "Euros" => 0.321,
                "Dólares" when to == "Pesos Colombianos" => 44,
                "Pesos Colombianos" when to == "Euros" => 0.21,
                "Pesos Colombianos" when to == "Dólares" => 0.22,
                _ => 1 
            };

            return amount * rate;
        }
    }
}
