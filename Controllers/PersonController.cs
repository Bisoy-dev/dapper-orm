using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonDetailWithQrCodeApp.Models;

namespace PersonDetailWithQrCodeApp.Controllers;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase
{
    private readonly SqlConnection _connection;
    public PersonController(SqlConnection connection)
    {
        _connection = connection;
        _connection.Open();
    } 

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _connection.QueryAsync<Person>("SELECT * FROM Person;");
        return Ok(people);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var people = await _connection.QueryFirstAsync<Person>("SELECT * FROM Person WHERE Id = @Id;", new { Id = id });
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Person newPerson)
    {
        var sql = "INSERT INTO Person(FirstName, LastName, CreatedAt, QRCode) VALUES(@FirstName, @LastName, @CreatedAt, @QRCode)";
        await _connection.ExecuteAsync(sql, newPerson);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody]Person updatedPerson)
    {
        var sql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, updatedPerson);
        return Ok();
    } 
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var sql = "DELETE FROM Person WHERE Id = @Id;";
        await _connection.ExecuteAsync(sql, new { Id = id });
        return Ok();
    }
}