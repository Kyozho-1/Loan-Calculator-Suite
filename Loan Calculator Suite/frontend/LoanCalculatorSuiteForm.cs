using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

using Loan_Calculator_Suite.backend;

namespace Loan_Calculator_Suite.frontend {
    public class LoanCalculatorSuiteForm : Form {
        private readonly int[] _windowDimensions = [800, 800];

        private TableLayoutPanel _loanInfoInputPanel;

        private TextBox _numOfYearsTextBox;
        private TextBox _annualRateTextBox;
        private TextBox _PVTextBox;
        private ComboBox _loanTypesComobBox;

        private Loan _loan;

        private Button _loanSummaryButton;
        
        public LoanCalculatorSuiteForm() {
            /* Creating GUIs */
            InitializeWindow();
            CreateLoanInputs();
            
            /* Adding GUI Functionality */
            AddButtonFunctionality();
        }
        
        private void InitializeWindow() {
            Text = "Loan Calculator Suite";
            Width = _windowDimensions[0];
            Height = _windowDimensions[1];
        }

        private void CreateLoanInputs() {
            string[] labelStrs = [
                    "Number of years to pay off loan",
                    "Annual rate",
                    "PV (Present Value/Principal)",
                    "Type of Loan"
                ];
            string[] loanTypes = ["Mortgage", "Auto", "Student"];

            Label[] labels = [new(), new(), new(), new()];

            _loanInfoInputPanel = new() {
                RowCount = 8,
                ColumnCount = 1,
                Dock = DockStyle.Left,
                AutoSize = true,
                Width = 350
            };

            _numOfYearsTextBox = new();
            _annualRateTextBox = new();
            _PVTextBox = new();
            _loanTypesComobBox = new();

            for (int i = 0; i < labelStrs.Length; i++) {
                labels[i].Text = labelStrs[i];
                labels[i].AutoSize = true;
            }

            this.Controls.Add(_loanInfoInputPanel);

            _loanInfoInputPanel.Controls.Add(labels[0], 0, 0);
            _loanInfoInputPanel.Controls.Add(_numOfYearsTextBox, 0, 1);

            _loanInfoInputPanel.Controls.Add(labels[1], 0, 2);
            _loanInfoInputPanel.Controls.Add(_annualRateTextBox, 0, 3);

            _loanInfoInputPanel.Controls.Add(labels[2], 0, 4);
            _loanInfoInputPanel.Controls.Add(_PVTextBox, 0, 5);

            _loanInfoInputPanel.Controls.Add(labels[3], 0, 6);
            _loanTypesComobBox.Items.AddRange(loanTypes);
            _loanInfoInputPanel.Controls.Add(_loanTypesComobBox, 0, 7);

            CreateLoanSummaryButton();

            _loanInfoInputPanel.BorderStyle = BorderStyle.FixedSingle;

            foreach (var label in labels) {
                label.Font = new Font("Arial", 10, FontStyle.Regular);
            }

            void CreateLoanSummaryButton() {
                _loanSummaryButton = new() {
                    Text = "Create Loan Summary",
                    BackColor = Color.Orchid,
                    AutoSize = true
                };

                _loanInfoInputPanel.Controls.Add(_loanSummaryButton, 0, 8);
            }
        }

        private void AddButtonFunctionality() {
            const string INVALID_DATA_EXCEPTION_STR = "Error! Please enter a number";

            var SLIGHTLY_LIGHT_RED = Color.FromArgb(255, 51, 51);

            _loanSummaryButton.Click += (s, e) => {
                try {
                    if (_numOfYearsTextBox.Text == string.Empty) {
                        _numOfYearsTextBox.BackColor = SLIGHTLY_LIGHT_RED;

                        MessageBox.Show(
                                "Please enter information in the number of years text box."
                            );
                    }
                } catch (InvalidDataException ide) {
                    MessageBox.Show(INVALID_DATA_EXCEPTION_STR);
                }
            };
        }
    }
}