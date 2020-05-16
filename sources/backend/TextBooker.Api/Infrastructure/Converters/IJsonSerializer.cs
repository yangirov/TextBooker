namespace TextBooker.Api.Infrastructure.Converters
{
	public interface IJsonSerializer
	{
		string SerializeObject<T>(T serializingObject);

		T DeserializeObject<T>(string serializedObject);
	}
}
