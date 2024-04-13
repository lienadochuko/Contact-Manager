using Contact_Manager.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace Contact_Manager.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        public readonly ILogger<PersonsListActionFilter> Logger;

        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger) { Logger = logger; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To Do: add after logic here
            Logger.LogInformation("{FilterName}.{MethodName} method", nameof(PersonsListActionFilter), nameof(OnActionExecuted));

            PersonsController personsController = (PersonsController) context.Controller;
            
        } 

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To Do: add before logic here
            Logger.LogInformation("PersonListActionFilter.OnActionExecuting method");

            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
                
                //Validate the searchBy Parameter value 
                if (!string.IsNullOrEmpty(searchBy))
                {
                    var searchByOption = new List<string>()
                    {
                        
                        nameof(PersonResponse.DOB),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.Address),
                        nameof(PersonResponse.CountryID),
                        nameof(PersonResponse.PersonName),
                    };

                    if (searchByOption.Any(temp => temp == searchBy) == false)
                    {
                        Logger.LogInformation("searchBy actual value "+searchBy);
                        context.ActionArguments["searchBy"] = nameof(PersonResponse.PersonName);
                    }
                    else
                    {
                        Logger.LogInformation("searchBy actual value "+searchBy);
                    }
                }
            }
        }
    }
}
