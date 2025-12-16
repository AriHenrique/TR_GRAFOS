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
            visitado = new bool[grafo.NumVertices + 1];
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
            resultado.ExisteCiclo = ciclo != null && ciclo.Count > 0;
            resultado.Sequencia = ciclo;
            resultado.NumeroArestasPercorridas = ciclo != null ? ciclo.Count - 1 : 0;

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
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                int grauEntrada = grafo.ObterGrauEntrada(i);
                int grauSaida = grafo.ObterGrauSaida(i);
                if (grauEntrada != grauSaida)
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
            return Hierholzer(1);
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

            Dictionary<int, List<Aresta>> arestasDisponiveis = new Dictionary<int, List<Aresta>>();
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                arestasDisponiveis[i] = new List<Aresta>(grafo.ObterVizinhos(i));
            }

            pilha.Push(inicio);

            while (pilha.Count > 0)
            {
                int v = pilha.Peek();

                if (arestasDisponiveis[v].Count > 0)
                {
                    var aresta = arestasDisponiveis[v][0];
                    int u = aresta.Destino;

                    arestasDisponiveis[v].RemoveAt(0);

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
            if (grafo.NumVertices < 3)
                return false;

            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                if (grafo.ObterGrauSaida(i) < 1)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Encontra um ciclo Hamiltoniano usando backtracking.
        /// </summary>
        /// <param name="inicio">O vértice inicial.</param>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> EncontrarCicloHamiltoniano(int inicio)
        {
            visitado = new bool[grafo.NumVertices + 1];
            List<int> caminhoHamiltoniano = new List<int>();
            caminhoHamiltoniano.Add(inicio);
            visitado[inicio] = true;

            if (BacktrackHamiltoniano(caminhoHamiltoniano, 1))
            {
                caminhoHamiltoniano.Add(inicio);
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
                int ultimoVertice = caminho[pos - 1];
                int primeiroVertice = caminho[0];
                return grafo.ExisteAresta(ultimoVertice, primeiroVertice);
            }

            int verticeAtual = caminho[pos - 1];

            foreach (var aresta in grafo.ObterVizinhos(verticeAtual))
            {
                int v = aresta.Destino;
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
