using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Career.Mongo.Document;

[MetadataType(typeof(IDocument))]
public abstract class Document : IDocument
{
    [BsonIgnoreIfDefault]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string Id { get; set; }
}