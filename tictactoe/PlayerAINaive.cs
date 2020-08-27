using System;

namespace tictactoe
{
    public class PlayerAINaive : Player
    {
        public PlayerAINaive(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        // a tactic method will:
        // - get all legal moves and check whether whey satisfy a condition specific for that tactic
        // - sort them into two list - good moves (those that satisfy the condition), bad moves (rest)
        // - if good moves is empty, return (-1, -1) and try the next tactic
        // - else return 



        public override (int, int) GetNextMove(GameState state)
        {
            (int, int) bestMove = (-1, -1);



            throw new NotImplementedException();
        }
    }
}
