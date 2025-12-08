using System;
using System.IO;

namespace TP_Grafos
{
    /// <summary>
    /// Gerencia a escrita de logs de eventos e resultados em um arquivo.
    /// </summary>
    public class ArquivoLog
    {
        /// <summary>
        /// Caminho completo para o arquivo de log.
        /// </summary>
        private string caminhoLog;

        /// <summary>
        /// Objeto para escrever no arquivo de log.
        /// </summary>
        private StreamWriter writer;

        /// <summary>
        /// Objeto para controle de concorrência (thread-safe).
        /// </summary>
        private object lockObj = new object();

        /// <summary>
        /// Construtor que cria ou abre um arquivo de log.
        /// </summary>
        /// <param name="nomeBase">O nome base para o arquivo de log.</param>
        public ArquivoLog(string nomeBase)
        {
            var pasta = Path.Combine(AppContext.BaseDirectory, "logs");
            Directory.CreateDirectory(pasta);

            caminhoLog = Path.Combine(pasta, $"{nomeBase}_{DateTime.Now:yyyyMMdd_HHmmss}.log");
            writer = new StreamWriter(caminhoLog, append: false) { AutoFlush = true };
            EscreverInfo($"Log iniciado em {DateTime.Now}");
        }

        /// <summary>
        /// Escreve uma mensagem genérica no log.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser escrita.</param>
        public void Escrever(string mensagem)
        {
            lock (lockObj)
            {
                writer.WriteLine(mensagem);
            }
        }

        /// <summary>
        /// Escreve o resultado de uma análise no log.
        /// </summary>
        /// <param name="problema">O nome do problema analisado.</param>
        /// <param name="resultado">O objeto de resultado.</param>
        public void EscreverResultado(string problema, object resultado)
        {
            Escrever($"[{DateTime.Now:HH:mm:ss}] Resultado - {problema}");
            Escrever(resultado?.ToString() ?? "sem resultado");
            Escrever(new string('-', 50));
        }

        /// <summary>
        /// Escreve uma mensagem de erro no log.
        /// </summary>
        /// <param name="erro">A mensagem de erro.</param>
        /// <param name="ex">A exceção (opcional).</param>
        public void EscreverErro(string erro, Exception ex = null)
        {
            Escrever($"ERRO: {erro}");
            if (ex != null)
            {
                Escrever(ex.ToString());
            }
        }

        /// <summary>
        /// Escreve uma mensagem informativa no log.
        /// </summary>
        /// <param name="info">A mensagem de informação.</param>
        public void EscreverInfo(string info)
        {
            Escrever($"INFO: {info}");
        }

        /// <summary>
        /// Garante que todos os dados em buffer sejam escritos no arquivo.
        /// </summary>
        public void Flush()
        {
            lock (lockObj)
            {
                writer.Flush();
            }
        }

        /// <summary>
        /// Fecha o arquivo de log.
        /// </summary>
        public void Fechar()
        {
            lock (lockObj)
            {
                writer.Flush();
                writer.Dispose();
            }
        }

        /// <summary>
        /// Lê todo o conteúdo do arquivo de log.
        /// </summary>
        /// <returns>O conteúdo do log.</returns>
        public string LerLog()
        {
            writer.Flush();
            return File.ReadAllText(caminhoLog);
        }

        /// <summary>
        /// Limpa o conteúdo do arquivo de log.
        /// </summary>
        public void LimparLog()
        {
            writer.Flush();
            File.WriteAllText(caminhoLog, string.Empty);
        }
    }
}
