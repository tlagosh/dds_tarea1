
public class SuperStar
{
    // Agregamos los atributos de la clase
    public string Title;
    public int handSize;
    public int value;
    public string ability;
    public bool useBeforeDraw;

    // Agregamos el constructor de la clase
    public SuperStar(string Title)
    {
        this.Title = Title;
        if (Title == "StoneCold")
        {
            this.handSize = 7;
            this.value = 5;
            this.ability = "Once during your turn, you may draw a card, but you must then take a card from your hand and place it on the bottom of your Arsenal.";
            this.useBeforeDraw = false;
        }
        if (Title == "Undertaker")
        {
            this.handSize = 7;
            this.value = 5;
            this.ability = "Once during your turn, you may discard 2 cards to the Ringside pile and take 1 card from the Ringside pile and place it into your hand.";
            this.useBeforeDraw = false;
        }
        if (Title == "Mankind")
        {
            this.handSize = 7;
            this.value = 5;
            this.ability = "You must always draw 2 cards, if possible, during your draw segment. All damage from opponent is at -1D.";
            this.useBeforeDraw = true;
        }
        if (Title == "HHH")
        {
            this.handSize = 10;
            this.value = 3;
            this.ability = "None";
            this.useBeforeDraw = false;
        }
        if (Title == "TheRock")
        {
            this.handSize = 7;
            this.value = 5;
            this.ability = "At the start of your turn, before your draw segment, you may take 1 card from your Ringside pile and place it on the bottom of your Arsenal.";
            this.useBeforeDraw = true;
        }
        if (Title == "Kane")
        {
            this.handSize = 7;
            this.value = 2;
            this.ability = "At the start of your turn, before your draw segment, opponent must take the top card from his Arsenal and place it into his Ringside pile.";
            this.useBeforeDraw = true;
        }
        if (Title == "Jericho")
        {
            this.handSize = 7;
            this.value = 3;
            this.ability = "Once during your turn, you may discard a card from your hand to force your opponent to discard a card from his hand.";
            this.useBeforeDraw = false;
        }
        if (Title == "")
        {
            this.handSize = 0;
            this.value = 0;
            this.ability = "";
            this.useBeforeDraw = false;
        }
    }
}

public class Jugador
{
    // Agregamos los atributos del jugador
    public List <Card> hand;
    public SuperStar SuperStar;
    public List <Card> Arsenal;
    public List <Card> RingArea;
    public List <Card> GraveYard;
    public int Points;
    public string Title;
    public int Fortitude;

    // Agregamos el constructor del jugador
    public Jugador(string title, SuperStar superStar)
    {
        this.Title = title;
        this.SuperStar = superStar;
        this.hand = new List<Card>();
        this.Arsenal = new List<Card>();
        this.RingArea = new List<Card>();
        this.GraveYard = new List<Card>();
        this.Points = 0;
        this.Fortitude = 0;
    }

    // Agregamos el método para agregar una carta al RingArea
    public void PlayCard(Card carta)
    {
        this.RingArea.Add(carta);

        this.Fortitude += carta.Damage;
    }

    // Agregamos el método para mostrar las cartas de la mano
    public void ShowHandCards()
    {
        int number = 0;
        foreach (Card card in this.hand)
        {
            card.ShowCard(number);
            number++;
        }
    }

    // Agregamos el método para mostrar las cartas del Arsenal
    public void ShowArsenalCards()
    {
        int number = 0;
        foreach (Card card in this.Arsenal)
        {
            card.ShowCard(number);
            number++;
        }
    }

    // Agregamos el método para mostrar las cartas del RingArea
    public void ShowRingAreaCards()
    {
        int number = 0;
        foreach (Card card in this.RingArea)
        {
            card.ShowCard(number);
            number++;
        }
    }

    // Agregamos el método para mostrar las cartas del GraveYard
    public void ShowGraveYardCards()
    {
        int number = 0;
        foreach (Card card in this.GraveYard)
        {
            card.ShowCard(number);
            number++;
        }
    }

    // Agregamos el método para mostrar los puntos del jugador
    public void ShowPoints()
    {
        Console.WriteLine("Puntos del jugador:");
        Console.WriteLine(this.Points);
    }

    // Agregamos el método para mostrar el Title del jugador
    public void ShowTitle()
    {
        Console.WriteLine("Name del jugador:");
        Console.WriteLine(this.Title);
    }

    // Agregamos el método para mostrar el SuperStar del jugador
    public void ShowSuperStar()
    {
        Console.WriteLine("SuperStar del jugador:");
        Console.WriteLine(this.SuperStar.Title);
    }

    // Agregamos el método para mostrar el estado del jugador
    public void ShowEstado()
    {
        Console.WriteLine("Estado del jugador:");
        Console.WriteLine("Name: " + this.Title);
        Console.WriteLine("Puntos: " + this.Points);
        Console.WriteLine("SuperStar: " + this.SuperStar.Title);
        Console.WriteLine("Cartas en la mano:");
        foreach (Card carta in this.hand)
        {
            Console.WriteLine(carta.Title);
        }
        Console.WriteLine("Cartas en el Arsenal:");
        foreach (Card carta in this.Arsenal)
        {
            Console.WriteLine(carta.Title);
        }
        Console.WriteLine("Cartas en el RingArea:");
        foreach (Card carta in this.RingArea)
        {
            Console.WriteLine(carta.Title);
        }
        Console.WriteLine("Cartas en el GraveYard:");
        foreach (Card carta in this.GraveYard)
        {
            Console.WriteLine(carta.Title);
        }
    }

    // Método para robar una carta del arsenal y ponerla en la mano
    public void Draw()
    {
        if(this.Arsenal.Count > 0)
        {
            this.hand.Add(this.Arsenal[0]);
            this.Arsenal.RemoveAt(0);
        }
    }

    public void useSuperStarAbility()
    {
        // aplicar superstar ability
    }

}

public class Card
{
    // Agregamos los atributos de la carta
    public string Title;
    public List <string> Types;
    public List <string> Subtypes;
    public string CardEffect;
    public int Damage;
    public int Fortitude;
    public int StunValue;

    // Agregamos el constructor de la carta
    public Card(string title, List <string> types, List <string> subtypes, string cardEffect, int damage, int fortitude, int stunValue)
    {
        this.Title = title;
        this.Types = types;
        this.Subtypes = subtypes;
        this.CardEffect = cardEffect;
        this.Damage = damage;
        this.Fortitude = fortitude;
        this.StunValue = stunValue;
    }

    // Agregamos el método para mostrar los atributos de la carta
    public void ShowCard(int number)
    {
        Console.WriteLine("------------- Cartd #" + number);
        Console.WriteLine("Title: " + this.Title);
        Console.WriteLine("Stats: [" + this.Fortitude + "F/" + this.Damage + "D/" + this.StunValue + "SV]");
        Console.Write("Types: ");
        foreach (string type in this.Types)
        {
            Console.Write(type);
        }
        Console.WriteLine();
        Console.Write("Subtypes: ");
        foreach (string subtype in this.Subtypes)
        {
            Console.Write(subtype + ", ");
        }
        Console.WriteLine();
        Console.WriteLine("Effect: " + this.CardEffect);
    }

}

public class StringCard
{
    public string Title;
    public List <string> Types;
    public List <string> Subtypes;
    public string CardEffect;
    public string Damage;
    public string Fortitude;
    public string StunValue;
}