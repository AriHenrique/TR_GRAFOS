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
        }

        /// <summary>
        /// Retorna uma representação do resultado em string.
        /// </summary>
        /// <returns>A string que representa o resultado.</returns>
        public override string ToString()
        {
            return "";
        }

        /// <summary>
        /// Converte o resultado para o formato JSON.
        /// </summary>
        /// <returns>A string JSON.</returns>
        public string ToJson()
        {
            return "";
        }
    }
}
