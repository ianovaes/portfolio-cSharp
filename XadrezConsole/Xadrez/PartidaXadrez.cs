using System;
using JogoTabuleiro.Tabuleiro;
using Xadrez;

namespace Xadrez
{
    class PartidaXadrez
    {
        private Tabuleiro _tabuleiro;

        private int _turno;

        private Cor _jogadorAaual;

        private bool _terminada;

        public PartidaXadrez()
        {
            _tabuleiro = new(8, 8);
            _turno = 1;
            _jogadorAaual = Cor.Branca;
            _terminada = false;
            ColocarPecas();
        }

        public Tabuleiro Tabuleiro
        {
            get { return _tabuleiro; }
        }

        public bool Terminada
        {
            get { return _terminada; }
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = _tabuleiro.RetirarPeca(origem);
            peca.ImcrementarQtdeMovimento();
            Peca pecaCapturada = _tabuleiro.RetirarPeca(destino);
            _tabuleiro.ColocarPeca(peca, destino);
        }

        private void ColocarPecas()
        {
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            _tabuleiro.ColocarPeca(new Rei(_tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            _tabuleiro.ColocarPeca(new Rei(_tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
