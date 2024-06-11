using System;
using System.IO;
using System.Windows;

namespace FinancialGoalVisualizationTool
{
    public partial class MainWindow : Window
    {
        private readonly string[] financialQuotes = {
            "The best time to plant a tree was 20 years ago. The second best time is now.",
            "It's not how much money you make, but how much money you keep, how hard it works for you, and how many generations you keep it for.",
            "Don't save what is left after spending; spend what is left after saving.",
            "Investing should be more like watching paint dry or watching grass grow. If you want excitement, take $800 and go to Las Vegas.",
            "Formal education will make you a living; self-education will make you a fortune.",
            "The stock market is filled with individuals who know the price of everything, but the value of nothing.",
            "The four most dangerous words in investing are: 'This time it's different.'",
            "The most important quality for an investor is temperament, not intellect.",
            "The stock market is a device for transferring money from the impatient to the patient.",
            "It's not whether you're right or wrong, but how much money you make when you're right and how much you lose when you're wrong."
        };

        public MainWindow()
        {
            InitializeComponent();
            DisplayRandomQuote();
        }

        private void SetGoal_Click(object sender, RoutedEventArgs e)
        {
            // Get user inputs
            string goalName = txtGoalName.Text;
            string goalAmountStr = txtGoalAmount.Text;
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.Now;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.Now;

            // Validate goal amount
            if (!decimal.TryParse(goalAmountStr, out decimal goalAmount))
            {
                MessageBox.Show("Invalid goal amount. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate goal dates
            if (endDate < startDate)
            {
                MessageBox.Show("End date should be after start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create financial goal object
            FinancialGoal goal = new FinancialGoal
            {
                GoalName = goalName,
                GoalAmount = goalAmount,
                StartDate = startDate,
                EndDate = endDate
            };

            // Display goal details
            MessageBox.Show($"Financial goal set:\nName - {goal.GoalName}\nAmount - {goal.GoalAmount}\nStart Date - {goal.StartDate}\nEnd Date - {goal.EndDate}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Save output to file
            SaveOutputToFile($"Financial Goal: {goal.GoalName}\nAmount: {goal.GoalAmount}\nStart Date: {goal.StartDate}\nEnd Date: {goal.EndDate}");
        }

        private void DisplayRandomQuote()
        {
            Random rand = new Random();
            int index = rand.Next(financialQuotes.Length);
            txtFinancialQuote.Text = financialQuotes[index];
        }

        private void SaveOutputToFile(string output)
        {
            string filePath = "FinancialGoalOutput.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(output);

                    // If the goal includes savings per day, save the daily savings amount
                    if (dpEndDate.SelectedDate.HasValue && (dpEndDate.SelectedDate.Value - dpStartDate.SelectedDate.Value).TotalDays > 0)
                    {
                        decimal dailySavingsAmount = decimal.TryParse(txtGoalAmount.Text, out decimal goalAmount) ? goalAmount / (decimal)(dpEndDate.SelectedDate.Value - dpStartDate.SelectedDate.Value).TotalDays : 0;
                        writer.WriteLine($"Daily Savings Amount: {dailySavingsAmount:C} for the entire duration of the goal");
                    }
                    writer.WriteLine("------------------------------");
                }
                MessageBox.Show("Output saved to file successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving output to file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

