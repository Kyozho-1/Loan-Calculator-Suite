using Loan_Calculator_Suite.backend;
using Microsoft.VisualBasic.Devices;

namespace Loan_Calculator_Suite.frontend {
    public class LoanCalculatorSuiteForm : Form {
        private readonly int[] _windowDimensions = [800, 800];

        private SplitContainer splitContainer;
        private TableLayoutPanel _loanInfoInputPanel;
        private TextBox[] _loanInfoInputTextBoxes;
        private ComboBox _loanTypesComboBox;
        private Loan _loan;
        private Button _loanSummaryButton;

        private readonly string DEFAULT_FONT = "Agency FB";

        private bool _errorFound = false;

        public LoanCalculatorSuiteForm() {
            /* Creating GUIs */
            InitializeWindow();
            CreateLoanInputs();
            
            /* Adding GUI Functionality */
            AddButtonFunctionality();
        }
        
        private void InitializeWindow() {
            this.Text = "Loan Calculator Suite";
            this.TopMost = true;

            /* This is for when the user makes the window smaller. */
            this.Width = _windowDimensions[0];
            this.Height = _windowDimensions[1];

            this.WindowState = FormWindowState.Maximized;
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

            splitContainer = new() {
                Orientation = Orientation.Vertical,
                Dock = DockStyle.Fill,
                Height = Screen.FromControl(this).WorkingArea.Height,
                SplitterDistance = labelStrs.Max(s => s.Length) - 5,
                SplitterWidth = 10,
                /* TODO: Fix width issue with 2nd panel. */
                BorderStyle = BorderStyle.FixedSingle
            };

            _loanInfoInputPanel = new() {
                AutoSize = true,
                Dock = DockStyle.Left,
                RowCount = 8
            };

            splitContainer.Panel1.Controls.Add(_loanInfoInputPanel);

            _loanInfoInputTextBoxes = [new(), new(), new()];
            _loanTypesComboBox = new();

            for (int i = 0; i < labelStrs.Length; i++) {
                labels[i].Text = labelStrs[i];
                labels[i].AutoSize = true;
            }

            _loanInfoInputPanel.Controls.Add(labels[0], 0, 0);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[0], 0, 1);
            _loanInfoInputPanel.Controls.Add(labels[1], 0, 2);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[1], 0, 3);

            _loanInfoInputPanel.Controls.Add(labels[2], 0, 4);
            _loanInfoInputPanel.Controls.Add(_loanInfoInputTextBoxes[2], 0, 5);

            _loanInfoInputPanel.Controls.Add(labels[3], 0, 6);
            _loanTypesComboBox.Items.AddRange(loanTypes);
            _loanInfoInputPanel.Controls.Add(_loanTypesComboBox, 0, 7);

            /* Setting the default selected item. */
            _loanTypesComboBox.SelectedItem = loanTypes[0];

            /* This makes the combo box uneditable. */
            _loanTypesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            _loanTypesComboBox.TextChanged += TextChanged;
            
            foreach (TextBox txtBox in _loanInfoInputTextBoxes) {
                txtBox.TextChanged += TextChanged;
            }

            CreateLoanSummaryButton();
            StylizeForm();

            this.Controls.Add(splitContainer);

            void CreateLoanSummaryButton() {
                _loanSummaryButton = new() {
                    Text = "Create Loan Summary",
                    BackColor = CustomColors.MagentaBloom,
                    AutoSize = true
                };

                _loanInfoInputPanel.Controls.Add(_loanSummaryButton, 0, 8);
            }

            void StylizeForm() {
                _loanInfoInputPanel.BorderStyle = BorderStyle.FixedSingle;

                foreach (var label in labels) {
                    label.Font = new(DEFAULT_FONT, 16, FontStyle.Regular);
                }

                foreach (var txtBox in _loanInfoInputTextBoxes) {
                    txtBox.BorderStyle = BorderStyle.FixedSingle;
                    txtBox.BackColor = Color.LightGray;
                    txtBox.Size = new() { Width = 150 };
                    txtBox.Font = new(DEFAULT_FONT, 12, FontStyle.Regular);
                }

                _loanSummaryButton.Font = new(DEFAULT_FONT, 14, FontStyle.Regular);
                _loanSummaryButton.Margin = new(0, 20, 0, 0);
                _loanSummaryButton.Cursor = Cursors.Hand;

                _loanTypesComboBox.Font = new(DEFAULT_FONT, 14, FontStyle.Regular);
                _loanTypesComboBox.BackColor = Color.LightGray;
                _loanTypesComboBox.Cursor = Cursors.Hand;

                splitContainer.Panel1.BackColor = CustomColors.GhostWhite;
                splitContainer.Panel2.BackColor = CustomColors.GhostWhite;
            }
        }

        private void AddButtonFunctionality() {
            const string INVALID_DATA_EXCEPTION_STR = "Error! Please enter a number in the box(s).";
            const string NO_INFO_STR = "Error! Please enter some info the box(es).";

            _loanSummaryButton.Click += (s, e) => {
                try {
                    _errorFound = false;

                    foreach (var txtBox in _loanInfoInputTextBoxes) {
                        if (txtBox.Text == string.Empty) {
                            txtBox.BackColor = Color.Red;

                            _errorFound = true;
                        }
                    }

                    if (_errorFound) { 
                        MessageBox.Show(NO_INFO_STR);
                    } else {
                        int years = int.Parse(_loanInfoInputTextBoxes[0].Text);
                        decimal annualRate = decimal.Parse(_loanInfoInputTextBoxes[1].Text);

                        if (_loanInfoInputTextBoxes[2].Text.Contains(',')) {
                            _loanInfoInputTextBoxes[2].Text = _loanInfoInputTextBoxes[2].Text.Replace(",", "");
                        }

                        decimal PV = decimal.Parse(_loanInfoInputTextBoxes[2].Text);
                        Loan.LoanType loanType = Enum.Parse<Loan.LoanType>(_loanTypesComboBox.SelectedItem.ToString());
                        
                        _loan = new(years, annualRate, PV, loanType);

                        Console.WriteLine(_loan);

                        //CreateLoanSummary();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(INVALID_DATA_EXCEPTION_STR);
                }
            };
        }

        private void CreateLoanSummary() {
            TableLayoutPanel loanSummary = new() {
                Dock = DockStyle.Left,
                ColumnCount = 2,
                RowCount = 6,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            string[] ROW_DETAILS = [
                "PMT", "Total Interest", "Total Amount Paid", 
                "Total Number of Payments", "Loan Type"
            ];

            for (int i = 0; i < ROW_DETAILS.Length; i++) {
                loanSummary.Controls.Add(
                    new Label() {
                        Text = ROW_DETAILS[i],
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = false
                    },
                    0, i
                );

                loanSummary.Controls.Add(
                    new Label() {
                        Text = "",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = false
                    },
                    1, i
                );
            }

            this.Controls.Add(loanSummary);
        }
        void TextChanged(object? sender, EventArgs e) {
            if (sender is TextBox txtBox) {
                if (!string.IsNullOrEmpty(txtBox.Text)) {
                    txtBox.BackColor = Color.LightGray;

                    _errorFound = false;
                }
            }
        }
    }
}