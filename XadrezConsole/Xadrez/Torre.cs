using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "T";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            // acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha--;
            }

            // abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }

                posicao.Linha++;
            }

            // direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }

                posicao.Coluna++;
            }

            // esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }

                posicao.Coluna--;

            }

            return matriz;
        }
    }
}
