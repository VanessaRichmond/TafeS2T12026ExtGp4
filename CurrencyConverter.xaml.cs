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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		private readonly Dictionary<string, Dictionary<string, double>> rates =
			new Dictionary<string, Dictionary<string, double>>
			{
				{ "USD", new Dictionary<string, double>
					{
						{ "EUR", 0.85189982 },
						{ "GBP", 0.72872436 },
						{ "INR", 74.257327 }
					}
				},
				{ "EUR", new Dictionary<string, double>
					{
						{ "USD", 1.1739732 },
						{ "GBP", 0.8556672 },
						{ "INR", 87.00755 }
					}
				},
				{ "GBP", new Dictionary<string, double>
					{
						{ "USD", 1.371907 },
						{ "EUR", 1.1686692 },
						{ "INR", 101.68635 }
					}
				},
				{ "INR", new Dictionary<string, double>
					{
						{ "USD", 0.011492628 },
						{ "EUR", 0.013492774 },
						{ "GBP", 0.0098339397 }
					}
				}
			};

		public CurrencyConverter()
		{
			this.InitializeComponent();

			Loaded += (s, e) =>
			{
				FromCurrencyComboBox.SelectedIndex = 0;
				ToCurrencyComboBox.SelectedIndex = 0;
			};
		}

		private void CalculateCurrency_Click(object sender, RoutedEventArgs e)
		{
			// Validate amount
			if (!double.TryParse(AmountTextBox.Text, out double amount))
			{
				ConvertedAmountText.Text = "Invalid amount entered.";
				return;
			}

			string from = GetCurrencyCode(FromCurrencyComboBox);
			string to = GetCurrencyCode(ToCurrencyComboBox);

			// Same currency → no conversion needed
			if (from == to)
			{
				ConversionResultText.Text = $"{amount} {from} =";
				ConvertedAmountText.Text = $"{amount:F2} {to}";
				Rate1Text.Text = $"1 {from} = 1 {to}";
				Rate2Text.Text = $"1 {to} = 1 {from}";
				return;
			}			

			// Lookup rate
			double rate = rates[from][to];
			double converted = amount * rate;

			// Update UI
			ConversionResultText.Text = $"{amount} {from} =";
			ConvertedAmountText.Text = $"{converted:F2} {to}";
			Rate1Text.Text = $"1 {from} = {rate:F8} {to}";
			Rate2Text.Text = $"1 {to} = {(1 / rate):F8} {from}";
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}

		private string GetCurrencyCode(ComboBox combo)
		{
			var item = combo.SelectedItem as ComboBoxItem;
			return item?.Tag?.ToString() ?? string.Empty;
		}
	}
}
