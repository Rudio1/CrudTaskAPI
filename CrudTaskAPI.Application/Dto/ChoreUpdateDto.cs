using CrudTaskAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTaskAPI.Application.Dto
{
    public class ChoreUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsCompleted { get; set; }
        public ChoreProgressEnum Progress { get; set; }
    }
}
