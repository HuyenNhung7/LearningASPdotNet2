global using Microsoft.EntityFrameworkCore;
global using LearningASPdotNet2.Models;
using LearningASPdotNet2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// Đăng ký các controllers để xử lý các yêu cầu HTTP
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Thêm hỗ trợ để khám phá và cung cấp thông tin về các điểm cuối (endpoints) API
builder.Services.AddEndpointsApiExplorer();
// Thêm dịch vụ để tạo tài liệu API dựa trên Swagger/OpenAPI.
builder.Services.AddSwaggerGen();

// typeof(Program).Assembly cái này lấy ra assembly (tập tin .dll) mà Program thuộc về.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Thêm vào dịch vụ chúng ta tạo nè với lifetime là scoped
// builder.Services.AddScoped<ICharacterService, CharacterService>();

// bước khởi tạo thực sự cho ứng dụng web
var app = builder.Build();

// Configure the HTTP request pipeline.
// Cấu hình HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// một middleware trong ASP.NET Core dùng để chuyển 
// hướng các yêu cầu HTTP sang HTTPS
app.UseHttpsRedirection();

// Thêm hỗ trợ xác thực và ủy quyền
app.UseAuthorization();

// Định tuyến đến các controllers
app.MapControllers();

//Bắt đầu lắng nghe và xử lý các yêu cầu HTTP
app.Run();