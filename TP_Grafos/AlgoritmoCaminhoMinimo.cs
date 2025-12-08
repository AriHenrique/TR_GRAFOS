using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para encontrar o caminho mínimo em um grafo.
    /// </summary>
    public class AlgoritmoCaminhoMinimo
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Array para armazenar as distâncias da origem a cada vértice.
        /// </summary>
        private double[] distancias;

        /// <summary>
        /// Array para armazenar os predecessores de cada vértice no caminho.
        /// </summary>
        private int[] predecessores;

        /// <summary>
        /// Array para marcar os vértices já visitados.
        /// </summary>
        private bool[] visitados;

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoCaminhoMinimo(Grafo grafo)
        {
            this.grafo = grafo;
            distancias = Array.Empty<double>();
            predecessores = Array.Empty<int>();
            visitados = Array.Empty<bool>();
        }

        /// <summary>
        /// Executa o algoritmo de Dijkstra para encontrar o caminho mínimo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O resultado do caminho mínimo.</returns>
        public ResultadoCaminho Dijkstra(int origem, int destino)
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            InicializarEstruturas(origem);

            var pq = new SortedSet<(double dist, int v)>();
            pq.Add((0, origem));

            while (pq.Count > 0)
            {
                var (dist, u) = pq.Min;
                pq.Remove(pq.Min);

                if (visitados[u])
                {
                    continue;
                }

                visitados[u] = true;

                foreach (var aresta in grafo.ObterVizinhos(u))
                {
                    RelaxarAresta(u, aresta.Destino, aresta.Peso);
                    if (!visitados[aresta.Destino])
                    {
                        pq.Add((distancias[aresta.Destino], aresta.Destino));
                    }
                }
            }

            medidor.Parar();

            var resultado = new ResultadoCaminho
            {
                Caminho = ReconstruirCaminho(origem, destino),
                CustoTotal = distancias[destino],
                CaminhoEncontrado = distancias[destino] != double.PositiveInfinity,
                TempoExecucao = medidor.ObterTempoDecorrido(),
                AlgoritmoUsado = "Dijkstra"
            };

            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de Bellman-Ford para encontrar o caminho mínimo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O resultado do caminho mínimo.</returns>
        public ResultadoCaminho BellmanFord(int origem, int destino)
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            InicializarEstruturas(origem);

            var arestas = grafo.ObterTodasArestas();
            int n = grafo.NumVertices;

            for (int i = 1; i <= n - 1; i++)
            {
                foreach (var a in arestas)
                {
                    RelaxarAresta(a.Origem, a.Destino, a.Peso);
                }
            }

            medidor.Parar();

            var resultado = new ResultadoCaminho
            {
                Caminho = ReconstruirCaminho(origem, destino),
                CustoTotal = distancias[destino],
                CaminhoEncontrado = distancias[destino] != double.PositiveInfinity,
                TempoExecucao = medidor.ObterTempoDecorrido(),
                AlgoritmoUsado = "Bellman-Ford"
            };

            return resultado;
        }

        /// <summary>
        /// Realiza a operação de relaxamento de uma aresta.
        /// </summary>
        /// <param name="u">Vértice de origem da aresta.</param>
        /// <param name="v">Vértice de destino da aresta.</param>
        /// <param name="peso">Peso da aresta.</param>
        private void RelaxarAresta(int u, int v, double peso)
        {
            if (distancias[u] + peso < distancias[v])
            {
                distancias[v] = distancias[u] + peso;
                predecessores[v] = u;
            }
        }

        /// <summary>
        /// Extrai o vértice com a menor distância da fila de prioridade.
        /// </summary>
        /// <param name="pq">A fila de prioridade.</param>
        /// <returns>O vértice com a menor distância.</returns>
        private int ExtrairMinimo(SortedSet<(double, int)> pq)
        {
            var (dist, v) = pq.Min;
            pq.Remove(pq.Min);
            return v;
        }

        /// <summary>
        /// Reconstrói o caminho a partir dos predecessores.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>A lista de vértices no caminho.</returns>
        private List<int> ReconstruirCaminho(int origem, int destino)
        {
            var caminho = new List<int>();
            if (distancias[destino] == double.PositiveInfinity)
            {
                return caminho;
            }

            int atual = destino;
            while (atual != -1)
            {
                caminho.Add(atual);
                if (atual == origem)
                {
                    break;
                }
                atual = predecessores[atual];
            }

            caminho.Reverse();
            return caminho;
        }

        /// <summary>
        /// Inicializa as estruturas de dados para a execução do algoritmo.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        private void InicializarEstruturas(int origem)
        {
            int n = grafo.NumVertices;
            distancias = new double[n + 1];
            predecessores = new int[n + 1];
            visitados = new bool[n + 1];

            for (int i = 1; i <= n; i++)
            {
                distancias[i] = double.PositiveInfinity;
                predecessores[i] = -1;
                visitados[i] = false;
            }

            distancias[origem] = 0;
        }
    }
}
