using AcspNet.Owin;
using Owin;

namespace Roulette
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseAcspNet();
		}
	}
}