using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "B";
        }
    }
}
