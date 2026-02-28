namespace Ex_05
{
    public class GameManager
    {
        private readonly int r_BoardHeight;
        private readonly int r_BoardLength;
        private readonly bool r_IsSinglePlayer;
        private readonly Board r_GameBoard;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;

        public GameManager(int i_Height, int i_Length, bool i_IsSinglePlayer, string i_PlayerOneName, string i_PlayerTwoName)
        {
            r_BoardHeight = i_Height;
            r_BoardLength = i_Length;
            r_IsSinglePlayer = i_IsSinglePlayer;

            r_GameBoard = new Board(r_BoardHeight, r_BoardLength);
            r_Player1 = new Player(false, ePlayer.Player1, i_PlayerOneName);
            r_Player2 = new Player(r_IsSinglePlayer, ePlayer.Player2, i_PlayerTwoName);

            m_CurrentPlayer = r_Player1;
        }

        public eDropResult PlayMove(int i_Column)
        {
            eDropResult result = r_GameBoard.DropToken(m_CurrentPlayer.r_PlayerId, i_Column);
            return result;
        }
        public Board GetBoard()
        {
            return r_GameBoard;
        }

        public Player GetCurrentPlayer()
        {
            return m_CurrentPlayer;
        }

        public Player GetPlayer1()
        {
            return r_Player1;
        }

        public Player GetPlayer2()
        {
            return r_Player2;
        }

        public void RestartGame()
        {
            r_GameBoard.Reset();
            m_CurrentPlayer = r_Player1;
        }
        public void SwitchTurn()
        {
            if (m_CurrentPlayer == r_Player1)
            {
                m_CurrentPlayer = r_Player2;
            }
            else
            {
                m_CurrentPlayer = r_Player1;
            }
        }

        public bool IsComputerTurn()
        {
            return m_CurrentPlayer.IsComputer;
        }

        public int GetComputerMove()
        {
            return m_CurrentPlayer.GetComputerMove(r_GameBoard);
        }

        public bool CheckForWin()
        {
            return r_GameBoard.GetWinner() == m_CurrentPlayer.r_PlayerId;
        }

        public bool IsBoardFull()
        {
            return r_GameBoard.IsBoardFull();
        }

        public void UpdateScoreForWinner()
        {
            m_CurrentPlayer.Score++;
        }
    }
}
