public class AddCardToDeckGA : GameAction
{
    public Card card { get; private set; }
    public AddCardToDeckGA(Card card){
        this.card = card;
    }
}
