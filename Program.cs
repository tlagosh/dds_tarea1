
public class Game
{
    // instanciamos los atributos del juego
    public List<Card> Cards = new List<Card>();
    public Jugador Jugador1;
    public Jugador Jugador2;

    // instanciamos el constructor del juego
    public Game()
    {
        // Creamos las cartas
        string cards_json = File.ReadAllText("./cards/cards.json");
        var cards = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StringCard>>(cards_json);
        Console.WriteLine(cards.Count + " cartas cargadas");
        Console.WriteLine("Title: " + cards[0].Title);
        Console.WriteLine("Types: " + cards[0].Types);
        Console.WriteLine("Subtypes: " + cards[0].Subtypes);
        Console.WriteLine("Damage: " + cards[0].Damage);
        Console.WriteLine("Fortitude: " + cards[0].Fortitude);
        Console.WriteLine("StunValue: " + cards[0].StunValue);
        Console.WriteLine("Effect: " + cards[0].CardEffect);

        foreach (var card in cards)
        {
            int damage = 0;
            if (Int32.TryParse(card.Damage, out damage))
            {
            }
            else
            {
                damage = -1;
            }
            Card newCard = new Card(card.Title, card.Types, card.Subtypes, card.CardEffect, damage, Int32.Parse(card.Fortitude), Int32.Parse(card.StunValue));
            this.Cards.Add(newCard);
        }

        // Creamos los jugadores
        this.Jugador1 = new Jugador("Jugador 1", new SuperStar("", 0, 0, ""));
        this.Jugador2 = new Jugador("Jugador 2", new SuperStar("", 0, 0, ""));
    }

    // Creamos el flujo del juego
    public void GameFlow()
    {
        // Hacemos a los Jugadores elegir su Mazo
        Console.WriteLine("--------------------------");
        if (this.ChooseDeck(this.Jugador1))
        {
            Console.WriteLine("--------------------------");
        }
        else
        {
            Console.WriteLine("El jugador 1 no eligió un mazo válido");
            return;
        }
        if (this.ChooseDeck(this.Jugador2))
        {
            Console.WriteLine("--------------------------");
        }
        else
        {
            Console.WriteLine("El jugador 2 no eligió un mazo válido");
            return;
        }
        Console.WriteLine("--------------------------");
        Console.WriteLine("Se enfrentan: " + this.Jugador1.SuperStar.Title + " y " + this.Jugador2.SuperStar.Title);
        Console.WriteLine(this.Jugador1.SuperStar.Title + " tiene " + this.Jugador1.Fortitude + "F, " + this.Jugador1.hand.Count + " cartas en su mano y le quedan " + this.Jugador1.Arsenal.Count + " cartas en su arsenal.");
        Console.WriteLine(this.Jugador2.SuperStar.Title + " tiene " + this.Jugador2.Fortitude + "F, " + this.Jugador2.hand.Count + " cartas en su mano y le quedan " + this.Jugador2.Arsenal.Count + " cartas en su arsenal.");
        Console.WriteLine("--------------------------");
    }

    // hacemos al jugador 1 elegir su mazo
    public bool ChooseDeck(Jugador jugador)
    {
        Console.WriteLine("Elige alguno de estos mazos");
        // leemos los mazos disponibles en cards/decks
        string[] mazos = Directory.GetFiles("cards/decks");
        // mostramos los mazos disponibles
        for (int i = 0; i < mazos.Length; i++)
        {
            Console.WriteLine(i + 1 + ". " + mazos[i]);
        }
        Console.WriteLine("(Ingresa un número del 1 al " + mazos.Length + ")");
        // leemos la opción elegida
        int opcion = int.Parse(Console.ReadLine());
        // leemos el archivo del mazo elegido
        string[] mazo = File.ReadAllLines(mazos[opcion - 1]);
        // leemos la primera linea, que indica la superestrella
        string superStar = mazo[0].Split('(')[0];
        if (superStar == "HHH ")
        {
            jugador.SuperStar = new SuperStar("HHH", 10, 3, "");
        }
        else if (superStar == "CHRIS JERICHO ")
        {
            jugador.SuperStar = new SuperStar("Jericho", 7, 3, "");
        }
        else if (superStar == "KANE ")
        {
            jugador.SuperStar = new SuperStar("Kane", 7, 3, "");
        }
        else if (superStar == "MANKIND ")
        {
            jugador.SuperStar = new SuperStar("Mankind", 7, 3, "");
        }
        else if (superStar == "STONE COLD STEVE AUSTIN ")
        {
            jugador.SuperStar = new SuperStar("StoneCold", 7, 3, "");
        }
        else if (superStar == "THE ROCK ")
        {
            jugador.SuperStar = new SuperStar("TheRock", 7, 3, "");
        }
        else if (superStar == "THE UNDERTAKER ")
        {
            jugador.SuperStar = new SuperStar("Undertaker", 7, 3, "");
        }

        // agregamos las cartas del mazo al jugador
        foreach (string card in mazo)
        {
            if (card != mazo[0])
            {
                char firstChar = card[0];
                int number = Int32.Parse(firstChar.ToString());
                string title = card.Remove(0, 2);
                Card cardToAdd = this.Cards.Find(card => card.Title == title);
                for (int i = 0; i < number; i++)
                {
                    jugador.Arsenal.Add(cardToAdd);
                }
            }
        }

        bool validation = ValidateDeck(jugador.Arsenal, jugador);

        // el jugador roba 10 cartas del arsenal a su mano
        for (int i = 0; i < 10; i++)
        {
            jugador.hand.Add(jugador.Arsenal[0]);
            jugador.Arsenal.RemoveAt(0);
        }

        return validation;
    }

    // función para validar el mazo
    public bool ValidateDeck(List<Card> deck, Jugador jugador)
    {
        if (deck.Count != 60)
        {
            return false;
        }

        string superStarName = jugador.SuperStar.Title;
        List<string> superStars = new List<string>() {"HHH", "Jericho", "Kane", "Mankind", "StoneCold", "TheRock", "Undertaker"};
        bool hasHell = false;
        bool hasFace = false;
        
        foreach (Card card in deck)
        {
            foreach (string superStar in superStars)
            {
                if (card.Subtypes.Contains(superStar))
                {
                    if (superStar != superStarName)
                    {
                        return false;
                    }
                }
            }
            if (card.Subtypes.Contains("Hell"))
            {
                hasHell = true;
            }
            if(card.Subtypes.Contains("Face"))
            {
                hasFace = true;
            }
            if(card.Subtypes.Contains("SetUp")) {}
            else
            {
                if(card.Subtypes.Contains("Unique"))
                {
                    int uniquesQuantity = deck.Count(x => x.Title == card.Title);
                    if (uniquesQuantity > 1)
                    {
                        return false;
                    }
                }
                else
                {
                    int cardsQuantity = deck.Count(x => x.Title == card.Title);
                    if (cardsQuantity > 3)
                    {
                        return false;
                    }
                }
            }
        }

        if (hasHell && hasFace)
        {
            return false;
        }

        return true;
    }
}

// Hacemos el programa principal
class Program
{
    static void Main(string[] args)
    {
        // instanciamos el juego
        Game game = new Game();
        // iniciamos el juego
        game.GameFlow();
    }
}


