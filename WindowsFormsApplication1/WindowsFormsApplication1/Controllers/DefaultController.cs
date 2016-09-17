using AcspNet;
using AcspNet.Attributes;
using AcspNet.Responses;
using System.Web;

namespace roulette.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new Tpl("hello, world!");
		}
	}
}
