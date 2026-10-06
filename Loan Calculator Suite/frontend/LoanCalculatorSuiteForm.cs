using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Loan_Calculator_Suite.frontend {
    public class LoanCalculatorSuiteForm : Form {
        private readonly int[] _windowDimensions = [800, 800];

        
        private readonly ToolStripButton[] _toolstripButtons = [new(), new(), new(), new()];
        
        private ToolStrip _toolStrip;
        private Panel _toolStripPanel;
        private TableLayoutPanel _loanInfoInputPanel;

        private TextBox _numOfYearsTextBox;
        private TextBox _annualRateTextBox;
        private TextBox _PVTextBox;
        private ComboBox _loanTypesComobBox;
        public LoanCalculatorSuiteForm() {
            InitializeWindow();
            CreateLoanInputs();
            CreateToolStrip(); /* ADD THIS LAST! */
        }
        
        private void InitializeWindow() {
            Text = "Loan Calculator Suite";
            Width = _windowDimensions[0];
            Height = _windowDimensions[1];
        }
        
        private void CreateToolStrip() {
            string[] buttonNames = ["Mortgage", "Auto", "Student", "Quit"];

            _toolStrip = new() {
                GripStyle = ToolStripGripStyle.Hidden
            };

            _toolStripPanel = new() {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Top,
                Height = 40
            };

            for (int i = 0; i < _toolstripButtons.Length; i++) {
                _toolstripButtons[i].Text = buttonNames[i];
            }

            /* Moving the Quit button to the far right of the toolstrip. */
            _toolstripButtons[^1].Alignment = ToolStripItemAlignment.Right;

            StylizeButtons();

            int sepCounter = 0;
            const int MAX_SEPERATORS = 2;

            foreach (var btn in _toolstripButtons) {
                _toolStrip.Items.Add(btn);

                if (sepCounter < MAX_SEPERATORS) {
                    _toolStrip.Items.Add(new ToolStripSeparator());
                }

                sepCounter++;
            }

            _toolStrip.Dock = DockStyle.Fill;

            this.Controls.Add(_toolStripPanel);
            _toolStripPanel.Controls.Add(_toolStrip);
            

            void StylizeButtons() {
                foreach (var btn in _toolstripButtons) {
                    btn.BackColor = Color.Silver;
                    btn.Padding = new Padding(4);
                    btn.AutoToolTip = false;
                }

                /* Coloring the Quit button to salmon. */
                _toolstripButtons[^1].BackColor = Color.Salmon;
            }
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
        }
    }
}