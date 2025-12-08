using System.Collections.Generic;

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
            NumeroTurnos = 0;
            TempoExecucao = 0;
        }

        /// <summary>
        /// Atribui uma cor a um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <param name="cor">A cor.</param>
        public void AtribuirCor(int vertice, int cor)
        {
            // Mapeia vértice → cor
            CorPorVertice[vertice] = cor;

            // Cria o grupo da cor, se necessário
            if (!GruposPorCor.ContainsKey(cor))
            {
                GruposPorCor[cor] = new List<int>();
            }

            // Evita duplicação
            if (!GruposPorCor[cor].Contains(vertice))
            {
                GruposPorCor[cor].Add(vertice);
            }
        }

        /// <summary>
        /// Obtém a lista de vértices com uma determinada cor.
        /// </summary>
        /// <param name="cor">A cor.</param>
        /// <returns>A lista de vértices.</returns>
        public List<int> ObterVerticesPorCor(int cor)
        {
            if (GruposPorCor.ContainsKey(cor))
            {
                return GruposPorCor[cor];
            }

            return new List<int>();
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Resultado da Coloração");
            sb.AppendLine("Algoritmo: " + AlgoritmoUsado);
            sb.AppendLine("Número de cores utilizadas: " + NumeroTurnos);
            sb.AppendLine("Tempo de execução: " + TempoExecucao + " ms");
            sb.AppendLine();

            foreach (var grupo in GruposPorCor)
            {
                sb.Append("Cor ").Append(grupo.Key).Append(": ");

                for (int i = 0; i < grupo.Value.Count; i++)
                {
                    sb.Append(grupo.Value[i]);
                    if (i < grupo.Value.Count - 1)
                        sb.Append(", ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converte o resultado para o formato JSON.
        /// </summary>
        /// <returns>A string JSON.</returns>
        public string ToJson()
        {
            StringBuilder json = new StringBuilder();

            json.Append("{");
            json.Append("\"algoritmo\":\"").Append(AlgoritmoUsado).Append("\",");
            json.Append("\"numeroTurnos\":").Append(NumeroTurnos).Append(",");
            json.Append("\"tempoExecucao\":").Append(TempoExecucao).Append(",");
            json.Append("\"gruposPorCor\":{");

            int contadorCores = 0;
            foreach (var grupo in GruposPorCor)
            {
                json.Append("\"").Append(grupo.Key).Append("\":[");
                for (int i = 0; i < grupo.Value.Count; i++)
                {
                    json.Append(grupo.Value[i]);
                    if (i < grupo.Value.Count - 1)
                        json.Append(",");
                }
                json.Append("]");

                contadorCores++;
                if (contadorCores < GruposPorCor.Count)
                    json.Append(",");
            }

            json.Append("}}");
            return json.ToString();
        }
    }
}
