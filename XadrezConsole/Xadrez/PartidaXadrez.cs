using JogoTabuleiro.Tabuleiro;

namespace Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }

        public int Turno { get; private set; }

        public Cor JogadorAaual { get; private set; }

        public bool Terminada { get; private set; }

        private HashSet<Peca> _pecas;

        private HashSet<Peca> _capturadas;

        public bool Xeque { get; private set; }

        public Peca VuneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAaual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VuneravelEnPassant = null;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca? ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQtdeMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            
            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.Coluna == (origem.Coluna + 2))
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimento();
                Tabuleiro.ColocarPeca(torre, destinoTorre);

            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.Coluna == (origem.Coluna - 2))
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimento();
                Tabuleiro.ColocarPeca(torre, destinoTorre);

            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branca)
                    {
                        posicaoPeao = new(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new(destino.Linha - 1, destino.Coluna);
                    }
                    
                    pecaCapturada = Tabuleiro.RetirarPeca(posicaoPeao);
                    _capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarQtdeMovimento();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(peca, origem);

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.Coluna == (origem.Coluna + 2))
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQtdeMovimento();
                Tabuleiro.ColocarPeca(torre, origemTorre);

            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.Coluna == (origem.Coluna - 2))
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQtdeMovimento();
                Tabuleiro.ColocarPeca(torre, origemTorre);

            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VuneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branca)
                    {
                        posicaoPeao = new(3, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new(4, destino.Coluna);
                    }

                    Tabuleiro.ColocarPeca(peao, posicaoPeao);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca? pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAaual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = Tabuleiro.Peca(destino);

            // #jogadaespecial promoção
            if (peca is Peao)
            {
                if ((peca.Cor == Cor.Branca & destino.Linha == 0)
                    || (peca.Cor == Cor.Preta && destino.Linha == 7))
                {
                    peca = Tabuleiro.RetirarPeca(destino);
                    _pecas.Remove(peca);
                    Peca dama = new Dama(Tabuleiro, peca.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    _pecas.Add(dama);
                }
            }


            if (EstaEmXeque(Adversaria(JogadorAaual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (Xequemate(Adversaria(JogadorAaual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudarJogador();
            }

            // #jogadaespecial en passant
            if (peca is Peao && (destino.Linha == origem.Linha - 2
                || destino.Linha == origem.Linha + 2))
            {
                VuneravelEnPassant = peca;
            }
            else
            {
                VuneravelEnPassant = null;
            }
        }

        public void ValidarPosicaoOrigem(Posicao possicao)
        {
            if (Tabuleiro.Peca(possicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAaual != Tabuleiro.Peca(possicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tabuleiro.Peca(possicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudarJogador()
        {
            if (JogadorAaual == Cor.Branca)
            {
                JogadorAaual = Cor.Preta;
            }
            else
            {
                JogadorAaual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> capturadas = new HashSet<Peca>();

            foreach (Peca peca in _capturadas)
            {
                if (peca.Cor == cor)
                {
                    capturadas.Add(peca);
                }
            }

            return capturadas;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> emJogo = new HashSet<Peca>();

            foreach (Peca peca in _pecas)
            {
                if (peca.Cor == cor)
                {
                    emJogo.Add(peca);
                }
            }

            emJogo.ExceptWith(PecasCapturadas(cor));
            return emJogo;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca? GetRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca? rei = GetRei(cor);

            if (rei == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();

                if (matriz[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool Xequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                for (int indexLinha = 0; indexLinha < Tabuleiro.Linhas; indexLinha++)
                {
                    for (int indexColuna = 0; indexColuna < Tabuleiro.Linhas; indexColuna++)
                    {
                        if (matriz[indexLinha, indexColuna])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(indexLinha, indexColuna);

                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool emXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!emXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
