namespace tictactoe
{
    public abstract class PlayerBase
    {
        public char symbol;
        public string name;

        public PlayerBase(char symbol)
        {
            this.symbol = symbol;
            this.name = "Unknown player";
        }

        //public abstract (int, int) GetNextMove(char[,] gameBoard);
        public abstract bool IsHuman();
    }
}
