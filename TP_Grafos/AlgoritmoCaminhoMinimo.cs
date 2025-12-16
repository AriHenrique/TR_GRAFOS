using System;
using System.Collections.Generic;
using System.Linq;

namespace TP_Grafos
{
    public class AlgoritmoCaminhoMinimo
    {
        private Grafo grafo;
        private double[] distancias;
        private int[] predecessores;
        private bool[] visitados;

        public AlgoritmoCaminhoMinimo(Grafo grafo)
        {
            this.grafo = grafo;
        }

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

        public ResultadoCaminho Dijkstra(int origem, int destino)
        {
            InicializarEstruturas(origem);
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var pq = new SortedSet<(double Dist, int Vert)>(Comparer<(double Dist, int Vert)>.Create((a, b) =>
            {
                int res = a.Dist.CompareTo(b.Dist);
                if (res == 0) return a.Vert.CompareTo(b.Vert);
                return res;
            }));

            pq.Add((0, origem));

            while (pq.Count > 0)
            {
                int u = ExtrairMinimo(pq);

                if (visitados[u]) continue;
                visitados[u] = true;

                if (u == destino) break;

                foreach (var aresta in grafo.ObterVizinhos(u))
                {
                    int v = aresta.Destino;
                    if (!visitados[v])
                    {
                        RelaxarAresta(u, v, aresta.Peso, pq);
                    }
                }
            }

            medidor.Parar();

            var resultado = new ResultadoCaminho
            {
                AlgoritmoUsado = "Dijkstra",
                TempoExecucao = medidor.ObterTempoDecorrido(),
                CustoTotal = distancias[destino]
            };

            if (distancias[destino] < double.PositiveInfinity)
            {
                resultado.Caminho = ReconstruirCaminho(origem, destino);
                resultado.CaminhoEncontrado = true;
            }
            else
            {
                resultado.CaminhoEncontrado = false;
            }

            return resultado;
        }

        public ResultadoCaminho BellmanFord(int origem, int destino)
        {
            InicializarEstruturas(origem);
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            int V = grafo.NumVertices;
            var arestas = grafo.ObterTodasArestas();

            for (int i = 1; i < V; i++)
            {
                foreach (var aresta in arestas)
                {
                    RelaxarAresta(aresta.Origem, aresta.Destino, aresta.Peso);
                }
            }

            medidor.Parar();

            var resultado = new ResultadoCaminho
            {
                AlgoritmoUsado = "Bellman-Ford",
                TempoExecucao = medidor.ObterTempoDecorrido(),
                CustoTotal = distancias[destino]
            };

            if (distancias[destino] < double.PositiveInfinity)
            {
                resultado.Caminho = ReconstruirCaminho(origem, destino);
                resultado.CaminhoEncontrado = true;
            }
            else
            {
                resultado.CaminhoEncontrado = false;
            }

            return resultado;
        }

        private void RelaxarAresta(int u, int v, double peso, SortedSet<(double, int)> pq = null)
        {
            if (distancias[u] != double.PositiveInfinity && distancias[u] + peso < distancias[v])
            {
                if (pq != null && distancias[v] != double.PositiveInfinity)
                {
                    pq.Remove((distancias[v], v));
                }

                distancias[v] = distancias[u] + peso;
                predecessores[v] = u;

                if (pq != null)
                {
                    pq.Add((distancias[v], v));
                }
            }
        }

        private int ExtrairMinimo(SortedSet<(double, int)> pq)
        {
            var item = pq.Min;
            pq.Remove(item);
            return item.Item2;
        }

        private List<int> ReconstruirCaminho(int origem, int destino)
        {
            var caminho = new List<int>();
            int atual = destino;

            while (atual != -1)
            {
                caminho.Add(atual);
                if (atual == origem) break;
                atual = predecessores[atual];
            }

            caminho.Reverse();
            if (caminho.Count > 0 && caminho[0] == origem)
                return caminho;
            return new List<int>();
        }
    }
}