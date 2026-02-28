using System.Collections.Generic;

namespace Ex_05
{
    public class Board
    {
        private ePlayer[,] m_Matrix;
        public int m_Rows { get { return m_Matrix.GetLength(0); } }
        public int m_Cols { get { return m_Matrix.GetLength(1); } }
        private int m_TokensCount = 0;

        public Board(int i_Rows, int i_Cols) //ctor
        {
            m_Matrix = new ePlayer[i_Rows, i_Cols];
        }

        public eDropResult DropToken(ePlayer i_Token, int i_Col)
        {
            eDropResult result = eDropResult.OutOfBounds;

            if (!(IsValidCol(i_Col)))
            {
                result = eDropResult.OutOfBounds;
            }
            else
            {
                if(!IsColumnFull(i_Col))
                {
                    result = eDropResult.Success;

                    for (int i = (m_Rows - 1); i >= 0; i--)
                    {
                        if(m_Matrix[i, i_Col] == ePlayer.None)
                        {
                            m_Matrix[i, i_Col] = i_Token;
                            m_TokensCount++;
                            break;
                        }
                    }
                }
                else
                    result = eDropResult.FullColumn;
            }

            return result;
        }

        public ePlayer GetWinner()
        {
            ePlayer winner = checkRowWin();
            if (winner == ePlayer.None)
            {
                winner = checkColWin();
            }
            if (winner == ePlayer.None)
            {
                winner = checkDiagonalWin();
            }

            return winner;
        }
        public ePlayer GetData(int i_RowIndex, int i_ColIndex)
        {
            return m_Matrix[i_RowIndex, i_ColIndex];
        }
        public bool IsBoardFull()
        {
            return (m_TokensCount == (m_Rows * m_Cols));
        }

        public bool IsColumnFull(int i_ColumnIndex)
        {
            return m_Matrix[0, i_ColumnIndex] != ePlayer.None;
        }

        public bool IsValidCol(int i_Col)
        {
            return (i_Col < m_Cols && (i_Col >= 0));
        }

        private bool isValidRow(int i_Row)
        {
            return (i_Row < m_Rows && (i_Row >= 0));
        }

        private ePlayer checkRowWin()
        {
            int playerOneStreak, playerTwoStreak, row = 0;
            ePlayer winner = ePlayer.None;

            while ((isValidRow(row)) && (winner == ePlayer.None))
            {
                //Init before checking next row
                playerOneStreak = 0;
                playerTwoStreak = 0;

                for (int i = 0; (i < m_Cols) && (winner == ePlayer.None); i++)
                {
                    if (m_Matrix[row, i] == ePlayer.Player1)
                    {
                        playerOneStreak++;
                        playerTwoStreak = 0;
                    }
                    else if (m_Matrix[row, i] == ePlayer.Player2)
                    {
                        playerOneStreak = 0;
                        playerTwoStreak++;
                    }
                    else
                    {
                        playerOneStreak = 0;
                        playerTwoStreak = 0;
                    }

                    if (playerOneStreak == 4)
                    {
                        winner = ePlayer.Player1;
                    }
                    else if (playerTwoStreak == 4)
                    {
                        winner = ePlayer.Player2;
                    }
                }
                row++;
            }
            return winner;
        }
        private ePlayer checkColWin()
        {
            int playerOneStreak, playerTwoStreak, col = 0;
            ePlayer winner = ePlayer.None;

            while ((IsValidCol(col)) && (winner == ePlayer.None))
            {
                //Init before checking next row
                playerOneStreak = 0;
                playerTwoStreak = 0;

                for (int i = 0; (i < m_Rows) && (winner == ePlayer.None); i++)
                {
                    if (m_Matrix[i, col] == ePlayer.Player1)
                    {
                        playerOneStreak++;
                        playerTwoStreak = 0;
                    }
                    else if (m_Matrix[i, col] == ePlayer.Player2)
                    {
                        playerOneStreak = 0;
                        playerTwoStreak++;
                    }
                    else
                    {
                        playerOneStreak = 0;
                        playerTwoStreak = 0;
                    }

                    if (playerOneStreak == 4)
                    {
                        winner = ePlayer.Player1;
                    }
                    else if (playerTwoStreak == 4)
                    {
                        winner = ePlayer.Player2;
                    }
                }
                col++;
            }
            return winner;
        }
        private ePlayer checkDiagonalWin()
        {
            ePlayer winner = ePlayer.None;
            int diagonalLength = 4;

            for (int i = 0; (i <= m_Rows - diagonalLength) && (winner == ePlayer.None); i++)
            {
                for (int j = 0; (j <= m_Cols - diagonalLength) && (winner == ePlayer.None); j++)
                {
                    ePlayer currentCell = m_Matrix[i, j];

                    if (currentCell != ePlayer.None)
                    {
                        if (m_Matrix[i + 1, j + 1] == currentCell &&
                            m_Matrix[i + 2, j + 2] == currentCell &&
                            m_Matrix[i + 3, j + 3] == currentCell)
                        {
                            winner = currentCell;
                        }
                    }
                }
            }

            for (int i = 0; (i <= (m_Rows - diagonalLength)) && (winner == ePlayer.None); i++)
            {
                for (int j = (diagonalLength - 1); (j < m_Cols) && (winner == ePlayer.None); j++)
                {
                    ePlayer current = m_Matrix[i, j];

                    if (current != ePlayer.None)
                    {
                        if (m_Matrix[i + 1, j - 1] == current &&
                            m_Matrix[i + 2, j - 2] == current &&
                            m_Matrix[i + 3, j - 3] == current)
                        {
                            winner = current;
                        }
                    }
                }
            }
            return winner;
        }

        public List<int> GetValidMoves()
        {
            List<int> validMoves = new List<int>();

            for (int c = 0; c < m_Cols; c++)
            {
                if (!IsColumnFull(c))
                    validMoves.Add(c);
            }

            return validMoves;
        }

        public void UndoMove(int i_Col)
        {
            for (int i = 0; i < m_Rows; i++)
            {
                if (m_Matrix[i, i_Col] != ePlayer.None)
                {
                    m_Matrix[i, i_Col] = ePlayer.None; 
                    m_TokensCount--;
                    break;
                }
            }
        }
        public void Reset()
        {
            m_TokensCount = 0;

            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    m_Matrix[i, j] = ePlayer.None;
                }
            }
        }

    }

}
