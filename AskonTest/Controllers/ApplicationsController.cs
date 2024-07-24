using AskonTest.Database;
using AskonTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskonTest.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplcationsController : ControllerBase
{
	private readonly AskonTestContext _dbContext;

	public ApplcationsController(AskonTestContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Applications>>> Get()
	{
		return await _dbContext.Applications.ToListAsync();
	}

	[HttpPost]
	public async Task<ActionResult> Post(long id, Applications application)
	{
		_dbContext.Applications.Add(application);
		await _dbContext.SaveChangesAsync();
		return CreatedAtAction("Get", application);
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(long id)
	{
		var app = await _dbContext.Applications.FindAsync(id);
		_dbContext.Applications.Remove(app);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Put(Applications application)
	{
		_dbContext.Entry(application).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}