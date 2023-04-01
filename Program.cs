using Settings;
using Services;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HotelDatabaseSettings>(
    builder.Configuration.GetSection("HotelDatabase")
);
builder.Services.AddSingleton<CustomerCollectionService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var customers = app.MapGroup("/customers");
customers.MapGet("/", CustomerService.GetCustomers);
customers.MapGet("/{id}", CustomerService.GetCustomerById);
customers.MapPost("/", CustomerService.CreateCustomer);
customers.MapPut("/{id}", CustomerService.UpdateCustomer);
customers.MapDelete("/{id}", CustomerService.DeleteCustomer);

app.Run();
