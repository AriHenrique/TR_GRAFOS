using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoArvoreGeradora(Grafo grafo)
        {
            this.grafo = grafo;
        }

        /// <summary>
        /// Executa o algoritmo de Kruskal para encontrar a AGM.
        /// </summary>
        /// <returns>O resultado da árvore geradora.</returns>
        public ResultadoArvore Kruskal()
        {
            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = new ResultadoArvore
            {
                AlgoritmoUsado = "Kruskal",
                Arestas = new List<Aresta>(),
                ArvoreEncontrada = false,
                CustoTotal = 0
            };

            if (!VerificarConectividade())
            {
                medidor.Parar();
                resultado.TempoExecucao = medidor.ObterTempoDecorrido();
                return resultado;
            }

            unionFind = new UnionFind(grafo.NumVertices);
            var todasArestas = grafo.ObterTodasArestas();
            todasArestas.Sort();

            int arestasAdicionadas = 0;
            int verticesNecessarios = grafo.NumVertices - 1;

            foreach (var aresta in todasArestas)
            {
                if (!unionFind.MesmoConjunto(aresta.Origem, aresta.Destino))
                {
                    unionFind.Union(aresta.Origem, aresta.Destino);
                    resultado.Arestas.Add(aresta);
                    resultado.CustoTotal += aresta.Peso;
                    arestasAdicionadas++;

                    if (arestasAdicionadas == verticesNecessarios)
                        break;
                }
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            resultado.ArvoreEncontrada = arestasAdicionadas == verticesNecessarios;

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
                AlgoritmoUsado = "Prim",
                Arestas = new List<Aresta>(),
                ArvoreEncontrada = false,
                CustoTotal = 0
            };

            if (!VerificarConectividade())
            {
                medidor.Parar();
                resultado.TempoExecucao = medidor.ObterTempoDecorrido();
                return resultado;
            }

            visitado = new bool[grafo.NumVertices + 1];
            var chave = new double[grafo.NumVertices + 1];
            var pai = new int[grafo.NumVertices + 1];

            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                chave[i] = double.PositiveInfinity;
                pai[i] = -1;
                visitado[i] = false;
            }

            chave[verticeInicial] = 0;

            for (int count = 0; count < grafo.NumVertices - 1; count++)
            {
                int u = -1;
                double minChave = double.PositiveInfinity;

                for (int v = 1; v <= grafo.NumVertices; v++)
                {
                    if (!visitado[v] && chave[v] < minChave)
                    {
                        minChave = chave[v];
                        u = v;
                    }
                }

                if (u == -1) break;

                visitado[u] = true;

                if (pai[u] != -1)
                {
                    var aresta = new Aresta(pai[u], u, chave[u], grafo.ObterCapacidade(pai[u], u));
                    resultado.Arestas.Add(aresta);
                    resultado.CustoTotal += chave[u];
                }

                foreach (var aresta in grafo.ObterVizinhos(u))
                {
                    int v = aresta.Destino;
                    if (!visitado[v] && aresta.Peso < chave[v])
                    {
                        pai[v] = u;
                        chave[v] = aresta.Peso;
                    }
                }
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            resultado.ArvoreEncontrada = resultado.Arestas.Count == grafo.NumVertices - 1;

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
                AlgoritmoUsado = "Boruvka",
                Arestas = new List<Aresta>(),
                ArvoreEncontrada = false,
                CustoTotal = 0
            };

            if (!VerificarConectividade())
            {
                medidor.Parar();
                resultado.TempoExecucao = medidor.ObterTempoDecorrido();
                return resultado;
            }

            unionFind = new UnionFind(grafo.NumVertices);
            var todasArestas = grafo.ObterTodasArestas();
            var arestasSelecionadas = new HashSet<Aresta>();

            while (unionFind.ObterNumComponentes() > 1)
            {
                var arestaMinima = new Dictionary<int, Aresta>();

                foreach (var aresta in todasArestas)
                {
                    int raizOrigem = unionFind.Find(aresta.Origem);
                    int raizDestino = unionFind.Find(aresta.Destino);

                    if (raizOrigem != raizDestino)
                    {
                        if (!arestaMinima.ContainsKey(raizOrigem) || 
                            aresta.Peso < arestaMinima[raizOrigem].Peso)
                        {
                            arestaMinima[raizOrigem] = aresta;
                        }

                        if (!arestaMinima.ContainsKey(raizDestino) || 
                            aresta.Peso < arestaMinima[raizDestino].Peso)
                        {
                            arestaMinima[raizDestino] = aresta;
                        }
                    }
                }

                if (arestaMinima.Count == 0)
                    break;

                foreach (var kvp in arestaMinima)
                {
                    var aresta = kvp.Value;
                    int raizOrigem = unionFind.Find(aresta.Origem);
                    int raizDestino = unionFind.Find(aresta.Destino);

                    if (raizOrigem != raizDestino && !arestasSelecionadas.Contains(aresta))
                    {
                        unionFind.Union(aresta.Origem, aresta.Destino);
                        arestasSelecionadas.Add(aresta);
                        resultado.Arestas.Add(aresta);
                        resultado.CustoTotal += aresta.Peso;
                    }
                }
            }

            medidor.Parar();
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();
            resultado.ArvoreEncontrada = resultado.Arestas.Count == grafo.NumVertices - 1;

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
        private Aresta ObterArestaMinimaComponente(int componente)
        {
            Aresta minima = null;
            double pesoMinimo = double.PositiveInfinity;

            foreach (var aresta in grafo.ObterTodasArestas())
            {
                if (unionFind.Find(aresta.Origem) == componente && 
                    unionFind.Find(aresta.Destino) != componente)
                {
                    if (aresta.Peso < pesoMinimo)
                    {
                        pesoMinimo = aresta.Peso;
                        minima = aresta;
                    }
                }
            }

            return minima;
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
