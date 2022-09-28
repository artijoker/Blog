using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.Domain.Services;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.Repositories;
using Blog.Server.Filters;
using Blog.Server.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtConfig = builder.Configuration
        .GetSection("JwtConfig")
        .Get<JwtConfig>();


const string dbPath = "myapp.db";
var connectionString = "server=localhost;port=3306;user=root;password=56537;database=NewMyBlog";
builder.Services.AddDbContext<AppDbContext>(
    //options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    options => options.UseSqlite($"Data Source={dbPath};")
    );

builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,

                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudiences = new[]
                {
                    jwtConfig.Audience
                },
                ValidIssuer = jwtConfig.Issuer
            };
        });



builder.Services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEf>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<JwtTokenService>();




builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();

});

var app = builder.Build();

app.UseMiddleware<JwtTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear();
//options.DefaultFileNames.Add("user.html");
//options.DefaultFileNames.Add("admin.html");

//app.UseDefaultFiles(options);
app.UseStaticFiles();


app.UseCors(policy =>
{
    policy.AllowAnyMethod()
     .AllowAnyHeader()
     .WithOrigins("http://localhost:4200", "http://localhost:63903")
     .AllowCredentials();
});






app.Run();

