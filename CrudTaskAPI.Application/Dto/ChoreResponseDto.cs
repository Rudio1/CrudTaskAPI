using CrudTaskAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTaskAPI.Application.Dto
{
    public class ChoreResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ChoreProgressEnum Progress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 