using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Loan_Calculator_Suite.frontend {
    public class LoanCalculatorSuiteForm : Form {
        private int[] WINDOW_DIMENSIONS = [500, 500];

        public LoanCalculatorSuiteForm() {
            InitializeWindow();
        }

        private void InitializeWindow() {
            Text = "Loan Calculator Suite";
            Width = WINDOW_DIMENSIONS[0];
            Height = WINDOW_DIMENSIONS[1];
        }
    }
}