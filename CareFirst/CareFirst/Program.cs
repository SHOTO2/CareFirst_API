using CareFirst.Context;
using CareFirst.IRepository;
using CareFirst.Repository;
using CareFirst.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//Services Options

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
var smtpOptions = builder.Configuration.GetSection("Smtp").Get<SmtpOptions>();
var twilioOptions = builder.Configuration.GetSection("Twilio").Get<TwilioOptions>();

builder.Services.AddSingleton(jwtOptions!);
builder.Services.AddSingleton(smtpOptions!);
builder.Services.AddSingleton(twilioOptions!);

////AddScop To Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHash, PasswordHash>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<INurseRepository, NurseRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

////set options to Services JWT
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
    {
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions!.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audiennce,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey!))
        };
    });

//Coniction String
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);

}, ServiceLifetime.Transient);

builder.Services.AddOpenApi();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"root")),
//    RequestPath="/Resources"
//});


app.UseAuthorization();

app.MapControllers();

app.Run();
