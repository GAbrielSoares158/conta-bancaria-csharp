namespace ContaBancaria
{
    /// <summary>
    /// Representa uma Conta Corrente bancária.
    /// Herda de <see cref="Conta"/> e adiciona suporte a limite de crédito,
    /// permitindo saques além do saldo disponível até o limite configurado.
    /// </summary>
    public class ContaCorrente : Conta
    {
        /// <summary>
        /// Limite de crédito disponível na conta corrente.
        /// Permite saques que ultrapassem o saldo até este valor.
        /// </summary>
        public decimal Limite { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ContaCorrente"/>.
        /// O tipo é definido automaticamente como <c>1</c> (Conta Corrente).
        /// </summary>
        /// <param name="agencia">Número da agência bancária.</param>
        /// <param name="titular">Nome do titular da conta.</param>
        /// <param name="saldoInicial">Saldo inicial da conta. Padrão: 0.</param>
        /// <param name="limite">Limite de crédito disponível. Padrão: R$ 1.000,00.</param>
        public ContaCorrente(int agencia, string titular, decimal saldoInicial = 0m, decimal limite = 1000m)
            : base(agencia, tipo: 1, titular, saldoInicial)
        {
            Limite = limite;
        }

        /// <summary>
        /// Realiza um saque na conta corrente considerando o limite de crédito.
        /// O valor máximo permitido é <c>Saldo + Limite</c>.
        /// Sobrescreve o comportamento da classe base <see cref="Conta.Sacar"/>.
        /// </summary>
        /// <param name="valor">Valor a ser sacado. Deve ser maior que zero e não exceder Saldo + Limite.</param>
        /// <returns>
        /// <c>true</c> se o saque foi realizado com sucesso;
        /// <c>false</c> se o valor for inválido ou exceder o limite disponível.
        /// </returns>
        public new bool Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                Cores.Erro("Valor de saque inválido.");
                return false;
            }
            if (valor > Saldo + Limite)
            {
                Cores.Erro($"Saldo + Limite insuficiente. Disponível: {Saldo + Limite:C}");
                return false;
            }
            Saldo -= valor;
            Cores.Sucesso($"Saque de {valor:C} realizado! Saldo atual: {Saldo:C}");
            return true;
        }

        /// <summary>
        /// Exibe no terminal todos os dados da conta corrente,
        /// incluindo número, agência, titular, saldo e limite de crédito.
        /// </summary>
        public override void Visualizar()
        {
            Cores.Titulo("Dados da Conta Corrente");
            Cores.Info($"  Número  : {Numero}");
            Cores.Info($"  Agência : {Agencia}");
            Cores.Info($"  Tipo    : Conta Corrente");
            Cores.Info($"  Titular : {Titular}");
            Cores.Info($"  Saldo   : {Saldo:C}");
            Cores.Info($"  Limite  : {Limite:C}");
        }
    }
}
