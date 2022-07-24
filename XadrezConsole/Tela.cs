using System;
using JogoTabuleiro.Tabuleiro;


namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int indexLinha = 0; indexLinha < tabuleiro.Linhas; indexLinha++)
            {
                Console.Write($"{8 - indexLinha} ");

                for (int indexColuna = 0; indexColuna < tabuleiro.Colunas; indexColuna++)
                {
                    if (tabuleiro.Peca(indexLinha, indexColuna) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(indexLinha, indexColuna));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
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
        }
    }
}
