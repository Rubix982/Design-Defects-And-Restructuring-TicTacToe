namespace k180307_DDR_A1.Library.Frontend
{
    public struct Player
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public Player(string name, string symbol)
        {
            this.Name = name;
            this.Symbol = symbol;
        }
    };
}