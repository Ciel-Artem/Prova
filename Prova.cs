using System;

namespace Batalha
{
    public class Magia
    {
        private string nome;
        private float custoUT;
        private float dano;

        public Magia(string nome, float custoUT, float dano)
        {
            this.nome = nome;
            this.custoUT = custoUT;
            this.dano = dano;
        }

        public string GetNome() => nome;
        public float GetCustoUT() => custoUT;
        public float GetDano() => dano;

        public float Lancar()
        {
            return dano;
        }
    }

    public class Arma
    {
        private string nome;
        private float custoUT;
        private float dano;

        public Arma(string nome, float custoUT, float dano)
        {
            this.nome = nome;
            this.custoUT = custoUT;
            this.dano = dano;
        }

        public string GetNome() => nome;
        public float GetCustoUT() => custoUT;
        public float GetDano() => dano;

        public float Atacar()
        {
            return dano;
        }
    }

    public abstract class Heroi
    {
        protected string nome;
        protected float ptsVida;
        protected float utHeroi;
        protected Magia magia;
        protected Arma armaHeroi;

        public Heroi(string nome, float ptsVida)
        {
            this.nome = nome;
            this.ptsVida = ptsVida;
            this.utHeroi = 7;
        }

        public string GetNome() => nome;
        public float GetPtsVida() => ptsVida;
        public float GetUTHeroi() => utHeroi;

        public Magia GetMagia() => magia;
        public Arma GetArma() => armaHeroi;

        public void AumentarUT(float valor) => utHeroi += valor;
        public void ReduzirUT(float valor) => utHeroi -= valor;

        public virtual void ReduzirVida(float dano)
        {
            this.ptsVida -= dano;
        }

        public abstract void LancarMagia(Heroi alvo);
        public abstract void AtacarComArma(Heroi alvo);
    }

    public class HeroiAlianca : Heroi
    {
        public HeroiAlianca(string nome, float ptsVida) : base(nome, ptsVida)
        {
            this.magia = new Magia("Força Rutilante", 4, 20);
            this.armaHeroi = new Arma("Espada", 12, 10);
        }

        public override void LancarMagia(Heroi alvo)
        {
            float danoCausado = this.magia.Lancar();
            this.ReduzirUT(this.magia.GetCustoUT());
            alvo.ReduzirVida(danoCausado);

            Console.WriteLine($"{this.nome} conjurou {this.magia.GetNome()} gastando {this.magia.GetCustoUT()} UT!");
        }

        public override void AtacarComArma(Heroi alvo)
        {
            float danoCausado = this.armaHeroi.Atacar();
            this.ReduzirUT(this.armaHeroi.GetCustoUT());
            alvo.ReduzirVida(danoCausado);

            Console.WriteLine($"{this.nome} desferiu um ataque com {this.armaHeroi.GetNome()} gastando {this.armaHeroi.GetCustoUT()} UT!");
        }
    }

    public class HeroiHorda : Heroi
    {
        public HeroiHorda(string nome, float ptsVida) : base(nome, ptsVida)
        {
            this.magia = new Magia("Caminho de Chamas", 6, 30);
            this.armaHeroi = new Arma("Machado", 14, 20);
        }

        public override void LancarMagia(Heroi alvo)
        {
            float danoCausado = this.magia.Lancar();
            this.ReduzirUT(this.magia.GetCustoUT());
            alvo.ReduzirVida(danoCausado);

            Console.WriteLine($"{this.nome} canalizou {this.magia.GetNome()} gastando {this.magia.GetCustoUT()} UT!");
        }

        public override void AtacarComArma(Heroi alvo)
        {
            float danoCausado = this.armaHeroi.Atacar();
            this.ReduzirUT(this.armaHeroi.GetCustoUT());
            alvo.ReduzirVida(danoCausado);

            Console.WriteLine($"{this.nome} brandiu seu {this.armaHeroi.GetNome()} gastando {this.armaHeroi.GetCustoUT()} UT!");
        }
    }

