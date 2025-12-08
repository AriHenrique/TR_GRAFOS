using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Fornece métodos estáticos para validar diferentes aspectos de um grafo.
    /// </summary>
    public static class ValidadorGrafo
    {
        /// <summary>
        /// Valida o formato de um arquivo DIMACS.
        /// </summary>
        /// <param name="arquivo">O caminho do arquivo.</param>
        /// <returns>True se o formato for válido, false caso contrário.</returns>
        public static bool ValidarFormatoDIMACS(string arquivo)
        {
            try
            {
                var linhas = File.ReadAllLines(arquivo);
                if (linhas.Length < 2) return false;

                var header = linhas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (header.Length < 2) return false;

                int vertices = int.Parse(header[0]);
                int arestas = int.Parse(header[1]);

                int contArestas = 0;
                for (int i = 1; i < linhas.Length; i++)
                {
                    var p = linhas[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (p.Length < 4) return false;
                    _ = int.Parse(p[0]);
                    _ = int.Parse(p[1]);
                    _ = double.Parse(p[2]);
                    _ = double.Parse(p[3]);
                    contArestas++;
                }

                return contArestas == arestas && vertices > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida se o grafo é conexo.
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>True se for conexo, false caso contrário.</returns>
        public static bool ValidarConectividade(Grafo grafo)
        {
            return grafo.EhConexo();
        }

        /// <summary>
        /// Valida se todos os pesos das arestas são positivos.
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>True se todos os pesos forem positivos, false caso contrário.</returns>
        public static bool ValidarPesosPositivos(Grafo grafo)
        {
            foreach (var aresta in grafo.ObterTodasArestas())
            {
                if (aresta.Peso < 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Valida se as capacidades das arestas são válidas (não negativas).
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>True se as capacidades forem válidas, false caso contrário.</returns>
        public static bool ValidarCapacidades(Grafo grafo)
        {
            foreach (var aresta in grafo.ObterTodasArestas())
            {
                if (aresta.Capacidade < 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Valida se o grafo atende às condições para ser Euleriano.
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>True se for Euleriano, false caso contrário.</returns>
        public static bool ValidarGrafoEuleriano(Grafo grafo)
        {
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                if (grafo.ObterGrauEntrada(i) != grafo.ObterGrauSaida(i))
                {
                    return false;
                }
            }

            return grafo.EhConexo();
        }

        /// <summary>
        /// Valida se o grafo atende a alguma condição necessária para ser Hamiltoniano.
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>True se as condições forem atendidas, false caso contrário.</returns>
        public static bool ValidarGrafoHamiltoniano(Grafo grafo)
        {
            // Critério simples: todos os vértices com grau >= 2
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                if (grafo.ObterGrauVertice(i) < 2)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Obtém a lista de erros de validação encontrados.
        /// </summary>
        /// <returns>Uma lista de strings descrevendo os erros.</returns>
        public static List<string> ObterErros()
        {
            return new List<string>();
        }

        /// <summary>
        /// Gera um relatório de validação para o grafo.
        /// </summary>
        /// <param name="grafo">O grafo a ser validado.</param>
        /// <returns>Uma string contendo o relatório de validação.</returns>
        public static string GerarRelatorioValidacao(Grafo grafo)
        {
            var erros = new List<string>();
            if (!ValidarConectividade(grafo)) erros.Add("Grafo não é conexo.");
            if (!ValidarPesosPositivos(grafo)) erros.Add("Existem pesos negativos.");
            if (!ValidarCapacidades(grafo)) erros.Add("Existem capacidades negativas.");

            if (erros.Count == 0) return "Grafo validado com sucesso.";
            return string.Join("; ", erros);
        }
    }
}
