using JogoTabuleiro.Tabuleiro;
using Xadrez;


namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int indexLinha = 0; indexLinha < tabuleiro.Linhas; indexLinha++)
            {
                Console.Write($"{8 - indexLinha} ");

                for (int indexColuna = 0; indexColuna < tabuleiro.Colunas; indexColuna++)
                {
                    ImprimirPeca(tabuleiro.Peca(indexLinha, indexColuna));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            
            for (int indexLinha = 0; indexLinha < tabuleiro.Linhas; indexLinha++)
            {
                Console.Write($"{8 - indexLinha} ");

                for (int indexColuna = 0; indexColuna < tabuleiro.Colunas; indexColuna++)
                {
                    if (posicoesPossiveis[indexLinha, indexColuna])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimirPeca(tabuleiro.Peca(indexLinha, indexColuna));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicao = Console.ReadLine();
            char coluna = posicao[0];
            int linha = int.Parse($"{posicao[1]}");
            return new(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }

                Console.Write(" ");
            }
        }
    }
}
