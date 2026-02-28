using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex_05
{
    public class FormGame : Form
    {
        private const int k_ButtonSize = 50;
        private const int k_ButtonSpaces = 10;
        private const int k_BoardHeight = 60;

        private readonly GameManager r_GameManager;
        private Button[] m_ColumnButtons;
        private Button[,] m_BoardButtons;
        private Label m_LabelPlayer1Score;
        private Label m_LabelPlayer2Score;

        public FormGame(FormGameSettings i_Settings)
        {
            r_GameManager = new GameManager(
                i_Settings.Rows,
                i_Settings.Cols,
                i_Settings.IsSinglePlayer,
                i_Settings.Player1Name,
                i_Settings.Player2Name);

            initializeGameComponent();
        }

        private void initializeGameComponent()
        {
            this.Text = "4 in a Row !!";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            int rows = r_GameManager.GetBoard().m_Rows;
            int cols = r_GameManager.GetBoard().m_Cols;

            int clientWidth = (cols * k_ButtonSize) + ((cols + 1) * k_ButtonSpaces);
            int clientHeight = (rows * (k_ButtonSize + k_ButtonSpaces)) + k_BoardHeight + k_ButtonSize;
            this.ClientSize = new Size(clientWidth, clientHeight);

            m_ColumnButtons = new Button[cols];
            for (int col = 0; col < cols; col++)
            {
                Button colButton = new Button();
                colButton.Text = (col + 1).ToString();
                colButton.Size = new Size(k_ButtonSize, 30);
                colButton.Location = new Point(k_ButtonSpaces + (col * (k_ButtonSize + k_ButtonSpaces)), k_ButtonSpaces);
                colButton.Tag = col;
                colButton.Click += new EventHandler(columnButton_Click);

                this.Controls.Add(colButton);
                m_ColumnButtons[col] = colButton;
            }

            m_BoardButtons = new Button[rows, cols];

            for (int currentRow = 0; currentRow < rows; currentRow++)
            {
                for (int currentCol = 0; currentCol < cols; currentCol++)
                {
                    Button cellButton = new Button();
                    cellButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    cellButton.Location = new Point(
                        k_ButtonSpaces + (currentCol * (k_ButtonSize + k_ButtonSpaces)),
                        k_BoardHeight + (currentRow * (k_ButtonSize + k_ButtonSpaces)));
                    cellButton.Enabled = false;
                    cellButton.BackColor = Color.LightGray;
                    cellButton.Font = new Font("Arial", 12, FontStyle.Bold);

                    this.Controls.Add(cellButton);
                    m_BoardButtons[currentRow, currentCol] = cellButton;
                }
            }

            int labelY = clientHeight - 30;

            m_LabelPlayer1Score = new Label();
            m_LabelPlayer1Score.Text = string.Format("{0}: 0", r_GameManager.GetPlayer1().Name);
            m_LabelPlayer1Score.Location = new Point(k_ButtonSpaces, labelY);
            m_LabelPlayer1Score.AutoSize = true;
            this.Controls.Add(m_LabelPlayer1Score);

            m_LabelPlayer2Score = new Label();
            m_LabelPlayer2Score.Text = string.Format("{0}: 0", r_GameManager.GetPlayer2().Name);
            m_LabelPlayer2Score.Location = new Point(clientWidth / 2 + k_ButtonSpaces, labelY);
            m_LabelPlayer2Score.AutoSize = true;
            this.Controls.Add(m_LabelPlayer2Score);
        }

        private void columnButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int colIndex = (int)clickedButton.Tag;

            handleMove(colIndex);

            if (r_GameManager.IsComputerTurn())
            {
                int computerMove = r_GameManager.GetComputerMove();
                handleMove(computerMove);
            }
        }

        private void handleMove(int i_ColIndex)
        {
            eDropResult result = r_GameManager.PlayMove(i_ColIndex);

            updateBoardUI();

            if (checkGameStatus())
            {
                return;
            }

            r_GameManager.SwitchTurn();
        }

        private void updateBoardUI()
        {
            Board board = r_GameManager.GetBoard();

            for (int currRow = 0; currRow < board.m_Rows; currRow++)
            {
                for (int currCol = 0; currCol < board.m_Cols; currCol++)
                {
                    ePlayer cellValue = board.GetData(currRow, currCol);
                    if (cellValue == ePlayer.Player1)
                    {
                        m_BoardButtons[currRow, currCol].Text = "X";
                    }
                    else if (cellValue == ePlayer.Player2)
                    {
                        m_BoardButtons[currRow, currCol].Text = "O";
                    }
                    else
                    {
                        m_BoardButtons[currRow, currCol].Text = "";
                    }
                }
            }

            for (int CurrCol = 0; CurrCol < board.m_Cols; CurrCol++)
            {
                m_ColumnButtons[CurrCol].Enabled = !board.IsColumnFull(CurrCol);
            }
        }

        private bool checkGameStatus()
        {
            bool isGameOver = false;
            string title = "";
            string message = "";
            MessageBoxIcon icon = MessageBoxIcon.Information;

            if (r_GameManager.CheckForWin())
            {
                isGameOver = true;
                r_GameManager.UpdateScoreForWinner();
                title = "A Win!";
                message = string.Format("{0} Won!!{1}Another Round?", r_GameManager.GetCurrentPlayer().Name, Environment.NewLine);
            }
            else if (r_GameManager.IsBoardFull())
            {
                isGameOver = true;
                title = "A Tie!";
                message = "Tie!!" + Environment.NewLine + "Another Round?";
            }

            if (isGameOver)
            {
                m_LabelPlayer1Score.Text = string.Format("{0}: {1}", r_GameManager.GetPlayer1().Name, r_GameManager.GetPlayer1().Score);
                m_LabelPlayer2Score.Text = string.Format("{0}: {1}", r_GameManager.GetPlayer2().Name, r_GameManager.GetPlayer2().Score);

                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, icon);

                if (result == DialogResult.Yes)
                {
                    r_GameManager.RestartGame();
                    updateBoardUI();

                    foreach (Button currButton in m_ColumnButtons)
                    {
                        currButton.Enabled = true;
                    }
                }
                else
                {
                    this.Close();
                }
            }

            return isGameOver;
        }
    }
}