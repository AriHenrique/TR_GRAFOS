using System.Collections.Generic;
using System.Linq;

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
            grauEntrada = new int[grafo.NumVertices + 1];
            grauSaida = new int[grafo.NumVertices + 1];
        }

        /// <summary>
        /// Verifica a existência e encontra um ciclo Euleriano.
        /// </summary>
        /// <returns>O resultado da busca pelo ciclo.</returns>
        public ResultadoCiclo VerificarCicloEuleriano()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoCiclo
            {
                TipoCiclo = "Ciclo Euleriano"
            };

            if (!TemCicloEuleriano())
            {
                resultado.ExisteCiclo = false;
            }
            else
            {
                resultado.Sequencia = EncontrarCicloEuleriano();
                resultado.ExisteCiclo = resultado.Sequencia.Count > 0;
                resultado.NumeroArestasPercorridas = resultado.Sequencia.Count;
                resultado.NumeroVerticesVisitados = resultado.Sequencia.Distinct().Count();
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Verifica a existência e encontra um ciclo Hamiltoniano.
        /// </summary>
        /// <returns>O resultado da busca pelo ciclo.</returns>
        public ResultadoCiclo VerificarCicloHamiltoniano()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoCiclo
            {
                TipoCiclo = "Ciclo Hamiltoniano"
            };

            if (!TemCicloHamiltoniano())
            {
                resultado.ExisteCiclo = false;
            }
            else
            {
                resultado.Sequencia = EncontrarCicloHamiltoniano(1);
                resultado.ExisteCiclo = resultado.Sequencia.Count > 0;
                resultado.NumeroVerticesVisitados = resultado.Sequencia.Count;
                resultado.NumeroArestasPercorridas = resultado.Sequencia.Count;
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Verifica se o grafo possui as condições para um ciclo Euleriano.
        /// </summary>
        /// <returns>True se as condições são satisfeitas, false caso contrário.</returns>
        private bool TemCicloEuleriano()
        {
            int n = grafo.NumVertices;
            for (int i = 1; i <= n; i++)
            {
                grauEntrada[i] = grafo.ObterGrauEntrada(i);
                grauSaida[i] = grafo.ObterGrauSaida(i);
                if (grauEntrada[i] != grauSaida[i])
                {
                    return false;
                }
            }

            // Checa conectividade forte (usando BFS ida e volta)
            bool ConexoDirecionado()
            {
                var visited = new bool[n + 1];
                var fila = new Queue<int>();
                fila.Enqueue(1);
                visited[1] = true;
                while (fila.Count > 0)
                {
                    int u = fila.Dequeue();
                    foreach (var a in grafo.ObterVizinhos(u))
                    {
                        if (!visited[a.Destino])
                        {
                            visited[a.Destino] = true;
                            fila.Enqueue(a.Destino);
                        }
                    }
                }

                for (int i = 1; i <= n; i++)
                {
                    if (!visited[i] && grafo.ObterGrauVertice(i) > 0)
                    {
                        return false;
                    }
                }

                // Grafo transposto
                Array.Fill(visited, false);
                fila.Enqueue(1);
                visited[1] = true;
                while (fila.Count > 0)
                {
                    int u = fila.Dequeue();
                    for (int v = 1; v <= n; v++)
                    {
                        if (grafo.ExisteAresta(v, u) && !visited[v])
                        {
                            visited[v] = true;
                            fila.Enqueue(v);
                        }
                    }
                }

                for (int i = 1; i <= n; i++)
                {
                    if (!visited[i] && grafo.ObterGrauVertice(i) > 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            return ConexoDirecionado();
        }

        /// <summary>
        /// Encontra o caminho de um ciclo Euleriano usando o algoritmo de Hierholzer.
        /// </summary>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> EncontrarCicloEuleriano()
        {
            // Algoritmo de Hierholzer
            return Hierholzer(1);
        }

        /// <summary>
        /// Implementação do algoritmo de Hierholzer.
        /// </summary>
        /// <param name="inicio">O vértice inicial.</param>
        /// <returns>A lista de vértices no ciclo.</returns>
        private List<int> Hierholzer(int inicio)
        {
            var adj = new Dictionary<int, Stack<int>>();
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                adj[i] = new Stack<int>(grafo.ObterVizinhos(i).Select(a => a.Destino));
            }

            var caminhoAtual = new Stack<int>();
            var circuito = new List<int>();
            int v = inicio;
            caminhoAtual.Push(v);

            while (caminhoAtual.Count > 0)
            {
                if (adj[v].Count > 0)
                {
                    caminhoAtual.Push(v);
                    v = adj[v].Pop();
                }
                else
                {
                    circuito.Add(v);
                    v = caminhoAtual.Pop();
                }
            }

            circuito.Reverse();
            return circuito;
        }

        /// <summary>
        /// Verifica se o grafo possui as condições para um ciclo Hamiltoniano (heurística).
        /// </summary>
        /// <returns>True se as condições são satisfeitas, false caso contrário.</returns>
        private bool TemCicloHamiltoniano()
        {
            // Utiliza condição suficiente de Dirac para heurística; se não satisfaz, ainda tentamos backtracking
            int n = grafo.NumVertices;
            if (n <= 2)
            {
                return true;
            }

            for (int i = 1; i <= n; i++)
            {
                if (grafo.ObterGrauVertice(i) < n / 2)
                {
                    // Não satisfaz Dirac, mas continuamos; heurística
                    return true;
                }
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
            var caminho = Enumerable.Repeat(-1, grafo.NumVertices).ToList();
            caminho[0] = inicio;
            if (BacktrackHamiltoniano(caminho, 1))
            {
                caminho.Add(inicio); // fecha o ciclo
                return caminho;
            }

            return new List<int>();
        }

        /// <summary>
        /// Função de backtracking para encontrar o ciclo Hamiltoniano.
        /// </summary>
        /// <param name="caminho">O caminho atual.</param>
        /// <param name="pos">A posição atual no caminho.</param>
        /// <returns>True se um ciclo foi encontrado, false caso contrário.</returns>
        private bool BacktrackHamiltoniano(List<int> caminho, int pos)
        {
            int n = grafo.NumVertices;
            if (pos == n)
            {
                return grafo.ExisteAresta(caminho[pos - 1], caminho[0]);
            }

            for (int v = 2; v <= n; v++)
            {
                if (grafo.ExisteAresta(caminho[pos - 1], v) && !caminho.Contains(v))
                {
                    caminho[pos] = v;
                    if (BacktrackHamiltoniano(caminho, pos + 1))
                    {
                        return true;
                    }
                    caminho[pos] = -1;
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
            return visitado.Skip(1).All(v => v);
        }
    }
}
