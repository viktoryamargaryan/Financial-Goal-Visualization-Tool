using System;

namespace FinancialGoalVisualizationTool
{
    public class FinancialGoal
    {
        public decimal GoalAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public string Recommendations { get; internal set; }
        public string GoalName { get; internal set; }
    }
}
