using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Tabuada.Class
{
    class SpeechRecognizer
    {
        MediaElement mediaplayer = new MediaElement();

        public async void Speech(string Text)
        {          
            using (var speech = new SpeechSynthesizer())
            {
                SpeechSynthesisStream stream = await speech.SynthesizeTextToStreamAsync(Text);

                mediaplayer.SetSource(stream, stream.ContentType);
                mediaplayer.Play();
            }
        }
        public void StopSpeech()
        {
            mediaplayer.Stop();
        }

        public void SpeechSomando(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_somando = "Tabuada de soma\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    double temp = NumTabuar + i;
                    resultado_somando += NumTabuar + " mais " + i + " é igual a " + temp + "\n";
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    double temp = NumTabuar + i;
                    resultado_somando += NumTabuar + " mais " + i + " é igual a " + temp + "\n";
                }
            }

            Speech(resultado_somando);
        }

        public void SpeechSubtraindo(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_subtraindo = "Tabuada de subtração\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    double temp = NumTabuar - i;
                    resultado_subtraindo += NumTabuar + " menos " + i + " é igual a " + temp + "\n";
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    double temp = NumTabuar - i;
                    resultado_subtraindo += NumTabuar + " menos " + i + " é igual a " + temp + "\n";
                }
            }

            Speech(resultado_subtraindo);
        }

        // MULTIPLICANDO
        public void SpeechMultiplicando(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_multiplicando = "Tabuada de multiplicação\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    double temp = NumTabuar * i;
                    resultado_multiplicando += NumTabuar + " multiplicado por " + i + " é igual a " + temp + "\n";
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    double temp = NumTabuar * i;
                    resultado_multiplicando += NumTabuar + " multiplicado por " + i + " é igual a " + temp + "\n";
                }
            }

            Speech(resultado_multiplicando);
        }

        public void SpeechDividindo(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_dividindo = "Tabuada de divisão\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    resultado_dividindo += CalcularDivisao(NumTabuar, i);
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    resultado_dividindo += CalcularDivisao(NumTabuar, i);
                }
            }

            Speech(resultado_dividindo);
        }

        private string CalcularDivisao(double NumTabuar, int i)
        {
            if (i != 0)
            {
                double ResultadoDivisao = NumTabuar / i;
                return NumTabuar + " dividido por " + i + " é igual a " + string.Format("{0:0.###}", ResultadoDivisao) + "\n";
            }
            else
            {
                return "";
            }
        }


        public void SpeechResto(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_resto = "Tabuada do resto da divisão\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    if (i != 0)
                    {
                        resultado_resto += CreateSpeechResto(NumTabuar, i);
                    }
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    if (i != 0)
                    {
                        resultado_resto += CreateSpeechResto(NumTabuar, i);
                    }
                }
            }
            Speech(resultado_resto);
        }

        private string CreateSpeechResto(double NumTabuar, int i)
        {
            if (i != 0)
            {
                double RestoDaDivisao = NumTabuar % i;
                double resultadoDivisao = NumTabuar / i;

                if (RestoDaDivisao < NumTabuar)
                {
                    return NumTabuar.ToString() + " dividido por " + i + " é igual a " + string.Format("{0:0.###}", resultadoDivisao) + " e sobra " + RestoDaDivisao + "\n";
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


        public void SpeechRaiz(double NumTabuar, int NumAteOnde, OrdemDosResultados ordem)
        {
            string resultado_raiz = "Tabuada da raiz quadrada\n";

            if (ordem == OrdemDosResultados.Asc)
            {
                for (int i = 0; i <= NumAteOnde; i++)
                {
                    if (i != 0)
                    {
                        resultado_raiz += CreateSpeechRaiz(NumTabuar, i);
                    }
                }
            }
            else
            {
                for (int i = NumAteOnde; i >= 0; i--)
                {
                    if (i != 0)
                    {
                        resultado_raiz += CreateSpeechRaiz(NumTabuar, i);
                    }
                }
            }
            Speech(resultado_raiz);
        }

        private string CreateSpeechRaiz(double NumTabuar, int i)
        {
            if (i != 0)
            {
                double raiz = Math.Sqrt(i);

                string resultado = "A raiz quadrada de " + i + " é " + String.Format("{0:0.###}", raiz) + "\n";

                return resultado;
            }
            else
            {
                return "";
            }
        }
    }
}
