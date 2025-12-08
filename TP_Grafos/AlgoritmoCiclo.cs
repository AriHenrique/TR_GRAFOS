using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para encontrar ciclos Eulerianos e Hamiltonianos.
    /// </summary>
    public class AlgoritmoCiclo
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Array para marcar os vértices já visitados.
        /// </summary>
        private bool[] visitado;

        /// <summary>
        /// Pilha para auxiliar na busca de ciclos.
        /// </summary>
        private Stack<int> pilha;

        /// <summary>
        /// Lista para armazenar o caminho do ciclo encontrado.
        /// </summary>
        private List<int> caminho;

        /// <summary>
        /// Array para armazenar o grau de entrada de cada vértice.
        /// </summary>
        private int[] grauEntrada;

        /// <summary>
        /// Array para armazenar o grau de saída de cada vértice.
        /// </summary>
        private int[] grauSaida;

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoCiclo(Grafo grafo)
        {
            this.grafo = grafo;
            visitado = new bool[grafo.NumVertices];
            pilha = new Stack<int>();
            caminho = new List<int>();
        }

        /// <summary>
        /// Verifica a existência e encontra um ciclo Euleriano.
        /// </summary>
        /// <returns>O resultado da busca pelo ciclo.</returns>
        public ResultadoCiclo VerificarCicloEuleriano()
        {
            ResultadoCiclo resultado = new ResultadoCiclo();

            if (!TemCicloEuleriano())
            {
                resultado.ExisteCiclo = false;
                return resultado;
            }

            List<int> ciclo = EncontrarCicloEuleriano();
            resultado.ExisteCiclo = true;
            resultado.Sequencia = ciclo;

            return resultado;
        }

        /// <summary>
        /// Verifica a existência e encontra um ciclo Hamiltoniano.
        /// </summary>
        /// <returns>O resultado da busca pelo ciclo.</returns>
        public ResultadoCiclo VerificarCicloHamiltoniano()
        {
            ResultadoCiclo resultado = new ResultadoCiclo();

            if (!TemCicloHamiltoniano())
            {
                resultado.ExisteCiclo = false;
                return resultado;
            }

            List<int> ciclo = EncontrarCicloHamiltoniano(0);

            resultado.ExisteCiclo = ciclo != null;
            resultado.Sequencia = ciclo;

            return resultado;
        }

        /// <summary>
        /// Verifica se o grafo possui as condições para um ciclo Euleriano.
        /// </summary>
        /// <returns>True se as condições são satisfeitas, false caso contrário.</returns>
        private bool TemCicloEuleriano()
        {
            for (int i = 0; i < grafo.NumVertices; i++)
            {
                if (grafo.Grau(i) % 2 != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Encontra o caminho de um ciclo Euleriano usando o algoritmo de Hierholzer.
        /// </summary>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> EncontrarCicloEuleriano()
        {
            return Hierholzer(0);
        }

        /// <summary>
        /// Implementação do algoritmo de Hierholzer.
        /// </summary>
        /// <param name="inicio">O vértice inicial.</param>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> Hierholzer(int inicio)
        {
            Stack<int> pilha = new Stack<int>();
            List<int> ciclo = new List<int>();

            // Cópia local das adjacências
            List<int>[] adj = new List<int>[grafo.NumVertices];
            for (int i = 0; i < grafo.NumVertices; i++)
            {
                adj[i] = new List<int>(grafo.Adjacencias[i]);
            }

            pilha.Push(inicio);

            while (pilha.Count > 0)
            {
                int v = pilha.Peek();

                if (adj[v].Count > 0)
                {
                    int u = adj[v][0];

                    //Remove apenas a aresta v → u (grafo direcionado)
                    adj[v].RemoveAt(0);

                    pilha.Push(u);
                }
                else
                {
                    ciclo.Add(v);
                    pilha.Pop();
                }
            }

            ciclo.Reverse();
            return ciclo;
        }

        /// <summary>
        /// Verifica se o grafo possui as condições para um ciclo Hamiltoniano (heurística).
        /// </summary>
        /// <returns>True se as condições são satisfeitas, false caso contrário.</returns>
        private bool TemCicloHamiltoniano()
        {
            for (int i = 0; i < grafo.NumVertices; i++)
                visitado[i] = false;

            return true;
        }

        /// <summary>
        /// Encontra um ciclo Hamiltoniano usando backtracking.
        /// </summary>
        /// <param name="inicio">O vértice inicial.</param>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> EncontrarCicloHamiltoniano(int inicio)
        {
            List<int> caminhoHamiltoniano = new List<int>();
            caminhoHamiltoniano.Add(inicio);
            visitado[inicio] = true;

            if (BacktrackHamiltoniano(caminhoHamiltoniano, 1))
            {
                caminhoHamiltoniano.Add(inicio); // Fecha o ciclo
                return caminhoHamiltoniano;
            }

            return null;
        }

        /// <summary>
        /// Função de backtracking para encontrar o ciclo Hamiltoniano.
        /// </summary>
        /// <param name="caminho">O caminho atual.</param>
        /// <param name="pos">A posição atual no caminho.</param>
        /// <returns>True se um ciclo foi encontrado, false caso contrário.</returns>
        private bool BacktrackHamiltoniano(List<int> caminho, int pos)
        {
            if (pos == grafo.NumVertices)
            {
                return grafo.ExisteAresta(caminho[pos - 1], caminho[0]);
            }

            int ultimo = caminho[pos - 1];

            foreach (int v in grafo.Adjacentes(ultimo))
            {
                if (!visitado[v])
                {
                    visitado[v] = true;
                    caminho.Add(v);

                    if (BacktrackHamiltoniano(caminho, pos + 1))
                        return true;

                    visitado[v] = false;
                    caminho.RemoveAt(caminho.Count - 1);
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica se todos os vértices foram visitados.
        /// </summary>
        /// <returns>True se todos foram visitados, false caso contrário.</returns>
        private bool TodosVerticesVisitados()
        {
            for (int i = 0; i < visitado.Length; i++)
            {
                if (!visitado[i])
                    return false;
            }
            return true;
        }
    }
}
