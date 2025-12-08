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
            string nomeArquivo = $"{nomeBase}_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            caminhoLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);

            writer = new StreamWriter(caminhoLog, true);
            writer.AutoFlush = true;

            EscreverInfo("Arquivo de log criado.");
        }

        /// <summary>
        /// Escreve uma mensagem genérica no log.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser escrita.</param>
        public void Escrever(string mensagem)
        {
            lock (lockObj)
            {
                writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] {mensagem}");
            }
        }

        /// <summary>
        /// Escreve o resultado de uma análise no log.
        /// </summary>
        /// <param name="problema">O nome do problema analisado.</param>
        /// <param name="resultado">O objeto de resultado.</param>
        public void EscreverResultado(string problema, object resultado)
        {
            lock (lockObj)
            {
                writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] RESULTADO - {problema}");
                writer.WriteLine(resultado?.ToString() ?? "Resultado não disponível");
                writer.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Escreve uma mensagem de erro no log.
        /// </summary>
        /// <param name="erro">A mensagem de erro.</param>
        /// <param name="ex">A exceção (opcional).</param>
        public void EscreverErro(string erro, Exception ex = null)
        {
            lock (lockObj)
            {
                writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] ERRO: {erro}");

                if (ex != null)
                {
                    writer.WriteLine($"Exceção: {ex.Message}");
                    writer.WriteLine(ex.StackTrace);
                }

                writer.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Escreve uma mensagem informativa no log.
        /// </summary>
        /// <param name="info">A mensagem de informação.</param>
        public void EscreverInfo(string info)
        {
            lock (lockObj)
            {
                writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] INFO: {info}");
            }
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
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
        }

        /// <summary>
        /// Lê todo o conteúdo do arquivo de log.
        /// </summary>
        /// <returns>O conteúdo do log.</returns>
        public string LerLog()
        {
            Flush();

            if (!File.Exists(caminhoLog))
                return string.Empty;

            return File.ReadAllText(caminhoLog);
        }

        /// <summary>
        /// Limpa o conteúdo do arquivo de log.
        /// </summary>
        public void LimparLog()
        {
            lock (lockObj)
            {
                writer.Close();
                File.WriteAllText(caminhoLog, string.Empty);
                writer = new StreamWriter(caminhoLog, true);
                writer.AutoFlush = true;
            }
        }
    }
}
