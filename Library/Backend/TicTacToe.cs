using System;
using System.Collections.Generic;

using k180307_DDR_A1.Library.Frontend;
using k180307_DDR_A1.Library.Controller;

namespace k180307_DDR_A1.Library.Backend
{
    public class TicTacToe : ITicTacToe
    {
        /* Class for conceptually representing everything that
         * is needed to make a TicTacToe game work
         *
         * Responsibilities:
         *  - Abstract logic for manipulating board and list of players
         */
        private BoardService BoardService { get; }
        private List<Player> Players { get; }

        public TicTacToe(uint dimensions)
        {
            this.Players = new List<Player>();
            this.BoardService = new BoardService(dimensions: dimensions);
        }

        public void ClearBoard()
        {
            this.BoardService.ClearBoard();
        }
        
        public bool AddPlayer(Player player)
        {
            this.Players.Add(player);
            return true;
        }

        public void ClearPlayerList()
        {
            this.Players.Clear();
        }

        public bool IsGameWon()
        {
            return BoardService.IsWinningMoveAchieved();
        }

        public bool UpdateBoard(int position, string symbol)
        {
            return this.BoardService.UpdateBoard(position: position, symbol: symbol);   
        }

        public bool IsSpaceLeft()
        {
            return this.BoardService.IsSpaceLeft();
        }

        public bool IsDrawAchieved()
        {
            return this.BoardService.IsDrawAchieved();
        }

        public Board FetchBoard()
        {
            // ! I'm afraid of inappropriate intimacy here
            // ! In this case, I would prefer turning the reference into
            // ! into a value type for services to deal with the board,
            // ! such as BoardPrinter
            return this.BoardService.FetchBoard();
        }

        public Player FetchPlayer(int identity)
        {
            // Detect out of range identifier
            if (identity > this.Players.Count || identity < 0)
                // Throw IndexOutOfRangeException is needed
                throw new IndexOutOfRangeException(message: $"[TicTacToe - FetchPlayer] Invalid player range given," +
                                                            $"{identity.ToString()}");

            // Return player indexed by identity
            return this.Players[identity];
        }
    }
}