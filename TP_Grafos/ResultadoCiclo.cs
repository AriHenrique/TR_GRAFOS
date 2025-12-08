using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Armazena o resultado da execução de um algoritmo de busca de ciclo.
    /// </summary>
    public class ResultadoCiclo
    {
        /// <summary>
        /// Indica se um ciclo foi encontrado.
        /// </summary>
        public bool ExisteCiclo { get; set; }

        /// <summary>
        /// A sequência de vértices que formam o ciclo.
        /// </summary>
        public List<int> Sequencia { get; set; }

        /// <summary>
        /// O tipo de ciclo encontrado (Euleriano ou Hamiltoniano).
        /// </summary>
        public string TipoCiclo { get; set; }

        /// <summary>
        /// Número de vértices visitados durante a busca.
        /// </summary>
        public int NumeroVerticesVisitados { get; set; }

        /// <summary>
        /// Número de arestas percorridas no ciclo.
        /// </summary>
        public int NumeroArestasPercorridas { get; set; }

        /// <summary>
        /// Tempo de execução do algoritmo.
        /// </summary>
        public double TempoExecucao { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ResultadoCiclo()
        {
            Sequencia = new List<int>();
            TipoCiclo = string.Empty;
        }

        /// <summary>
        /// Adiciona um vértice à sequência do ciclo.
        /// </summary>
        /// <param name="vertice">O vértice a ser adicionado.</param>
        public void AdicionarVertice(int vertice)
        {
            Sequencia.Add(vertice);
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            if (!ExisteCiclo)
            {
                return $"{TipoCiclo}: inexistente.";
            }

            return $"{TipoCiclo}: {string.Join(" -> ", Sequencia)} | Vértices visitados: {NumeroVerticesVisitados} | Arestas: {NumeroArestasPercorridas} | Tempo: {TempoExecucao:F2} ms";
        }

        /// <summary>
        /// Converte o resultado para o formato JSON.
        /// </summary>
        /// <returns>A string JSON.</returns>
        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
