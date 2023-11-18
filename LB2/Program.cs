using LB2.Services;
using LB2.Services.Contracts;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddOptions<RsaOptions>().BindConfiguration(nameof(RsaOptions));
builder.Services.AddSingleton<RsaOptions>(sp =>
    sp.GetRequiredService<IOptions<RsaOptions>>().Value
);
builder.Services.AddSingleton<IRsaService, RsaService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

public class RsaOptions
{
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
}