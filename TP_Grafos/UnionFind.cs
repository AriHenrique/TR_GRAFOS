namespace TP_Grafos
{
    /// <summary>
    /// Implementa a estrutura de dados Union-Find (ou Disjoint Set Union).
    /// </summary>
    public class UnionFind
    {
        /// <summary>
        /// Array que armazena o pai de cada elemento.
        /// </summary>
        private int[] pai;

        /// <summary>
        /// Array que armazena o rank (altura aproximada da árvore) de cada conjunto.
        /// </summary>
        private int[] rank;

        /// <summary>
        /// Número de componentes (conjuntos disjuntos).
        /// </summary>
        private int numComponentes;

        /// <summary>
        /// Construtor que inicializa a estrutura para n elementos.
        /// </summary>
        /// <param name="n">O número de elementos.</param>
        public UnionFind(int n)
        {
            pai = new int[n + 1];
            rank = new int[n + 1];
            numComponentes = n;

            for (int i = 1; i <= n; i++)
            {
                pai[i] = i;
                rank[i] = 0;
            }
        }

        /// <summary>
        /// Encontra o representante (raiz) do conjunto ao qual x pertence.
        /// </summary>
        /// <param name="x">O elemento.</param>
        /// <returns>O representante do conjunto.</returns>
        public int Find(int x)
        {
            if (pai[x] != x)
            {
                // Path compression
                pai[x] = Find(pai[x]);
            }
            return pai[x];
        }

        /// <summary>
        /// Une os conjuntos que contêm os elementos x e y.
        /// </summary>
        /// <param name="x">Um elemento do primeiro conjunto.</param>
        /// <param name="y">Um elemento do segundo conjunto.</param>
        public void Union(int x, int y)
        {
            int raizX = Find(x);
            int raizY = Find(y);

            if (raizX == raizY)
                return; // Já estão no mesmo conjunto

            // Union by rank
            if (rank[raizX] < rank[raizY])
            {
                pai[raizX] = raizY;
            }
            else if (rank[raizX] > rank[raizY])
            {
                pai[raizY] = raizX;
            }
            else
            {
                pai[raizY] = raizX;
                rank[raizX]++;
            }

            numComponentes--;
        }

        /// <summary>
        /// Verifica se dois elementos pertencem ao mesmo conjunto.
        /// </summary>
        /// <param name="x">O primeiro elemento.</param>
        /// <param name="y">O segundo elemento.</param>
        /// <returns>True se estiverem no mesmo conjunto, false caso contrário.</returns>
        public bool MesmoConjunto(int x, int y)
        {
            return Find(x) == Find(y);
        }

        /// <summary>
        /// Obtém o número atual de componentes (conjuntos disjuntos).
        /// </summary>
        /// <returns>O número de componentes.</returns>
        public int ObterNumComponentes()
        {
            return numComponentes;
        }

        /// <summary>
        /// Reseta a estrutura para seu estado inicial.
        /// </summary>
        public void Reset()
        {
            int n = pai.Length - 1;
            numComponentes = n;
            for (int i = 1; i <= n; i++)
            {
                pai[i] = i;
                rank[i] = 0;
            }
        }
    }
}
