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

        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }
    }
}
