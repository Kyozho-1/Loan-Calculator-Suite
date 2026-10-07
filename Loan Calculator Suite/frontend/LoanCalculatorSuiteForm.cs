using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

using Loan_Calculator_Suite.backend;

namespace Loan_Calculator_Suite.frontend {
    public class LoanCalculatorSuiteForm : Form {
        private readonly int[] _windowDimensions = [800, 800];

        private TableLayoutPanel _loanInfoInputPanel;

        private TextBox[] _loanInfoInputTextBoxes;
        private ComboBox _loanTypesComboBox;
        private Loan _loan;

        private Button _loanSummaryButton;

        private bool _errorFound = false;

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

            _loanInfoInputTextBoxes = [new(), new(), new()];
            _loanTypesComboBox = new();

            for (int i = 0; i < labelStrs.Length; i++) {
                labels[i].Text = labelStrs[i];
                labels[i].AutoSize = true;
            }

            this.Controls.Add(_loanInfoInputPanel);

            _loanInfoInputPanel.Controls.Add(labels[0], 0, 0);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[0], 0, 1);

            _loanInfoInputPanel.Controls.Add(labels[1], 0, 2);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[1], 0, 3);

            _loanInfoInputPanel.Controls.Add(labels[2], 0, 4);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[2], 0, 5);

            _loanInfoInputPanel.Controls.Add(labels[3], 0, 6);
            _loanTypesComboBox.Items.AddRange(loanTypes);
            _loanInfoInputPanel.Controls.Add(_loanTypesComboBox, 0, 7);

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
            const string NO_INFO_STR = "Error! Please enter some info the box(es).";

            var SLIGHTLY_LIGHT_RED = Color.FromArgb(255, 51, 51);

            _loanSummaryButton.Click += (s, e) => {
                try {
                    foreach (var txtBox in _loanInfoInputTextBoxes) {
                        txtBox.TextChanged += TextChanged;

                        if (txtBox.Text == string.Empty) {
                            txtBox.BackColor = SLIGHTLY_LIGHT_RED;

                            _errorFound = true;
                        }
                    }

                    // TODO: Check for the combo box's emptiness!

                    if (_errorFound) { MessageBox.Show(NO_INFO_STR); }
                } catch (InvalidDataException ide) {
                    MessageBox.Show(INVALID_DATA_EXCEPTION_STR);
                }
            };

            void TextChanged(object? sender, EventArgs e) {
                if (sender is TextBox txtBox) {
                    if (!string.IsNullOrEmpty(txtBox.Text)) {
                        txtBox.BackColor = Color.White;

                        _errorFound = false;
                    }
                }
            }
        }
    }
}