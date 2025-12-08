using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Armazena o resultado da execução de um algoritmo de árvore geradora.
    /// </summary>
    public class ResultadoArvore
    {
        /// <summary>
        /// Lista de arestas que compõem a árvore.
        /// </summary>
        public List<Aresta> Arestas { get; set; }

        /// <summary>
        /// Custo total da árvore (soma dos pesos das arestas).
        /// </summary>
        public double CustoTotal { get; set; }

        /// <summary>
        /// Indica se uma árvore foi encontrada.
        /// </summary>
        public bool ArvoreEncontrada { get; set; }

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
        public ResultadoArvore()
        {
        }

        /// <summary>
        /// Adiciona uma aresta à árvore.
        /// </summary>
        /// <param name="aresta">A aresta a ser adicionada.</param>
        public void AdicionarAresta(Aresta aresta)
        {
            if (Arestas == null)
                Arestas = new List<Aresta>();
            
            Arestas.Add(aresta);
            CustoTotal += aresta.Peso;
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Algoritmo: {AlgoritmoUsado}");
            sb.AppendLine($"Árvore Encontrada: {ArvoreEncontrada}");
            sb.AppendLine($"Custo Total: {CustoTotal:F2}");
            sb.AppendLine($"Tempo de Execução: {TempoExecucao:F2} ms");
            
            if (Arestas != null && Arestas.Count > 0)
            {
                sb.AppendLine($"Arestas ({Arestas.Count}):");
                foreach (var aresta in Arestas)
                {
                    sb.AppendLine($"  {aresta.Origem} -> {aresta.Destino} (peso: {aresta.Peso:F2})");
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
            json.AppendFormat("\"algoritmo\": \"{0}\", ", AlgoritmoUsado);
            json.AppendFormat("\"arvoreEncontrada\": {0}, ", ArvoreEncontrada.ToString().ToLower());
            json.AppendFormat("\"custoTotal\": {0}, ", CustoTotal.ToString("F2"));
            json.AppendFormat("\"tempoExecucao\": {0}, ", TempoExecucao.ToString("F2"));
            
            if (Arestas != null)
            {
                json.Append("\"arestas\": [");
                for (int i = 0; i < Arestas.Count; i++)
                {
                    json.AppendFormat("{{\"origem\": {0}, \"destino\": {1}, \"peso\": {2}}}", 
                        Arestas[i].Origem, Arestas[i].Destino, Arestas[i].Peso.ToString("F2"));
                    if (i < Arestas.Count - 1) json.Append(", ");
                }
                json.Append("]");
            }
            
            json.Append("}");
            return json.ToString();
        }
    }
}
