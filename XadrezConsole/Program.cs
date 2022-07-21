using Xadrez.Tabuleiro;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new(8, 8);

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.ReadLine();
        }
    }
}