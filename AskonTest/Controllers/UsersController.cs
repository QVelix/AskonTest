using AskonTest.Database;
using AskonTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskonTest.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly AskonTestContext _dbContext;

	public UsersController(AskonTestContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Users>>> Get()
	{
		return await _dbContext.Users.ToListAsync();
	}

	[HttpPost]
	public async Task<ActionResult> Post(long id, Users user)
	{
		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync();
		return CreatedAtAction("Get", user);
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(long id)
	{
		var user = await _dbContext.Users.FindAsync(id);
		_dbContext.Users.Remove(user);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Put(Users user)
	{
		_dbContext.Entry(user).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}