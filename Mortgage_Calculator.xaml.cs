using System;
using System.Collections.Generic;
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

		private void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			double loanAmount, annualRate;
			int loanYearsAmount;

			loanAmount = double.Parse(principalBorrowdTextBox.Text);
			annualRate = double.Parse(annualInterestRateTextBox.Text);
			loanYearsAmount = int.Parse(yearsTextBox.Text);

			loanCalculations(loanAmount, annualRate, loanYearsAmount);

			//monthsAmount = loanYearsAmount * 12;
			//monthlyRate = (annualRate / 12) / 100;
			//rateFactor = Math.Pow(1 + monthlyRate, monthsAmount);
			//monthlyRepayments = loanAmount * (monthlyRate * rateFactor) / (rateFactor - 1);

			//monthsTextBox.Text = monthsAmount.ToString();
			//monthlyInterestRateBox.Text = (monthlyRate*100).ToString();
			//monthlyRepaymentTextBox.Text = $"${monthlyRepayments:N2}";
		}

		private void loanCalculations(double amount, double anualRate, int yearsLength) {
			double monthlyRate, monthlyRepayments, rateFactor;
			int monthsAmount;
			monthsAmount = yearsLength * 12;
			monthlyRate = (anualRate / 12) / 100;
			rateFactor = Math.Pow(1 + monthlyRate, monthsAmount);
			monthlyRepayments = amount * (monthlyRate * rateFactor) / (rateFactor - 1);

			monthsTextBox.Text = monthsAmount.ToString();
			monthlyInterestRateBox.Text = (monthlyRate * 100).ToString();
			monthlyRepaymentTextBox.Text = $"${monthlyRepayments:N2}";
		}
	}
}
