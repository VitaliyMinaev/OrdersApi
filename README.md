# Orders api
A set of clean approaches based on cqrs (mediatR) to api development using the fictional example of customer orders


## 🧐 About app
The API describes a fictional example of creating product orders for a specific customer. The API, built using RESTful principles, allows you to perform CRUD operations on 3 data types: Customer, Product, and Order.

This project is based on <code>ASP .NET Core</code> using the <code>MediatR</code> library. The application uses <code>Sqlite</code> as a persistent data store. The main reasons for this are ease of implementation and no need for a SQL server, which is acceptable for a home project.
## 🚀 Deployment

This application has already been deployed to the web using the <code>Heroku</code> service.

<a href="https://clean-order-api.herokuapp.com/">Click it!</a>

To use it as API add to the route <code>api/v1/[ControllerName]</code>

## 🛠️ Run Locally

Clone the project

```bash
  git clone https://github.com/VitaliyMinaev/OrdersApi.git
```

Go to the project directory

```bash
  cd OrdersApi
```

Start the server

```bash
  dotnet run
```


## 🧿 Environment Variables

To run this project, you will need to add the following environment variables

`DatabaseCreated` - Responsible for the state of the data source, takes the value "true" if the database has already been created and the current state of the objects describes their representation in the database (no migrations are required) and "false" if the database requires a migration operation (recommended value for the first run)

## 💻‍‍ Libraries and frameworks with which the application was created

<ul>
    <li><code>ASP .NET Core Web API</code> - as backend framework</li>
    <li><code>MediatR</code></li>
    <li><code>Entity Framework Core</code> - as ORM</li>
    <li><code>Sqlite</code> - as data storage</li>
    <li><code>In Memory caching</code> - with <code>Scrutor</code></li>
</ul>

## 👨‍💻 Authors

- [@VitaliyMinaev](https://github.com/VitaliyMinaev)

## Screenshots

![image](https://user-images.githubusercontent.com/87979065/226609919-6cacc567-dc61-4f19-8ad9-100f04949270.png)
