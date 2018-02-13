using System.Threading.Tasks;
using Orleans;

namespace Tutorial1
{
	/// <summary>
	/// Grain implementation class Grain1.
	/// </summary>
	public class HelloGrain : Grain, IHello
	{
		private string text = "Hello World!";

		public Task<string> SayHello(string greeting)
		{
			var oldText = text;
			text = greeting;
			return Task.FromResult(oldText);
		}
	}
}
