using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System.Net;

namespace Tutorial1
{
	/// <summary>
	/// Orleans test silo host
	/// </summary>
	public class Program
	{
		static SiloHost siloHost;


		static void Main(string[] args)
		{
			Console.Title = "Silo";

			// Orleans should run in its own AppDomain, we set it up like this
			AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null,
				new AppDomainSetup()
				{
					AppDomainInitializer = InitSilo
				});

			Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
			Console.ReadLine();

			// We do a clean shutdown in the other AppDomain
			hostDomain.DoCallBack(ShutdownSilo);
		}

		static void InitSilo(string[] args)
		{
			siloHost = new SiloHost(Dns.GetHostName());
			siloHost.LoadOrleansConfig();

			siloHost.InitializeOrleansSilo();
			var startedok = siloHost.StartOrleansSilo();
			if (!startedok)
				throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
		}

		static void ShutdownSilo()
		{
			if (siloHost != null)
			{
				siloHost.Dispose();
				GC.SuppressFinalize(siloHost);
				siloHost = null;
			}
		}
	}

}
