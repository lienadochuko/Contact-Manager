using Microsoft.AspNetCore.Mvc.Filters;

namespace Contact_Manager.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        public readonly ILogger<PersonsListActionFilter> Logger;

        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger) { Logger = logger; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To Do: add after logic here
            Logger.LogInformation("PersonListActionFilter.OnActionExecuted method");
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To Do: add before logic here
            Logger.LogInformation("PersonListActionFilter.OnActionExecuting method");
        }
    }
}
