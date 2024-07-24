using AskonTest.Database;
using AskonTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskonTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsStatusesController : ControllerBase
{
	private readonly AskonTestContext _dbContext;

	public SettingsStatusesController(AskonTestContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<SettingsStatuses>>> Get()
	{
		return await _dbContext.SettingsStatuses.ToListAsync();
	}

	[HttpPost]
	public async Task<ActionResult> Post(long id, SettingsStatuses settingsStatuses)
	{
		_dbContext.SettingsStatuses.Add(settingsStatuses);
		await _dbContext.SaveChangesAsync();
		return CreatedAtAction("Get", settingsStatuses);
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(long id)
	{
		var settingsStatuses = await _dbContext.SettingsStatuses.FindAsync(id);
		_dbContext.SettingsStatuses.Remove(settingsStatuses);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Put(SettingsStatuses settingsStatuses)
	{
		_dbContext.Entry(settingsStatuses).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}