using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para encontrar a Árvore Geradora Mínima (AGM).
    /// </summary>
    public class AlgoritmoArvoreGeradora
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Estrutura de dados Union-Find para o algoritmo de Kruskal.
        /// </summary>
        private UnionFind unionFind;

        /// <summary>
        /// Lista de arestas ordenadas por peso.
        /// </summary>
        private List<Aresta> arestasOrdenadas;

        /// <summary>
        /// Array para marcar os vértices já visitados.
        /// </summary>
        private bool[] visitado;

        /// <summary>
        /// Fila de prioridade para o algoritmo de Prim.
        /// </summary>
        // private PriorityQueue<Aresta> filaPrioridade; // .NET 6+

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoArvoreGeradora(Grafo grafo)
        {
        }

        /// <summary>
        /// Executa o algoritmo de Kruskal para encontrar a AGM.
        /// </summary>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Kruskal()
        {
            return null;
        }

        /// <summary>
        /// Executa o algoritmo de Prim para encontrar a AGM.
        /// </summary>
        /// <param name="verticeInicial">O vértice inicial.</param>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Prim(int verticeInicial)
        {
            return null;
        }

        /// <summary>
        /// Executa o algoritmo de Boruvka para encontrar a AGM.
        /// </summary>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Boruvka()
        {
            return null;
        }

        /// <summary>
        /// Ordena as arestas do grafo por peso.
        /// </summary>
        private void OrdenarArestasPorPeso()
        {
        }

        /// <summary>
        /// Obtém a aresta de peso mínimo de um componente.
        /// </summary>
        /// <param name="componente">O identificador do componente.</param>
        /// <returns>A aresta de peso mínimo.</returns>
        private Aresta ObterArestaMinimaComponente(int componente)
        {
            return null;
        }

        /// <summary>
        /// Verifica se o grafo é conexo.
        /// </summary>
        /// <returns>True se o grafo for conexo, false caso contrário.</returns>
        private bool VerificarConectividade()
        {
            return false;
        }
    }
}
