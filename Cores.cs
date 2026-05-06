namespace ContaBancaria
{
    /// <summary>
    /// Classe utilitária estática responsável por aplicar cores e estilos
    /// ANSI ao terminal, tornando a interface do menu mais legível e amigável.
    /// </summary>
    public static class Cores
    {
        /// <summary>Sequência ANSI que reseta todas as formatações aplicadas.</summary>
        public const string Reset   = "\u001b[0m";

        /// <summary>Sequência ANSI que aplica negrito ao texto.</summary>
        public const string Negrito = "\u001b[1m";

        /// <summary>Cor de texto: preto.</summary>
        public const string Preto   = "\u001b[30m";

        /// <summary>Cor de texto: vermelho.</summary>
        public const string Vermelho= "\u001b[31m";

        /// <summary>Cor de texto: verde.</summary>
        public const string Verde   = "\u001b[32m";

        /// <summary>Cor de texto: amarelo.</summary>
        public const string Amarelo = "\u001b[33m";

        /// <summary>Cor de texto: azul.</summary>
        public const string Azul    = "\u001b[34m";

        /// <summary>Cor de texto: magenta.</summary>
        public const string Magenta = "\u001b[35m";

        /// <summary>Cor de texto: ciano.</summary>
        public const string Ciano   = "\u001b[36m";

        /// <summary>Cor de texto: branco.</summary>
        public const string Branco  = "\u001b[37m";

        /// <summary>Cor de fundo: vermelho.</summary>
        public const string FundoVermelho = "\u001b[41m";

        /// <summary>Cor de fundo: verde.</summary>
        public const string FundoVerde    = "\u001b[42m";

        /// <summary>Cor de fundo: amarelo.</summary>
        public const string FundoAmarelo  = "\u001b[43m";

        /// <summary>Cor de fundo: azul.</summary>
        public const string FundoAzul     = "\u001b[44m";

        /// <summary>
        /// Exibe um título formatado com bordas de separação em azul negrito,
        /// usado para identificar seções do menu.
        /// </summary>
        /// <param name="texto">Texto do título a ser exibido.</param>
        public static void Titulo(string texto)
        {
            Console.WriteLine($"\n{Negrito}{Azul}{'=', -60}");
            Console.WriteLine($"  {texto}");
            Console.WriteLine($"{'=', -60}{Reset}\n");
        }

        /// <summary>
        /// Exibe uma mensagem de sucesso em verde negrito,
        /// indicando que uma operação foi concluída com êxito.
        /// </summary>
        /// <param name="texto">Mensagem de sucesso a ser exibida.</param>
        public static void Sucesso(string texto)
            => Console.WriteLine($"{Verde}{Negrito}{texto}{Reset}");

        /// <summary>
        /// Exibe uma mensagem de erro em vermelho negrito,
        /// indicando falha em uma operação ou entrada inválida.
        /// </summary>
        /// <param name="texto">Mensagem de erro a ser exibida.</param>
        public static void Erro(string texto)
            => Console.WriteLine($"{Vermelho}{Negrito}{texto}{Reset}");

        /// <summary>
        /// Exibe uma mensagem de aviso em amarelo negrito,
        /// utilizada para alertas que não são erros críticos.
        /// </summary>
        /// <param name="texto">Mensagem de aviso a ser exibida.</param>
        public static void Aviso(string texto)
            => Console.WriteLine($"{Amarelo}{Negrito}{texto}{Reset}");

        /// <summary>
        /// Exibe uma mensagem informativa em ciano,
        /// geralmente utilizada para apresentar dados de uma conta.
        /// </summary>
        /// <param name="texto">Mensagem informativa a ser exibida.</param>
        public static void Info(string texto)
            => Console.WriteLine($"{Ciano}{texto}{Reset}");
    }
}