    public class Uou
    {
        public static void Main()
        {
            HeroiAlianca alianca = new HeroiAlianca("Davi", 80);
            HeroiHorda horda = new HeroiHorda("JP", 80);

            bool jogoAtivo = true;
            float recuperacaoDescanso = 5f;

            while (jogoAtivo)
            {
                string escolha;
                bool acaoValida;

                alianca.AumentarUT(3);
                Console.WriteLine($"\n>>> TURNO DA ALIANÇA: {alianca.GetNome()} <<<");

                acaoValida = false;
                while (!acaoValida)
                {
                    Console.WriteLine($"Seu saldo atual: {alianca.GetUTHeroi()} UT | [Magia: {alianca.GetMagia().GetCustoUT()} UT | Arma: {alianca.GetArma().GetCustoUT()} UT]");
                    Console.Write("Escolha a ação - [M] Magia, [A] Arma ou [D] Descansar (+5 UT): ");
                    escolha = Console.ReadLine().ToUpper();

                    if (escolha == "M")
                    {
                        if (alianca.GetUTHeroi() >= alianca.GetMagia().GetCustoUT())
                        {
                            alianca.LancarMagia(horda);
                            acaoValida = true;
                        }
                        else
                        {
                            Console.WriteLine("UT Insuficiente para lançar Magia! Escolha outra ação.");
                        }
                    }
                    else if (escolha == "A")
                    {
                        if (alianca.GetUTHeroi() >= alianca.GetArma().GetCustoUT())
                        {
                            alianca.AtacarComArma(horda);
                            acaoValida = true;
                        }
                        else
                        {
                            Console.WriteLine("UT Insuficiente para atacar com Arma! Escolha outra ação.");
                        }
                    }
                    else if (escolha == "D")
                    {
                        alianca.AumentarUT(recuperacaoDescanso);
                        Console.WriteLine($"{alianca.GetNome()} decidiu descansar e concentrar sua energia! Recuperou +{recuperacaoDescanso} UT.");
                        acaoValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Comando inválido! Digite M, A ou D.");
                    }
                }

                Console.WriteLine($"Vida restante de {horda.GetNome()}: {horda.GetPtsVida()} HP");

                if (horda.GetPtsVida() <= 0)
                {
                    Console.WriteLine($"\nO Grande Vencedor é: {alianca.GetNome()} com {alianca.GetPtsVida()} de vida restante!");
                    Console.WriteLine("Game Over!");
                    break;
                }

                horda.AumentarUT(3);
                Console.WriteLine($"\n>>> TURNO DA HORDA: {horda.GetNome()} <<<");

                acaoValida = false;
                while (!acaoValida)
                {
                    Console.WriteLine($"Seu saldo atual: {horda.GetUTHeroi()} UT | [Magia: {horda.GetMagia().GetCustoUT()} UT | Arma: {horda.GetArma().GetCustoUT()} UT]");
                    Console.Write("Escolha a ação - [M] Magia, [A] Arma ou [D] Descansar (+5 UT): ");
                    escolha = Console.ReadLine().ToUpper();

                    if (escolha == "M")
                    {
                        if (horda.GetUTHeroi() >= horda.GetMagia().GetCustoUT())
                        {
                            horda.LancarMagia(alianca);
                            acaoValida = true;
                        }
                        else
                        {
                            Console.WriteLine("UT Insuficiente para lançar Magia! Escolha outra ação.");
                        }
                    }
                    else if (escolha == "A")
                    {
                        if (horda.GetUTHeroi() >= horda.GetArma().GetCustoUT())
                        {
                            horda.AtacarComArma(alianca);
                            acaoValida = true;
                        }
                        else
                        {
                            Console.WriteLine("UT Insuficiente para atacar com Arma! Escolha outra ação.");
                        }
                    }
                    else if (escolha == "D")
                    {
                        horda.AumentarUT(recuperacaoDescanso);
                        Console.WriteLine($"{horda.GetNome()} decidiu descansar e concentrar sua energia! Recuperou +{recuperacaoDescanso} UT.");
                        acaoValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Comando inválido! Digite M, A ou D.");
                    }
                }

                Console.WriteLine($"Vida restante de {alianca.GetNome()}: {alianca.GetPtsVida()} HP");

                if (alianca.GetPtsVida() <= 0)
                {
                    Console.WriteLine($"\nO Grande Vencedor é: {horda.GetNome()} com {horda.GetPtsVida()} de vida restante!");
                    Console.WriteLine("Game Over!");
                    break;
                }
            }
        }
    }
}