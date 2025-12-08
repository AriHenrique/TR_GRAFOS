using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TP_Grafos
{
    public class Grafo
    {
        private Dictionary<int, List<Aresta>> listaAdjacencia;
        private double[,] matrizAdjacencia;
        private double[,] matrizCapacidade;
        private int numVertices;
        private int numArestas;
        private bool usarMatriz;
        private double densidade;

        public int NumVertices => numVertices;
        public int NumArestas => numArestas;

        public Grafo(string arquivoDIMACS)
        {
            listaAdjacencia = new Dictionary<int, List<Aresta>>();
            numArestas = 0;
            CarregarDIMACS(arquivoDIMACS);
        }

        public Grafo(int vertices)
        {
            numVertices = vertices;
            numArestas = 0;
            listaAdjacencia = new Dictionary<int, List<Aresta>>();
            for (int i = 1; i <= numVertices; i++)
            {
                listaAdjacencia[i] = new List<Aresta>();
            }
            DefinirEstrutura();
        }

        public void CarregarDIMACS(string arquivo)
        {
            if (!File.Exists(arquivo))
                throw new FileNotFoundException("Arquivo DIMACS não encontrado.", arquivo);

            var linhas = File.ReadAllLines(arquivo);
            bool primeiraLinha = true;
            
            foreach (var linha in linhas)
            {
                var partes = linha.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 0) continue;

                if (primeiraLinha && partes.Length >= 2)
                {
                    numVertices = int.Parse(partes[0]);
                    for (int i = 1; i <= numVertices; i++)
                        listaAdjacencia[i] = new List<Aresta>();
                    primeiraLinha = false;
                }
                else if (!primeiraLinha)
                {
                    if (partes.Length >= 3)
                    {
                        int u = int.Parse(partes[0]);
                        int v = int.Parse(partes[1]);
                        double w = double.Parse(partes[2]);
                        double cap = partes.Length >= 4 ? double.Parse(partes[3]) : w;
                        
                        AdicionarAresta(u, v, w, cap);
                    }
                }
            }
            CalcularDensidade();
            DefinirEstrutura();
        }

        private void CalcularDensidade()
        {
            if (numVertices <= 1)
            {
                densidade = 0;
                return;
            }
            double maxArestas = (double)numVertices * (numVertices - 1);
            densidade = numArestas / maxArestas;
        }

        private void DefinirEstrutura()
        {
            usarMatriz = densidade > 0.7 && numVertices < 5000;

            if (usarMatriz && matrizAdjacencia == null)
            {
                matrizAdjacencia = new double[numVertices + 1, numVertices + 1];
                matrizCapacidade = new double[numVertices + 1, numVertices + 1];

                foreach (var kvp in listaAdjacencia)
                {
                    foreach (var aresta in kvp.Value)
                    {
                        matrizAdjacencia[aresta.Origem, aresta.Destino] = aresta.Peso;
                        matrizCapacidade[aresta.Origem, aresta.Destino] = aresta.Capacidade;
                    }
                }
            }
        }

        public void AdicionarAresta(int origem, int destino, double peso, double capacidade)
        {
            if (!listaAdjacencia.ContainsKey(origem))
                listaAdjacencia[origem] = new List<Aresta>();

            var novaAresta = new Aresta(origem, destino, peso, capacidade);
            listaAdjacencia[origem].Add(novaAresta);
            numArestas++;

            if (usarMatriz && matrizAdjacencia != null)
            {
                matrizAdjacencia[origem, destino] = peso;
                matrizCapacidade[origem, destino] = capacidade;
            }
        }

        public void RemoverAresta(int origem, int destino)
        {
            if (listaAdjacencia.ContainsKey(origem))
            {
                var aresta = listaAdjacencia[origem].FirstOrDefault(a => a.Destino == destino);
                if (aresta != null)
                {
                    listaAdjacencia[origem].Remove(aresta);
                    numArestas--;
                }
            }

            if (usarMatriz && matrizAdjacencia != null)
            {
                matrizAdjacencia[origem, destino] = 0; // Ou infinito/null dependendo da lógica
                matrizCapacidade[origem, destino] = 0;
            }
        }

        public List<Aresta> ObterVizinhos(int vertice)
        {
            if (listaAdjacencia.ContainsKey(vertice))
                return listaAdjacencia[vertice];
            return new List<Aresta>();
        }

        public List<Aresta> ObterTodasArestas()
        {
            var todas = new List<Aresta>();
            foreach (var lista in listaAdjacencia.Values)
            {
                todas.AddRange(lista);
            }
            return todas;
        }

        public double ObterPeso(int origem, int destino)
        {
            if (usarMatriz && matrizAdjacencia != null)
            {
                return matrizAdjacencia[origem, destino];
            }
            
            if (listaAdjacencia.ContainsKey(origem))
            {
                var aresta = listaAdjacencia[origem].FirstOrDefault(a => a.Destino == destino);
                if (aresta != null) return aresta.Peso;
            }
            return double.PositiveInfinity; // Indica sem aresta
        }

        public double ObterCapacidade(int origem, int destino)
        {
            if (usarMatriz && matrizCapacidade != null)
            {
                return matrizCapacidade[origem, destino];
            }

            if (listaAdjacencia.ContainsKey(origem))
            {
                var aresta = listaAdjacencia[origem].FirstOrDefault(a => a.Destino == destino);
                if (aresta != null) return aresta.Capacidade;
            }
            return 0;
        }

        public bool ExisteAresta(int origem, int destino)
        {
            if (usarMatriz && matrizAdjacencia != null)
                return matrizAdjacencia[origem, destino] != 0;

            if (listaAdjacencia.ContainsKey(origem))
                return listaAdjacencia[origem].Any(a => a.Destino == destino);

            return false;
        }

        public int ObterGrauVertice(int vertice)
        {
            return ObterGrauEntrada(vertice) + ObterGrauSaida(vertice);
        }

        public int ObterGrauEntrada(int vertice)
        {
            int grau = 0;
            foreach (var u in listaAdjacencia.Keys)
            {
                if (listaAdjacencia[u].Any(a => a.Destino == vertice))
                    grau++;
            }
            return grau;
        }

        public int ObterGrauSaida(int vertice)
        {
            if (listaAdjacencia.ContainsKey(vertice))
                return listaAdjacencia[vertice].Count;
            return 0;
        }

        public bool EhConexo()
        {
            if (numVertices == 0) return true;
            
            var visitados = new HashSet<int>();
            var fila = new Queue<int>();
            
            // Começa do primeiro vértice disponível
            int inicio = listaAdjacencia.Keys.FirstOrDefault();
            if (inicio == 0) return false; // Grafo vazio ou inválido

            fila.Enqueue(inicio);
            visitados.Add(inicio);

            while (fila.Count > 0)
            {
                var u = fila.Dequeue();
                foreach (var aresta in ObterVizinhos(u))
                {
                    // Considerando conexidade fraca (grafo não direcionado para fins de travessia)
                    // Se for fortemente conexo, a lógica seria mais complexa (Kosaraju/Tarjan)
                    // Mas para grafo geral, BFS simples:
                    if (!visitados.Contains(aresta.Destino))
                    {
                        visitados.Add(aresta.Destino);
                        fila.Enqueue(aresta.Destino);
                    }
                }
            }

            // Nota: Para grafos direcionados, isso testa apenas acessibilidade a partir da raiz.
            // Para verificar se é "fortemente conexo", seriam necessárias duas passadas.
            // Assumindo verificação simples de componentes conexos para grafo não direcionado ou fracamente conexo.
            return visitados.Count == numVertices;
        }

        /// <summary>
        /// Obtém o grau de um vértice (grau de entrada + grau de saída).
        /// </summary>
        /// <param name="vertice">O vértice (índice baseado em 1).</param>
        /// <returns>O grau do vértice.</returns>
        public int Grau(int vertice)
        {
            return ObterGrauVertice(vertice);
        }

        /// <summary>
        /// Obtém a lista de vértices adjacentes a um vértice.
        /// </summary>
        /// <param name="vertice">O vértice (índice baseado em 1).</param>
        /// <returns>Lista de índices dos vértices adjacentes.</returns>
        public List<int> Adjacentes(int vertice)
        {
            var adjacentes = new List<int>();
            foreach (var aresta in ObterVizinhos(vertice))
            {
                adjacentes.Add(aresta.Destino);
            }
            return adjacentes;
        }

        public override string ToString()
        {
            return $"Grafo: V={numVertices}, A={numArestas}, Densidade={densidade:F2}";
        }
    }
}