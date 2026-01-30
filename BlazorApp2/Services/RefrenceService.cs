using System.Text.Json;
using YourApp.Models;

namespace YourApp.Services;

public sealed class ReferenceService
{
    private readonly IWebHostEnvironment _env;

    public ReferenceService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<List<RefUser>> GetUsersFromLocalJsonAsync()
    {
        // ContentRootPath pekar på projektets körkatalog (där Data/ ska finnas efter copy)
        var path = Path.Combine(_env.ContentRootPath, "Data", "users.json");

        if (!File.Exists(path))
            throw new FileNotFoundException("Hittar inte Data/users.json i körmiljön.", path);

        var json = await File.ReadAllTextAsync(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var users = JsonSerializer.Deserialize<List<RefUser>>(json, options);
        return users ?? new List<RefUser>();
    }
}
