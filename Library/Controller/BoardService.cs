using System;
using System.Collections.Generic;
using System.Linq;
using k180307_DDR_A1.Library.Services;

namespace k180307_DDR_A1.Library.Controller
{
    public class BoardService : IBoardService
    {
        private Board Board { get; }

        public BoardService(uint dimensions)
        {
            this.Board = new Board(dimension: dimensions);
        }

        public bool UpdateBoard(int position, string symbol)
        {
            var boardLength = this.Board.Cells.Length;
            var symbolPlaced = true;

            if (position < 1 || position > boardLength)
            {
                symbolPlaced = false;
                Console.WriteLine("Invalid Move: Position is out of bound\n" +
                                  "Press any key to continue...");

                Console.ReadKey();
            }
            else
            {
                if (this.Board.Cells[position - 1].Symbol == " ")
                {
                    this.Board.Cells[position - 1].Symbol = symbol;
                }
                else
                {
                    symbolPlaced = false;
                    Console.WriteLine("Invalid Move: Position is already taken\n" +
                                      "Press any key to continue...");

                    Console.ReadKey();
                }
            }

            return symbolPlaced;
        }

        public bool IsWinningMoveAchieved()
        {
            var validWinningPositions = new List<List<int>>
            {
                // Winning conditions for ...
                new List<int>() {1, 2, 3}, // ... 1st row
                new List<int>() {4, 5, 6}, // ... 2nd row
                new List<int>(){6, 7, 8}, // ... 3rd row
                new List<int>(){1, 4, 7}, // ... 1st col 
                new List<int>(){2, 5, 8}, // ... 2nd col
                new List<int>(){2, 5, 8}, // ... 3rd col
                new List<int>(){0, 4, 8}, // ... top-left to bottom-right
                new List<int>(){2, 4, 6} // ... top-right to bottom-left
            };

            var positionResult = false;

            foreach (var validWinningPosition in validWinningPositions)
            {
                var firstSymbol = this.Board.Cells[validWinningPosition[0]].Symbol;
                var secondSymbol = this.Board.Cells[validWinningPosition[1]].Symbol;
                var thirdSymbol = this.Board.Cells[validWinningPosition[2]].Symbol;
                
                if (firstSymbol != " " && secondSymbol != " " && thirdSymbol != " ")
                    positionResult = positionResult || (firstSymbol == secondSymbol &&
                                                        secondSymbol == thirdSymbol);
            }

            return positionResult;
        }

        public void ClearBoard()
        {
            for (var iterator = 0; iterator < this.Board.Cells.Length; ++iterator)
            {
                this.Board.Cells[iterator].Symbol = " ";
            }
        }

        public bool IsSpaceLeft()
        {
            return this.Board.Cells.Aggregate(false, (current, cell) =>
                    current || (cell.Symbol == " ")
                );
        }

        public bool IsDrawAchieved()
        {
            return !this.IsWinningMoveAchieved();
        }

        public Board FetchBoard()
        {
            return this.Board;
        }
    }
}