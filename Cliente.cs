public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Saldo { get; set; }
    public List<Compra> Compras { get; set; } = new List<Compra>();
}