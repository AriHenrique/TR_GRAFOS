using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Grafos
{
    /// <summary>
    /// Gera relatórios consolidados das análises do grafo.
    /// </summary>
    public class GeradorRelatorio
    {
        /// <summary>
        /// Nome base para o arquivo de relatório.
        /// </summary>
        private string nomeArquivo;

        /// <summary>
        /// Data e hora da geração do relatório.
        /// </summary>
        private DateTime dataGeracao;

        /// <summary>
        /// Construtor de string para montar o conteúdo do relatório.
        /// </summary>
        private StringBuilder conteudo;

        /// <summary>
        /// Construtor que define o nome base para os arquivos de relatório.
        /// </summary>
        /// <param name="nomeBase">O nome base do arquivo.</param>
        public GeradorRelatorio(string nomeBase)
        {
            nomeArquivo = nomeBase;
            dataGeracao = DateTime.Now;
            conteudo = new StringBuilder();

            conteudo.AppendLine("RELATÓRIO DE ANÁLISE DE GRAFOS");
            conteudo.AppendLine($"Data de geração: {dataGeracao}");
            conteudo.AppendLine(new string('=', 50));
            conteudo.AppendLine();
        }

        /// <summary>
        /// Adiciona uma seção ao relatório.
        /// </summary>
        /// <param name="titulo">O título da seção.</param>
        /// <param name="conteudo">O conteúdo da seção.</param>
        public void AdicionarSecao(string titulo, string conteudo)
        {
            conteudo.AppendLine(titulo.ToUpper());
            conteudo.AppendLine(new string('-', titulo.Length));
            conteudo.AppendLine(texto);
            conteudo.AppendLine();
        }

        /// <summary>
        /// Adiciona um resultado de análise formatado ao relatório.
        /// </summary>
        /// <param name="problema">O nome do problema analisado.</param>
        /// <param name="resultado">O objeto de resultado.</param>
        public void AdicionarResultado(string problema, object resultado)
        {
            conteudo.AppendLine($"Problema: {problema}");
            conteudo.AppendLine(new string('-', 40));

            if (resultado != null)
                conteudo.AppendLine(resultado.ToString());
            else
                conteudo.AppendLine("Nenhum resultado disponível.");

            conteudo.AppendLine();
        }

        /// <summary>
        /// Adiciona um gráfico ao relatório (placeholder).
        /// </summary>
        /// <param name="tipo">O tipo de gráfico.</param>
        /// <param name="dados">Os dados para o gráfico.</param>
        public void AdicionarGrafico(string tipo, object dados)
        {
            conteudo.AppendLine($"[Gráfico: {tipo}]");
            conteudo.AppendLine("Visualização gráfica não suportada neste formato.");
            conteudo.AppendLine();
        }

        /// <summary>
        /// Adiciona uma tabela formatada ao relatório.
        /// </summary>
        /// <param name="titulo">O título da tabela.</param>
        /// <param name="dados">Os dados da tabela (lista de linhas).</param>
        public void AdicionarTabela(string titulo, List<List<string>> dados)
        {
            conteudo.AppendLine(titulo);
            conteudo.AppendLine(new string('-', titulo.Length));

            foreach (var linha in dados)
            {
                conteudo.AppendLine(string.Join(" | ", linha));
            }

            conteudo.AppendLine();
        }

        /// <summary>
        /// Gera um relatório completo em formato de texto.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioTexto(List<object> resultados)
        {
            AdicionarSecao("Resultados das Análises", "");

            foreach (var resultado in resultados)
            {
                AdicionarResultado("Análise Executada", resultado);
            }
        }

        /// <summary>
        /// Gera um relatório completo em formato HTML.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioHTML(List<object> resultados)
        {
            conteudo.AppendLine("<html><body>");
            conteudo.AppendLine("<h1>Relatório de Análise de Grafos</h1>");
            conteudo.AppendLine($"<p>Data: {dataGeracao}</p>");

            foreach (var resultado in resultados)
            {
                conteudo.AppendLine("<hr/>");
                conteudo.AppendLine("<pre>");
                conteudo.AppendLine(resultado?.ToString());
                conteudo.AppendLine("</pre>");
            }

            conteudo.AppendLine("</body></html>");
        }

        /// <summary>
        /// Gera um relatório completo em formato PDF (placeholder).
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioPDF(List<object> resultados)
        {
            conteudo.Clear();
            conteudo.AppendLine("Geração de PDF não implementada.");
            conteudo.AppendLine("Utilize bibliotecas externas como iTextSharp ou QuestPDF.");
        }

        /// <summary>
        /// Gera um relatório completo em formato JSON.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioJSON(List<object> resultados)
        {
            conteudo.Clear();
            conteudo.AppendLine("{");
            conteudo.AppendLine("  \"resultados\": [");

            for (int i = 0; i < resultados.Count; i++)
            {
                conteudo.Append("    ");
                conteudo.Append(resultados[i]?.ToString());

                if (i < resultados.Count - 1)
                    conteudo.Append(",");

                conteudo.AppendLine();
            }

            conteudo.AppendLine("  ]");
            conteudo.AppendLine("}");
        }

        /// <summary>
        /// Salva o conteúdo do relatório em um arquivo.
        /// </summary>
        /// <returns>O caminho do arquivo salvo.</returns>
        public string SalvarRelatorio()
        {
            string caminho = $"{nomeArquivo}_{dataGeracao:yyyyMMdd_HHmmss}.txt";
            File.WriteAllText(caminho, conteudo.ToString());
            return caminho;
        }
    }
}
