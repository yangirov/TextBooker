namespace TextBooker.DataAccess.Entities
{
	public interface IEntity<TKey>
	{
		TKey Id { get; set; }
	}
}
