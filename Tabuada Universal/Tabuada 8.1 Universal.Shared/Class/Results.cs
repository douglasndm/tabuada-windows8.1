using System;

namespace Tabuada.Class
{
    class Results
    {
        public string TabuadaSomando(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_somando = "";
            
            if (ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_somando += CalcularSoma(NumTabuar, i);
                }
            }
            else
            {
                for (short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_somando += CalcularSoma(NumTabuar, i);
                }
            }

            return resultado_somando;
        }

        private string CalcularSoma(double NumTabuar, short i)
        {
            double ResultadoSomando = NumTabuar + i;
            return string.Format("{0} + {1} = {2}\n", NumTabuar, i, ResultadoSomando);
        }


        public string TabuadaSubtraindo(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_subtraindo = "";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_subtraindo += CalcularSubtraindo(NumTabuar, i);
                }
            }
            else
            {
                for (short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_subtraindo += CalcularSubtraindo(NumTabuar, i);
                }
            }

            return resultado_subtraindo;
        }

        private string CalcularSubtraindo(double NumTabuar, short i)
        {
            double ResultadoSubtraindo = NumTabuar - i;
            return string.Format("{0} - {1} = {2}\n", NumTabuar, i, ResultadoSubtraindo);
        }


        public string TabuadaMultiplicando(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_multiplicando = "";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_multiplicando += CalcularMultiplicacao(NumTabuar, i);
                }
            }
            else
            {
                for (short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_multiplicando += CalcularMultiplicacao(NumTabuar, i);
                }
            }

            return resultado_multiplicando;
        }

        private string CalcularMultiplicacao(double NumTabuar, short i)
        {
            double ResultadoMultiplicacao = NumTabuar * i;
            return string.Format("{0} X {1} = {2}\n", NumTabuar, i, ResultadoMultiplicacao);
        }


        public string TabuadaDividindo(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_dividindo = "";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_dividindo += CalcularDivisao(NumTabuar, i);
                }
            }
            else
            {
                for (short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_dividindo += CalcularDivisao(NumTabuar, i);
                }
            }            

            return resultado_dividindo;
        }

        private string CalcularDivisao(double NumTabuar, short i)
        {
            if (i != 0)
            {
                double ResultadoDivisao = NumTabuar / i;
                return string.Format("{0} / {1} = {2}\n", NumTabuar, i, string.Format("{0:0.###}", ResultadoDivisao));
            }
            else
            {
                return "";
            }
            //string.Format("{0:0.##}", 123.586);
        }

 
        public string TabuadaResto(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_resto = "";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_resto += CalcularResto(NumTabuar, i);
                }
            }
            else
            {
                for (short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_resto += CalcularResto(NumTabuar, i);
                }
            }

            return resultado_resto;
        }

        private string CalcularResto(double NumTabuar, short i)
        {
            if (i != 0)
            {
                double RestoDaDivisao = NumTabuar % i;
                double resultadoDivisao = NumTabuar / i;

                if (RestoDaDivisao < NumTabuar)
                {
                    //string.Format("{0:0.##}", 123.586);
                    return string.Format("{0} / {1} = {2} sobra {3}\n", NumTabuar, i, string.Format("{0:0.###}", resultadoDivisao), RestoDaDivisao);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        public string TabuadaRaiz(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_raiz = "";

            if(ordem == OrdemDosResultados.Asc)
            {
                for (short i = 0; i <= NumAteOnde; i++)
                {
                    resultado_raiz += CalcularRaiz(NumTabuar, i);
                }
            }
            else
            {
                for(short i = NumAteOnde; i >= 0; i--)
                {
                    resultado_raiz += CalcularRaiz(NumTabuar, i);
                }
            }

            return resultado_raiz;
        }

        private string CalcularRaiz(double NumTabuar, short i)
        {
            if(i != 0)
            {
                double raiz = Math.Sqrt(i);
                return string.Format("√{0} = {1}\n", i, String.Format("{0:0.###}", raiz));
            }
            else
            {
                return "";
            }
        }
    }

    public enum OrdemDosResultados
    {
        Asc,
        Desc
    }
}
