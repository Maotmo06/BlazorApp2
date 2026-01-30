using BlazorApp2.Components;
using YourApp.Services;
using System.ComponentModel.DataAnnotations;
using YourApp.Dtos;
using YourApp.Models;
using YourApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IssueService>();
builder.Services.AddScoped<ReferenceService>();
var app = builder.Build();

app.MapGet("/api/issues", (IssueService service) =>
{
    // Returnerar JSON-array av Issue
    return Results.Ok(service.GetAll());
});

app.MapGet("/api/issues/{id:int}", (int id, IssueService service) =>
{
    var issue = service.GetById(id);
    return issue is null ? Results.NotFound() : Results.Ok(issue);
});

app.MapPost("/api/issues", (CreateIssueDto dto, IssueService service) =>
{
    // Minimal API kör inte DataAnnotations automatiskt för DTO.
    // Validera explicit för att få förutsägbara fel (som i Express middleware).
    var errors = Validate(dto);
    if (errors is not null)
        return Results.BadRequest(errors);

    var created = service.Add(dto.Title, dto.Description);
    return Results.Created($"/api/issues/{created.Id}", created);
});

app.MapPatch("/api/issues/{id:int}/status", (int id, UpdateIssueStatusDto dto, IssueService service) =>
{
    var errors = Validate(dto);
    if (errors is not null)
        return Results.BadRequest(errors);

    var ok = service.UpdateStatus(id, dto.Status);
    return ok ? Results.NoContent() : Results.NotFound();
});

// Enkel helper för DataAnnotations-validering i Minimal API
static Dictionary<string, string[]>? Validate(object dto)
{
    var context = new ValidationContext(dto);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

    if (isValid) return null;

    // Gruppér fel per property
    var dict = new Dictionary<string, List<string>>();
    foreach (var r in results)
    {
        var key = r.MemberNames.FirstOrDefault() ?? "_";
        if (!dict.ContainsKey(key))
            dict[key] = new List<string>();

        dict[key].Add(r.ErrorMessage ?? "Validation error.");
    }

    return dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
 
app.Run();
