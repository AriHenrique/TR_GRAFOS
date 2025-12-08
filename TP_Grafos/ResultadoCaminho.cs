using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Armazena o resultado da execução de um algoritmo de caminho mínimo.
    /// </summary>
    public class ResultadoCaminho
    {
        /// <summary>
        /// Lista de vértices que compõem o caminho.
        /// </summary>
        public List<int> Caminho { get; set; }

        /// <summary>
        /// Custo total do caminho.
        /// </summary>
        public double CustoTotal { get; set; }

        /// <summary>
        /// Indica se um caminho foi encontrado.
        /// </summary>
        public bool CaminhoEncontrado { get; set; }

        /// <summary>
        /// Tempo de execução do algoritmo.
        /// </summary>
        public double TempoExecucao { get; set; }

        /// <summary>
        /// Nome do algoritmo utilizado.
        /// </summary>
        public string AlgoritmoUsado { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ResultadoCaminho()
        {
            Caminho = new List<int>();
            AlgoritmoUsado = string.Empty;
        }

        /// <summary>
        /// Adiciona um vértice ao caminho.
        /// </summary>
        /// <param name="vertice">O vértice a ser adicionado.</param>
        public void AdicionarVertice(int vertice)
        {
            Caminho.Add(vertice);
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            if (!CaminhoEncontrado)
            {
                return "Nenhum caminho encontrado.";
            }

            return $"Algoritmo: {AlgoritmoUsado} | Custo: {CustoTotal} | Caminho: {string.Join(" -> ", Caminho)} | Tempo: {TempoExecucao:F2} ms";
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
