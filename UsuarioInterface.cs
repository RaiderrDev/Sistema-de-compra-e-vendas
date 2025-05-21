using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Runtime.InteropServices;

public class UsuarioInterface
{
    #region GerenciamentoLoja

    public void MenuCadastrarLoja() //Menu interface para cadastrar produtos
    {
        Console.WriteLine("Digite o nome da loja:");
        string nomeLoja = Console.ReadLine();

        if (!Validador.ValidarString(nomeLoja, 3, "Tente novamente com um nome com mais de 3 letras.")) //Método que válida a string
        {
            return;
        }
        Console.WriteLine("Digite em que cidade fica a loja:");
        string cidadeLoja = Console.ReadLine();

        if (!Validador.ValidarString(cidadeLoja, 2, "Tente novamente com um nome de cidade com mais de 2 letras."))
        {
            return;
        }
        Console.WriteLine("Digite o páis da loja:");
        string paisLoja = Console.ReadLine();

        if (!Validador.ValidarString(paisLoja, 3, "Tente novamente com um nome de país com mais de 3 caracteres."))
        {
            return;
        }

        GerenciadorLoja.CadastrarLoja(nomeLoja, cidadeLoja, paisLoja); //Passa os dados pro gerenciador de lojas
    }


    public void MenuListarLojas() //Menu interface para listar lojas
    {
        Console.WriteLine("====Lojas====");
        if (GerenciadorLoja.lojas.Count == 0)
        {
            Console.WriteLine("Nenhuma loja para listar.");
            return;
        }
        foreach (var loja in GerenciadorLoja.lojas) //Itera sobre a lista de lojas em gerenciadorLoja
        {
            Console.WriteLine($"Nome: {loja.Nome}. Localização: {loja.Localizacao.Cidade}. Quantidade de categorias: {loja.Categorias.Count}");
        }
    }

    #endregion GerenciamentoLoja



    #region GerenciamentoProdutos

    public void MenuCriarCategoria()
    {
        Console.WriteLine("Digite o nome da loja que deseja criar a categoria de produtos:");
        string nomeLoja = Console.ReadLine();

        if (!Validador.ValidarString(nomeLoja, 3, "Tente novamente com um nome válido")) //Método que válida string
        {
            return;
        }

        Loja lojaBuscada = GerenciadorLoja.BuscarLoja(nomeLoja); //Retorna uma loja que pode ser null ou não

        if (lojaBuscada == null)
        {
            throw new LojaNaoEncontradaException("Nenhuma loja encontrada com esse nome."); //Exception se for null
        }

        Console.WriteLine($"Loja encontrada. Nome: {lojaBuscada.Nome}");
        Console.WriteLine("Digite um ID para a categoria:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um id válido", 0)) //Método pra validar int
        {
            return;
        }

        Console.WriteLine("Digite o nome da categoria, Exemplo: (Bolsas de couro):");
        string nomeCategoria = Console.ReadLine();

        if (!Validador.ValidarString(nomeCategoria, 2, "Tente novamente com um nome de categoria válida."))
        {
            return;
        }

