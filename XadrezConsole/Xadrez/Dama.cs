using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }
    }
}
