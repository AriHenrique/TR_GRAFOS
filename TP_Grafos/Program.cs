using System;
using System.IO;

namespace TP_Grafos
{
    /// <summary>
    /// Classe principal que gerencia a interface com o usuário e a execução das análises.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Instância do grafo a ser analisado.
        /// </summary>
        private static Grafo? grafo;

        /// <summary>
        /// Caminho do arquivo de log atual.
        /// </summary>
        private static string arquivoLogAtual = string.Empty;

        /// <summary>
        /// Logger atual.
        /// </summary>
        private static ArquivoLog? logger;

        /// <summary>
        /// Ponto de entrada do programa.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        static void Main(string[] args)
        {
            var pastaGrafos = args.Length > 0
                ? args[0]
                : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "grafos_dimacs"));

            if (!Directory.Exists(pastaGrafos))
            {
                Console.WriteLine($"Pasta de grafos não encontrada: {pastaGrafos}");
                return;
            }

            var arquivos = Directory.GetFiles(pastaGrafos, "*.dimacs");
            if (arquivos.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo DIMACS encontrado.");
                return;
            }

            Console.WriteLine("Iniciando processamento automático dos grafos DIMACS...");

            Array.Sort(arquivos, StringComparer.OrdinalIgnoreCase);

            foreach (var arquivo in arquivos)
            {
                try
                {
                    Console.WriteLine($"Processando {Path.GetFileName(arquivo)}");
                    grafo = new Grafo(arquivo);
                    arquivoLogAtual = Path.GetFileNameWithoutExtension(arquivo);
                    logger = new ArquivoLog(arquivoLogAtual);

                    ExecutarTodasAnalises();
                    logger.Flush();
                    logger.Fechar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar {arquivo}: {ex.Message}");
                    try
                    {
                        logger?.EscreverErro($"Falha ao processar {arquivo}", ex);
                        logger?.Fechar();
                    }
                    catch
                    {
                        // ignora erro de log
                    }
                }
            }

            Console.WriteLine("Concluído.");
        }

        /// <summary>
        /// Exibe o menu principal de opções para o usuário.
        /// </summary>
        private static void ExibirMenu()
        {
            Console.WriteLine("Menu de Análises");
            Console.WriteLine("1 - Roteamento de menor custo");
            Console.WriteLine("2 - Fluxo máximo");
            Console.WriteLine("3 - Árvore geradora mínima");
            Console.WriteLine("4 - Coloração (agendamento)");
            Console.WriteLine("5 - Ciclo Euleriano");
            Console.WriteLine("6 - Ciclo Hamiltoniano");
            Console.WriteLine("7 - Executar todas");
            Console.WriteLine("0 - Sair");
        }

        /// <summary>
        /// Processa a escolha do usuário a partir do menu.
        /// </summary>
        /// <param name="opcao">A opção escolhida pelo usuário.</param>
        private static void ProcessarEscolha(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    ExecutarRoteamentoMenorCusto();
                    break;
                case 2:
                    ExecutarFluxoMaximo();
                    break;
                case 3:
                    ExecutarArvoreGeradora();
                    break;
                case 4:
                    ExecutarColoracao();
                    break;
                case 5:
                    ExecutarCicloEuleriano();
                    break;
                case 6:
                    ExecutarCicloHamiltoniano();
                    break;
                case 7:
                    ExecutarTodasAnalises();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Executa a análise de roteamento de menor custo.
        /// </summary>
        private static void ExecutarRoteamentoMenorCusto()
        {
            var algoritmo = new AlgoritmoCaminhoMinimo(grafo);
            int origem = 1;
            int destino = grafo.NumVertices;
            var resultado = algoritmo.Dijkstra(origem, destino);

            Console.WriteLine(resultado);
            logger.EscreverResultado("Roteamento de menor custo (Dijkstra)", resultado);
        }

        /// <summary>
        /// Executa a análise de fluxo máximo.
        /// </summary>
        private static void ExecutarFluxoMaximo()
        {
            var algoritmo = new AlgoritmoFluxoMaximo(grafo);
            int origem = 1;
            int destino = grafo.NumVertices;
            var resultado = algoritmo.EdmondsKarp(origem, destino);

            Console.WriteLine(resultado);
            logger.EscreverResultado("Fluxo máximo (Edmonds-Karp)", resultado);
        }

        /// <summary>
        /// Executa a análise de árvore geradora mínima.
        /// </summary>
        private static void ExecutarArvoreGeradora()
        {
            var algoritmo = new AlgoritmoArvoreGeradora(grafo);
            var resultado = algoritmo.Kruskal();

            Console.WriteLine(resultado);
            logger.EscreverResultado("Árvore geradora mínima (Kruskal)", resultado);
        }

        /// <summary>
        /// Executa a análise de coloração de vértices.
        /// </summary>
        private static void ExecutarColoracao()
        {
            var algoritmo = new AlgoritmoColoracao(grafo);
            var resultado = algoritmo.ColoracaoDSATUR();

            Console.WriteLine(resultado);
            logger.EscreverResultado("Coloração / agendamento (DSATUR)", resultado);
        }

        /// <summary>
        /// Executa a análise de ciclo Euleriano.
        /// </summary>
        private static void ExecutarCicloEuleriano()
        {
            var algoritmo = new AlgoritmoCiclo(grafo);
            var resultado = algoritmo.VerificarCicloEuleriano();

            Console.WriteLine(resultado);
            logger.EscreverResultado("Ciclo Euleriano", resultado);
        }

        /// <summary>
        /// Executa a análise de ciclo Hamiltoniano.
        /// </summary>
        private static void ExecutarCicloHamiltoniano()
        {
            var algoritmo = new AlgoritmoCiclo(grafo);
            var resultado = algoritmo.VerificarCicloHamiltoniano();

            Console.WriteLine(resultado);
            logger.EscreverResultado("Ciclo Hamiltoniano", resultado);
        }

        /// <summary>
        /// Executa todas as análises disponíveis em sequência.
        /// </summary>
        private static void ExecutarTodasAnalises()
        {
            ExecutarRoteamentoMenorCusto();
            ExecutarFluxoMaximo();
            ExecutarArvoreGeradora();
            ExecutarColoracao();
            ExecutarCicloEuleriano();
            ExecutarCicloHamiltoniano();
        }

        /// <summary>
        /// Gera um relatório completo com os resultados de todas as análises.
        /// </summary>
        private static void GerarRelatorioCompleto()
        {
            var gerador = new GeradorRelatorio(arquivoLogAtual);
            gerador.SalvarRelatorio();
        }

        /// <summary>
        /// Salva uma mensagem no arquivo de log.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser salva.</param>
        private static void SalvarLog(string mensagem)
        {
            logger?.Escrever(mensagem);
        }
    }
}
