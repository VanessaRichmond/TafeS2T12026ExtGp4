using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
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
	public sealed partial class Mortgage_Calculator : Page
	{
		public Mortgage_Calculator()
		{
			this.InitializeComponent();
		}

		private void MainPage_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			double loanAmount, annualRate;
			int loanYearsAmount;

			try {

				loanAmount = double.Parse(principalBorrowdTextBox.Text);
				annualRate = double.Parse(annualInterestRateTextBox.Text);
				loanYearsAmount = int.Parse(yearsTextBox.Text);

				loanCalculations(loanAmount, annualRate, loanYearsAmount);
			}
			catch {
				ContentDialog dialog = new ContentDialog
				{
					XamlRoot = this.Content.XamlRoot,
					Title = "Error Message",
					Content = "Please check input values. Empty fields are not allowed",
					CloseButtonText = "OK"
				};
				_ = await dialog.ShowAsync();
				if (string.IsNullOrWhiteSpace(principalBorrowdTextBox.Text))
					{
					principalBorrowdTextBox.Focus(FocusState.Programmatic);
				}
				else if (string.IsNullOrWhiteSpace(yearsTextBox.Text))
				{
					yearsTextBox.Focus(FocusState.Programmatic);
				}
				else
				{
					annualInterestRateTextBox.Focus(FocusState.Programmatic);
				}
				
				return;

			}


		}

		private void loanCalculations(double amount, double anualRate, int yearsLength) {
			double monthlyRate, monthlyRepayments, rateFactor;
			int monthsAmount;
			monthsAmount = yearsLength * 12;
			monthlyRate = (anualRate / 12) / 100;
			rateFactor = Math.Pow(1 + monthlyRate, monthsAmount);
			monthlyRepayments = amount * (monthlyRate * rateFactor) / (rateFactor - 1);

			monthsTextBox.Text = $"Months: {monthsAmount}";
			monthlyInterestRateBox.Text = $"Monthly Interest Rate: {monthlyRate * 100:N2} %";
			monthlyRepaymentTextBox.Text = $"Monthly Repayment: $ {monthlyRepayments:N2}";
		}
	}
}
