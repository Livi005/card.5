namespace Virus
{
    public class Logic
    {
        public List<player> Players{get; private set;}
        public Game game{get; private set;}
        public int i{get; private set;}
        public Logic(List<player> Players, Game game)
        {
            this.Players = Players;
            this.game = game;
            i = 0;
        }
        
        public void Turns()
        {
            while(!game.EndGame())
            {
                Players[i%Players.Count].Move(game);
                i++;
            }

        }
    }

}
