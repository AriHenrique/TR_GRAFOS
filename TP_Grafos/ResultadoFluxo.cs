using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Armazena o resultado da execução de um algoritmo de fluxo máximo.
    /// </summary>
    public class ResultadoFluxo
    {
        /// <summary>
        /// O valor do fluxo máximo encontrado.
        /// </summary>
        public double FluxoMaximo { get; set; }

        /// <summary>
        /// A lista de arestas que compõem o corte mínimo.
        /// </summary>
        public List<Aresta> CorteMinimo { get; set; }

        /// <summary>
        /// Dicionário que mapeia cada aresta ao seu valor de fluxo.
        /// </summary>
        public Dictionary<(int, int), double> FluxoPorAresta { get; set; }

        /// <summary>
        /// Lista de caminhos aumentantes encontrados durante a execução.
        /// </summary>
        public List<List<int>> CaminhosAumentantes { get; set; }

        /// <summary>
        /// Tempo de execução do algoritmo.
        /// </summary>
        public double TempoExecucao { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ResultadoFluxo()
        {
            CorteMinimo = new List<Aresta>();
            FluxoPorAresta = new Dictionary<(int, int), double>();
            CaminhosAumentantes = new List<List<int>>();
        }

        /// <summary>
        /// Adiciona o valor do fluxo para uma aresta específica.
        /// </summary>
        /// <param name="origem">Vértice de origem da aresta.</param>
        /// <param name="destino">Vértice de destino da aresta.</param>
        /// <param name="fluxo">Valor do fluxo.</param>
        public void AdicionarFluxoAresta(int origem, int destino, double fluxo)
        {
            FluxoPorAresta[(origem, destino)] = fluxo;
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            return $"Fluxo máximo: {FluxoMaximo} | Corte mínimo: {string.Join(", ", CorteMinimo)} | Tempo: {TempoExecucao:F2} ms";
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
