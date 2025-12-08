using System;
using System.Collections.Generic;
using System.IO;

namespace TP_Grafos
{
    /// <summary>
    /// Representa um grafo, que pode ser direcionado ou não, com pesos e capacidades nas arestas.
    /// </summary>
    public class Grafo
    {
        /// <summary>
        /// Lista de adjacência para representar o grafo.
        /// </summary>
        private Dictionary<int, List<Aresta>> listaAdjacencia = new();

        /// <summary>
        /// Matriz de adjacência para representar os pesos das arestas.
        /// </summary>
        private double[,] matrizAdjacencia = new double[0, 0];

        /// <summary>
        /// Matriz de capacidade para algoritmos de fluxo máximo.
        /// </summary>
        private double[,] matrizCapacidade = new double[0, 0];

        /// <summary>
        /// Número de vértices no grafo.
        /// </summary>
        private int numVertices;

        /// <summary>
        /// Número de arestas no grafo.
        /// </summary>
        private int numArestas;

        /// <summary>
        /// Indica se a estrutura de matriz deve ser usada.
        /// </summary>
        private bool usarMatriz;

        /// <summary>
        /// Densidade do grafo.
        /// </summary>
        private double densidade;

        /// <summary>
        /// Construtor que inicializa um grafo a partir de um arquivo no formato DIMACS.
        /// </summary>
        /// <param name="arquivoDIMACS">Caminho do arquivo DIMACS.</param>
        public Grafo(string arquivoDIMACS)
        {
            CarregarDIMACS(arquivoDIMACS);
        }

        /// <summary>
        /// Construtor que inicializa um grafo com um número específico de vértices.
        /// </summary>
        /// <param name="vertices">Número de vértices.</param>
        public Grafo(int vertices)
        {
            numVertices = vertices;
            numArestas = 0;
            listaAdjacencia = new Dictionary<int, List<Aresta>>();
            matrizAdjacencia = new double[vertices + 1, vertices + 1];
            matrizCapacidade = new double[vertices + 1, vertices + 1];
            densidade = 0;
            usarMatriz = false;
        }

        /// <summary>
        /// Carrega um grafo a partir de um arquivo no formato DIMACS.
        /// </summary>
        /// <param name="arquivo">Caminho do arquivo DIMACS.</param>
        public void CarregarDIMACS(string arquivo)
        {
            if (!File.Exists(arquivo))
            {
                throw new FileNotFoundException("Arquivo DIMACS não encontrado", arquivo);
            }

            var linhas = File.ReadAllLines(arquivo);
            if (linhas.Length == 0)
            {
                throw new InvalidDataException("Arquivo DIMACS vazio.");
            }

            var primeira = linhas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (primeira.Length < 2)
            {
                throw new InvalidDataException("Primeira linha deve conter número de vértices e arestas.");
            }

            numVertices = int.Parse(primeira[0]);
            numArestas = int.Parse(primeira[1]);

            listaAdjacencia = new Dictionary<int, List<Aresta>>();
            matrizAdjacencia = new double[numVertices + 1, numVertices + 1];
            matrizCapacidade = new double[numVertices + 1, numVertices + 1];

            // Inicializa listas
            for (int i = 1; i <= numVertices; i++)
            {
                listaAdjacencia[i] = new List<Aresta>();
                for (int j = 1; j <= numVertices; j++)
                {
                    matrizAdjacencia[i, j] = double.PositiveInfinity;
                    matrizCapacidade[i, j] = 0;
                }
            }

            for (int i = 1; i < linhas.Length; i++)
            {
                var partes = linhas[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length < 4)
                {
                    continue;
                }

                int origem = int.Parse(partes[0]);
                int destino = int.Parse(partes[1]);
                double peso = double.Parse(partes[2]);
                double capacidade = double.Parse(partes[3]);

                AdicionarAresta(origem, destino, peso, capacidade);
            }

            CalcularDensidade();
            DefinirEstrutura();
        }

        /// <summary>
        /// Calcula a densidade do grafo.
        /// </summary>
        private void CalcularDensidade()
        {
            if (numVertices <= 1)
            {
                densidade = 0;
                return;
            }

            // Grafo direcionado: máximo de V*(V-1) arestas
            double maxArestas = numVertices * (numVertices - 1);
            densidade = numArestas / maxArestas;
        }

        /// <summary>
        /// Define a estrutura de dados a ser usada (lista ou matriz) com base na densidade.
        /// </summary>
        private void DefinirEstrutura()
        {
            // Usa matriz para grafos mais densos
            usarMatriz = densidade >= 0.5;
        }

        /// <summary>
        /// Adiciona uma aresta ao grafo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <param name="capacidade">Capacidade da aresta.</param>
        public void AdicionarAresta(int origem, int destino, double peso, double capacidade)
        {
            var aresta = new Aresta(origem, destino, peso, capacidade);

            if (!listaAdjacencia.ContainsKey(origem))
            {
                listaAdjacencia[origem] = new List<Aresta>();
            }

            listaAdjacencia[origem].Add(aresta);
            matrizAdjacencia[origem, destino] = peso;
            matrizCapacidade[origem, destino] = capacidade;
            numArestas++;
        }

