using System;

namespace TP_Grafos
{
    /// <summary>
    /// Representa uma aresta em um grafo, com origem, destino, peso e capacidade.
    /// </summary>
    public class Aresta : IComparable<Aresta>
    {
        /// <summary>
        /// Vértice de origem da aresta.
        /// </summary>
        public int Origem { get; }

        /// <summary>
        /// Vértice de destino da aresta.
        /// </summary>
        public int Destino { get; }

        /// <summary>
        /// Peso associado à aresta.
        /// </summary>
        public double Peso { get; }

        /// <summary>
        /// Capacidade da aresta (para algoritmos de fluxo).
        /// </summary>
        public double Capacidade { get; }

        /// <summary>
        /// Construtor da classe Aresta.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <param name="capacidade">Capacidade da aresta.</param>
        public Aresta(int origem, int destino, double peso, double capacidade)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
            Capacidade = capacidade;
        }

        /// <summary>
        /// Compara esta aresta com outra com base no peso.
        /// </summary>
        /// <param name="outra">A outra aresta a ser comparada.</param>
        /// <returns>Um valor que indica a ordem relativa das arestas.</returns>
        public int CompareTo(Aresta? outra)
        {
            if (outra is null) return 1;
            return Peso.CompareTo(outra.Peso);
        }

        /// <summary>
        /// Verifica se esta aresta é igual a outro objeto.
        /// </summary>
        /// <param name="obj">O objeto a ser comparado.</param>
        /// <returns>True se os objetos são iguais, false caso contrário.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Aresta outra)
            {
                return false;
            }

            return Origem == outra.Origem &&
                   Destino == outra.Destino &&
                   Peso.Equals(outra.Peso) &&
                   Capacidade.Equals(outra.Capacidade);
        }

        /// <summary>
        /// Retorna o código de hash para esta aresta.
        /// </summary>
        /// <returns>O código de hash.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Origem, Destino, Peso, Capacidade);
        }

        /// <summary>
        /// Retorna uma representação em string da aresta.
        /// </summary>
        /// <returns>A string que representa a aresta.</returns>
        public override string ToString()
        {
            return $"{Origem} -> {Destino} | peso={Peso} | capacidade={Capacidade}";
        }
    }
}
