namespace mongoDb_push_showcase.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public record NoSqlBaseEntity
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid Id { get; init; }

    public required DateTime DateCreated { get; init; }

    public DateTime? DateUpdated { get; init; }

    public DateTime? DateDeleted { get; init; }
}


public sealed record Transaction : NoSqlBaseEntity
{
    public required int PersonId { get; init; }

    public required TransactionObject TransactionObject { get; init; }

    public required string TransactionType { get; init; }

    public TransactionStatus? TransactionStatus { get; init; }
}

public sealed record TransactionObject
{
    public required string ClientType { get; init; }

    public required string CardType { get; init; }

    public required string PaymentArea { get; init; }

    public required string Channel { get; init; }

    public required string TerminalId { get; init; }

    public required string MccCode { get; init; }

    public required double Amount { get; init; }

    public required DateTime TransactionDate { get; init; }
}

public sealed record TransactionStatus
{
    public required double Point { get; init; }

    public required int TryCount { get; init; }

    public required string ProcessMessage { get; init; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CombinationId { get; init; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CampaignId { get; init; }

    public DateTime? DateUpdated { get; init; }
}