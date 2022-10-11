
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
            this.handSize = 6;
            this.value = 4;
            this.ability = "Once during your turn, you may discard 2 cards to the Ringside pile and take 1 card from the Ringside pile and place it into your hand.";
            this.useBeforeDraw = false;
        }
        if (Title == "Mankind")
        {
            this.handSize = 2;
            this.value = 4;
            this.ability = "You must always draw 2 cards, if possible, during your draw segment. All damage from opponent is at -1D.";
            this.useBeforeDraw = true;
        }
        if (Title == "HHH")
        {
            this.handSize = 10;
            this.value = 3;
            this.ability = "None";
            this.useBeforeDraw = true;
        }
        if (Title == "TheRock")
        {
            this.handSize = 5;
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
    public string Title;
    public int Fortitude;
    public Jugador oponente;
    public int DamageDelta;
    public int DrawCount;

    // Agregamos el constructor del jugador
    public Jugador(string title, SuperStar superStar)
    {
        this.Title = title;
        this.SuperStar = superStar;
        this.hand = new List<Card>();
        this.Arsenal = new List<Card>();
        this.RingArea = new List<Card>();
        this.GraveYard = new List<Card>();
        this.Fortitude = 0;
        this.DamageDelta = 0;
        this.DrawCount = 1;
    }

    // Agregamos el método para jugar una carta
    public bool PlayCard(Card carta)
    {   
        Console.WriteLine("--------------------");
        Console.WriteLine(this.SuperStar.Title + " Intenta jugar la siguiente carta como [" + carta.Types[0] + "]");
        carta.ShowCard(-1);

        // Verificamos si el oponente puede (y quiere) revertir la carta
        List <Card> reverts = this.oponente.CanDefend(carta);

        if (reverts.Count == 0)
        {
            return DropCard(carta);
        }
        else
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Pero " + this.oponente.SuperStar.Title + " tiene la opción de revertir la carta:");
            Console.WriteLine("Estas son las cartas que puedes jugar:");
            int number = 0;
            foreach (Card revert in reverts)
            {
                revert.ShowCard(number);
                number++;
            }
            Console.WriteLine("Ingresa el número de la carta que quieres jugar. Si no quieres jugar ninguna, ingresa -1");
            int option = Convert.ToInt32(Console.ReadLine());
            
            if (option == -1)
            {
                return DropCard(carta);
            }
            else
            {
                this.hand.Remove(carta);
                this.GraveYard.Add(carta);
                if (reverts[option].Title == "Rolling Takedown" || reverts[option].Title == "Knee to the Gut")
                {
                    this.oponente.DefendFromHand(reverts[option], applyFortitude: false);
                }
                else
                {
                    this.oponente.DefendFromHand(reverts[option]);
                }
                return false;
            }
        }
    }

    // Agregamos el método para jugar exitosamente una carta
    public bool DropCard(Card carta)
    {
        Console.WriteLine("--------------------");
        Console.WriteLine(this.oponente.SuperStar.Title + " no revierte la carta de " + this.SuperStar.Title);
        Console.WriteLine("La carta " + carta.Title + "[" + carta.Types[0] + "] es exitosamente jugada.");
        carta.ShowCard(-1);

        this.hand.Remove(carta);
        this.RingArea.Add(carta);
        this.Fortitude += carta.Damage;

        // Aplicamos el effecto de la carta

        // Aplicamos el daño de la carta
        return this.oponente.ApplyDamageToSelf(carta);


    }
    // Agregamos el método para defendernos
    public List <Card> CanDefend(Card carta)
    {
        List <Card> reverts = new List<Card>();
        foreach (Card card in this.hand)
        {
            if (card.IsReversal(carta, this.Fortitude))
            {
                reverts.Add(card);
            }
        }

        return reverts;
    }

    // Agregamos el metodo para defender con un reversal desde la mano
    public void DefendFromHand(Card carta, bool applyFortitude = true)
    {
        this.RingArea.Add(carta);
        this.hand.Remove(carta);
        if (applyFortitude)
        {
            this.Fortitude += carta.Damage;
        }

        this.oponente.ApplyDamageToSelf(carta, canReverse: false);

        if (carta.Title == "Manager Interferes")
        {
            this.Draw();
        }

        if (carta.Title == "Clean Break")
        {
            for (int i = 0; i < 4; i++)
            {
                this.oponente.GraveYard.Add(this.oponente.hand[0]);
                Console.WriteLine("La carta " + this.oponente.hand[0].Title + " es descartada de la mano de " + this.oponente.SuperStar.Title);
                this.oponente.hand.RemoveAt(0);
            }
            this.Draw();
        }
    }

    // Agregamos el método para aplicarnos daño
    public bool ApplyDamageToSelf(Card carta, bool canReverse = true)
    {
        Console.WriteLine("--------------------");
        Console.WriteLine(this.SuperStar.Title + " recibe " + carta.Damage + " de daño.");
        
        // Sacamos una carta del arsenal por cada punto de daño
        for (int i = 1; i <= (carta.Damage + this.DamageDelta); i++)
        {
            if (this.Arsenal.Count > 0)
            {
                Card cartaSacada = this.Arsenal[0];
                
                Console.WriteLine("-------------------- " + i.ToString() + "/" + carta.Damage + " damage");
                cartaSacada.ShowCard(-1);

                this.Arsenal.Remove(cartaSacada);
                this.GraveYard.Add(cartaSacada);

                if (canReverse)
                {
                    if (cartaSacada.IsReversal(carta, this.Fortitude, playedFromHand: false))
                    {
                        Console.WriteLine("--------------------");
                        Console.WriteLine("Sale " + cartaSacada.Title + "[" + cartaSacada.Types[0] + "] y " + this.SuperStar.Title + " la usa para revertir el daño.");

                        // Aplicamos effecto SV de la carta
                        if (i < (carta.Damage + this.DamageDelta) && carta.StunValue > 0)
                        {
                            for (int j = 1; j <= carta.StunValue; j++)
                            {
                                this.oponente.Draw(drawOne: true);
                            }
                        }
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine(this.SuperStar.Title + " no tiene más cartas en su arsenal.");
                return true;
            }
        }
        return true;
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
    public void Draw(bool drawOne = false)
    {
        if(this.Arsenal.Count > 0)
        {
            if (drawOne)
            {
                this.hand.Add(this.Arsenal[0]);
                this.Arsenal.RemoveAt(0);
            }
            else
            {
                for (int i = 0; i < this.DrawCount; i++)
                {
                    this.hand.Add(this.Arsenal[0]);
                    this.Arsenal.RemoveAt(0);
                }
            }
        }
    }

    // Método para usar la super habilidad de la super Estrella
    public void UseSuperStarAbility()
    {
        Console.WriteLine("--------------------");
        
        if (this.SuperStar.Title == "TheRock")
        {
            Console.WriteLine("Eres The Rock, quieres usar tu habilidad especial? [Y/N]");
            string respuesta = Console.ReadLine();

            if (respuesta == "Y" || respuesta == "y")
            {
                Console.WriteLine("La habilidad especial de The Rock es que puedes robar una carta del Ring Side y ponerla en el fondo de tu Arsenal.");
                if (this.GraveYard.Count > 0)
                {
                    Card cartaElegida = this.ExtractCardFromRingSide();
                    this.Arsenal.Add(cartaElegida);
                    Console.WriteLine("La carta " + cartaElegida.Title + " fue robada del Ring Side y puesta en tu mano.");
                }
                else
                {
                    Console.WriteLine("No tienes cartas en tu RingSide.");
                }
            }
            else if (respuesta == "N" || respuesta == "n")
            {
                Console.WriteLine("No usaste la habilidad especial de The Rock.");
            }
            else
            {
                Console.WriteLine("Ingresa un valor válido.");
            }
        }
        
        if (this.SuperStar.Title == "Kane")
        {
            Card cartaSacada = this.oponente.Arsenal[0];
            this.oponente.Arsenal.Remove(cartaSacada);
            this.oponente.GraveYard.Add(cartaSacada);

            Console.WriteLine("La habilidad especial de Kane es que su oponente debe descartar una carta de su arsenal.");
            Console.WriteLine("La carta " + cartaSacada.Title + " de " + this.oponente.SuperStar.Title + " fue robada de su Arsenal y puesta en su Ring Side.");
        }

        if (this.SuperStar.Title == "StoneCold")
        {
            Console.WriteLine("La habilidad especial de StoneCold es que puede robar otra carta del arsenal. Pero cambiarla por una de su mano, poniendo esta al final de su arsenal.");

            this.Draw();

            Console.WriteLine("La carta " + this.hand[0].Title + " fue robada de su Arsenal y puesta en su mano.");

            Console.WriteLine("Elija una carta de su mano para ponerla al final de su Arsenal.");
            this.ShowHandCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.hand.Count - 1).ToString() + ")");
            int cartaElegida = Convert.ToInt32(Console.ReadLine());

            Card cartaSacada = this.hand[cartaElegida];
            this.hand.Remove(cartaSacada);
            this.Arsenal.Add(cartaSacada);

            Console.WriteLine("La carta " + cartaSacada.Title + " fue robada de su mano y puesta al final de su Arsenal.");
        }

        if (this.SuperStar.Title == "Undertaker")
        {
            Console.WriteLine("La habilidad especial de Undertaker es que puede descartar 2 cartas de su mano y poner una carta de su Ring Side en su mano.");

            Console.WriteLine("Elija una carta de su mano para descartar.");
            this.ShowHandCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.hand.Count - 1).ToString() + ")");
            int cartaElegida1 = Convert.ToInt32(Console.ReadLine());

            Card cartaSacada1 = this.hand[cartaElegida1];
            this.hand.Remove(cartaSacada1);
            this.GraveYard.Add(cartaSacada1);

            Console.WriteLine("La carta " + cartaSacada1.Title + " fue robada de su mano y puesta en su Ring Side.");

            Console.WriteLine("Elija otra carta de su mano para descartar.");
            this.ShowHandCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.hand.Count - 1).ToString() + ")");
            int cartaElegida2 = Convert.ToInt32(Console.ReadLine());

            Card cartaSacada2 = this.hand[cartaElegida2];
            this.hand.Remove(cartaSacada2);
            this.GraveYard.Add(cartaSacada2);

            Console.WriteLine("La carta " + cartaSacada2.Title + " fue robada de su mano y puesta en su Ring Side.");

            Console.WriteLine("Elija una carta de su Ring Side para poner en su mano.");
            this.ShowGraveYardCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.GraveYard.Count - 1).ToString() + ")");
            int cartaElegida3 = Convert.ToInt32(Console.ReadLine());

            Card cartaSacada3 = this.GraveYard[cartaElegida3];
            this.GraveYard.Remove(cartaSacada3);
            this.hand.Add(cartaSacada3);

            Console.WriteLine("La carta " + cartaSacada3.Title + " fue robada de su Ring Side y puesta en su mano.");
        }

        if (this.SuperStar.Title == "Jericho")
        {
            Console.WriteLine("La habilidad especial de Jericho es que puede descartar una carta de su mano, y obligar a su oponente a hacer lo mismo.");

            Console.WriteLine("Elija una carta de su mano para descartarla.");
            this.ShowHandCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.hand.Count - 1).ToString() + ")");
            int cartaElegida = Convert.ToInt32(Console.ReadLine());

            Card cartaSacada = this.hand[cartaElegida];
            this.hand.Remove(cartaSacada);
            this.GraveYard.Add(cartaSacada);

            Console.WriteLine("La carta " + cartaSacada.Title + " fue robada de su mano y puesta en su Ring Side.");

            Console.WriteLine("Su oponente debe elegir una carta de su mano para descartarla.");
            this.oponente.ShowHandCards();
            Console.WriteLine("(Ingrese un número del 0 al " + (this.oponente.hand.Count - 1).ToString() + ")");
            int cartaElegidaOponente = Convert.ToInt32(Console.ReadLine());

            Card cartaSacadaOponente = this.oponente.hand[cartaElegidaOponente];
            this.oponente.hand.Remove(cartaSacadaOponente);
            this.oponente.GraveYard.Add(cartaSacadaOponente);

            Console.WriteLine("La carta " + cartaSacadaOponente.Title + " fue robada de su mano y puesta en su Ring Side.");
        }
    }

    // Método para jugar una carta de la mano
    public bool DecideWichCardToPlay()
    {
        List <Card> playableCards = ShowPlayableCards();

        if (playableCards.Count > 0)
        {
            Console.WriteLine("Ingresa el ID de la carta que quieres jugar. Puedes ingresar '-1' para cancelar.");
            Console.WriteLine("Ingresa un número entre -1 y " + (playableCards.Count - 1).ToString() + ")");
        
            int cardID = Convert.ToInt32(Console.ReadLine());

            if (cardID == -1)
            {
                Console.WriteLine("Cancelaste la acción.");
                return true;
            }
            else
            {
                if (cardID >= 0 && cardID < playableCards.Count)
                {
                    return this.PlayCard(playableCards[cardID]);
                }
                else
                {
                    Console.WriteLine("El ID ingresado no es válido.");
                    return true;
                }
            }
        }
        else
        {
            Console.WriteLine("No tienes cartas jugables.");
            return true;
        }
    }

    // Método para mostrar las cartas jugables
    public List <Card> ShowPlayableCards()
    {
        Console.WriteLine("Estas son las cartas que puedes jugar:");
        List <Card> pleyableCards = this.GetPlayableCards();
        int number = 0;
        foreach (Card carta in pleyableCards)
        {
            carta.ShowCard(number);
            number++;
        }

        return pleyableCards;
    }

    // Funcion que obtiene las cartas que se pueden jugar de la mano del jugador
    public List <Card> GetPlayableCards()
    {
        
        List <Card> playableCards = new List<Card>();
        foreach (Card card in this.hand)
        {
            if (card.Fortitude <= this.Fortitude && (card.Types.Contains("Action") || card.Types.Contains("Maneuver")))
            {
                playableCards.Add(card);
            }
        }
        return playableCards;
    }

    public Card ExtractCardFromRingSide()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("Estas son las cartas en tu RingSide:");
        int number = 0;
        foreach (Card card in this.GraveYard)
        {
            card.ShowCard(number);
            number++;
        }
        
        Console.WriteLine("Ingresa el ID de la carta que quieres extraer.");
        Console.WriteLine("Ingresa un número entre 0 y " + (this.GraveYard.Count - 1).ToString() + ")");

        int cardID = Convert.ToInt32(Console.ReadLine());

        Card carta = this.GraveYard[cardID];
        this.GraveYard.RemoveAt(cardID);

        return carta;
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
        if (number != -1)
        {
            Console.WriteLine("------------- Card #" + number);
        }
        Console.WriteLine("Title: " + this.Title);
        Console.WriteLine("Stats: [" + this.Fortitude + "F/" + this.Damage + "D/" + this.StunValue + "SV]");
        Console.Write("Types: ");
        foreach (string type in this.Types)
        {
            Console.Write(type + ", ");
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

    // Función que verifica si una carta es un reversal posible para otra
    public bool IsReversal(Card card, int FortitudeValueOfPlayer, bool playedFromHand = true)
    {
        if (this.Fortitude > FortitudeValueOfPlayer)
        {
            return false;
        }

        if (this.Types.Contains("Reversal") == false)
        {
            return false;
        }

        // Verificamos que la carta a la que queremos aplicar el reversal contenga un subtipo que coincida con el reversal
        foreach (string subtype in this.Subtypes)
        {
            string subtypeToCheck = subtype.Replace("Reversal", "");
            if (card.Subtypes.Contains(subtypeToCheck))
            {
                return true;
            }
        }

        if (this.Title == "Rolling Takedown")
        {
            if (card.Subtypes.Contains("Grapple") && card.Damage <= 7)
            {
                this.Damage = card.Damage;
                return true;
            }
        }

        if (this.Title == "Knee to the Gut")
        {
            if (card.Subtypes.Contains("Strike") && card.Damage <= 7)
            {
                this.Damage = card.Damage;
                return true;
            }
        }
        
        if (this.Title == "Elbow to the Face")
        {
            if (card.Damage <= 7)
            {
                return true;
            }
        }
        
        if (this.Title == "Clean Break")
        {
            if (playedFromHand && card.Title == "Jockeying for Position")
            {
                return true;
            }
        }

        if (this.Title == "Manager Interferes")
        {
            if (card.Types.Contains("Maneuver"))
            {
                return true;
            }
        }

        if (this.Title == "No Chance in Hell")
        {
            if (card.Types.Contains("Action"))
            {
                return true;
            }
        }

        return false;
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