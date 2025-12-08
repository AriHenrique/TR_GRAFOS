using System.Collections.Generic;

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
            var n = grafo.NumVertices;
            fluxo = new double[n + 1, n + 1];
            capacidadeResidual = grafo.ObterMatrizCapacidade();
            pai = new int[n + 1];
            visitado = new bool[n + 1];
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

            double fluxoMax = 0;
            while (true)
            {
                Array.Fill(pai, -1);
                Array.Fill(visitado, false);
                double aumento = DFS(origem, destino, double.PositiveInfinity);
                if (aumento == 0)
                {
                    break;
                }

                fluxoMax += aumento;
                AtualizarFluxo(destino, origem, aumento); // pai preenchido na DFS
            }

            medidor.Parar();

            var resultado = new ResultadoFluxo
            {
                FluxoMaximo = fluxoMax,
                CorteMinimo = EncontrarCorteMinimo(origem, destino),
                TempoExecucao = medidor.ObterTempoDecorrido()
            };

            // salva fluxo por aresta
            for (int u = 1; u <= grafo.NumVertices; u++)
            {
                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (fluxo[u, v] > 0)
                    {
                        resultado.AdicionarFluxoAresta(u, v, fluxo[u, v]);
                    }
                }
            }

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

            double fluxoMax = 0;
            while (BFS(origem, destino))
            {
                double caminhoFluxo = double.PositiveInfinity;
                for (int v = destino; v != origem; v = pai[v])
                {
                    int u = pai[v];
                    caminhoFluxo = Math.Min(caminhoFluxo, capacidadeResidual[u, v]);
                }

                for (int v = destino; v != origem; v = pai[v])
                {
                    int u = pai[v];
                    fluxo[u, v] += caminhoFluxo;
                    fluxo[v, u] -= caminhoFluxo;
                    capacidadeResidual[u, v] -= caminhoFluxo;
                    capacidadeResidual[v, u] += caminhoFluxo;
                }

                fluxoMax += caminhoFluxo;
            }

            medidor.Parar();

            var resultado = new ResultadoFluxo
            {
                FluxoMaximo = fluxoMax,
                CorteMinimo = EncontrarCorteMinimo(origem, destino),
                TempoExecucao = medidor.ObterTempoDecorrido()
            };

            for (int u = 1; u <= grafo.NumVertices; u++)
            {
                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (fluxo[u, v] > 0)
                    {
                        resultado.AdicionarFluxoAresta(u, v, fluxo[u, v]);
                    }
                }
            }

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
            Array.Fill(visitado, false);
            Array.Fill(pai, -1);

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
                        fila.Enqueue(v);
                        visitado[v] = true;
                        pai[v] = u;
                    }
                }
            }

            return visitado[destino];
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
            {
                return fluxoMin;
            }

            visitado[origem] = true;

            for (int v = 1; v <= grafo.NumVertices; v++)
            {
                if (!visitado[v] && capacidadeResidual[origem, v] > 0)
                {
                    double possivel = DFS(v, destino, Math.Min(fluxoMin, capacidadeResidual[origem, v]));
                    if (possivel > 0)
                    {
                        pai[v] = origem;
                        capacidadeResidual[origem, v] -= possivel;
                        capacidadeResidual[v, origem] += possivel;
                        return possivel;
                    }
                }
            }

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
            return BFS(origem, destino) ? 1 : 0;
        }

        /// <summary>
        /// Atualiza o fluxo nas arestas do caminho aumentante.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <param name="valorFluxo">O valor do fluxo a ser atualizado.</param>
        private void AtualizarFluxo(int origem, int destino, double valorFluxo)
        {
            for (int v = destino; v != origem; v = pai[v])
            {
                int u = pai[v];
                fluxo[u, v] += valorFluxo;
                fluxo[v, u] -= valorFluxo;
            }
        }

        /// <summary>
        /// Encontra o corte mínimo no grafo.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        /// <param name="destino">O vértice de destino.</param>
        /// <returns>A lista de arestas que compõem o corte mínimo.</returns>
        public List<Aresta> EncontrarCorteMinimo(int origem, int destino)
        {
            // Usa vetor visitado da última BFS (ou DFS no FF) para definir o conjunto S
            var corte = new List<Aresta>();
            var visit = new bool[grafo.NumVertices + 1];
            var fila = new Queue<int>();
            fila.Enqueue(origem);
            visit[origem] = true;

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();
                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (capacidadeResidual[u, v] > 0 && !visit[v])
                    {
                        visit[v] = true;
                        fila.Enqueue(v);
                    }
                }
            }

            for (int u = 1; u <= grafo.NumVertices; u++)
            {
                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (visit[u] && !visit[v] && grafo.ObterCapacidade(u, v) > 0)
                    {
                        corte.Add(new Aresta(u, v, grafo.ObterPeso(u, v), grafo.ObterCapacidade(u, v)));
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
            // Já iniciamos capacidadeResidual no construtor usando a matriz de capacidade do grafo
        }
    }
}
