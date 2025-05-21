public class GerarRelatorios
{
    public static void RelatorioVenda(Loja loja)
    {
        string caminhoArquivo = $"{loja.Nome}(RelatorioVendas)"; //Caminho personalizado
        decimal totalValorVendas = 0;
        int totalVendas = 0;

        if (loja.vendas.Count == 0)
        {
            Console.WriteLine("A loja não possui vendas.");
            return;
        }

        foreach (var vendas in loja.vendas)
        {
            totalValorVendas += vendas.PrecoProduto;
            totalVendas++;
            File.AppendAllText(caminhoArquivo, $"Produto: {vendas.NomeProduto}. Data: {vendas.Data}. Preço: {vendas.PrecoProduto}" + "\n");
        }
        File.AppendAllText(caminhoArquivo, $"Total de vendas realizada: {totalVendas}. Valor total vendido: {totalValorVendas:C2}R$");
    }

    public static void RelatorioEstoque(Loja loja, int quantidade)
    {
        string caminhoArquivo = $"{loja.Nome}(RelatorioEstoque)";
        List<Produto> listaTempProdutos = new List<Produto>();

        foreach (var categoria in loja.Categorias)
        {
            foreach (var prod in categoria.Produtos) //itera sobre categoria e lista de produtos
            {
                if (prod.Estoque <= quantidade)
                {
                    listaTempProdutos.Add(prod);
                }
            }
        }
        int qtddProdutos = listaTempProdutos.Count;

        if (listaTempProdutos.Count == 0)
        {
            Console.WriteLine("Nenhum produto com estoque semelhante");
            return;
        }

        foreach (var prod in listaTempProdutos)
        {
            File.AppendAllText(caminhoArquivo, $"Produto: {prod.Nome}. Quantidade em estoque: {prod.Estoque}" + "\n"); //escreve no arquivo
        }
        File.AppendAllText(caminhoArquivo, $"Total de produtos: {qtddProdutos}");
    }
}