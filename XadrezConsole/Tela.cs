using JogoTabuleiro.Tabuleiro;
using Xadrez;


namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);

            Console.WriteLine();
            ImprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAaual}");

            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            ConsoleColor colorOriginal = Console.ForegroundColor;

            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();

            Console.Write("Preta: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.WriteLine();

            Console.ForegroundColor = colorOriginal;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");

            foreach (Peca peca in conjunto)
            {
                Console.Write($"{peca} ");
            }

            Console.Write("]");
        }

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
