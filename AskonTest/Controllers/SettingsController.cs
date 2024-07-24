using AskonTest.Database;
using AskonTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskonTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
	private readonly AskonTestContext _dbContext;

	public SettingsController(AskonTestContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Settings>>> Get()
	{
		return await _dbContext.Settings.ToListAsync();
	}

	[HttpPost]
	public async Task<ActionResult> Post(long id, Settings settings)
	{
		_dbContext.Settings.Add(settings);
		await _dbContext.SaveChangesAsync();
		return CreatedAtAction("Get", settings);
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(long id)
	{
		var settings = await _dbContext.Settings.FindAsync(id);
		_dbContext.Settings.Remove(settings);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Put(Settings settings)
	{
		_dbContext.Entry(settings).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}