using ForumWebsite.Datas;
using ForumWebsite.Repositories.Comments;
using ForumWebsite.Repositories.ForumThreads;
using ForumWebsite.Repositories.Users;
using ForumWebsite.Repositories.Votes;
using ForumWebsite.Services.Comments;
using ForumWebsite.Services.ForumThreads;
using ForumWebsite.Services.Users;
using ForumWebsite.Services.Votes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Forum API", Version = "v1" });

    // ✅ Cấu hình xác thực Bearer Token cho Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token theo định dạng: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Key"];


builder.Services.AddAuthentication(options =>
{
    // Chọn scheme mặc định là JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    //Cấu hình cách xác thực token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier,
        ValidateIssuer = true, 
        ValidateAudience = true,
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 

        ValidIssuer = jwtSettings["Issuer"], //  Phải trùng Issuer trong token
        ValidAudience = jwtSettings["Audience"], //  Phải trùng Audience trong token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)), //  Khóa bí mật để xác thực chữ ký
        ClockSkew = TimeSpan.Zero //  Không cho thời gian lệch (mặc định cho phép lệch 5 phút)
    };
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
builder.Services.AddScoped<ICommentVoteRepository, CommentVoteRepository>();
builder.Services.AddScoped<IForumThreadVoteRepository, ForumThreadVoteRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IForumThreadService, ForumThreadService>();
builder.Services.AddScoped<ICommentService, CommentService>();
//builder.Services.AddScoped<ICommentVoteService, CommentVoteService>();



var app = builder.Build();
Console.WriteLine("🛠️ Secret key backend đang dùng: " + secretKey);
Console.WriteLine("🌍 ENV đang chạy: " + builder.Environment.EnvironmentName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    Console.WriteLine("🛠️ Secret key backend đang dùng: " + secretKey);
    Console.WriteLine("🌍 ENV đang chạy: " + builder.Environment.EnvironmentName);
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
