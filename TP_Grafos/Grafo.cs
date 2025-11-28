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
        private Dictionary<int, List<Aresta>> listaAdjacencia;

        /// <summary>
        /// Matriz de adjacência para representar os pesos das arestas.
        /// </summary>
        private double[,] matrizAdjacencia;

        /// <summary>
        /// Matriz de capacidade para algoritmos de fluxo máximo.
        /// </summary>
        private double[,] matrizCapacidade;

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
        }

        /// <summary>
        /// Construtor que inicializa um grafo com um número específico de vértices.
        /// </summary>
        /// <param name="vertices">Número de vértices.</param>
        public Grafo(int vertices)
        {
        }

        /// <summary>
        /// Carrega um grafo a partir de um arquivo no formato DIMACS.
        /// </summary>
        /// <param name="arquivo">Caminho do arquivo DIMACS.</param>
        public void CarregarDIMACS(string arquivo)
        {
        }

        /// <summary>
        /// Calcula a densidade do grafo.
        /// </summary>
        private void CalcularDensidade()
        {
        }

        /// <summary>
        /// Define a estrutura de dados a ser usada (lista ou matriz) com base na densidade.
        /// </summary>
        private void DefinirEstrutura()
        {
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
        }

        /// <summary>
        /// Remove uma aresta do grafo.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        public void RemoverAresta(int origem, int destino)
        {
        }

        /// <summary>
        /// Obtém a lista de vizinhos de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>Lista de arestas adjacentes.</returns>
        public List<Aresta> ObterVizinhos(int vertice)
        {
            return null;
        }

        /// <summary>
        /// Obtém todas as arestas do grafo.
        /// </summary>
        /// <returns>Lista com todas as arestas.</returns>
        public List<Aresta> ObterTodasArestas()
        {
            return null;
        }

        /// <summary>
        /// Obtém o peso de uma aresta.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>O peso da aresta.</returns>
        public double ObterPeso(int origem, int destino)
        {
            return 0;
        }

        /// <summary>
        /// Obtém a capacidade de uma aresta.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>A capacidade da aresta.</returns>
        public double ObterCapacidade(int origem, int destino)
        {
            return 0;
        }

        /// <summary>
        /// Verifica se existe uma aresta entre dois vértices.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>True se a aresta existe, false caso contrário.</returns>
        public bool ExisteAresta(int origem, int destino)
        {
            return false;
        }

        /// <summary>
        /// Obtém o grau de um vértice (entrada + saída).
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau do vértice.</returns>
        public int ObterGrauVertice(int vertice)
        {
            return 0;
        }

        /// <summary>
        /// Obtém o grau de entrada de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de entrada.</returns>
        public int ObterGrauEntrada(int vertice)
        {
            return 0;
        }

        /// <summary>
        /// Obtém o grau de saída de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de saída.</returns>
        public int ObterGrauSaida(int vertice)
        {
            return 0;
        }

        /// <summary>
        /// Verifica se o grafo é conexo.
        /// </summary>
        /// <returns>True se for conexo, false caso contrário.</returns>
        public bool EhConexo()
        {
            return false;
        }

        /// <summary>
        /// Retorna uma representação do grafo em string.
        /// </summary>
        /// <returns>String representando o grafo.</returns>
        public override string ToString()
        {
            return "";
        }
    }
}
