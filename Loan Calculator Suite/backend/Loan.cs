namespace Loan_Calculator_Suite.backend {
    public class Loan(int years, decimal annualRate, decimal PV, Loan.LoanType loanType) {
        public enum LoanType : int {
            Mortgage,
            Auto,
            Student
        }
        /// <summary>
        /// Total number of payments.
        /// </summary>
        private readonly int n = years * 12;
        /// <summary>
        /// Interest rate per payment period.
        /// </summary>
        private readonly decimal r = (annualRate / 100m) / 12m;
        /// <summary>
        /// Monthly payment.
        /// </summary>
        private decimal PMT; 

        public decimal CalculatePMT() {
            if (r == 0m) {
                return PV / n;
            } else {
                decimal factor = (decimal)Math.Pow((double)(1m + r), -n);

                PMT = (r * PV) / (1m - factor);
            }
            
            return PMT;
        }

        public decimal CalculateTotalPaid() {
            return PMT * n;
        }

        public decimal CalculateTotalInterest() {
            decimal totalPaid = CalculateTotalPaid();

            return totalPaid - PV;
        }

        public int GetTotalNumOfPayments() {
            return n;
        }

        public override string ToString() {
            return $"Loan Type: {loanType}\n" +
                   $"PMT: ${CalculatePMT():0.00}\n" +
                   $"Total Amount Paid: ${CalculateTotalPaid():0.00}\n" +
                   $"Total Interest Paid: ${CalculateTotalInterest():0.00}\n" +
                   $"Total Number of Payments: {GetTotalNumOfPayments()}";
        }
    }
}