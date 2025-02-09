namespace CardMicroservice.Models;

public record CardDetails(string CardNumber, CardKind CardKind, CardStatus CardStatus, bool IsPinSet);