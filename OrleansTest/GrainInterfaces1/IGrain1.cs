using System.Threading.Tasks;
using Orleans;

namespace Tutorial1
{
	/// <summary>
	/// Grain interface IGrain1
	/// </summary>
	public interface IHello : IGrainWithIntegerKey
	{
		Task<string> SayHello(string msg);

	}
}
