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
            conteudo.AppendLine($"Relatório - {nomeArquivo} - {dataGeracao}");
            conteudo.AppendLine(new string('=', 60));
        }

        /// <summary>
        /// Adiciona uma seção ao relatório.
        /// </summary>
        /// <param name="titulo">O título da seção.</param>
        /// <param name="conteudo">O conteúdo da seção.</param>
        public void AdicionarSecao(string titulo, string conteudo)
        {
            this.conteudo.AppendLine(titulo);
            this.conteudo.AppendLine(new string('-', titulo.Length));
            this.conteudo.AppendLine(conteudo);
            this.conteudo.AppendLine();
        }

        /// <summary>
        /// Adiciona um resultado de análise formatado ao relatório.
        /// </summary>
        /// <param name="problema">O nome do problema analisado.</param>
        /// <param name="resultado">O objeto de resultado.</param>
        public void AdicionarResultado(string problema, object resultado)
        {
            AdicionarSecao(problema, resultado?.ToString() ?? "Sem resultado");
        }

        /// <summary>
        /// Adiciona um gráfico ao relatório (placeholder).
        /// </summary>
        /// <param name="tipo">O tipo de gráfico.</param>
        /// <param name="dados">Os dados para o gráfico.</param>
        public void AdicionarGrafico(string tipo, object dados)
        {
            AdicionarSecao($"Gráfico {tipo}", "Geração de gráfico não implementada.");
        }

        /// <summary>
        /// Adiciona uma tabela formatada ao relatório.
        /// </summary>
        /// <param name="titulo">O título da tabela.</param>
        /// <param name="dados">Os dados da tabela (lista de linhas).</param>
        public void AdicionarTabela(string titulo, List<List<string>> dados)
        {
            var sb = new StringBuilder();
            foreach (var linha in dados)
            {
                sb.AppendLine(string.Join(" | ", linha));
            }

            AdicionarSecao(titulo, sb.ToString());
        }

        /// <summary>
        /// Gera um relatório completo em formato de texto.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioTexto(List<object> resultados)
        {
            foreach (var r in resultados)
            {
                AdicionarResultado("Resultado", r);
            }

            SalvarRelatorio();
        }

        /// <summary>
        /// Gera um relatório completo em formato HTML.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioHTML(List<object> resultados)
        {
            // Placeholder: reutiliza texto
            GerarRelatorioTexto(resultados);
        }

        /// <summary>
        /// Gera um relatório completo em formato PDF (placeholder).
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioPDF(List<object> resultados)
        {
            // Placeholder: reutiliza texto
            GerarRelatorioTexto(resultados);
        }

        /// <summary>
        /// Gera um relatório completo em formato JSON.
        /// </summary>
        /// <param name="resultados">A lista de resultados das análises.</param>
        public void GerarRelatorioJSON(List<object> resultados)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(resultados);
            conteudo.AppendLine(json);
            SalvarRelatorio();
        }

        /// <summary>
        /// Salva o conteúdo do relatório em um arquivo.
        /// </summary>
        /// <returns>O caminho do arquivo salvo.</returns>
        public string SalvarRelatorio()
        {
            var pasta = Path.Combine(AppContext.BaseDirectory, "relatorios");
            Directory.CreateDirectory(pasta);
            var caminho = Path.Combine(pasta, $"{nomeArquivo}_{dataGeracao:yyyyMMdd_HHmmss}.txt");
            File.WriteAllText(caminho, conteudo.ToString());
            return caminho;
        }
    }
}
