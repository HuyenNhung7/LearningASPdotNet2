using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningASPdotNet2.DTOs
{
    // public class BackpackDto
    // {
    //     public string Description { get; set; }  
    // }
    // Khi bạn khởi tạo một đối tượng phải truyền vào description và des này ko thể thay đổi
    public record struct BackpackDto(string Description);
}