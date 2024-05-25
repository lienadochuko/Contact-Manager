using Microsoft.AspNetCore.Mvc.Filters;

namespace Contact_Manager.Filters.ExceptionFilters
{
	public class HandleExceptionFilter : IExceptionFilter
	{
		private readonly ILogger<HandleExceptionFilter> _logger;

		public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{			
			_logger.LogError("{FilterName}.{MethodName}\nExceptionType{}\n{ExceptionMessage}", 
				nameof(HandleExceptionFilter), nameof(OnException), 
				context.Exception.GetType().ToString(), context.Exception.Message);

			cony
		}
	}
}
