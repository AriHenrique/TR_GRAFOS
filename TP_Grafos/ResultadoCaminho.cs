using System.Collections.Generic;
using System.Text;

namespace TP_Grafos
{
    public class ResultadoCaminho
    {
        public List<int> Caminho { get; set; }
        public double CustoTotal { get; set; }
        public bool CaminhoEncontrado { get; set; }
        public double TempoExecucao { get; set; }
        public string AlgoritmoUsado { get; set; }

        public ResultadoCaminho()
        {
            Caminho = new List<int>();
        }

        public void AdicionarVertice(int vertice)
        {
            Caminho.Add(vertice);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- Resultado Caminho Mínimo ({AlgoritmoUsado}) ---");
            sb.AppendLine($"Tempo de Execução: {TempoExecucao} ms");
            sb.AppendLine($"Caminho Encontrado: {CaminhoEncontrado}");
            
            if (CaminhoEncontrado)
            {
                sb.AppendLine($"Custo Total: {CustoTotal}");
                sb.AppendLine($"Caminho: {string.Join(" -> ", Caminho)}");
            }
            return sb.ToString();
        }

        public string ToJson()
        {
            string caminhoJson = CaminhoEncontrado ? "[" + string.Join(",", Caminho) + "]" : "[]";
            return $@"{{
                ""algoritmo"": ""{AlgoritmoUsado}"",
                ""encontrado"": {CaminhoEncontrado.ToString().ToLower()},
                ""custo"": {CustoTotal},
                ""tempo_ms"": {TempoExecucao},
                ""caminho"": {caminhoJson}
            }}";
        }
    }
}