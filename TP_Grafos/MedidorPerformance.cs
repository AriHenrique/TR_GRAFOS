using System.Diagnostics;

namespace TP_Grafos
{
    /// <summary>
    /// Mede o tempo de execução e o uso de memória de operações.
    /// </summary>
    public class MedidorPerformance
    {
        /// <summary>
        /// Cronômetro para medir o tempo de execução.
        /// </summary>
        private Stopwatch cronometro;

        /// <summary>
        /// Memória alocada no início da medição.
        /// </summary>
        private long memoriaInicial;

        /// <summary>
        /// Memória alocada no final da medição.
        /// </summary>
        private long memoriaFinal;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public MedidorPerformance()
        {
            cronometro = new Stopwatch();
        }

        /// <summary>
        /// Inicia a medição de tempo e memória.
        /// </summary>
        public void Iniciar()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            memoriaInicial = GC.GetTotalMemory(true);
            cronometro.Restart();
        }

        /// <summary>
        /// Para a medição de tempo e memória.
        /// </summary>
        public void Parar()
        {
            cronometro.Stop();
            memoriaFinal = GC.GetTotalMemory(true);
        }

        /// <summary>
        /// Obtém o tempo decorrido em milissegundos.
        /// </summary>
        /// <returns>O tempo decorrido.</returns>
        public double ObterTempoDecorrido()
        {
            return cronometro.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Obtém o uso de memória em bytes.
        /// </summary>
        /// <returns>A quantidade de memória utilizada.</returns>
        public long ObterUsoMemoria()
        {
            return memoriaFinal - memoriaInicial;
        }

        /// <summary>
        /// Gera uma string com as estatísticas de performance.
        /// </summary>
        /// <returns>As estatísticas de performance.</returns>
        public string GerarEstatisticas()
        {
            return
               $"Tempo de execução: {ObterTempoDecorrido():F2} ms\n" +
               $"Uso de memória: {ObterUsoMemoria()} bytes";
        }
    }
}
