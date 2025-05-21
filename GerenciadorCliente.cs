class GerenciadorCliente
{
    public static List<Cliente> listaClientes { get; set; } = new List<Cliente>();

    public static void AdicionarClienteALista(string nome, int id) //Adiciona um cliente a lista
    {
        listaClientes.Add(new Cliente { Nome = nome, Id = id, Saldo = 0.00m });
    }

    public static Cliente RetornarCliente(int id) //Retorna um cliente que pode ser null ou não
    {
        var cliente = (from client in listaClientes
                       where client.Id == id
                       select client).FirstOrDefault();

        return cliente;
    }

    public static void AdicionarSaldoCliente(Cliente cliente, decimal saldo)
    {
        cliente.Saldo = saldo;
    }

    public static void RealizarCompra(Cliente cliente, Produto produto)
    {
        cliente.Saldo -= produto.Preco;
        produto.Estoque -= 1;

        cliente.Compras.Add(new Compra { ProdutoId = produto.Id, Data = DateTime.Now });
    }

    public static void ConsultarCompras(Cliente cliente)
    {
        if (cliente.Compras.Count == 0)
        {
            Console.WriteLine("O cliente não possui nenhuma compra.");
            return;
        }
        foreach (var compra in cliente.Compras)
        {
            Console.WriteLine($"Produto ID: {compra.ProdutoId}. Data: {compra.Data}");
        }
    }
}
