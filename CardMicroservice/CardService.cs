using DefaultNamespace;

namespace CardMicroservice;

public class CardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();

    public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    {
// At this point, we would typically make an HTTP call to an external service
// to fetch the data. For this example we use generated sample data.
        await Task.Delay(1000);
        if (!_userCards.TryGetValue(userId, out var cards)
            || !cards.TryGetValue(cardNumber, out var cardDetails))
        {
            return null;
        }

        return cardDetails;
    }

    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        for (var i = 1; i <= 3; i++)
        {
            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardKind cardType in Enum.GetValues(typeof(CardKind)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card{i}{cardIndex}";
                    cards.Add(cardNumber,
                        new CardDetails(
                            CardNumber: cardNumber,
                            CardKind: cardType,
                            CardStatus: cardStatus,
                            IsPinSet: cardIndex % 2 == 0));
                    cardIndex++;
                }
            }

            var userId = $"User{i}";
            userCards.Add(userId, cards);
        }

        return userCards;
    }

    public static string GetAllowedActionsForCard(CardDetails cd)
    {
        var allowedActions = CardActions.Action1 | CardActions.Action2 | CardActions.Action3 | CardActions.Action4
                             | CardActions.Action5 | CardActions.Action6 | CardActions.Action7 | CardActions.Action8
                             | CardActions.Action9 | CardActions.Action10 | CardActions.Action11 | CardActions.Action12
                             | CardActions.Action13;

        if (cd.CardKind is CardKind.Prepaid or CardKind.Debit)
            allowedActions &= ~CardActions.Action5;
        if (cd.CardStatus != CardStatus.Active)
            allowedActions &= ~CardActions.Action1;
        if (cd.CardStatus != CardStatus.Inactive)
            allowedActions &= ~CardActions.Action2;
        if (!cd.IsPinSet || (cd.CardStatus != CardStatus.Ordered && cd.CardStatus != CardStatus.Inactive &&
                             cd.CardStatus != CardStatus.Active && cd.CardStatus != CardStatus.Blocked))
            allowedActions &= ~CardActions.Action6;
        if (cd.IsPinSet && cd.CardStatus != CardStatus.Blocked)
            allowedActions &= ~CardActions.Action7;
        if (cd is {IsPinSet: false, CardStatus: not (CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active)})
            allowedActions &= ~CardActions.Action7;
        if (cd.CardStatus is CardStatus.Restricted or CardStatus.Expired or CardStatus.Closed)
            allowedActions &= ~CardActions.Action8;
        if (cd.CardStatus is not (CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active))
            allowedActions &= ~CardActions.Action10 & ~CardActions.Action12 & ~CardActions.Action13;       
        if (cd.CardStatus is not (CardStatus.Inactive or CardStatus.Active))
            allowedActions &= ~CardActions.Action11;
        return allowedActions.ToString();
    }
}