        GerenciadorLoja.CriarCategoria(lojaBuscada, idValido, nomeCategoria); //Cria a categoria
        Console.WriteLine("Categoria criada com sucesso!");
    }


    public void MenuCriarProduto()
    {
        Console.WriteLine("Digite o nome da loja que deseja criar um produto:");
        string nomeLoja = Console.ReadLine();

        Loja lojaBuscada = GerenciadorLoja.BuscarLoja(nomeLoja);

        if (lojaBuscada == null)
        {
            throw new LojaNaoEncontradaException("Loja não encontrada."); //Exception se nao encontrar pelo nome
        }

        Console.WriteLine($"Loja encontrada: {lojaBuscada.Nome}");
        Console.WriteLine("Digite o ID da categoria para adicionar o produto:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um id válido", 0)) //Método pra validar int
        {
            return;
        }

        Categoria categoria = GerenciadorLoja.BuscarCategoria(lojaBuscada, idValido); //Retorna uma categoria que pode ser null se nao encontrar

        if (categoria == null)
        {
            Console.WriteLine("Nenhuma categoria encontrada com esse ID.");
            return;
        }
        Console.WriteLine("Digite o nome do produto:");
        string nomeProduto = Console.ReadLine();

        if (!Validador.ValidarString(nomeProduto, 3, "Tente novamente com um nome de produto válido."))
        {
            return;
        }

        Console.WriteLine("Digite o id do produto:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idProdutoValido, "Tente novamente com um id válido.", 0))
        {
            return;
        }

        Console.WriteLine("Digite o preço do produto:");
        if (!Validador.ValidarDecimal(Console.ReadLine(), out decimal valorValido, "Tente novamente com um valor válido (acima de 5R$.)", 5.00m)) //Método pra validar decimal
        {
            return;
        }
        Console.WriteLine("Digite a quantidade em estoque:");
        if (!Validador.ValidarInt(Console.ReadLine(), out int quantidadeValida, "Tente novamente com uma quantidade válida de estoque", 1))
        {
            return;
        }
        GerenciadorLoja.AdicionarProdutoCategoria(categoria, nomeProduto, idProdutoValido, valorValido, quantidadeValida); //Adiciona o produto na categoria
        Console.WriteLine("Produto adicionado com sucesso!");
    }


    public void MenuListarProdutos()
    {
        Console.WriteLine("Digite o nome da loja:");
        string nomeLoja = Console.ReadLine();

        Loja loja = GerenciadorLoja.BuscarLoja(nomeLoja); //retorna um objeto loja se encontrar pelo nome

        if (loja == null)
        {
            throw new LojaNaoEncontradaException("Nenhuma loja encontrada com esse nome.");
        }

        if (loja.Categorias.Count == 0)
        {
            Console.WriteLine("Essa loja não possui categorias.");
            return;
        }

        foreach (var categoria in loja.Categorias) //Itera sobre as categorias da loja
        {
            Console.WriteLine($"===={categoria.Nome}====");
            foreach (var produto in categoria.Produtos)
            {
                Console.WriteLine($"Nome: {produto.Nome}. Preço: {produto.Preco:C2}R$. Quantidade: {produto.Estoque}. ID: {produto.Id}");
            }
        }
    }

    #endregion GerenciamentoProdutos


    #region GerenciamentoClientes

    public void MenuCriarCliente()
    {
        Console.WriteLine("Digite o nome do cliente:");
        string nomeCliente = Console.ReadLine();

        if (!Validador.ValidarString(nomeCliente, 3, "Tente novamente com um nome com pelo menos 3 caracteres."))
        {
            return;
        }

        Console.WriteLine("Digite um ID:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um ID válido.", 0))
        {
            return;
        }

        if (Validador.VerificarSeJaExisteID(idValido))
        {
            Console.WriteLine("Esse id já existe. Tente novamente com outro ID.");
            return;
        }

        GerenciadorCliente.AdicionarClienteALista(nomeCliente, idValido);
        Console.WriteLine("Cliente cadastrado com sucesso!");
    }


    public void MenuAdicionarSaldoCliente()
    {
        Console.WriteLine("Digite o ID do cliente:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um ID válido.", 0))
        {
            return;
        }

        Cliente clienteBuscado = GerenciadorCliente.RetornarCliente(idValido);

        if (clienteBuscado == null)
        {
            throw new ClienteNaoEncontradoException("Nenhum cliente encontrado com esse ID.");
        }
        Console.WriteLine($"Quanto de saldo você deseja adicionar?");

        if (!Validador.ValidarDecimal(Console.ReadLine(), out decimal saldoValido, "Tente novamente com um saldo válido. (Acima de 10.00R$)", 10.00m))
        {
            return;
        }
        GerenciadorCliente.AdicionarSaldoCliente(clienteBuscado, saldoValido);
        Console.WriteLine($"Saldo adicionado com sucesso a: {clienteBuscado.Nome}. Valor: {saldoValido:C2}R$.");
    }


    public void MenuClienteFazerCompras()
    {
        Console.WriteLine("Digite o ID do cliente:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um ID válido.", 0))
        {
            return;
        }

        Cliente clienteBuscado = GerenciadorCliente.RetornarCliente(idValido);

        if (clienteBuscado == null)
        {
            throw new ClienteNaoEncontradoException($"Nenhum cliente encontrado com esse ID: {idValido}");
        }

        Console.WriteLine("Deseja fazer compras em qual loja? digite o nome:");
        string nomeLoja = Console.ReadLine();
        Loja lojaBuscada = GerenciadorLoja.BuscarLoja(nomeLoja);

        if (lojaBuscada == null)
        {
            throw new LojaNaoEncontradaException($"Nenhuma loja encontrada com esse nome: {nomeLoja}");
        }
        Console.WriteLine("Digite o nome do produto:");
        string nomeProduto = Console.ReadLine();
        Produto produto = GerenciadorLoja.BuscarProdutoLoja(nomeProduto);

        if (produto.Nome == null)
        {
            Console.WriteLine("Nenhum produto encontrado com esse nome.");
            return;
        }
        Console.WriteLine($"Produto: {produto.Nome}. Preço: {produto.Preco:C2}R$. Quantidade em estoque: {produto.Estoque}");

        if (produto.Estoque == 0)
        {
            Console.WriteLine("Produto sem estoque.");
            return;
        }

        if (clienteBuscado.Saldo < produto.Preco)
        {
            throw new SaldoInsuficienteExcepException($"Você não tem saldo suficiente para concluir essa compra. Seu saldo: {clienteBuscado.Saldo:C2}R$.");
        }

        GerenciadorCliente.RealizarCompra(clienteBuscado, produto); //Adiciona compra na lista de compras do cliente
        GerenciadorLoja.RealizarVenda(lojaBuscada, produto); //Adiciona venda na lista de vendas da loja
        Console.WriteLine($"Parábens {clienteBuscado.Nome}, Você comprou {produto.Nome} por {produto.Preco:C2}R$.");
    }

    public void MenuClienteConsultarCompras()
    {
        Console.WriteLine("Digite o ID do cliente:");

        if (!Validador.ValidarInt(Console.ReadLine(), out int idValido, "Tente novamente com um ID válido.", 0))
        {
            return;
        }

        Cliente clienteBuscado = GerenciadorCliente.RetornarCliente(idValido);

        if (clienteBuscado == null)
        {
            throw new ClienteNaoEncontradoException("Nenhum cliente encontrado com esse ID.");
        }

        GerenciadorCliente.ConsultarCompras(clienteBuscado);
    }

    #endregion GerenciamentoClientes


    #region Relatorios

    public void MenuRelatorioVendas()
    {
        Console.WriteLine("Digite o nome da loja:");
        Loja lojaBuscada = GerenciadorLoja.BuscarLoja(Console.ReadLine());

        if (lojaBuscada == null)
        {
            throw new LojaNaoEncontradaException("Nenhuma loja encontrada com esse nome.");
        }

        GerarRelatorios.RelatorioVenda(lojaBuscada); //Chama o métooo que vai gerar relátorios
    }

    public void MenuRelatorioEstoque()
    {
        Console.WriteLine("Digite o nome da loja:");
        Loja lojaBuscada = GerenciadorLoja.BuscarLoja(Console.ReadLine());

        if (lojaBuscada == null)
        {
            throw new LojaNaoEncontradaException("Nenhuma loja encontrada com esse nome.");
        }

        Console.WriteLine("Deseja ver produtos com até quanto de estoque?");

        if (!Validador.ValidarInt(Console.ReadLine(), out int quantidadeValida, "Tente novamente com um valor válido.", 0))
        {
            return;
        }
        GerarRelatorios.RelatorioEstoque(lojaBuscada, quantidadeValida);
    }

    #endregion endregion Relatorios


    static void Main() //Ponto de entrada
    {
        var userUI = new UsuarioInterface();
        RepositorioPersistencia.PersistirDados(); //Chamando o método que guarda os dados em json

        while (true)
        {
            //Menu
            Console.WriteLine("====Gerenciamento de lojas====");
            Console.WriteLine("1 - Cadastrar loja.");
            Console.WriteLine("2 - Listar lojas.");
            Console.WriteLine("=====Gerenciamento de produtos");
            Console.WriteLine("3 - Criar categoria de produtos");
            Console.WriteLine("4 - Criar produto");
            Console.WriteLine("5 - Listar produtos por loja");
            Console.WriteLine("====Gerenciamento de clientes====");
            Console.WriteLine("6 - Criar cliente");
            Console.WriteLine("7 - Adicionar saldo a cliente");
            Console.WriteLine("8 - Fazer compras para cliente");
            Console.WriteLine("9 - Consultar compras do cliente");
            Console.WriteLine("====Gerar Relatórios====");
            Console.WriteLine("10 - Gerar relatório de venda");
            Console.WriteLine("11 - Gerar relatório de produtos por estoque");
            Console.WriteLine("====Persistesia de dados====");
            Console.WriteLine("12 - Persistir lojas e clientes");
            Console.WriteLine("13 - Puxar dados salvos em arquivos (se houver)");
            Console.WriteLine("0 - Sair");

            string escolha = Console.ReadLine();

            //TryCatch responsavel por capturar praticamente todas excessões 
            try
            {
                switch (escolha)
                {
                    case "1":
                        userUI.MenuCadastrarLoja();
                        break;
                    case "2":
                        userUI.MenuListarLojas();
                        break;
                    case "3":
                        userUI.MenuCriarCategoria();
                        break;
                    case "4":
                        userUI.MenuCriarProduto();
                        break;
                    case "5":
                        userUI.MenuListarProdutos();
                        break;
                    case "6":
                        userUI.MenuCriarCliente();
                        break;
                    case "7":
                        userUI.MenuAdicionarSaldoCliente();
                        break;
                    case "8":
                        userUI.MenuClienteFazerCompras();
                        break;
                    case "9":
                        userUI.MenuClienteConsultarCompras();
                        break;
                    case "10":
                        userUI.MenuRelatorioVendas();
                        break;
                    case "11":
                        userUI.MenuRelatorioEstoque();
                        break;
                    case "12":
                        RepositorioPersistencia.PersistirDados();
                        break;
                    case "13":
                        RepositorioPersistencia.DeserializarClientes();
                        RepositorioPersistencia.DeserializarLoja();
                        break;
                    case "0":
                        Console.WriteLine("Sistema encerrado.");
                        return;
                    default:
                        Console.WriteLine("Escolha uma opção válida.");
                        break;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Erro ao tentar acessar arquivos. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro de IO: {ex.Message}");
            }
            catch (ClienteNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (LojaNaoEncontradaException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SaldoInsuficienteExcepException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CategoriaNaoEncontradaException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Um erro inesperado ocorreu: {ex.Message}");
            }
        }
    }
}