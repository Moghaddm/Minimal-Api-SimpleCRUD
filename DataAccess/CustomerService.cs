using Services;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Models;

namespace DataAccess;

public class CustomerService
{
    private static readonly CustomerCollectionService _customers = default!;

    // public CustomerService(CustomerCollectionService customers) => _customers = customers;

    public static async Task<IResult> GetCustomers() =>
        TypedResults.Ok(await _customers.GetCustomersAsync());

    public static async Task<IResult> GetCustomerById(int id) =>
        TypedResults.Ok(await _customers.GetCustomer(id));

    public static async Task<IResult> CreateCustomer(AddEditCustomerDto customer)
    {
        var mainCustomer = new Customer(
            customer.FirstName,
            customer.LastName,
            customer.Gender,
            customer.Age
        );
        mainCustomer.NationalCode = customer.NationalCode;
        await _customers.CreateCustomer(mainCustomer);
        return TypedResults.CreatedAtRoute(nameof(GetCustomerById), mainCustomer);
    }

    public static async Task<IResult> DeleteCustomer(int id)
    {
        await _customers.RemoveCustomer(id);
        return TypedResults.Ok(id);
    }

    public static async Task<IResult> UpdateCustomer(int id, AddEditCustomerDto customer)
    {
        var book = await _customers.GetCustomer(id);

        if (book is null)
        {
            return TypedResults.NotFound();
        }
        var mainCustomer = new Customer(
            customer.FirstName,
            customer.LastName,
            customer.Gender,
            customer.Age
        );
        mainCustomer.NationalCode = customer.NationalCode;
        await _customers.UpdateCustomer(id, mainCustomer);
        return TypedResults.NoContent();
    }
}
