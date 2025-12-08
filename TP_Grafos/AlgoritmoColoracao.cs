using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para a coloração de vértices de um grafo.
    /// </summary>
    public class AlgoritmoColoracao
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Array para armazenar a cor de cada vértice.
        /// </summary>
        private int[] cores;

        /// <summary>
        /// O número de cores utilizadas.
        /// </summary>
        private int numCores;

        /// <summary>
        /// Ordem dos vértices a serem coloridos.
        /// </summary>
        private List<int> ordemVertices;

        /// <summary>
        /// Grau de saturação de cada vértice (para DSATUR).
        /// </summary>
        private int[] grauSaturacao;

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoColoracao(Grafo grafo)
        {
            this.grafo = grafo;
            cores = new int[grafo.NumVertices];
            grauSaturacao = new int[grafo.NumVertices];
            ordemVertices = new List<int>();
        }

        /// <summary>
        /// Executa um algoritmo de coloração guloso.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoGulosa()
        {
            Array.Fill(cores, -1);
            numCores = 0;

            ResultadoColoracao resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "Coloração Gulosa",
                CorPorVertice = new Dictionary<int, int>(),
                GruposPorCor = new Dictionary<int, List<int>>(),
                TempoExecucao = 0
            };

            for (int v = 0; v < grafo.NumVertices; v++)
            {
                int cor = ObterPrimeiraCor(v);
                cores[v] = cor;
                resultado.AtribuirCor(v, cor);
                numCores = Math.Max(numCores, cor + 1);
            }

            resultado.NumeroTurnos = numCores;
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de coloração DSATUR.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoDSATUR()
        {
            Array.Fill(cores, -1);
            Array.Fill(grauSaturacao, 0);
            numCores = 0;

            ResultadoColoracao resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "DSATUR",
                CorPorVertice = new Dictionary<int, int>(),
                GruposPorCor = new Dictionary<int, List<int>>(),
                TempoExecucao = 0
            };

            for (int i = 0; i < grafo.NumVertices; i++)
            {
                int v = ProximoVerticeDSATUR();
                int cor = ObterPrimeiraCor(v);

                cores[v] = cor;
                resultado.AtribuirCor(v, cor);
                numCores = Math.Max(numCores, cor + 1);

                foreach (int adj in grafo.Adjacentes(v))
                {
                    grauSaturacao[adj] = CalcularGrauSaturacao(adj);
                }
            }

            resultado.NumeroTurnos = numCores;
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de coloração de Welsh-Powell.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoWelshPowell()
        {
            Array.Fill(cores, -1);
            numCores = 0;

            OrdenarPorGrauDecrescente();

            ResultadoColoracao resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "Welsh-Powell",
                CorPorVertice = new Dictionary<int, int>(),
                GruposPorCor = new Dictionary<int, List<int>>(),
                TempoExecucao = 0
            };

            foreach (int v in ordemVertices)
            {
                int cor = ObterPrimeiraCor(v);
                cores[v] = cor;
                resultado.AtribuirCor(v, cor);
                numCores = Math.Max(numCores, cor + 1);
            }

            resultado.NumeroTurnos = numCores;
            return resultado;
        }

        /// <summary>
        /// Encontra o próximo vértice a ser colorido pelo algoritmo DSATUR.
        /// </summary>
        /// <returns>O índice do próximo vértice.</returns>
        private int ProximoVerticeDSATUR()
        {
            int escolhido = -1;
            int maiorSaturacao = -1;
            int maiorGrau = -1;

            for (int v = 0; v < grafo.NumVertices; v++)
            {
                if (cores[v] != -1)
                    continue;

                int saturacao = grauSaturacao[v];
                int grau = grafo.Grau(v);

                if (saturacao > maiorSaturacao ||
                    (saturacao == maiorSaturacao && grau > maiorGrau))
                {
                    maiorSaturacao = saturacao;
                    maiorGrau = grau;
                    escolhido = v;
                }
            }

            return escolhido;
        }

        /// <summary>
        /// Calcula o grau de saturação de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de saturação.</returns>
        private int CalcularGrauSaturacao(int vertice)
        {
            HashSet<int> coresAdj = new HashSet<int>();

            foreach (int adj in grafo.Adjacentes(vertice))
            {
                if (cores[adj] != -1)
                    coresAdj.Add(cores[adj]);
            }

            return coresAdj.Count;
        }

        /// <summary>
        /// Verifica se um vértice pode ser colorido com uma determinada cor.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <param name="cor">A cor.</param>
        /// <returns>True se a coloração for possível, false caso contrário.</returns>
        private bool PodeColorir(int vertice, int cor)
        {
            foreach (int adj in grafo.Adjacentes(vertice))
            {
                if (cores[adj] == cor)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Obtém a primeira cor disponível para um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>A primeira cor disponível.</returns>
        private int ObterPrimeiraCor(int vertice)
        {
            int cor = 0;
            while (!PodeColorir(vertice, cor))
            {
                cor++;
            }
            return cor;
        }

        /// <summary>
        /// Ordena os vértices por grau decrescente (para Welsh-Powell).
        /// </summary>
        private void OrdenarPorGrauDecrescente()
        {
            ordemVertices.Clear();

            for (int i = 0; i < grafo.NumVertices; i++)
            {
                ordemVertices.Add(i);
            }

            for (int i = 0; i < ordemVertices.Count - 1; i++)
            {
                for (int j = i + 1; j < ordemVertices.Count; j++)
                {
                    if (grafo.Grau(ordemVertices[j]) > grafo.Grau(ordemVertices[i]))
                    {
                        int aux = ordemVertices[i];
                        ordemVertices[i] = ordemVertices[j];
                        ordemVertices[j] = aux;
                    }
                }
            }
        }
    }
}
