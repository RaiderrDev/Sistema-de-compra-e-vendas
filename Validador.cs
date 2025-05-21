public class Validador
{
    public static bool ValidarString(string entrada, int tamanhoMin, string erroMensagem) //Válida strings 
    {
        if (string.IsNullOrEmpty(entrada) || entrada.Length < tamanhoMin)
        {
            Console.WriteLine(erroMensagem);
            return false;
        }
        return true;
    }

    public static bool ValidarInt(string entrada, out int saida, string erroMensagem, int valorMin) //Válida int
    {
        if (!int.TryParse(entrada, out saida) || saida < valorMin)
        {
            Console.WriteLine(erroMensagem);
            return false;
        }
        return true;
    }

    public static bool ValidarDecimal(string entrada, out decimal saida, string erroMensagem, decimal valorMin) //Válida decimal
    {
        if (!decimal.TryParse(entrada, out saida) || saida < valorMin)
        {
            Console.WriteLine(erroMensagem);
            return false;
        }
        return true;
    }

    public static bool VerificarSeJaExisteID(int idCliente) //Verifica se um ID existe 
    {
        bool JaExiste = (from cliente in GerenciadorCliente.listaClientes
                         where cliente.Id == idCliente
                         select cliente).Any();
        if (JaExiste)
        {
            return true;
        }
        
        return false;
    }
}