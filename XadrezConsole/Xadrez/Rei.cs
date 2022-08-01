using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaXadrez _partida;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return ((peca != null) && (peca is Torre)
                && (peca.Cor == Cor) && (QuantidadeMovimento == 0));
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            // acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // ne
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // se
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // so
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // no
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // #jogadaespecial roque
            if (QuantidadeMovimento == 0 && !_partida.Xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posicaoTorreRoquePequeno = new(Posicao.Linha, Posicao.Coluna + 3);
                if (TorreParaRoque(posicaoTorreRoquePequeno))
                {
                    Posicao posicaoRoque1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicaoRoque2 = new(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.Peca(posicaoRoque1) == null
                        && Tabuleiro.Peca(posicaoRoque2) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Posicao posicaoTorreRoqueGrande = new(Posicao.Linha, Posicao.Coluna - 4);
                if (TorreParaRoque(posicaoTorreRoqueGrande))
                {
                    Posicao posicaoRoque1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicaoRoque2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicaoRoque3 = new(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.Peca(posicaoRoque1) == null
                        && Tabuleiro.Peca(posicaoRoque2) == null
                        && Tabuleiro.Peca(posicaoRoque3) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }

            }

            return matriz;
        }
    }
}