        /// <summary>
        /// Remove uma aresta do grafo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        public void RemoverAresta(int origem, int destino)
        {
            if (listaAdjacencia.TryGetValue(origem, out var vizinhos))
            {
                vizinhos.RemoveAll(a => a.Destino == destino);
            }

            matrizAdjacencia[origem, destino] = double.PositiveInfinity;
            matrizCapacidade[origem, destino] = 0;
        }

        /// <summary>
        /// Obtém a lista de vizinhos de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>Lista de arestas adjacentes.</returns>
        public List<Aresta> ObterVizinhos(int vertice)
        {
            return listaAdjacencia.TryGetValue(vertice, out var vizinhos)
                ? vizinhos
                : new List<Aresta>();
        }

        /// <summary>
        /// Obtém todas as arestas do grafo.
        /// </summary>
        /// <returns>Lista com todas as arestas.</returns>
        public List<Aresta> ObterTodasArestas()
        {
            var resultado = new List<Aresta>();
            foreach (var (_, arestas) in listaAdjacencia)
            {
                resultado.AddRange(arestas);
            }

            return resultado;
        }

        /// <summary>
        /// Obtém o peso de uma aresta.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O peso da aresta.</returns>
        public double ObterPeso(int origem, int destino)
        {
            return matrizAdjacencia[origem, destino];
        }

        /// <summary>
        /// Obtém a capacidade de uma aresta.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>A capacidade da aresta.</returns>
        public double ObterCapacidade(int origem, int destino)
        {
            return matrizCapacidade[origem, destino];
        }

        /// <summary>
        /// Verifica se existe uma aresta entre dois vértices.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>True se a aresta existe, false caso contrário.</returns>
        public bool ExisteAresta(int origem, int destino)
        {
            return !double.IsPositiveInfinity(matrizAdjacencia[origem, destino]);
        }

        /// <summary>
        /// Obtém o grau de um vértice (entrada + saída).
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau do vértice.</returns>
        public int ObterGrauVertice(int vertice)
        {
            return ObterGrauEntrada(vertice) + ObterGrauSaida(vertice);
        }

        /// <summary>
        /// Obtém o grau de entrada de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de entrada.</returns>
        public int ObterGrauEntrada(int vertice)
        {
            int grau = 0;
            foreach (var (_, vizinhos) in listaAdjacencia)
            {
                foreach (var a in vizinhos)
                {
                    if (a.Destino == vertice)
                    {
                        grau++;
                    }
                }
            }

            return grau;
        }

        /// <summary>
        /// Obtém o grau de saída de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de saída.</returns>
        public int ObterGrauSaida(int vertice)
        {
            return listaAdjacencia.TryGetValue(vertice, out var vizinhos) ? vizinhos.Count : 0;
        }

        /// <summary>
        /// Verifica se o grafo é conexo.
        /// </summary>
        /// <returns>True se for conexo, false caso contrário.</returns>
        public bool EhConexo()
        {
            if (numVertices == 0)
            {
                return true;
            }

            var visitados = new bool[numVertices + 1];
            var stack = new Stack<int>();

            stack.Push(1);
            visitados[1] = true;

            while (stack.Count > 0)
            {
                var v = stack.Pop();
                foreach (var aresta in ObterVizinhos(v))
                {
                    if (!visitados[aresta.Destino])
                    {
                        visitados[aresta.Destino] = true;
                        stack.Push(aresta.Destino);
                    }
                }

                // também considera arestas de entrada para conexidade não direcionada
                for (int u = 1; u <= numVertices; u++)
                {
                    if (ExisteAresta(u, v) && !visitados[u])
                    {
                        visitados[u] = true;
                        stack.Push(u);
                    }
                }
            }

            for (int i = 1; i <= numVertices; i++)
            {
                if (!visitados[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Retorna uma representação do grafo em string.
        /// </summary>
        /// <returns>String representando o grafo.</returns>
        public override string ToString()
        {
            var sw = new StringWriter();
            sw.WriteLine($"Vértices: {numVertices}, Arestas: {numArestas}, Densidade: {densidade:F2}, Estrutura: {(usarMatriz ? "matriz" : "lista")}");

            foreach (var (origem, vizinhos) in listaAdjacencia)
            {
                foreach (var a in vizinhos)
                {
                    sw.WriteLine(a.ToString());
                }
            }

            return sw.ToString();
        }

        /// <summary>
        /// Retorna o número de vértices.
        /// </summary>
        public int NumVertices => numVertices;

        /// <summary>
        /// Retorna o número de arestas.
        /// </summary>
        public int NumArestas => numArestas;

        /// <summary>
        /// Retorna a matriz de capacidade (cópia).
        /// </summary>
        public double[,] ObterMatrizCapacidade()
        {
            var copia = new double[numVertices + 1, numVertices + 1];
            Array.Copy(matrizCapacidade, copia, matrizCapacidade.Length);
            return copia;
        }
    }
}
