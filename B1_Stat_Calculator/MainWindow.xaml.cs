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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace B1_Stat_Calculator //Peilin Chen
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

        private void cmdCalculate_Click(object sender, RoutedEventArgs e)
        {
            string inputNumbers = txtNumbers.Text;
            //Remove everything except numberic inputs and commas
            inputNumbers = Regex.Replace(inputNumbers, "[^0-9,.-]", "");
            //Remove multiple commas
            inputNumbers = Regex.Replace(inputNumbers, ",+,", ",");
            //Remove multiple dashes for negatives
            inputNumbers = Regex.Replace(inputNumbers, "-+-","-");
            //Remove comma in the beginning and replace with a space
            inputNumbers = Regex.Replace(inputNumbers, "^[,]", " ");
            //Remove comma at the end and replace with a space
            inputNumbers = Regex.Replace(inputNumbers, ",$", " ");
            //Store the string to an array
            double[] nums = inputNumbers.Split(',').Select(double.Parse).ToArray();


           //Outout the values on the calculator
           lblMean.Content = "Mean: " + Convert.ToString(mean(nums));
           lblMin.Content = "Min: " + Convert.ToString(min(nums));
           lblMax.Content = "Max: " + Convert.ToString(max(nums));
           lblRange.Content = "Range: " + Convert.ToString(range(nums));
           lblStandardDeviation.Content = "Sample Standard Deviation: " + Convert.ToString(standardDeviation(nums));
           lblMedian.Content = "Median: " + Convert.ToString(median(nums));
        }

        //Method to calculate the mean
        double mean(double[] numbers)
        {
            double sum = 0;
            //Sum the numbers
            for(int i = 0;i<numbers.Length;i++)
            {
                sum += numbers[i];
            }
            return sum / numbers.Length;
        }

        //Method to calculate the minimum
        double min(double[] numbers)
        {
            //Use built in LINQ function
            return numbers.Min();
        }

        //Method to calculate the maximum
        double max (double[] numbers)
        {
            //Use built in LINQ function
            return numbers.Max();
        }

        //Method to calculate the range
        double range (double[] numbers)
        { 
            return  max(numbers) - min(numbers) ;
        }

        //Method to calculate the median
        double standardDeviation (double[] numbers)
        {
            double difference = 0;
            double sum = 0;
            double variance = 0;
            //loop through to find (number-mean)^2
            for (int i = 0; i < numbers.Length;i++)
            {
                difference = Math.Pow(numbers[i] - mean(numbers), 2);
                sum += difference;
            }
            variance = sum / (numbers.Length - 1);
            return Math.Sqrt(variance);
        }

        //Method to calculate the median
        double median(double[] numbers)
        {
            Array.Sort(numbers);
            int numberLength = (numbers.Length % 2);
            double calcMed = 0;
            //if the array has even number of numbers
            if (numberLength % 2 == 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    double medianOne = numbers[(numbers.Length / 2) - 1];
                    double medianTwo = numbers[numbers.Length / 2];
                    //average of the two medians
                    calcMed = (medianOne + medianTwo) / 2;
                }
            } 
            //if the array has odd number of numbers
            else
            {
                calcMed = numbers[numbers.Length / 2];
            }
            return calcMed;
        }

        private void TxtNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
