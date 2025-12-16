    using System.Collections.Generic;
    using System.Text;

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
                ExisteCiclo = false;
                Sequencia = new List<int>();
                TipoCiclo = string.Empty;
                NumeroVerticesVisitados = 0;
                NumeroArestasPercorridas = 0;
                TempoExecucao = 0.0;
            }

            /// <summary>
            /// Adiciona um vértice à sequência do ciclo.
            /// </summary>
            /// <param name="vertice">O vértice a ser adicionado.</param>
            public void AdicionarVertice(int vertice)
            {
                if (Sequencia == null)
                    Sequencia = new List<int>();

                Sequencia.Add(vertice);
                NumeroVerticesVisitados = Sequencia.Count;

                if (Sequencia.Count > 1)
                    NumeroArestasPercorridas = Sequencia.Count - 1;
            }

            /// <summary>
            /// Retorna uma representação do resultado em string.
            /// </summary>
            /// <returns>A string que representa o resultado.</returns>
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Tipo do ciclo: {TipoCiclo}");
                sb.AppendLine($"Ciclo encontrado: {(ExisteCiclo ? "Sim" : "Não")}");
                sb.AppendLine($"Vertices visitados: {NumeroVerticesVisitados}");
                sb.AppendLine($"Arestas percorridas: {NumeroArestasPercorridas}");
                sb.AppendLine($"Tempo de execução: {TempoExecucao:F2} ms");

                if (ExisteCiclo && Sequencia != null && Sequencia.Count > 0)
                {
                    sb.Append("Sequência do ciclo: ");
                    sb.AppendLine(string.Join(" -> ", Sequencia));
                }

                return sb.ToString();
            }

            /// <summary>
            /// Converte o resultado para o formato JSON.
            /// </summary>
            /// <returns>A string JSON.</returns>
            public string ToJson()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("{");
                sb.AppendFormat("\"ExisteCiclo\": {0}, ", ExisteCiclo.ToString().ToLower());
                sb.AppendFormat("\"TipoCiclo\": \"{0}\", ", TipoCiclo);
                sb.AppendFormat("\"NumeroVerticesVisitados\": {0}, ", NumeroVerticesVisitados);
                sb.AppendFormat("\"NumeroArestasPercorridas\": {0}, ", NumeroArestasPercorridas);
                sb.AppendFormat("\"TempoExecucao\": {0}, ", TempoExecucao.ToString("F2"));

                sb.Append("\"Sequencia\": [");
                if (Sequencia != null)
                {
                    for (int i = 0; i < Sequencia.Count; i++)
                    {
                        sb.Append(Sequencia[i]);
                        if (i < Sequencia.Count - 1)
                            sb.Append(", ");
                    }
                }
                sb.Append("]}");

                return sb.ToString();
            }
        }
    }
