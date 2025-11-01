using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlayerLockerApi.Infrastructure.Documents;

public class LockerDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("Name")]
    public string Name { get; set; } = null!;
    [BsonElement("Skin")]
    public string Skin { get; set; } = null!;
    [BsonElement("Backblings")]
    public string Backblings { get; set; } = null!;
    [BsonElement("Pickaxe")]
    public string Pickaxe { get; set; } = null!;
    [BsonElement("Glider")]
    public string Glider { get; set; } = null!;
    [BsonElement("Contrail")]
    public string Contrail { get; set; } = null!;
    [BsonElement("Emote")]
    public string Emote { get; set; } = null!;
}