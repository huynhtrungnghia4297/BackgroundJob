using BackgroundJob;
using Hangfire;
using Hangfire.MemoryStorage;


var builder = WebApplication.CreateBuilder(args);





// Cấu hình MailSettings
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<EmailService>();
// Cấu hình dịch vụ cho Hangfire
builder.Services.AddHangfire(configuration =>
    configuration.UseMemoryStorage()); // Sử dụng bộ nhớ trong để lưu trữ job


builder.Services.AddHangfireServer(); // Thêm Hangfire server để xử lý background jobs
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddTransient<PasswordChangeJob>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cấu hình Hangfire Dashboard để giám sát các job
app.UseHangfireDashboard();


//RecurringJob.AddOrUpdate<PasswordChangeJob>(
//    "CheckPasswordChange",
//    job => job.Execute(),
//    Cron.Daily);

RecurringJob.AddOrUpdate<PasswordChangeJob>(
    "CheckPasswordChange",
    job => job.Execute(),
    "*/1 * * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
