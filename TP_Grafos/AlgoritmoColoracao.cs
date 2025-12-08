using System.Collections.Generic;
using System.Linq;

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
            cores = new int[grafo.NumVertices + 1];
            grauSaturacao = new int[grafo.NumVertices + 1];
            ordemVertices = new List<int>();
        }

        /// <summary>
        /// Executa um algoritmo de coloração guloso.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoGulosa()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            Array.Fill(cores, -1);

            // Ordena vértices por grau decrescente para melhorar o greedy
            OrdenarPorGrauDecrescente();

            int corAtual = 0;
            var resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "Guloso"
            };

            foreach (var v in ordemVertices)
            {
                var coresAdj = new HashSet<int>();
                foreach (var a in grafo.ObterVizinhos(v))
                {
                    if (cores[a.Destino] != -1)
                    {
                        coresAdj.Add(cores[a.Destino]);
                    }
                }

                int cor = 0;
                while (coresAdj.Contains(cor))
                {
                    cor++;
                }

                cores[v] = cor;
                corAtual = Math.Max(corAtual, cor);
                resultado.AtribuirCor(v, cor);
            }

            resultado.NumeroTurnos = corAtual + 1;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de coloração DSATUR.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoDSATUR()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            Array.Fill(cores, -1);
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                grauSaturacao[i] = 0;
            }

            var resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "DSATUR"
            };

            for (int i = 0; i < grafo.NumVertices; i++)
            {
                int v = ProximoVerticeDSATUR();
                int cor = ObterPrimeiraCor(v);
                cores[v] = cor;
                resultado.AtribuirCor(v, cor);

                foreach (var a in grafo.ObterVizinhos(v))
                {
                    if (cores[a.Destino] == -1)
                    {
                        grauSaturacao[a.Destino] = CalcularGrauSaturacao(a.Destino);
                    }
                }
            }

            resultado.NumeroTurnos = resultado.GruposPorCor.Count;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de coloração de Welsh-Powell.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoWelshPowell()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            Array.Fill(cores, -1);
            OrdenarPorGrauDecrescente();

            int corAtual = 0;
            var resultado = new ResultadoColoracao
            {
                AlgoritmoUsado = "Welsh-Powell"
            };

            foreach (var v in ordemVertices)
            {
                if (cores[v] != -1) continue;

                cores[v] = corAtual;
                resultado.AtribuirCor(v, corAtual);

                foreach (var u in ordemVertices)
                {
                    if (cores[u] == -1 && PodeColorir(u, corAtual))
                    {
                        cores[u] = corAtual;
                        resultado.AtribuirCor(u, corAtual);
                    }
                }

                corAtual++;
            }

            resultado.NumeroTurnos = corAtual;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Encontra o próximo vértice a ser colorido pelo algoritmo DSATUR.
        /// </summary>
        /// <returns>O índice do próximo vértice.</returns>
        private int ProximoVerticeDSATUR()
        {
            int melhor = -1;
            int melhorSat = -1;
            int melhorGrau = -1;

            for (int v = 1; v <= grafo.NumVertices; v++)
            {
                if (cores[v] != -1) continue;

                int sat = grauSaturacao[v];
                int grau = grafo.ObterGrauVertice(v);

                if (sat > melhorSat || (sat == melhorSat && grau > melhorGrau))
                {
                    melhor = v;
                    melhorSat = sat;
                    melhorGrau = grau;
                }
            }

            return melhor;
        }

        /// <summary>
        /// Calcula o grau de saturação de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de saturação.</returns>
        private int CalcularGrauSaturacao(int vertice)
        {
            var coresAdj = new HashSet<int>();
            foreach (var a in grafo.ObterVizinhos(vertice))
            {
                if (cores[a.Destino] != -1)
                {
                    coresAdj.Add(cores[a.Destino]);
                }
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
            foreach (var a in grafo.ObterVizinhos(vertice))
            {
                if (cores[a.Destino] == cor)
                {
                    return false;
                }
            }

            // também verifica arestas de entrada
            for (int u = 1; u <= grafo.NumVertices; u++)
            {
                foreach (var a in grafo.ObterVizinhos(u))
                {
                    if (a.Destino == vertice && cores[u] == cor)
                    {
                        return false;
                    }
                }
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
            ordemVertices = Enumerable.Range(1, grafo.NumVertices)
                .OrderByDescending(v => grafo.ObterGrauVertice(v))
                .ToList();
        }
    }
}
