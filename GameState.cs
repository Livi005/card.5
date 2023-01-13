namespace Virus
{
    public class Game
    {
        public Queue<Card> Deck{get; private set;}
        public Queue<Card> GraveYard{get; private set;}
        public Board board{get; private set;}
        public  List<player> players{get; private set;}

        public Game(Board board, Queue<Card> Deck,List<player> players)
        {
            this.board = board;
            GraveYard = new Queue<Card>();
            this.players = players;
            this.Deck = Deck;
        }


        //al metodo le entra la carta q se desea jugar y el jugador q la activa, devuelve si es posible o no jugarla y en caso de serlo dice donde puede ser. 
        public bool ValidMove (Card card, int player,int Organ)
        {

            for (int j = 0; j < players.Count; j++)
            {
                if(card is Treatment)
                {               
                    if(card.Organ[0] == -1)
                    {
                        if (board.Cards[player,card.Organ[Organ]].Item2 > 0 || board.Cards[players[j].Id,Organ].Item2 < 0 || board.Cards[players[j].Id,Organ].Item2 >= 3)//si es el ladron tu organo no puede estar vivo
                        return false;
                    }
                    else if (card.Organ[0] == -2)//intercambio
                    {
                        for (int i = 3; i < Move.Length; i++)
                        {
                            if (board.Cards[players[j].Id,card.Organ[Organ]].Item2 < 0 || board.Cards[Move[1],Move[i]].Item1 < 0 || board.Cards[Move[1],Move[i]].Item2 >= 3)
                            return false;
                        } 
                    }
                    else if(card.Organ[0] == -3)
                    {
                        return true;
                    }

                }

                else if (position > 0 || position < 3)//saber si el organo esta vivo o no inmunizado
                {
                    if (card is Virus)
                    {
                        if (Move[0] != player)
                        return true;
                    }
                    else if (card is Vaccine)
                    {
                        if (Move[0] == player)
                        return true;
                    }
                    else if(card is Pill)
                    {
                        return true;
                    }

                }
            
                return false;
            }
            
        }

        public void Play(int[] ActionCard, Card card)
        {   
            card.Efect(board,ActionCard);

            GraveYard.Enqueue(card);

            //metodo para quitarle la vida general al jugador cada vez q se queda sin un organo
            
            for (int j = 0; j < ActionCard.Length; j+=2)
            {
                if(board.Cards[ActionCard[j+1],ActionCard[j]].Item2 <= 0)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        if(players[i].Id == ActionCard[1])
                        players[i].QuitLive();
                    }
                }
            }
             
        }

        public bool EndGame()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].Life <= 0 || board.Cards[players[i].Id,5].Item2 >= 3)
                return true;
            }
            return false;
        }

        public Card Steal()
        {
            return Deck.Dequeue();
        }

    }

}