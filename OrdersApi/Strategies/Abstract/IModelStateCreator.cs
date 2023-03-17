using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrdersApi.Strategies.Abstract;

public interface IModelStateCreator
{
    ModelStateDictionary CreateErrorStateModel(List<IError> errors);
}