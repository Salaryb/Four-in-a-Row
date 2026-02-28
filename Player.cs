using System;
using System.Collections.Generic;

namespace Ex_05
{
    public class Player
    {
        private readonly bool r_IsComputer;
        public readonly ePlayer r_PlayerId;
        private readonly string r_Name;

        public int Score { get; set; }

        public Player(bool i_IsComputer, ePlayer i_PlayerId, string i_Name)
        {
            Score = 0;
            r_IsComputer = i_IsComputer;
            r_PlayerId = i_PlayerId;
            r_Name = i_Name;
        }

        public string Name
        {
            get { return r_Name; }
        }
        public bool IsComputer
        {
            get { return r_IsComputer; }
        }

        private bool isWinPossible(out int o_Column, Board i_GameBoard, ePlayer i_Player)
        {
            bool res = false;
            o_Column = -1;

            for(int currColumn = 0; currColumn < i_GameBoard.m_Cols; currColumn++)
            {
                if(i_GameBoard.DropToken(i_Player, currColumn) == eDropResult.Success)
                {
                    if(i_GameBoard.GetWinner() == i_Player)
                    {
                        i_GameBoard.UndoMove(currColumn);
                        o_Column = currColumn;
                        res = true;
                        break;
                    }

                    i_GameBoard.UndoMove(currColumn);
                }
            }

            return res;
        }


        public int GetComputerMove(Board i_GameBoard)
        {
            int result = 0, cell = 0;

            //PC win
            if(isWinPossible(out cell, i_GameBoard, ePlayer.Player2))
            {
                result = cell;
            }
            //Human enemy block (a win)
            else if(isWinPossible(out cell, i_GameBoard, ePlayer.Player1))
            {
                result = cell;
            }
            else
            {
                List<int> validMoves = i_GameBoard.GetValidMoves();
                Random rnd = new Random();
                int pureRandomOdd = rnd.Next(4);

                //25% to be Pure random Move
                if(pureRandomOdd == 0)
                {
                    result = validMoves[rnd.Next(validMoves.Count)];
                }
                //75% to aim for the center of the board. (3 most centered LEGAL moves (also randomly))
                else
                {
                    int middleIndex = validMoves.Count / 2;
                    int offset = rnd.Next(-1, 2);
                    int finalIndex = middleIndex + offset;

                    validMoves.Sort();

                    //Handles possible out of bounds scenarios
                    if(finalIndex < 0)
                    {
                        finalIndex = 0;
                    }

                    if(finalIndex >= validMoves.Count)
                    {
                        finalIndex = validMoves.Count - 1;
                    }

                    result = validMoves[finalIndex];
                }
            }

            return result;
        }
    }
}