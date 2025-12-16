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
        }

        /// <summary>
        /// Adiciona o valor do fluxo para uma aresta específica.
        /// </summary>
        /// <param name="origem">Vértice de origem da aresta.</param>
        /// <param name="destino">Vértice de destino da aresta.</param>
        /// <param name="fluxo">Valor do fluxo.</param>
        public void AdicionarFluxoAresta(int origem, int destino, double fluxo)
        {
            if (FluxoPorAresta == null)
                FluxoPorAresta = new Dictionary<(int, int), double>();
            
            FluxoPorAresta[(origem, destino)] = fluxo;
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Fluxo Máximo: {FluxoMaximo:F2}");
            sb.AppendLine($"Tempo de Execução: {TempoExecucao:F2} ms");
            
            if (CorteMinimo != null && CorteMinimo.Count > 0)
            {
                sb.AppendLine($"Corte Mínimo ({CorteMinimo.Count} arestas):");
                foreach (var aresta in CorteMinimo)
                {
                    sb.AppendLine($"  {aresta.Origem} -> {aresta.Destino}");
                }
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Converte o resultado para o formato JSON.
        /// </summary>
        /// <returns>A string JSON.</returns>
        public string ToJson()
        {
            System.Text.StringBuilder json = new System.Text.StringBuilder();
            json.Append("{");
            json.AppendFormat("\"fluxoMaximo\": {0}, ", FluxoMaximo.ToString("F2"));
            json.AppendFormat("\"tempoExecucao\": {0}, ", TempoExecucao.ToString("F2"));
            
            if (CorteMinimo != null)
            {
                json.Append("\"corteMinimo\": [");
                for (int i = 0; i < CorteMinimo.Count; i++)
                {
                    json.AppendFormat("{{\"origem\": {0}, \"destino\": {1}}}", 
                        CorteMinimo[i].Origem, CorteMinimo[i].Destino);
                    if (i < CorteMinimo.Count - 1) json.Append(", ");
                }
                json.Append("]");
            }
            
            json.Append("}");
            return json.ToString();
        }
    }
}
