namespace Virus
{
    public abstract class Card  //arreglar lo de las posiciones para saber segun la carta segun el organo q afecta.
    {
        public int[] Organ;
        public string Name{get; private set;}

        public abstract void Efect(Board Board, int[] ActionCard);
    
        public Card(string Name, int[] Organ)
        {
            this.Name = Name;
            this.Organ = Organ;
        }
    }
    public class Virus : Card
    {
        public override void Efect(Board Board, params int[] ActionCard)
        {

            Board.UpdateBoard(ActionCard[0], -20, 0, ActionCard[1], false);
        }

        public Virus(string Name,int[] Organ):base(Name, Organ){}
        
    }

    public class Medicin : Card
    {
        public override void Efect(Board Board, int[] ActionCard){}

        public Medicin(string Name,int[] Organ):base(Name,Organ){}
        
    }

    public class Pill : Medicin
    {
        public override void Efect(Board Board, int[] ActionCard)
        {
            Board.UpdateBoard(ActionCard[0], 20, 0, ActionCard[1], false);// organo afectado ,cant de punto a quitar ,inmunizacion, el jugador a dannar 
        }

        public Pill(string Name,int[] Organ):base(Name,Organ){}
        
    }

    public class SpecialCard : Medicin
    {
        public override void Efect(Board Board, int[] ActionCard)
        {
            Board.UpdateBoard(ActionCard[0], 0, 3, ActionCard[1], false);
        }

        public SpecialCard(string Name,int[] Organ):base(Name,Organ){}   
    }

    public class Vaccine : Medicin
    {
        public override void Efect(Board Board, int[] ActionCard)
        {
            Board.UpdateBoard(ActionCard[0], 15, 1, ActionCard[1], false);
        }

        public Vaccine(string Name,int[] Organ):base(Name,Organ){}
        
    }
    
   public abstract class Treatment : Card
   {
    
    public override abstract void Efect(Board Board, int[] ActionCard);
  
    public Treatment(string Name, int[] Organ):base(Name,Organ){}  
    

   }

}
