using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CrudTaskAPI.Domain.Enums;

namespace CrudTaskAPI.Domain.Entities
{
    public class Chore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool isCompleted { get; set; }
        public int CategoryId { get; set; }
        public ChoreProgressEnum Progress { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category Category = new Category();

        public Chore() 
        {
            Progress = ChoreProgressEnum.ToDo;
            CreatedAt = DateTime.UtcNow;
        }

        public Chore(string name, string description, int categoryId, bool active, bool isCompleted)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Active = active;
            this.isCompleted = isCompleted;
            Progress = ChoreProgressEnum.ToDo;
            CreatedAt = DateTime.UtcNow; 
        }
    }
}
