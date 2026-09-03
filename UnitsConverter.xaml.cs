using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class UnitsConverter : Page
	{
		public UnitsConverter()
		{
			this.InitializeComponent();
		}

		private void ConversionTypeComboBox_SelectionChanged(
			object sender,
			SelectionChangedEventArgs e)
		{
			FromUnitComboBox.Items.Clear();
			ToUnitComboBox.Items.Clear();

			if (ConversionTypeComboBox.SelectedItem == null)
				return;

			string conversionType =
				((ComboBoxItem)ConversionTypeComboBox.SelectedItem).Content.ToString();

			switch (conversionType)
			{
				case "Temperature":
					FromUnitComboBox.Items.Add("Celsius");
					FromUnitComboBox.Items.Add("Fahrenheit");

					ToUnitComboBox.Items.Add("Celsius");
					ToUnitComboBox.Items.Add("Fahrenheit");
					break;

				case "Distance":
					FromUnitComboBox.Items.Add("Meter");
					FromUnitComboBox.Items.Add("Foot");

					ToUnitComboBox.Items.Add("Meter");
					ToUnitComboBox.Items.Add("Foot");
					break;

				case "Mass":
					FromUnitComboBox.Items.Add("Kilogram");
					FromUnitComboBox.Items.Add("Pound");

					ToUnitComboBox.Items.Add("Kilogram");
					ToUnitComboBox.Items.Add("Pound");
					break;

				case "Pressure":
					FromUnitComboBox.Items.Add("kPa");
					FromUnitComboBox.Items.Add("PSI");

					ToUnitComboBox.Items.Add("kPa");
					ToUnitComboBox.Items.Add("PSI");
					break;
			}

			FromUnitComboBox.SelectedIndex = 0;
			ToUnitComboBox.SelectedIndex = 1;
		}

		private void ConvertButton_Click(object sender, RoutedEventArgs e)
		{
			double value;

			if (!double.TryParse(InputValueTextBox.Text, out value))
			{
				ResultTextBlock.Text = "Please enter a valid number.";
				return;
			}

			if (FromUnitComboBox.SelectedItem == null ||
				ToUnitComboBox.SelectedItem == null)
			{
				ResultTextBlock.Text = "Please select the units.";
				return;
			}

			string fromUnit = FromUnitComboBox.SelectedItem.ToString();
			string toUnit = ToUnitComboBox.SelectedItem.ToString();

			double result = ConvertUnits(value, fromUnit, toUnit);

			string conversionType =
				((ComboBoxItem)ConversionTypeComboBox.SelectedItem).Content.ToString();

			if (conversionType == "Temperature")
			{
				ResultTextBlock.Text = result.ToString("0.0");
			}
			else if (conversionType == "Distance")
			{
				ResultTextBlock.Text = result.ToString("0.0000");
			}
			else
			{
				ResultTextBlock.Text = result.ToString("0.########");
			}
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}

		// Main conversion method
		private double ConvertUnits(
			double value,
			string fromUnit,
			string toUnit)
		{
			if (fromUnit == toUnit)
				return value;

			// Temperature
			if (fromUnit == "Celsius" && toUnit == "Fahrenheit")
				return CelsiusToFahrenheit(value);

			if (fromUnit == "Fahrenheit" && toUnit == "Celsius")
				return FahrenheitToCelsius(value);

			// Distance
			if (fromUnit == "Meter" && toUnit == "Foot")
				return MeterToFoot(value);

			if (fromUnit == "Foot" && toUnit == "Meter")
				return FootToMeter(value);

			// Mass
			if (fromUnit == "Kilogram" && toUnit == "Pound")
				return KilogramToPound(value);

			if (fromUnit == "Pound" && toUnit == "Kilogram")
				return PoundToKilogram(value);

			// Pressure
			if (fromUnit == "kPa" && toUnit == "PSI")
				return KpaToPsi(value);

			if (fromUnit == "PSI" && toUnit == "kPa")
				return PsiToKpa(value);

			return 0;
		}

		// Temperature
		private double CelsiusToFahrenheit(double celsius)
		{
			return (celsius * 1.8) + 32;
		}

		private double FahrenheitToCelsius(double fahrenheit)
		{
			return (fahrenheit - 32) / 1.8;
		}

		// Distance
		private double MeterToFoot(double meter)
		{
			return meter / 0.3048;
		}

		private double FootToMeter(double foot)
		{
			return foot * 0.3048;
		}

		// Mass
		private double KilogramToPound(double kilogram)
		{
			return kilogram / 0.45359237;
		}

		private double PoundToKilogram(double pound)
		{
			return pound * 0.45359237;
		}

		// Pressure
		private double KpaToPsi(double kpa)
		{
			return kpa / 6.89475729;
		}

		private double PsiToKpa(double psi)
		{
			return psi * 6.89475729;
		}
	}
}