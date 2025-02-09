using DefaultNamespace;

namespace CardMicroservice.Tests;

public class CardServiceTests
{
    [Fact]
    public void PrepaidClosedWithPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Prepaid, CardStatus.Closed, true);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action9", result);
    }
    
    [Fact]
    public void PrepaidClosedWithoutPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Prepaid, CardStatus.Closed, false);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action9", result);
    }

    [Fact]
    public void CreditBlockedWithPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Credit, CardStatus.Blocked, true);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action5, Action6, Action7, Action8, Action9", result);
    }
    
    [Fact]
    public void CreditBlockedWithoutPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Credit, CardStatus.Blocked, false);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action5, Action8, Action9", result);
    }
    
    [Fact]
    public void DebitOrderedWithoutPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Debit, CardStatus.Ordered, false);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action7, Action8, Action9, Action10, Action12, Action13", result);
    }
    
    [Fact]
    public void DebitOrderedWithPin()
    {
        var cardDetails = new CardDetails(string.Empty, CardKind.Debit, CardStatus.Ordered, true);
        var result = CardService.GetAllowedActionsForCard(cardDetails);
        Assert.Equal("Action3, Action4, Action6, Action8, Action9, Action10, Action12, Action13", result);
    }
}