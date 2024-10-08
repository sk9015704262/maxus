using AccountingAPI.Application.Interfaces;
using AccountingAPI.Responses;
using Maxus.Application.Common;
using Maxus.Application.Common.Mapping;
using Maxus.Application.Interfaces;
using Maxus.Application.Services;
using Maxus.Domain.Interfaces;
using Maxus.Infrastructure.Helpers;
using Maxus.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    options.InvalidModelStateResponseFactory = context =>
//    {
//        var errors = context.ModelState
//            .Where(e => e.Value.Errors.Count > 0)
//            .ToDictionary(
//                e => e.Key,
//                e => e.Value.Errors.Select(err => err.ErrorMessage).ToList()
//            );

//        var response = new ApiResponse<object>(errors, "Validation Failed");

//        return new BadRequestObjectResult(response);
//    };
//});
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder
       .AllowAnyHeader()
       .AllowAnyOrigin()
       .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IIndustrySegmentsService, IndustrySegmentsService>();
builder.Services.AddScoped<IIndustrySegmentsRepository, IndustrySegmentsRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IVisitReportChekListRepository, VisitReportChekListRepository>();
builder.Services.AddScoped<IVisitReportChekListService, VisitReportChekListService>();
builder.Services.AddScoped<ICustomerFeedBackRepository, CustomerFeedbackRepository>();
builder.Services.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
builder.Services.AddScoped<ICustomerFeedbackOptionRepository, CustomerFeedbackOptionRepository>();
builder.Services.AddScoped<ICustomerFeedbackOptionService, CustomerFeedbackOptionService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<ITraningReportRepository, TrainingReportRepository>();
builder.Services.AddScoped<ITrainingReportService, TraninigReportService>();
builder.Services.AddScoped<IUserFormRepository, UserFormRightRepository>();
builder.Services.AddScoped<IUserFormRightService, UserFormRightService>();
builder.Services.AddScoped<IUserRightRepository, UserRightsRepository>();
builder.Services.AddScoped<IUserRightsService, UserRightService>();
builder.Services.AddScoped<IAttachmentLimitRepository, AttachmentLimitsRepository>();
builder.Services.AddScoped<IAttachmentLimitsService, AttachmentLimitsService>();
builder.Services.AddScoped<IMOMReportRepository, MOMReportRepository>();
builder.Services.AddScoped<IMOMReportService, MOMReportService>();
builder.Services.AddScoped<ICustomerFeedbackReportRepository, CustomerFeedbackReportRepository>();
builder.Services.AddScoped<ICustomerFeedbackReportService, CustomerFeedbackReportService>();
builder.Services.AddScoped<IVisitReportRepository , VisitReportRepository>();
builder.Services.AddScoped<IVisitReportService, VisitReportService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();
builder.Services.AddScoped<IDashBoardService, dashBoardService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddHttpClient();


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();
