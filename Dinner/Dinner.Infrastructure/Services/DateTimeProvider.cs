using Dinner.Application.Services.Services;

namespace Dinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get; set; }
}