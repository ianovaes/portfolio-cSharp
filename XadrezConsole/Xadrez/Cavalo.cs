using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }
    }
}
