namespace JogoTabuleiro.Tabuleiro
{
    abstract class Peca
    {
        public Posicao? Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int QuantidadeMovimento { get; protected set; }

        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Tabuleiro = tabuleiro;
            Cor = cor;
            Posicao = null;
            QuantidadeMovimento = 0;
        }

        public void IncrementarQtdeMovimento()
        {
            QuantidadeMovimento++;
        }

        public void DecrementarQtdeMovimento()
        {
            QuantidadeMovimento--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();

            for (int indexLinha = 0; indexLinha < Tabuleiro.Linhas; indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < Tabuleiro.Colunas; indexColuna++)
                {
                    if (matriz[indexLinha, indexColuna])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool MovimentoPossivel(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
