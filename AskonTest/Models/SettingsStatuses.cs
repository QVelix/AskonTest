namespace AskonTest.Models;

public class SettingsStatuses
{
	public string Status { get; set; }
	public long ApplicationId { get; set; }
	public long? UserId { get; set; } = null;
	public long SettingsId { get; set; }
	public Applications Application { get; set; } = null!;
	public Users? User { get; set; } = null;
	public Settings Settings { get; set; } = null!;
}