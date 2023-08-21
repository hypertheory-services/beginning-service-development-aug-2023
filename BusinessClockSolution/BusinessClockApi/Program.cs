using BusinessClockApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Above this line is "configuring" our web application services.
var app = builder.Build();
// After this line is "middleware" - the stuff that receives the HTTP request and makes
// the response. 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/status", () =>
{
    var fakeResponse = new ClockResponseModel
    {
        IsOpen = true,
        SupportContact = new SupportContactResponseModel
        {
            Name = "Mitch",
            Phone = "800 555-1212",
            Email = "mitch@company.com"
        }
    };
    return Results.Ok(fakeResponse);
});


// this is what starts our web server. This is a blocking call. It will keep running forever.
app.Run();

