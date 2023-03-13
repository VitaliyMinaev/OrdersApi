using CleanApi.Contracts.Responses.Abstract;

namespace CleanApi.Contracts.Responses;

public class CustomerResponse : ResponseBase
{
    public string FullName { get; set; }
}