using CrudTaskAPI.Domain.Entities;
using System.Text.Json.Serialization;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Chore> Chores { get; set; }
}
