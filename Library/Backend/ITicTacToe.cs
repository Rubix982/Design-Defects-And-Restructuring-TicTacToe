using k180307_DDR_A1.Library.Frontend;
using k180307_DDR_A1.Library.Controller;

namespace k180307_DDR_A1.Library.Backend
{
    public interface ITicTacToe : IGame
    {
        public bool UpdateBoard(int position, string symbol);

        public Board FetchBoard();

        public Player FetchPlayer(int identity);

        public bool IsDrawAchieved();
    }
}