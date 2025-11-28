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
        }

        /// <summary>
        /// Executa o algoritmo de Dijkstra para encontrar o caminho mínimo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O resultado do caminho mínimo.</returns>
        public ResultadoCaminho Dijkstra(int origem, int destino)
        {
            return null;
        }

        /// <summary>
        /// Executa o algoritmo de Bellman-Ford para encontrar o caminho mínimo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O resultado do caminho mínimo.</returns>
        public ResultadoCaminho BellmanFord(int origem, int destino)
        {
            return null;
        }

        /// <summary>
        /// Realiza a operação de relaxamento de uma aresta.
        /// </summary>
        /// <param name="u">Vértice de origem da aresta.</param>
        /// <param name="v">Vértice de destino da aresta.</param>
        /// <param name="peso">Peso da aresta.</param>
        private void RelaxarAresta(int u, int v, double peso)
        {
        }

        /// <summary>
        /// Extrai o vértice com a menor distância da fila de prioridade.
        /// </summary>
        /// <param name="pq">A fila de prioridade.</param>
        /// <returns>O vértice com a menor distância.</returns>
        private int ExtrairMinimo(SortedSet<(double, int)> pq)
        {
            return 0;
        }

        /// <summary>
        /// Reconstrói o caminho a partir dos predecessores.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>A lista de vértices no caminho.</returns>
        private List<int> ReconstruirCaminho(int origem, int destino)
        {
            return null;
        }

        /// <summary>
        /// Inicializa as estruturas de dados para a execução do algoritmo.
        /// </summary>
        /// <param name="origem">O vértice de origem.</param>
        private void InicializarEstruturas(int origem)
        {
        }
    }
}
