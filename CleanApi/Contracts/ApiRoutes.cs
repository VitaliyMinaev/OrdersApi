namespace CleanApi.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Order
    {
        public const string GetAll = $"{Base}/{nameof(Order)}";
        public const string GetById = $"{Base}/{nameof(Order)}" + "/{id}";
    }
}