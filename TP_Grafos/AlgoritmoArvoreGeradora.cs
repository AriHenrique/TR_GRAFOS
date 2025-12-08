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
            this.grafo = grafo;
            unionFind = new UnionFind(grafo.NumVertices);
            visitado = new bool[grafo.NumVertices + 1];
            arestasOrdenadas = new List<Aresta>();
        }

        /// <summary>
        /// Executa o algoritmo de Kruskal para encontrar a AGM.
        /// </summary>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Kruskal()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            OrdenarArestasPorPeso();
            var resultado = new ResultadoArvore
            {
                AlgoritmoUsado = "Kruskal",
                ArvoreEncontrada = true
            };

            foreach (var aresta in arestasOrdenadas)
            {
                if (!unionFind.MesmoConjunto(aresta.Origem, aresta.Destino))
                {
                    unionFind.Union(aresta.Origem, aresta.Destino);
                    resultado.AdicionarAresta(aresta);
                    if (resultado.Arestas.Count == grafo.NumVertices - 1)
                    {
                        break;
                    }
                }
            }

            resultado.ArvoreEncontrada = resultado.Arestas.Count == grafo.NumVertices - 1;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de Prim para encontrar a AGM.
        /// </summary>
        /// <param name="verticeInicial">O vértice inicial.</param>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Prim(int verticeInicial)
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoArvore
            {
                AlgoritmoUsado = "Prim"
            };

            var comparador = Comparer<(double peso, int origem, int destino)>.Create((a, b) =>
            {
                int comp = a.peso.CompareTo(b.peso);
                if (comp != 0) return comp;
                comp = a.destino.CompareTo(b.destino);
                if (comp != 0) return comp;
                return a.origem.CompareTo(b.origem);
            });

            var fila = new SortedSet<(double, int, int)>(comparador);

            visitado = new bool[grafo.NumVertices + 1];
            visitado[verticeInicial] = true;

            foreach (var a in grafo.ObterVizinhos(verticeInicial))
            {
                fila.Add((a.Peso, verticeInicial, a.Destino));
            }

            while (fila.Count > 0 && resultado.Arestas.Count < grafo.NumVertices - 1)
            {
                var (peso, origem, destino) = fila.Min;
                fila.Remove(fila.Min);

                if (visitado[destino])
                {
                    continue;
                }

                visitado[destino] = true;
                resultado.AdicionarAresta(new Aresta(origem, destino, peso, grafo.ObterCapacidade(origem, destino)));

                foreach (var a in grafo.ObterVizinhos(destino))
                {
                    if (!visitado[a.Destino])
                    {
                        fila.Add((a.Peso, destino, a.Destino));
                    }
                }
            }

            resultado.ArvoreEncontrada = resultado.Arestas.Count == grafo.NumVertices - 1;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Executa o algoritmo de Boruvka para encontrar a AGM.
        /// </summary>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Boruvka()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoArvore
            {
                AlgoritmoUsado = "Boruvka"
            };

            unionFind.Reset();

            while (resultado.Arestas.Count < grafo.NumVertices - 1)
            {
                var melhor = new Aresta[grafo.NumVertices + 1];

                foreach (var aresta in grafo.ObterTodasArestas())
                {
                    int c1 = unionFind.Find(aresta.Origem);
                    int c2 = unionFind.Find(aresta.Destino);

                    if (c1 == c2) continue;

                    if (melhor[c1] == null || aresta.Peso < melhor[c1].Peso)
                    {
                        melhor[c1] = aresta;
                    }

                    if (melhor[c2] == null || aresta.Peso < melhor[c2].Peso)
                    {
                        melhor[c2] = aresta;
                    }
                }

                bool adicionou = false;
                for (int i = 1; i <= grafo.NumVertices; i++)
                {
                    var aresta = melhor[i];
                    if (aresta == null) continue;

                    int c1 = unionFind.Find(aresta.Origem);
                    int c2 = unionFind.Find(aresta.Destino);
                    if (c1 == c2) continue;

                    resultado.AdicionarAresta(aresta);
                    unionFind.Union(c1, c2);
                    adicionou = true;
                }

                if (!adicionou) break;
            }

            resultado.ArvoreEncontrada = resultado.Arestas.Count == grafo.NumVertices - 1;
            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            return resultado;
        }

        /// <summary>
        /// Ordena as arestas do grafo por peso.
        /// </summary>
        private void OrdenarArestasPorPeso()
        {
            arestasOrdenadas = grafo.ObterTodasArestas();
            arestasOrdenadas.Sort();
        }

        /// <summary>
        /// Obtém a aresta de peso mínimo de um componente.
        /// </summary>
        /// <param name="componente">O identificador do componente.</param>
        /// <returns>A aresta de peso mínimo.</returns>
        private Aresta? ObterArestaMinimaComponente(int componente)
        {
            Aresta? melhor = null;
            foreach (var aresta in grafo.ObterTodasArestas())
            {
                int c1 = unionFind.Find(aresta.Origem);
                int c2 = unionFind.Find(aresta.Destino);
                if (c1 == c2) continue;
                if (c1 == componente || c2 == componente)
                {
                    if (melhor == null || aresta.Peso < melhor.Peso)
                    {
                        melhor = aresta;
                    }
                }
            }

            return melhor;
        }

        /// <summary>
        /// Verifica se o grafo é conexo.
        /// </summary>
        /// <returns>True se o grafo for conexo, false caso contrário.</returns>
        private bool VerificarConectividade()
        {
            return grafo.EhConexo();
        }
    }
}
