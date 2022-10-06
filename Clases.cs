
public class SuperStar
{
    // Agregamos los atributos de la clase
    public string Title;
    public int handSize;
    public int value;
    public string ability;

    // Agregamos el constructor de la clase
    public SuperStar(string Title, int handSize, int value, string ability)
    {
        this.Title = Title;
        this.handSize = handSize;
        this.value = value;
        this.ability = ability;
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

    // Agregamos el método para agregar una carta a la mano
    public void AddCardHand(Card carta)
    {
        this.hand.Add(carta);
    }

    // Agregamos el método para agregar una carta al Arsenal
    public void AddCardArsenal(Card carta)
    {
        this.Arsenal.Add(carta);
    }

    // Agregamos el método para agregar una carta al RingArea
    public void PlayCarta(Card carta)
    {
        this.RingArea.Add(carta);

        this.Fortitude += carta.Damage;
    }
    
    // Agregamos el método para descartar una carta
    public void AddCardGraveYard(Card carta)
    {
        this.GraveYard.Add(carta);
    }

    // Agregamos el método para mostrar las cartas de la mano
    public void ShowHandCards()
    {
        Console.WriteLine("Cartas en la mano:");
        foreach (Card carta in this.hand)
        {
            Console.WriteLine(carta.Title);
        }
    }

    // Agregamos el método para mostrar las cartas del Arsenal
    public void ShowArsenalCards()
    {
        Console.WriteLine("Cartas en el Arsenal:");
        foreach (Card carta in this.Arsenal)
        {
            Console.WriteLine(carta.Title);
        }
    }

    // Agregamos el método para mostrar las cartas del RingArea
    public void ShowRingAreaCards()
    {
        Console.WriteLine("Cartas en el RingArea:");
        foreach (Card carta in this.RingArea)
        {
            Console.WriteLine(carta.Title);
        }
    }

    // Agregamos el método para mostrar las cartas del GraveYard
    public void ShowGraveYardCards()
    {
        Console.WriteLine("Cartas en el GraveYard:");
        foreach (Card carta in this.GraveYard)
        {
            Console.WriteLine(carta.Title);
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
    public void ShowCard()
    {
        Console.WriteLine("Carta:");
        Console.WriteLine("Title: " + this.Title);
        Console.WriteLine("Types: " + this.Types);
        Console.WriteLine("Subtypes: " + this.Subtypes);
        Console.WriteLine("CardEffect: " + this.CardEffect);
        Console.WriteLine("Damage: " + this.Damage);
        Console.WriteLine("Fortitude: " + this.Fortitude);
        Console.WriteLine("StunValue: " + this.StunValue);
    }

    // Agregamos el método para mostrar el Title de la carta
    public void ShowTitle()
    {
        Console.WriteLine("Title de la carta:");
        Console.WriteLine(this.Title);
    }

    // Agregamos el método para mostrar el types de la carta
    public void ShowTypes()
    {
        Console.WriteLine("Types de la carta:");
        Console.WriteLine(this.Types);
    }

    // Agregamos el método para mostrar el subtypes de la carta
    public void ShowSubtypes()
    {
        Console.WriteLine("Subtypes de la carta:");
        Console.WriteLine(this.Subtypes);
    }

    // Agregamos el método para mostrar el cardEffect de la carta
    public void ShowCardEffect()
    {
        Console.WriteLine("CardEffect de la carta:");
        Console.WriteLine(this.CardEffect);
    }

    // Agregamos el método para mostrar el damage de la carta
    public void ShowDamage()
    {
        Console.WriteLine("Damage de la carta:");
        Console.WriteLine(this.Damage);
    }

    // Agregamos el método para mostrar el Fortitude de la carta
    public void ShowFortitude()
    {
        Console.WriteLine("Fortitude de la carta:");
        Console.WriteLine(this.Fortitude);
    }

    // Agregamos el método para mostrar el stunValue de la carta
    public void ShowStunValue()
    {
        Console.WriteLine("StunValue de la carta:");
        Console.WriteLine(this.StunValue);
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