namespace ContaBancaria
{
    /// <summary>
    /// Classe principal da aplicação. Contém o método <see cref="Main"/> responsável
    /// por inicializar o sistema e exibir o menu interativo no terminal.
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// Ponto de entrada da aplicação. Inicializa o <see cref="ContaController"/>
        /// e exibe um loop de menu interativo até que o usuário escolha sair.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var controller = new ContaController();

            while (true)
            {
                Cores.Titulo("Banco Belo Horizonte — Sistema de Contas");
                Console.WriteLine($"{Cores.Amarelo}  1{Cores.Reset} » Criar conta");
                Console.WriteLine($"{Cores.Amarelo}  2{Cores.Reset} » Listar todas as contas");
                Console.WriteLine($"{Cores.Amarelo}  3{Cores.Reset} » Buscar conta por número");
                Console.WriteLine($"{Cores.Amarelo}  4{Cores.Reset} » Atualizar titular");
                Console.WriteLine($"{Cores.Amarelo}  5{Cores.Reset} » Deletar conta");
                Console.WriteLine($"{Cores.Amarelo}  6{Cores.Reset} » Depositar");
                Console.WriteLine($"{Cores.Amarelo}  7{Cores.Reset} » Sacar");
                Console.WriteLine($"{Cores.Amarelo}  8{Cores.Reset} » Transferir");
                Console.WriteLine($"{Cores.Vermelho}  0{Cores.Reset} » Sair");
                Console.Write("\nEscolha uma opção: ");

                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        controller.CriarConta();
                        break;
                    case "2":
                        controller.ListarTodas();
                        break;
                    case "3":
                        int numBusca = LerInteiro("Número da conta: ");
                        controller.BuscarPorNumero(numBusca);
                        break;
                    case "4":
                        int numAtualizar = LerInteiro("Número da conta: ");
                        controller.AtualizarConta(numAtualizar);
                        break;
                    case "5":
                        int numDeletar = LerInteiro("Número da conta: ");
                        controller.DeletarConta(numDeletar);
                        break;
                    case "6":
                        int numDep     = LerInteiro("Número da conta: ");
                        decimal valDep = LerDecimal("Valor do depósito: R$ ");
                        controller.Depositar(numDep, valDep);
                        break;
                    case "7":
                        int numSaq     = LerInteiro("Número da conta: ");
                        decimal valSaq = LerDecimal("Valor do saque: R$ ");
                        controller.Sacar(numSaq, valSaq);
                        break;
                    case "8":
                        int origem       = LerInteiro("Conta de origem: ");
                        int destino      = LerInteiro("Conta de destino: ");
                        decimal valTrans = LerDecimal("Valor da transferência: R$ ");
                        controller.Transferir(origem, destino, valTrans);
                        break;
                    case "0":
                        Cores.Sucesso("Encerrando o sistema. Até logo!");
                        return;
                    default:
                        Cores.Erro("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey(intercept: true);
                Console.Clear();
            }
        }

        /// <summary>
        /// Solicita ao usuário um número inteiro não negativo via terminal.
        /// Repete a solicitação enquanto a entrada for inválida.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário antes da leitura.</param>
        /// <returns>Número inteiro válido e maior ou igual a zero informado pelo usuário.</returns>
        private static int LerInteiro(string prompt)
        {
            int valor;
            do
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                    return valor;
                Cores.Erro("Entrada inválida.");
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário um valor decimal positivo via terminal.
        /// Repete a solicitação enquanto a entrada for inválida ou zero.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário antes da leitura.</param>
        /// <returns>Valor decimal válido e maior que zero informado pelo usuário.</returns>
        private static decimal LerDecimal(string prompt)
        {
            decimal valor;
            do
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out valor) && valor > 0)
                    return valor;
                Cores.Erro("Valor inválido.");
            } while (true);
        }
    }
}
