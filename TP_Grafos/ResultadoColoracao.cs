using System.Collections.Generic;
using System.Linq;

namespace TP_Grafos
{
    /// <summary>
    /// Armazena o resultado da execução de um algoritmo de coloração.
    /// </summary>
    public class ResultadoColoracao
    {
        /// <summary>
        /// Dicionário que agrupa os vértices por cor.
        /// </summary>
        public Dictionary<int, List<int>> GruposPorCor { get; set; }

        /// <summary>
        /// O número de cores utilizadas (número cromático encontrado).
        /// </summary>
        public int NumeroTurnos { get; set; }

        /// <summary>
        /// Dicionário que mapeia cada vértice à sua cor.
        /// </summary>
        public Dictionary<int, int> CorPorVertice { get; set; }

        /// <summary>
        /// Nome do algoritmo utilizado.
        /// </summary>
        public string AlgoritmoUsado { get; set; }

        /// <summary>
        /// Tempo de execução do algoritmo.
        /// </summary>
        public double TempoExecucao { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ResultadoColoracao()
        {
            GruposPorCor = new Dictionary<int, List<int>>();
            CorPorVertice = new Dictionary<int, int>();
            AlgoritmoUsado = string.Empty;
        }

        /// <summary>
        /// Atribui uma cor a um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <param name="cor">A cor.</param>
        public void AtribuirCor(int vertice, int cor)
        {
            CorPorVertice[vertice] = cor;
            if (!GruposPorCor.ContainsKey(cor))
            {
                GruposPorCor[cor] = new List<int>();
            }

            GruposPorCor[cor].Add(vertice);
        }

        /// <summary>
        /// Obtém a lista de vértices com uma determinada cor.
        /// </summary>
        /// <param name="cor">A cor.</param>
        /// <returns>A lista de vértices.</returns>
        public List<int> ObterVerticesPorCor(int cor)
        {
            return GruposPorCor.TryGetValue(cor, out var lista) ? lista : new List<int>();
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            return $"Algoritmo: {AlgoritmoUsado} | Turnos (cores): {NumeroTurnos} | Cores: {string.Join(" | ", GruposPorCor.Select(kv => $"cor {kv.Key}: [{string.Join(',', kv.Value)}]"))} | Tempo: {TempoExecucao:F2} ms";
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
