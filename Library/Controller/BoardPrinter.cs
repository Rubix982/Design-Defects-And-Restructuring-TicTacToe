using System;
using System.Runtime;

namespace k180307_DDR_A1.Library.Controller
{
    public class BoardPrinter
    {
        public static void PrintBoard(Board board)
        {
            var dimensions = (uint) Math.Sqrt(board.Cells.Length);

            for (var iteration = 0; iteration < board.Cells.Length; ++iteration)
            {
                Console.Write($" {board.Cells[iteration].Symbol} ");
                
                if ((iteration + 1) % dimensions == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("-----------");
                }
                else
                {
                    Console.Write("|");
                }
            }
        }
    }
}