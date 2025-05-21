using System.Text.Json;
public class RepositorioPersistencia
{
    public static void PersistirDados()
    {
        var options = new JsonSerializerOptions { WriteIndented = true }; //Para formatar json

        if (GerenciadorLoja.lojas.Count > 0)
        {
            string jsonLojas = JsonSerializer.Serialize(GerenciadorLoja.lojas, options);
            File.WriteAllText("Lojas.json", jsonLojas);
            Console.WriteLine("Dados de lojas persistidos!");
        }
        else
        {
            Console.WriteLine("Não há dados de lojas para salvar.");
        }
        if (GerenciadorCliente.listaClientes.Count > 0)
        {
            string jsonClientes = JsonSerializer.Serialize(GerenciadorCliente.listaClientes, options);
            File.WriteAllText("Clientes.json", jsonClientes);
            Console.WriteLine("Dados de clientes persistidos!");
        }
        else
        {
            Console.WriteLine("Não há dados de clientes para salvar.");
        }
    }

    public static void DeserializarLoja()
    {
        if (File.Exists("Lojas.json"))
        {
            string jsonLoja = File.ReadAllText("Lojas.json");

            if (!string.IsNullOrEmpty(jsonLoja))
            {
                GerenciadorLoja.lojas = JsonSerializer.Deserialize<List<Loja>>(jsonLoja);
                Console.WriteLine("Lista de lojas preenchidas com dados salvos!");
            }
            else
            {
                Console.WriteLine("Não há dados salvos de loja para utilizar.");
            }
        }
        else
        {
            Console.WriteLine("Não há um arquivo de lojas.");
        }
    }

    public static void DeserializarClientes()
    {
        if (File.Exists("Clientes.json"))
        {

            string jsonClientes = File.ReadAllText("Clientes.json");

            if (!string.IsNullOrEmpty(jsonClientes))
            {
                GerenciadorCliente.listaClientes = JsonSerializer.Deserialize<List<Cliente>>(jsonClientes);
                Console.WriteLine("Lista de clientes preenchidas com dados salvos!");
            }
            else
            {
                Console.WriteLine("Não há dados salvos de clientes para utilizar.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Não há um arquivo de clientes.");
        }
    }
}