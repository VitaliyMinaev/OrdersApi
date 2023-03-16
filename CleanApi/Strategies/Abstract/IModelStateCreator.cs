using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanApi.Strategies.Abstract;

public interface IModelStateCreator
{
    ModelStateDictionary CreateErrorStateModel(List<IError> errors);
}