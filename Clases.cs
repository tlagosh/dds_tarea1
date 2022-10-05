
public class SuperStar
{
    // Agregamos los atributos de la clase
    public string title;
    public int handSize;
    public int value;
    public string ability;

    // Agregamos el constructor de la clase
    public SuperStar(string title, int handSize, int value, string ability)
    {
        this.title = title;
        this.handSize = handSize;
        this.value = value;
        this.ability = ability;
    }
}

public class Jugador
{
    // Agregamos los atributos del jugador
    public List <Carta> hand;
    public SuperStar superStar;
    public List <Carta> arsenal;
    public List <Carta> ringArea;
    public List <Carta> graveyard;
    public int points;
    public string title;
    public int fortitude;

    // Agregamos el constructor del jugador
    public Jugador(string title, SuperStar superStar)
    {
        this.title = title;
        this.superStar = superStar;
        this.hand = new List<Carta>();
        this.arsenal = new List<Carta>();
        this.ringArea = new List<Carta>();
        this.graveyard = new List<Carta>();
        this.points = 0;
        this.fortitude = 0;
    }

    // Agregamos el método para agregar una carta a la mano
    public void AddCardHand(Carta carta)
    {
        this.hand.Add(carta);
    }

    // Agregamos el método para agregar una carta al arsenal
    public void AddCardArsenal(Carta carta)
    {
        this.arsenal.Add(carta);
    }

    // Agregamos el método para agregar una carta al ringArea
    public void PlayCarta(Carta carta)
    {
        this.ringArea.Add(carta);

        this.fortitude += carta.damage;
    }
    
    // Agregamos el método para descartar una carta
    public void AddCardGraveyard(Carta carta)
    {
        this.graveyard.Add(carta);
    }

    // Agregamos el método para mostrar las cartas de la mano
    public void ShowHandCards()
    {
        Console.WriteLine("Cartas en la mano:");
        foreach (Carta carta in this.hand)
        {
            Console.WriteLine(carta.title);
        }
    }

    // Agregamos el método para mostrar las cartas del arsenal
    public void ShowArsenalCards()
    {
        Console.WriteLine("Cartas en el arsenal:");
        foreach (Carta carta in this.arsenal)
        {
            Console.WriteLine(carta.title);
        }
    }

    // Agregamos el método para mostrar las cartas del ringArea
    public void ShowRingAreaCards()
    {
        Console.WriteLine("Cartas en el ringArea:");
        foreach (Carta carta in this.ringArea)
        {
            Console.WriteLine(carta.title);
        }
    }

    // Agregamos el método para mostrar las cartas del graveyard
    public void ShowGraveyardCards()
    {
        Console.WriteLine("Cartas en el graveyard:");
        foreach (Carta carta in this.graveyard)
        {
            Console.WriteLine(carta.title);
        }
    }

    // Agregamos el método para mostrar los puntos del jugador
    public void ShowPoints()
    {
        Console.WriteLine("Puntos del jugador:");
        Console.WriteLine(this.points);
    }

    // Agregamos el método para mostrar el title del jugador
    public void ShowTitle()
    {
        Console.WriteLine("Name del jugador:");
        Console.WriteLine(this.title);
    }

    // Agregamos el método para mostrar el superStar del jugador
    public void ShowSuperStar()
    {
        Console.WriteLine("SuperStar del jugador:");
        Console.WriteLine(this.superStar.title);
    }

    // Agregamos el método para mostrar el estado del jugador
    public void ShowEstado()
    {
        Console.WriteLine("Estado del jugador:");
        Console.WriteLine("Name: " + this.title);
        Console.WriteLine("Puntos: " + this.points);
        Console.WriteLine("SuperStar: " + this.superStar.title);
        Console.WriteLine("Cartas en la mano:");
        foreach (Carta carta in this.hand)
        {
            Console.WriteLine(carta.title);
        }
        Console.WriteLine("Cartas en el arsenal:");
        foreach (Carta carta in this.arsenal)
        {
            Console.WriteLine(carta.title);
        }
        Console.WriteLine("Cartas en el ringArea:");
        foreach (Carta carta in this.ringArea)
        {
            Console.WriteLine(carta.title);
        }
        Console.WriteLine("Cartas en el graveyard:");
        foreach (Carta carta in this.graveyard)
        {
            Console.WriteLine(carta.title);
        }
    }
}

public class Carta
{
    // Agregamos los atributos de la carta
    public string title;
    public string type;
    public List <string> effect;
    public int damage;

    // Agregamos el constructor de la carta
    public Carta(string title, string type, List <string> effect, int damage)
    {
        this.title = title;
        this.type = type;
        this.effect = effect;
        this.damage = damage;
    }

    // Agregamos el método para mostrar el title de la carta
    public void ShowTitle()
    {
        Console.WriteLine("Title de la carta:");
        Console.WriteLine(this.title);
    }

    // Agregamos el método para mostrar el type de la carta
    public void ShowType()
    {
        Console.WriteLine("Type de la carta:");
        Console.WriteLine(this.type);
    }

    // Agregamos el método para mostrar el effect de la carta
    public void ShowEffect()
    {
        Console.WriteLine("Effect de la carta:");
        foreach (string efecto in this.effect)
        {
            Console.WriteLine(efecto);
        }
    }

    // Agregamos el método para mostrar el damage de la carta
    public void ShowDamage()
    {
        Console.WriteLine("Damage de la carta:");
        Console.WriteLine(this.damage);
    }

    // Agregamos el método para mostrar el estado de la carta
    public void ShowEstado()
    {
        Console.WriteLine("Estado de la carta:");
        Console.WriteLine("Title: " + this.title);
        Console.WriteLine("Type: " + this.type);
        Console.WriteLine("Effect: ");
        foreach (string efecto in this.effect)
        {
            Console.WriteLine(efecto);
        }
        Console.WriteLine("Damage: " + this.damage);
    }

}