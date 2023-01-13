using System.Collections.Generic;
using System.Globalization;
using System.Collections.ObjectModel;
namespace Virus
{
    public abstract class player
    {
        public string Name {get; private set;}
        public int Id {get; private set;}
        public int Life {get; private set;}
        public List<Card> Hand {get; private set;}
        public abstract void Move(Game game);

        public player(List<Card> Hand, string Name, int Id, Game g)
        {
            this.Id = Id;
            this.Name = Name;
            this.Hand = Hand;
            this.Life = 70;  
        }

        public void QuitLive()
        {
            Life -= 10;
        }

    }
    //jugador de defensa ve si tiene curas en su mano, si puede jugarlas y en caso de que no tenga juega un virus

    public class DefensePlayer : player
    {
        public DefensePlayer(List<Card> Hand, int Live, string Name, int Id, Game g) : base(Hand, Name, Id, g){}

        public override void Move(Game game)
        {
            List<(Card, int)> Valid = new List<(Card, int)>();//la carta q desea jugar y lo q vale

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i] is Medicin)
                {
                    if(Hand[i].Organ[0] == 0)
                    {
                        Valid.Add((Hand[i],8));
                    }
                    if(Hand[i].Organ[0] == 1)
                    {
                        Valid.Add((Hand[0],10));
                    }
                    if(Hand[i].Organ[0] == 2)
                    {
                        Valid.Add((Hand[0],10));
                    }
                    if(Hand[i].Organ[0] == 3)
                    {
                        Valid.Add((Hand[0],11));
                    }
                    if(Hand[i].Organ[0] == 4)
                    {
                        Valid.Add((Hand[i],14));
                    }
                    if(Hand[i].Organ[0] == 5)
                    {
                        Valid.Add((Hand[i],18));
                    }
                 
                }
                else if (Hand[i] is Virus)
                {
                    if(Hand[i].Organ[0] == 0)
                    {
                        Valid.Add((Hand[i],3));
                    }
                    if(Hand[i].Organ[0] == 1)
                    {
                        Valid.Add((Hand[i],5));
                    }
                    if(Hand[i].Organ[0] == 2)
                    {
                        Valid.Add((Hand[i],5));
                    }
                    if(Hand[i].Organ[0] == 3)
                    {
                        Valid.Add((Hand[i],5));
                    }
                    if(Hand[i].Organ[0] == 4)
                    {
                        Valid.Add((Hand[i],7));
                    }
                    if(Hand[i].Organ[0] == 5)
                    {
                        Valid.Add((Hand[i],9));
                    }
                }
                else Valid.Add((Hand[i],4));
            }
            //*******escoger jugada con mas valor y ver si es valida en caso de q lo sea jugarla
            Sort.Order(Valid);
            bool ValidMove = game.ValidMove(Valid[1].Item1, Id, 3);

            //game.Play(Listplayer,Valido[1].Item2);
            //Hand.Remove(Valido[1].Item2);
            Hand.Add(game.Steal());
            
        }
    }

    public class AtackPlayer : player
    {
        public AtackPlayer(List<Card> Hand, int Live, string Name, int Id, Game g) : base(Hand, Name, Id, g){}

        public override void Move(Game game)
        {
            List<Tuple<int,Card,int>> Valido = new List<Tuple<int,Card, int>>();
            for (int i = 0; i < Hand.Count; i++)
            {
                List<List<int>> Valid = game.ValidMove(Hand[i], Id);
                if (Valid.Count == 0)
                {
                    if(Hand[i] is Virus)
                    {
                      Valido.Add(new Tuple<int, Card, int>(Valid[0][1],Hand[i],3));  //*****************************************
                    }
                    else Valido.Add(new Tuple<int, Card, int>(Valid.Item1, Hand[i],0));  

                }
            }
            Tuple<int,Card, int> temp;
            
            for (int i = 0; i < Valido.Count; i++)
            {
                for (int j = i+1; j < Valido.Count; j++)
                {
                    if(Valido[i].Item3 < Valido[j].Item3)
                    {
                        temp = Valido[i];
                        Valido[i] = Valido[j];
                        Valido[j] = temp;

                    }
                }
            }

            List<int> Listplayer = new List<int>();
            for (int i = 0; i < Valido.Count; i++)
            {
                Listplayer.Add(Valido[i].Item1);
            }

            Sort.Order(Valido);
            game.Play(Listplayer,Valido[1].Item2);
            Hand.Remove(Valido[1].Item2);
            Hand.Add(game.Steal());
            
            
        }
    }
    public class Sort//ordenar las cartas de la mano segun tus prioridades
    {
        public static void Order(List<(Card, int)> Valid)
        {
            (Card,int) temp;
            
            for (int i = 0; i < Valid.Count; i++)
            {
                for (int j = i+1; j < Valid.Count; j++)
                {
                    if(Valid[i].Item2 > Valid[j].Item2)
                    {
                        temp = Valid[i];
                        Valid[i] = Valid[j];
                        Valid[j] = temp;

                    }
                }
            }
        }
        
    }
}    
           