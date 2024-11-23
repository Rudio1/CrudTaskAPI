using CrudTaskAPI.Domain.Entities;
using System.Text.Json.Serialization;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Adicionando a coleção de Chores
    [JsonIgnore]
    public ICollection<Chore> Chores { get; set; }  // Relacionamento com a entidade Chore
}
