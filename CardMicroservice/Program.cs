using CardMicroservice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CardService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/card/{userId}/{cardNumber}", async (string userId, string cardNumber, CardService cardService) =>
{
    var cardDetails = await cardService.GetCardDetails(userId, cardNumber);
    if (cardDetails == null)
    {
        return Results.NotFound("Card not found");
    }

    var allowedActions = CardService.GetAllowedActionsForCard(cardDetails);
    return Results.Ok(allowedActions);
});



app.Run();