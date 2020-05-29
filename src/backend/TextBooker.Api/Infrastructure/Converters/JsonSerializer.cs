using Newtonsoft.Json;

namespace TextBooker.Api.Infrastructure.Converters
{
	public class CustomJsonSerializer : IJsonSerializer
	{
		public string SerializeObject<T>(T serializingObject)
			=> JsonConvert.SerializeObject(serializingObject);

		public T DeserializeObject<T>(string serializedObject)
			=> JsonConvert.DeserializeObject<T>(serializedObject);
	}
}
