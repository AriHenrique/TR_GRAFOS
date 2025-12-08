using System;

namespace TP_Grafos
{
    /// <summary>
    /// Representa uma aresta em um grafo, com origem, destino, peso e capacidade.
    /// </summary>
    public class Aresta : IComparable<Aresta>
    {
        public int Origem { get; }
        public int Destino { get; }
        public double Peso { get; }
        public double Capacidade { get; }

        public Aresta(int origem, int destino, double peso, double capacidade)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
            Capacidade = capacidade;
        }

        public int CompareTo(Aresta outra)
        {
            if (outra == null) return 1;
            return Peso.CompareTo(outra.Peso);
        }

        public override bool Equals(object obj)
        {
            if (obj is Aresta outra)
            {
                return Origem == outra.Origem &&
                       Destino == outra.Destino &&
                       Math.Abs(Peso - outra.Peso) < 0.0001 &&
                       Math.Abs(Capacidade - outra.Capacidade) < 0.0001;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origem, Destino, Peso, Capacidade);
        }

        public override string ToString()
        {
            return $"({Origem} -> {Destino} | P:{Peso} | C:{Capacidade})";
        }
    }
}