using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{    
    public readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        this._service = service;        
    }

    // 6 - Sua aplicação deve ter o endpoint GET /user/:email
    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {                
        var users = new UserService();
        bool existing = users.UserExists(email);
        if (!existing)
        {
            return NotFound();
        }
        return Ok(users.GetUser(email));
    }

    // 7 - Sua aplicação deve ter o endpoint POST /user
    [HttpPost]
    public IActionResult Create([FromBody]User user)
    {
        var users = new UserService();
        users.AddUser(user);
        return StatusCode(201, user);
    }

    // "8 - Sua aplicação deve ter o endpoint PUT /user
    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody]User user)
    {
        var users = new UserService();
        bool existingParam = users.UserExists(email);
        bool existingUser = users.UserExists(user.Email);

        if (!existingParam)
        {
            return NotFound();
        }
        else if (!existingUser)
        {
            return BadRequest();
        }

        users.UpdateUser(user);
        return Ok(user);
    }

    // 9 - Sua aplicação deve ter o endpoint DEL /user
    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        var users = new UserService();
        bool existing = users.UserExists(email);
        if (existing)
        {
            users.DeleteUser(email);
            return NoContent();
        }

        return NotFound();
    } 
}