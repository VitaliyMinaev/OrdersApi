namespace OrdersApi.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Order
    {
        public const string GetAll = $"{Base}/{nameof(Order)}";
        public const string GetById = $"{Base}/{nameof(Order)}" + "/{id}";
        public const string Create = $"{Base}/{nameof(Order)}";
        public const string Update = $"{Base}/{nameof(Order)}" + "/{id}";
        public const string Delete = $"{Base}/{nameof(Order)}" + "/{id}";
    }
    public static class Customer
    {
        public const string GetAll = $"{Base}/{nameof(Customer)}";
        public const string GetById = $"{Base}/{nameof(Customer)}" + "/{id}";
        public const string Create = $"{Base}/{nameof(Customer)}";
        public const string Update = $"{Base}/{nameof(Customer)}" + "/{id}";
        public const string Delete = $"{Base}/{nameof(Customer)}" + "/{id}";
    }
    public static class Product
    {
        public const string GetAll = $"{Base}/{nameof(Product)}";
        public const string GetById = $"{Base}/{nameof(Product)}" + "/{id}";
        public const string Create = $"{Base}/{nameof(Product)}";
        public const string Update = $"{Base}/{nameof(Product)}" + "/{id}";
        public const string Delete = $"{Base}/{nameof(Product)}" + "/{id}";
    }
}