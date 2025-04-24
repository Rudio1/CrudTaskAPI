using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public Category Category = new Category();

        public Chore() 
        {
        }

        public Chore(string name, string description, int categoryId, bool active, bool isCompleted)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Active = active;
            this.isCompleted = isCompleted;
        }
    }
}
