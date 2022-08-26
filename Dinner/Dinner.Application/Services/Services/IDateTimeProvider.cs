namespace Dinner.Application.Services.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; set; }
    
    }
}