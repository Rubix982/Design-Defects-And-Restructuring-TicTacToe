using System;

namespace k180307_DDR_A1.Library.Controller
{
    public struct Board 
    {
        public Grid[] Cells { get; set; }

        public Board(uint dimension)
        {
            this.Cells = new Grid[dimension * dimension];

            for (var iteration = 0; iteration < dimension * dimension; ++iteration)
                this.Cells[iteration].Symbol = " ";
        }
    }
}