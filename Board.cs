namespace Virus
{
    public class Board// eltablero solo contiene los organos del jugador 
    {
        public Tuple<int, int>[,] Cards{get; private set;}

        public Board(int cantPlayer)
        {
          
            this.Cards = new Tuple<int, int>[cantPlayer,6];

            for (int i = 0; i < cantPlayer; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Cards[i,j] = new Tuple<int, int>((33 + j) * (j+1), 0);// le da valor a los organos de la mesa segun su posicion
                    
                } 
                
            }
            
        }
        //(int position1,int position2,int position3, int efect1, int efect2,int efect3,int efect4, int player, bool steal)

        public void UpdateBoard(int position1,int efect1,int efect2, int player, bool steal)// sirve para q el tablero se actualice segun la jugada q se hizo
        {
            
            if (!steal && Cards[player, position1].Item1 < 0 || Cards[player, position1].Item2 == 3) 
            throw new Exception("Su jugada no es valida :)");
            
            Cards[player, position1] = new Tuple<int, int>(Cards[player, position1].Item1 + efect1, Cards[player, position1].Item2 + efect2);
           
        }

        
    }


}