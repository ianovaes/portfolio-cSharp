using System;
using Xadrez.Tabuleiro;


namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int indexLinha = 0; indexLinha < tabuleiro.Linhas; indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < tabuleiro.Colunas; indexColuna++)
                {
                    if (tabuleiro.Peca(indexLinha, indexColuna) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{tabuleiro.Peca(indexLinha, indexColuna)} ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
