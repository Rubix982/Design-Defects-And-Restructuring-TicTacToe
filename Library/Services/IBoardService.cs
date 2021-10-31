using k180307_DDR_A1.Library.Controller;

namespace k180307_DDR_A1.Library.Services
{
    public interface IBoardService
    {
        // public bool ValidatePositions(bool current, List<int> validWinningPosition);
        
        public bool UpdateBoard(int position, string symbol);

        public bool IsWinningMoveAchieved();

        public Board FetchBoard();
    }
}