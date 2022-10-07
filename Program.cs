
public class Game
{
    // instanciamos los atributos del juego
    public List<Card> Cards = new List<Card>();
    public Jugador Jugador1;
    public Jugador Jugador2;
    public List<Jugador> Jugadores = new List<Jugador>();
    public int jugando = 0;
    public bool isOver = false;

    // instanciamos el constructor del juego
    public Game()
    {
        // Creamos las cartas
        string cards_json = File.ReadAllText("./cards/cards.json");
        var cards = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StringCard>>(cards_json);

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
        this.Jugador1 = new Jugador("Jugador 1", new SuperStar(""));
        this.Jugador2 = new Jugador("Jugador 2", new SuperStar(""));
        this.Jugadores.Add(this.Jugador1);
        this.Jugadores.Add(this.Jugador2);
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

        ChooseFirstPlayer();

        // Jugamos hasta que se acaben las cartas
        MainTurnLoop();

    }

    // Creamos el main loop de turno
    public void MainTurnLoop()
    {
        while(this.isOver == false)
        {
            if (this.Jugadores[this.jugando].SuperStar.useBeforeDraw)
            {
                this.Jugadores[this.jugando].useSuperStarAbility();
            }

            this.Jugadores[this.jugando].Draw();
            
            int currentPlayer = this.jugando;
            while (currentPlayer == this.jugando)
            {
                PrintGameStats();
                PrintGameOptions(this.Jugadores[this.jugando]);

                int jugada = int.Parse(Console.ReadLine());
                while (jugada > 3)
                {
                    Console.WriteLine("Ingresa una opción válida");
                    jugada = int.Parse(Console.ReadLine());
                }
                if (jugada == 0)
                {
                    // this.Jugadores[this.jugando].PlaySuperStarAbility();
                }
                if (jugada == 1)
                {
                    ShowCards();
                }
                if (jugada == 2)
                {
                    // this.Jugadores[this.jugando].PlayCard();
                }
                if (jugada == 3)
                {
                    this.jugando = this.jugando == 0 ? 1 : 0;
                }
            }
        }
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
            jugador.SuperStar = new SuperStar("HHH");
        }
        else if (superStar == "CHRIS JERICHO ")
        {
            jugador.SuperStar = new SuperStar("Jericho");
        }
        else if (superStar == "KANE ")
        {
            jugador.SuperStar = new SuperStar("Kane");
        }
        else if (superStar == "MANKIND ")
        {
            jugador.SuperStar = new SuperStar("Mankind");
        }
        else if (superStar == "STONE COLD STEVE AUSTIN ")
        {
            jugador.SuperStar = new SuperStar("StoneCold");
        }
        else if (superStar == "THE ROCK ")
        {
            jugador.SuperStar = new SuperStar("TheRock");
        }
        else if (superStar == "THE UNDERTAKER ")
        {
            jugador.SuperStar = new SuperStar("Undertaker");
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
                    jugador.Arsenal.Insert(0, cardToAdd);
                }
            }
        }

        bool validation = ValidateDeck(jugador.Arsenal, jugador);

        // el jugador roba 10 cartas del arsenal a su mano
        for (int i = 0; i < jugador.SuperStar.handSize; i++)
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

    // Función que elige quien parte
    public void ChooseFirstPlayer()
    {
        if (this.Jugador1.SuperStar.value == this.Jugador2.SuperStar.value)
        {
            Random aleatorio = new Random();
            int player = aleatorio.Next(1, 3);

            if (player == 1)
            {
                this.jugando = 0;
            }
            else
            {
                this.jugando = 1;
            }
        }
        else if (this.Jugador1.SuperStar.value > this.Jugador2.SuperStar.value)
        {
            this.jugando = 0;
        }
        else
        {
            this.jugando = 1;
        }
    }

    public void PrintGameStats()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("Se enfrentan: " + this.Jugador1.SuperStar.Title + " y " + this.Jugador2.SuperStar.Title);
        Console.WriteLine(this.Jugador1.SuperStar.Title + " tiene " + this.Jugador1.Fortitude + "F, " + this.Jugador1.hand.Count + " cartas en su mano y le quedan " + this.Jugador1.Arsenal.Count + " cartas en su arsenal.");
        Console.WriteLine(this.Jugador2.SuperStar.Title + " tiene " + this.Jugador2.Fortitude + "F, " + this.Jugador2.hand.Count + " cartas en su mano y le quedan " + this.Jugador2.Arsenal.Count + " cartas en su arsenal.");
    }

    public void PrintGameOptions(Jugador jugador)
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("juega " + jugador.SuperStar.Title + ". ¿Qué quieres hacer?");
        if (jugador.SuperStar.useBeforeDraw == false)
        {
            Console.WriteLine("\t0. Usar mi super habilidad");
        }
        Console.WriteLine("\t1. Ver mis cartas o las cartas de mi oponente");
        Console.WriteLine("\t2. Jugar una carta");
        Console.WriteLine("\t3. Terminar mi turno");
        Console.WriteLine("(Ingresa un número entre 0 y 3)");
    }

    public void ShowCards()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("Juega " + this.Jugadores[this.jugando].Title + ". ¿Qué cartas quieres ver?");
        Console.WriteLine("\t1. Mi mano.");
        Console.WriteLine("\t2. Mi ringside.");
        Console.WriteLine("\t3. Mi ring area.");
        Console.WriteLine("\t4. El ringside de mi oponente.");
        Console.WriteLine("\t5. El ring area de mi oponente.");
        Console.WriteLine("(Ingresa un número entre 1 y 5)");

        int option = Int32.Parse(Console.ReadLine());

        if (option == 1)
        {
            this.Jugadores[this.jugando].ShowHandCards();
        }
        else if (option == 2)
        {
            this.Jugadores[this.jugando].ShowGraveYardCards();
        }
        else if (option == 3)
        {
            this.Jugadores[this.jugando].ShowRingAreaCards();
        }
        else if (option == 4)
        {
            this.Jugadores[this.jugando == 0 ? 1 : 0].ShowGraveYardCards();
        }
        else if (option == 5)
        {
            this.Jugadores[this.jugando == 0 ? 1 : 0].ShowRingAreaCards();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar.");

        Console.ReadKey();
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
