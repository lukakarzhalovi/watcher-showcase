using Microsoft.AspNetCore.Mvc;
using mongoDb_push_showcase.BackgroundServices;
using mongoDb_push_showcase.Repos;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Watcher>();

builder.Services
    .AddSingleton<IMongoClient>(_ => new MongoClient(""))
    .AddSingleton(provider => provider.GetRequiredService<IMongoClient>().GetDatabase("LoyaltyCoin"));

builder.Services.AddScoped<TransactionRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPatch("Transactions/{id:guid}", async ([FromRoute] Guid id, TransactionRepo repo) =>
{
    var res = await repo.UpdateTransactions(id);
    return res;
});


app.Run();