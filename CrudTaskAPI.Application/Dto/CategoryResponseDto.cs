using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTaskAPI.Application.Dto
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChoreResponseDto> Chores { get; set; }
    }
} 