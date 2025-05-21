using System.Text.Json.Serialization;

public class Loja
{
    public string Nome { get; set; }
    public Localizacao Localizacao { get; set; }
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    [JsonIgnore]
    public List<Vendas> vendas { get; set; } = new List<Vendas>();
}
