using System.Reflection;

namespace TextBooker.BusinessLogic
{
	/// <summary>
	/// Class for getting the project's Assembly object
	/// </summary>
	public static class BusinessLogicAssembly
	{
		/// <summary>
		/// Assembly value
		/// </summary>
		public static readonly Assembly Value = typeof(BusinessLogicAssembly).Assembly;
	}
}
