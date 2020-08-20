namespace tictactoe
{
    public abstract class Player
    {
        public char Symbol { get; private set; } = '_';
        public string Name { get; private set; } = "Default name";
        public bool IsHuman { get; private set; } = true;

        public Player(char symbol, string name, bool isHuman)
        {
            Symbol = symbol;
            Name = name;
            IsHuman = isHuman;
        }

        public abstract (int, int) GetNextMove(char[,] gameBoard);
    }
}
