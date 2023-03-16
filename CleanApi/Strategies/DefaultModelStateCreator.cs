using CleanApi.Strategies.Abstract;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanApi.Strategies;

public class DefaultModelStateCreator : IModelStateCreator
{
    public ModelStateDictionary CreateErrorStateModel(List<IError> errors)
    {
        var modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            foreach (var reason in error.Reasons)
            {
                modelState.AddModelError(reason.Message, error.Message);
            }
        }
        return modelState;
    }
}