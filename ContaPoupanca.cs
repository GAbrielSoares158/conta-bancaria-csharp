namespace ContaBancaria
{
    /// <summary>
    /// Representa uma Conta Poupança bancária.
    /// Herda de <see cref="Conta"/> e adiciona o atributo de aniversário da poupança,
    /// que indica o dia do mês em que os rendimentos são creditados.
    /// </summary>
    public class ContaPoupanca : Conta
    {
        /// <summary>
        /// Dia do mês em que a poupança completa aniversário e recebe rendimentos.
        /// Valor válido entre 1 e 28.
        /// </summary>
        public int AniversarioPoupanca { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ContaPoupanca"/>.
        /// O tipo é definido automaticamente como <c>2</c> (Conta Poupança).
        /// Se o dia de aniversário informado for inválido, será ajustado para <c>1</c>.
        /// </summary>
        /// <param name="agencia">Número da agência bancária.</param>
        /// <param name="titular">Nome do titular da conta.</param>
        /// <param name="aniversario">Dia do mês do aniversário da poupança (1 a 28).</param>
        /// <param name="saldoInicial">Saldo inicial da conta. Padrão: 0.</param>
        public ContaPoupanca(int agencia, string titular, int aniversario, decimal saldoInicial = 0m)
            : base(agencia, tipo: 2, titular, saldoInicial)
        {
            AniversarioPoupanca = aniversario >= 1 && aniversario <= 28 ? aniversario : 1;
        }

        /// <summary>
        /// Exibe no terminal todos os dados da conta poupança,
        /// incluindo número, agência, titular, saldo e o dia de aniversário.
        /// </summary>
        public override void Visualizar()
        {
            Cores.Titulo("Dados da Conta Poupança");
            Cores.Info($"  Número       : {Numero}");
            Cores.Info($"  Agência      : {Agencia}");
            Cores.Info($"  Tipo         : Conta Poupança");
            Cores.Info($"  Titular      : {Titular}");
            Cores.Info($"  Saldo        : {Saldo:C}");
            Cores.Info($"  Aniversário  : Dia {AniversarioPoupanca}");
        }
    }
}
