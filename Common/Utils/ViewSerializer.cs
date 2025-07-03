using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace Common.Utils
{

	public class ViewSerializer
	{
		private readonly IRazorViewEngine _viewEngine;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IServiceProvider _serviceProvider;

		public ViewSerializer(
			IRazorViewEngine viewEngine,
			ITempDataProvider tempDataProvider,
			IServiceProvider serviceProvider)
		{
			_viewEngine = viewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
		}

		public async Task<string> RenderViewToStringAsync(ActionContext actionContext, string viewName, object model)
		{
			var viewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: false);
			if (!viewResult.Success)
			{
				throw new FileNotFoundException($"View '{viewName}' not found.");
			}

			var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {
				Model = model
			};

			await using var sw = new StringWriter();
			var viewContext = new ViewContext(
				actionContext,
				viewResult.View,
				viewDictionary,
				new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
				sw,
				new HtmlHelperOptions()
			);

			await viewResult.View.RenderAsync(viewContext);
			return sw.ToString();
		}
	}

}