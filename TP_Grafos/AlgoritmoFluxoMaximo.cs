using System;
using System.Collections.Generic;
using System.Linq;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para encontrar o fluxo máximo em um grafo.
    /// </summary>
    public class AlgoritmoFluxoMaximo
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Matriz para armazenar o fluxo atual em cada aresta.
        /// </summary>
        private double[,] fluxo;

        /// <summary>
        /// Matriz para armazenar a capacidade residual de cada aresta.
        /// </summary>
        private double[,] capacidadeResidual;

        /// <summary>
        /// Array para armazenar o pai de cada vértice no caminho aumentante.
        /// </summary>
        private int[] pai;

        /// <summary>
        /// Array para marcar os vértices já visitados na busca.
        /// </summary>
        private bool[] visitado;

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoFluxoMaximo(Grafo grafo)
        {
            this.grafo = grafo;
            int n = grafo.NumVertices;
            fluxo = new double[n + 1, n + 1];
            capacidadeResidual = new double[n + 1, n + 1];
            pai = new int[n + 1];
            visitado = new bool[n + 1];
            ConstruirGrafoResidual();
        }

        /// <summary>
        /// Executa o algoritmo de Ford-Fulkerson.
        /// </summary>
        /// <param name="origem">O vértice de origem (fonte).</param>
        /// <param name="destino">O vértice de destino (sumidouro).</param>
        /// <returns>O resultado do fluxo máximo.</returns>
        public ResultadoFluxo FordFulkerson(int origem, int destino)
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoFluxo
            {
                FluxoMaximo = 0,
                CorteMinimo = new List<Aresta>(),
                FluxoPorAresta = new Dictionary<(int, int), double>(),
                CaminhosAumentantes = new List<List<int>>()
            };

            ConstruirGrafoResidual();

            double fluxoTotal = 0;
            double fluxoAumentante;

            while ((fluxoAumentante = DFS(origem, destino, double.PositiveInfinity)) > 0)
            {
                fluxoTotal += fluxoAumentante;
                AtualizarFluxo(origem, destino, fluxoAumentante);
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            resultado.FluxoMaximo = fluxoTotal;

            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                for (int j = 1; j <= grafo.NumVertices; j++)
                {
                    if (fluxo[i, j] > 0)
                    {
                        resultado.FluxoPorAresta[(i, j)] = fluxo[i, j];
                    }
                }
            }

            resultado.CorteMinimo = EncontrarCorteMinimo(origem, destino);

            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de Edmonds-Karp.
        /// </summary>
        /// <param name="origem">O vértice de origem (fonte).</param>
        /// <param name="destino">O vértice de destino (sumidouro).</param>
        /// <returns>O resultado do fluxo máximo.</returns>
        public ResultadoFluxo EdmondsKarp(int origem, int destino)
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoFluxo
            {
                FluxoMaximo = 0,
                CorteMinimo = new List<Aresta>(),
                FluxoPorAresta = new Dictionary<(int, int), double>(),
                CaminhosAumentantes = new List<List<int>>()
            };

            ConstruirGrafoResidual();

            double fluxoTotal = 0;

            while (BFS(origem, destino))
            {
                double fluxoAumentante = double.PositiveInfinity;

                for (int v = destino; v != origem; v = pai[v])
                {
                    int u = pai[v];
                    fluxoAumentante = Math.Min(fluxoAumentante, capacidadeResidual[u, v]);
                }

                for (int v = destino; v != origem; v = pai[v])
                {
                    int u = pai[v];
                    capacidadeResidual[u, v] -= fluxoAumentante;
                    capacidadeResidual[v, u] += fluxoAumentante;
                    fluxo[u, v] += fluxoAumentante;
                    fluxo[v, u] -= fluxoAumentante;
                }

                fluxoTotal += fluxoAumentante;
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            resultado.FluxoMaximo = fluxoTotal;

            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                for (int j = 1; j <= grafo.NumVertices; j++)
                {
                    if (fluxo[i, j] > 0)
                    {
                        resultado.FluxoPorAresta[(i, j)] = fluxo[i, j];
                    }
                }
            }

            resultado.CorteMinimo = EncontrarCorteMinimo(origem, destino);

            return resultado;
        }

        /// <summary>
        /// Realiza uma busca em largura (BFS) para encontrar um caminho aumentante.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <returns>True se um caminho foi encontrado, false caso contrário.</returns>
        private bool BFS(int origem, int destino)
        {
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                visitado[i] = false;
                pai[i] = -1;
            }

            var fila = new Queue<int>();
            fila.Enqueue(origem);
            visitado[origem] = true;
            pai[origem] = -1;

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();

                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (!visitado[v] && capacidadeResidual[u, v] > 0)
                    {
                        visitado[v] = true;
                        pai[v] = u;
                        fila.Enqueue(v);

                        if (v == destino)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Realiza uma busca em profundidade (DFS) para encontrar um caminho aumentante.
        /// </summary>
        /// <param name="origem">O vértice atual.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <param name="fluxoMin">O fluxo mínimo no caminho até agora.</param>
        /// <returns>O valor do fluxo no caminho encontrado.</returns>
        private double DFS(int origem, int destino, double fluxoMin)
        {
            if (origem == destino)
                return fluxoMin;

            visitado[origem] = true;

            for (int v = 1; v <= grafo.NumVertices; v++)
            {
                if (!visitado[v] && capacidadeResidual[origem, v] > 0)
                {
                    double fluxoEncontrado = DFS(v, destino, Math.Min(fluxoMin, capacidadeResidual[origem, v]));
                    
                    if (fluxoEncontrado > 0)
                    {
                        capacidadeResidual[origem, v] -= fluxoEncontrado;
                        capacidadeResidual[v, origem] += fluxoEncontrado;
                        fluxo[origem, v] += fluxoEncontrado;
                        fluxo[v, origem] -= fluxoEncontrado;
                        visitado[origem] = false;
                        return fluxoEncontrado;
                    }
                }
            }

            visitado[origem] = false;
            return 0;
        }

        /// <summary>
        /// Encontra um caminho aumentante no grafo residual.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <returns>O valor do fluxo que pode ser enviado pelo caminho.</returns>
        private double EncontrarCaminhoAumentante(int origem, int destino)
        {
            for (int i = 1; i <= grafo.NumVertices; i++)
                visitado[i] = false;

            return DFS(origem, destino, double.PositiveInfinity);
        }

        /// <summary>
        /// Atualiza o fluxo nas arestas do caminho aumentante.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <param name="valorFluxo">O valor do fluxo a ser atualizado.</param>
        private void AtualizarFluxo(int origem, int destino, double valorFluxo)
        {
        }

        /// <summary>
        /// Encontra o corte mínimo no grafo.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <returns>A lista de arestas que compõem o corte mínimo.</returns>
        public List<Aresta> EncontrarCorteMinimo(int origem, int destino)
        {
            var corte = new List<Aresta>();

            for (int i = 1; i <= grafo.NumVertices; i++)
                visitado[i] = false;

            var fila = new Queue<int>();
            fila.Enqueue(origem);
            visitado[origem] = true;

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();
                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (!visitado[v] && capacidadeResidual[u, v] > 0)
                    {
                        visitado[v] = true;
                        fila.Enqueue(v);
                    }
                }
            }

            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                for (int j = 1; j <= grafo.NumVertices; j++)
                {
                    if (visitado[i] && !visitado[j] && grafo.ExisteAresta(i, j))
                    {
                        corte.Add(new Aresta(i, j, grafo.ObterPeso(i, j), grafo.ObterCapacidade(i, j)));
                    }
                }
            }

            return corte;
        }

        /// <summary>
        /// Constrói ou atualiza o grafo residual.
        /// </summary>
        private void ConstruirGrafoResidual()
        {
            int n = grafo.NumVertices;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    capacidadeResidual[i, j] = 0;
                    fluxo[i, j] = 0;
                }
            }

            var todasArestas = grafo.ObterTodasArestas();
            foreach (var aresta in todasArestas)
            {
                capacidadeResidual[aresta.Origem, aresta.Destino] = aresta.Capacidade;
            }
        }
    }
}
