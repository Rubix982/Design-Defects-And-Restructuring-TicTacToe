using k180307_DDR_A1.Library.Frontend;

namespace k180307_DDR_A1.Library.Backend
{
    public interface IGame
    {
        public bool AddPlayer(Player player);
     
        public bool IsGameWon();
    }
}