using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimento == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }
            else
            {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimento == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
