using System;
using System.Windows.Forms;
using System.Drawing;

namespace Ex_05
{
    public class FormGameSettings : Form
    {
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        private TextBox m_TextBoxPlayer1;
        private TextBox m_TextBoxPlayer2;
        private CheckBox m_CheckBoxPlayer2;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Button m_ButtonStart;

        public string Player1Name { get { return m_TextBoxPlayer1.Text; } }
        public string Player2Name
        {
            get
            {
                return m_CheckBoxPlayer2.Checked ? m_TextBoxPlayer2.Text : "Computer";
            }
        }
        public bool IsSinglePlayer { get { return !m_CheckBoxPlayer2.Checked; } }
        public int Rows { get { return (int)m_NumericUpDownRows.Value; } }
        public int Cols { get { return (int)m_NumericUpDownCols.Value; } }

        public FormGameSettings()
        {
            initializeForm();
            initializePlayers();
            initializeBoard();
            addControlsToForm();
        }

        private void initializeForm()
        {
            this.Text = "Game Settings";
            this.Size = new Size(250, 250);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;

            m_ButtonStart = new Button { Text = "Start!", Location = new Point(15, 170), Width = 200 };
            m_ButtonStart.Click += buttonStart_Click;
        }

        private void initializePlayers()
        {
            m_LabelPlayers = new Label { Text = "Players:", Location = new Point(10, 10) };

            m_LabelPlayer1 = new Label { Text = "Player 1:", Location = new Point(20, 35), AutoSize = true };
            m_TextBoxPlayer1 = new TextBox { Text = "Player 1", Location = new Point(120, 32), Width = 100 };

            m_CheckBoxPlayer2 = new CheckBox();
            m_CheckBoxPlayer2.Text = "Player 2:";
            m_CheckBoxPlayer2.Location = new Point(22, 60);
            m_CheckBoxPlayer2.AutoSize = true;
            m_CheckBoxPlayer2.Checked = false;
            m_CheckBoxPlayer2.CheckedChanged += checkBoxPlayer2_CheckedChanged;

            m_TextBoxPlayer2 = new TextBox();
            m_TextBoxPlayer2.Location = new Point(120, 58);
            m_TextBoxPlayer2.Width = 100;
            m_TextBoxPlayer2.Text = "[Computer]";
            m_TextBoxPlayer2.Enabled = false;
        }

        private void initializeBoard()
        {
            m_LabelBoardSize = new Label { Text = "Board Size:", Location = new Point(10, 100) };

            m_LabelRows = new Label { Text = "Rows:", Location = new Point(20, 125), AutoSize = true };
            m_NumericUpDownRows = new NumericUpDown { Minimum = 4, Maximum = 10, Value = 4, Location = new Point(60, 123), Width = 40 };

            m_LabelCols = new Label { Text = "Cols:", Location = new Point(110, 125), AutoSize = true };
            m_NumericUpDownCols = new NumericUpDown { Minimum = 4, Maximum = 10, Value = 7, Location = new Point(145, 123), Width = 40 };
        }

        private void addControlsToForm()
        {
            this.Controls.Add(m_LabelPlayers);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_TextBoxPlayer1);
            this.Controls.Add(m_CheckBoxPlayer2);
            this.Controls.Add(m_TextBoxPlayer2);
            this.Controls.Add(m_LabelBoardSize);
            this.Controls.Add(m_LabelRows);
            this.Controls.Add(m_NumericUpDownRows);
            this.Controls.Add(m_LabelCols);
            this.Controls.Add(m_NumericUpDownCols);
            this.Controls.Add(m_ButtonStart);
        }


        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayer2.Enabled = m_CheckBoxPlayer2.Checked;
            

            //Wipes bot Box whenever is unclicked
            if (m_CheckBoxPlayer2.Checked)
            {
                m_TextBoxPlayer2.Text = "";
            }
            else
            {
                m_TextBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
  
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormGameSettings
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormGameSettings";
            this.ResumeLayout(false);

        }
    }
}