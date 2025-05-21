using System.Windows.Markup;

public class GerenciadorLoja
{
    public static List<Loja> lojas { get; set; } = new List<Loja>(); //Lista de lojas

    public static void CadastrarLoja(string nome, string cidade, string pais)
    {
        var loc = new Localizacao { Cidade = cidade, Pais = pais };
        lojas.Add(new Loja { Nome = nome, Localizacao = loc });

        Console.WriteLine("Loja cadastrada com sucesso!");
    }

    public static void CriarCategoria(Loja loja, int id, string nome)
    {
        loja.Categorias.Add(new Categoria { Nome = nome, Id = id });
    }

    public static Loja BuscarLoja(string nomeLoja)
    {
        var lojaBuscada = (from loja in lojas
                           where loja.Nome == nomeLoja
                           select loja).FirstOrDefault();

        return lojaBuscada;
    }

    public static Categoria BuscarCategoria(Loja loja, int id)
    {
        var categoriaBuscada = (from categoria in loja.Categorias
                                where categoria.Id == id
                                select categoria).FirstOrDefault();

        return categoriaBuscada;
    }

    public static Produto BuscarProdutoLoja(string nome)
    {
        Produto produto = new Produto();

        foreach (var loja in lojas)
        {
            foreach (var categoria in loja.Categorias)
            {
                foreach (var prod in categoria.Produtos)
                {
                    if (prod.Nome == nome)
                    {
                        return produto = prod;
                    }
                }
            }
        }
        return produto;
    }

    public static void AdicionarProdutoCategoria(Categoria categoria, string nome, int id, decimal valor, int quantidade)
    {
        categoria.Produtos.Add(new Produto { Nome = nome, Estoque = quantidade, Id = id, Preco = valor });
    }

    public static void RealizarVenda(Loja loja, Produto prod)
    {
        loja.vendas.Add(new Vendas { NomeProduto = prod.Nome, Data = DateTime.Now, PrecoProduto = prod.Preco });
    }
}