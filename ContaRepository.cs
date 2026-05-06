namespace ContaBancaria
{
    /// <summary>
    /// Interface que define o contrato de operações bancárias do sistema.
    /// Toda classe que gerenciar contas deve implementar estes métodos,
    /// garantindo consistência e desacoplamento entre camadas.
    /// </summary>
    public interface ContaRepository
    {
        /// <summary>
        /// Solicita os dados ao usuário via terminal e cria uma nova conta
        /// (Corrente ou Poupança), adicionando-a ao repositório.
        /// </summary>
        void CriarConta();

        /// <summary>
        /// Exibe no terminal os dados de todas as contas cadastradas no sistema.
        /// Caso não haja nenhuma conta, emite um aviso informando que o repositório está vazio.
        /// </summary>
        void ListarTodas();

        /// <summary>
        /// Localiza uma conta pelo seu número e exibe seus dados no terminal.
        /// Emite mensagem de erro caso a conta não seja encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser buscada.</param>
        void BuscarPorNumero(int numero);

        /// <summary>
        /// Atualiza os dados de uma conta existente identificada pelo número informado.
        /// Emite mensagem de erro caso a conta não seja encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser atualizada.</param>
        void AtualizarConta(int numero);

        /// <summary>
        /// Remove permanentemente uma conta do repositório com base no número informado.
        /// Emite mensagem de erro caso a conta não seja encontrada.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser deletada.</param>
        void DeletarConta(int numero);

        /// <summary>
        /// Realiza um depósito em uma conta específica, adicionando o valor ao saldo.
        /// Emite mensagem de erro caso a conta não seja encontrada ou o valor seja inválido.
        /// </summary>
        /// <param name="numero">Número identificador da conta destino do depósito.</param>
        /// <param name="valor">Valor a ser depositado. Deve ser maior que zero.</param>
        void Depositar(int numero, decimal valor);

        /// <summary>
        /// Realiza um saque em uma conta específica, subtraindo o valor do saldo.
        /// Para Conta Corrente, considera o limite de crédito disponível.
        /// Emite mensagem de erro caso a conta não seja encontrada ou o saldo seja insuficiente.
        /// </summary>
        /// <param name="numero">Número identificador da conta a ser debitada.</param>
        /// <param name="valor">Valor a ser sacado. Deve ser maior que zero.</param>
        void Sacar(int numero, decimal valor);

        /// <summary>
        /// Transfere um valor entre duas contas distintas.
        /// O débito ocorre na conta de origem e o crédito na conta de destino.
        /// A operação é cancelada se o saque na origem falhar.
        /// </summary>
        /// <param name="origem">Número da conta de onde o valor será debitado.</param>
        /// <param name="destino">Número da conta que receberá o valor.</param>
        /// <param name="valor">Valor a ser transferido. Deve ser maior que zero.</param>
        void Transferir(int origem, int destino, decimal valor);
    }
}
