namespace ContaBancaria
{
    /// <summary>
    /// Implementa a interface <see cref="ContaRepository"/> e centraliza
    /// toda a lógica de negócio do sistema bancário.
    /// Gerencia a lista de contas em memória e coordena as operações
    /// de CRUD e transações financeiras.
    /// </summary>
    public class ContaController : ContaRepository
    {
        /// <summary>Repositório em memória que armazena todas as contas cadastradas.</summary>
        private readonly List<Conta> _contas = new();

        /// <summary>
        /// Localiza uma conta na lista pelo seu número identificador.
        /// </summary>
        /// <param name="numero">Número da conta a ser localizada.</param>
        /// <returns>A instância de <see cref="Conta"/> encontrada, ou <c>null</c> se não existir.</returns>
        private Conta? Localizar(int numero)
            => _contas.Find(c => c.Numero == numero);

        /// <summary>
        /// Solicita ao usuário um valor decimal positivo via terminal,
        /// repetindo a solicitação enquanto a entrada for inválida.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário antes da leitura.</param>
        /// <returns>Valor decimal válido e maior que zero informado pelo usuário.</returns>
        private static decimal LerDecimal(string prompt)
        {
            decimal valor;
            do
            {
                Console.Write(prompt);
                string? entrada = Console.ReadLine();
                if (decimal.TryParse(entrada, out valor) && valor > 0)
                    return valor;
                Cores.Erro("Valor inválido. Digite um número maior que zero.");
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário um número inteiro dentro de um intervalo via terminal,
        /// repetindo a solicitação enquanto a entrada for inválida.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário antes da leitura.</param>
        /// <param name="min">Valor mínimo aceito. Padrão: 1.</param>
        /// <param name="max">Valor máximo aceito. Padrão: <see cref="int.MaxValue"/>.</param>
        /// <returns>Número inteiro válido dentro do intervalo informado pelo usuário.</returns>
        private static int LerInteiro(string prompt, int min = 1, int max = int.MaxValue)
        {
            int valor;
            do
            {
                Console.Write(prompt);
                string? entrada = Console.ReadLine();
                if (int.TryParse(entrada, out valor) && valor >= min && valor <= max)
                    return valor;
                Cores.Erro($"Entrada inválida. Digite um número entre {min} e {max}.");
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário uma string não vazia via terminal,
        /// repetindo a solicitação enquanto a entrada estiver em branco.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário antes da leitura.</param>
        /// <returns>String não vazia informada pelo usuário.</returns>
        private static string LerString(string prompt)
        {
            string? valor;
            do
            {
                Console.Write(prompt);
                valor = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(valor));
            return valor;
        }

        /// <summary>
        /// Solicita os dados ao usuário via terminal e cria uma nova conta
        /// do tipo escolhido (Corrente ou Poupança), adicionando-a ao repositório.
        /// </summary>
        public void CriarConta()
        {
            Cores.Titulo("Criar Nova Conta");

            int tipo      = LerInteiro("Tipo (1 = Corrente / 2 = Poupança): ", 1, 2);
            int agencia   = LerInteiro("Agência: ");
            string nome   = LerString("Titular: ");
            decimal saldo = LerDecimal("Saldo inicial: R$ ");

            Conta nova;
            if (tipo == 1)
            {
                decimal limite = LerDecimal("Limite de crédito: R$ ");
                nova = new ContaCorrente(agencia, nome, saldo, limite);
            }
            else
            {
                int aniversario = LerInteiro("Dia do aniversário da poupança (1-28): ", 1, 28);
                nova = new ContaPoupanca(agencia, nome, aniversario, saldo);
            }

            _contas.Add(nova);
            Cores.Sucesso($"Conta nº {nova.Numero} criada com sucesso!");
        }

        /// <summary>
        /// Exibe no terminal os dados de todas as contas cadastradas.
        /// Emite aviso se o repositório estiver vazio.
        /// </summary>
        public void ListarTodas()
        {
            Cores.Titulo("Lista de Contas");
            if (_contas.Count == 0)
            {
                Cores.Aviso("Nenhuma conta cadastrada.");
                return;
            }
            foreach (var c in _contas)
                c.Visualizar();
        }

        /// <summary>
        /// Localiza uma conta pelo número e exibe seus dados no terminal.
        /// Emite erro se a conta não for encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser buscada.</param>
        public void BuscarPorNumero(int numero)
        {
            var conta = Localizar(numero);
            if (conta is null) Cores.Erro($"Conta nº {numero} não encontrada.");
            else conta.Visualizar();
        }

        /// <summary>
        /// Atualiza o nome do titular de uma conta existente.
        /// Recria o objeto preservando agência, saldo e atributos específicos do tipo.
        /// Emite erro se a conta não for encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser atualizada.</param>
        public void AtualizarConta(int numero)
        {
            var conta = Localizar(numero);
            if (conta is null) { Cores.Erro($"Conta nº {numero} não encontrada."); return; }

            Cores.Titulo($"Atualizar Conta nº {numero}");
            string novoNome = LerString("Novo nome do titular: ");

            if (conta is ContaCorrente cc)
            {
                var updated = new ContaCorrente(cc.Agencia, novoNome, cc.Saldo, cc.Limite);
                _contas[_contas.IndexOf(conta)] = updated;
            }
            else if (conta is ContaPoupanca cp)
            {
                var updated = new ContaPoupanca(cp.Agencia, novoNome, cp.AniversarioPoupanca, cp.Saldo);
                _contas[_contas.IndexOf(conta)] = updated;
            }
            Cores.Sucesso("Titular atualizado com sucesso!");
        }

        /// <summary>
        /// Remove permanentemente uma conta do repositório.
        /// Emite erro se a conta não for encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser deletada.</param>
        public void DeletarConta(int numero)
        {
            var conta = Localizar(numero);
            if (conta is null) { Cores.Erro($"Conta nº {numero} não encontrada."); return; }
            _contas.Remove(conta);
            Cores.Sucesso($"Conta nº {numero} removida com sucesso!");
        }

        /// <summary>
        /// Realiza um depósito em uma conta específica.
        /// Emite erro se a conta não for encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta destino.</param>
        /// <param name="valor">Valor a ser depositado. Deve ser maior que zero.</param>
        public void Depositar(int numero, decimal valor)
        {
            var conta = Localizar(numero);
            if (conta is null) { Cores.Erro($"Conta nº {numero} não encontrada."); return; }
            conta.Depositar(valor);
        }

        /// <summary>
        /// Realiza um saque em uma conta específica.
        /// Para <see cref="ContaCorrente"/>, considera o limite de crédito.
        /// Emite erro se a conta não for encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser debitada.</param>
        /// <param name="valor">Valor a ser sacado. Deve ser maior que zero.</param>
        public void Sacar(int numero, decimal valor)
        {
            var conta = Localizar(numero);
            if (conta is null) { Cores.Erro($"Conta nº {numero} não encontrada."); return; }

            if (conta is ContaCorrente cc) cc.Sacar(valor);
            else conta.Sacar(valor);
        }

        /// <summary>
        /// Transfere um valor da conta de origem para a conta de destino.
        /// O débito só ocorre se o saque na origem for aprovado.
        /// Emite erro se qualquer uma das contas não for encontrada.
        /// </summary>
        /// <param name="origem">Número da conta de onde o valor será debitado.</param>
        /// <param name="destino">Número da conta que receberá o valor.</param>
        /// <param name="valor">Valor a ser transferido. Deve ser maior que zero.</param>
        public void Transferir(int origem, int destino, decimal valor)
        {
            var contaOrigem  = Localizar(origem);
            var contaDestino = Localizar(destino);

            if (contaOrigem is null)  { Cores.Erro($"Conta de origem nº {origem} não encontrada."); return; }
            if (contaDestino is null) { Cores.Erro($"Conta de destino nº {destino} não encontrada."); return; }

            bool sacou = contaOrigem is ContaCorrente cc
                ? cc.Sacar(valor)
                : contaOrigem.Sacar(valor);

            if (!sacou) return;

            contaDestino.Depositar(valor);
            Cores.Sucesso($"Transferência de {valor:C} da conta {origem} para {destino} concluída!");
        }
    }
}
