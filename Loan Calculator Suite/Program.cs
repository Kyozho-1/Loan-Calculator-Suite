using Loan_Calculator_Suite.backend;
using Loan_Calculator_Suite.frontend;

internal static class Program {
    [STAThread]
    static void Main() {
        ApplicationConfiguration.Initialize();
        Application.Run(new LoanCalculatorSuiteForm());
    }
}