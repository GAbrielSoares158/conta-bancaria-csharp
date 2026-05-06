namespace ContaBancaria
{
    /// <summary>
    /// Classe abstrata que representa uma conta bancária genérica.
    /// Define os atributos e comportamentos comuns a todos os tipos de conta,
    /// servindo como base para <see cref="ContaCorrente"/> e <see cref="ContaPoupanca"/>.
    /// </summary>
    public abstract class Conta
    {
        private static int _proximoNumero = 1;

        /// <summary>Número único da conta, gerado automaticamente de forma sequencial.</summary>
        public int Numero { get; private set; }

        /// <summary>Número da agência bancária à qual a conta pertence.</summary>
        public int Agencia { get; protected set; }

        /// <summary>Tipo da conta: <c>1</c> para Conta Corrente, <c>2</c> para Conta Poupança.</summary>
        public int Tipo { get; protected set; }

        /// <summary>Nome completo do titular da conta.</summary>
        public string Titular { get; protected set; }

        /// <summary>Saldo atual disponível na conta.</summary>
        public decimal Saldo { get; protected set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="Conta"/> com os dados fornecidos.
        /// O número da conta é atribuído automaticamente de forma incremental.
        /// </summary>
        /// <param name="agencia">Número da agência bancária.</param>
        /// <param name="tipo">Tipo da conta (1 = Corrente, 2 = Poupança).</param>
        /// <param name="titular">Nome do titular da conta.</param>
        /// <param name="saldoInicial">Saldo inicial ao criar a conta. Padrão: 0.</param>
        protected Conta(int agencia, int tipo, string titular, decimal saldoInicial = 0m)
        {
            Numero  = _proximoNumero++;
            Agencia = agencia;
            Tipo    = tipo;
            Titular = titular;
            Saldo   = saldoInicial;
        }

        /// <summary>
        /// Realiza um depósito na conta, adicionando o valor informado ao saldo.
        /// Rejeita valores menores ou iguais a zero.
        /// </summary>
        /// <param name="valor">Valor a ser depositado. Deve ser maior que zero.</param>
        /// <returns>
        /// <c>true</c> se o depósito foi realizado com sucesso;
        /// <c>false</c> se o valor for inválido.
        /// </returns>
        public bool Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                Cores.Erro("Valor de depósito inválido.");
                return false;
            }
            Saldo += valor;
            Cores.Sucesso($"Depósito de {valor:C} realizado com sucesso! Novo saldo: {Saldo:C}");
            return true;
        }

        /// <summary>
        /// Realiza um saque na conta, subtraindo o valor informado do saldo.
        /// Rejeita valores inválidos ou superiores ao saldo disponível.
        /// </summary>
        /// <param name="valor">Valor a ser sacado. Deve ser maior que zero e não exceder o saldo.</param>
        /// <returns>
        /// <c>true</c> se o saque foi realizado com sucesso;
        /// <c>false</c> se o valor for inválido ou o saldo for insuficiente.
        /// </returns>
        public bool Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                Cores.Erro("Valor de saque inválido.");
                return false;
            }
            if (valor > Saldo)
            {
                Cores.Erro("Saldo insuficiente para o saque.");
                return false;
            }
            Saldo -= valor;
            Cores.Sucesso($"Saque de {valor:C} realizado com sucesso! Novo saldo: {Saldo:C}");
            return true;
        }

        /// <summary>
        /// Exibe no terminal os dados completos da conta.
        /// Cada subclasse implementa sua própria versão com os atributos específicos.
        /// </summary>
        public abstract void Visualizar();

        /// <summary>
        /// Retorna a descrição textual do tipo de conta com base no código informado.
        /// </summary>
        /// <param name="tipo">Código do tipo: <c>1</c> = Corrente, <c>2</c> = Poupança.</param>
        /// <returns>String descritiva do tipo de conta.</returns>
        public static string TipoDescricao(int tipo)
            => tipo == 1 ? "Conta Corrente" : "Conta Poupança";
    }
}
