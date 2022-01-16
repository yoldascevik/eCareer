using Career.Domain;
using MongoDB.Bson.Serialization;

namespace Career.Mongo.Serializers;

public enum EnumerationBsonType
{
    Int = 0,
    String = 1
}
    
public class EnumerationSerializer<TEnum>: IBsonSerializer<TEnum> where TEnum: Enumeration
{
    private readonly EnumerationBsonType _representation;
        
    public EnumerationSerializer(EnumerationBsonType representation)
    {
        ValueType = typeof(TEnum);
        _representation = representation;
    }
        
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
        
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is Enumeration obj)
        {
            Serialize(context,args, (TEnum)obj);
        }
        else
        {
            throw new ArgumentException(nameof(value));
        }
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
    {
        if (value == null)
            return;
            
        switch (_representation)
        {
            case EnumerationBsonType.Int:
                context.Writer.WriteInt32(value.Id);
                break;
            case EnumerationBsonType.String:
                context.Writer.WriteString(value.Name);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return _representation switch
        {
            EnumerationBsonType.Int => Enumeration.FromValue<TEnum>(context.Reader.ReadInt32()),
            EnumerationBsonType.String => Enumeration.FromName<TEnum>(context.Reader.ReadString()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public Type ValueType { get; }
}