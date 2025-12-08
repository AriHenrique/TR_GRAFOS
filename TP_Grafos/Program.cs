using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


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
        private static Grafo grafo;

        /// <summary>
        /// Caminho do arquivo de log atual.
        /// </summary>
        private static string arquivoLogAtual;

        /// <summary>
        /// Caminho do arquivo atual carregado.
        /// </summary>
        private static string arquivoAtual;

        /// <summary>
        /// Ponto de entrada do programa.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        static void Main(string[] args)
        {
            arquivoLogAtual = $"log_execucao_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            try
            {
                Console.WriteLine("=== SISTEMA DE OTIMIZAÇÃO DE ROTAS LOGÍSTICAS (SORL) ===\n");
                Console.WriteLine("O sistema processa grafos no formato DIMACS.");
                Console.WriteLine("\nOpções:");
                Console.WriteLine("1 - Processar TODOS os arquivos da pasta grafos_dimacs automaticamente (PADRÃO)");
                Console.WriteLine("2 - Carregar um arquivo DIMACS específico para análise interativa");
                Console.Write("\nEscolha uma opção (pressione ENTER para opção 1): ");
                
                string? escolhaInicial = Console.ReadLine();
                int opcaoInicial = string.IsNullOrWhiteSpace(escolhaInicial) ? 1 : int.Parse(escolhaInicial);

                if (opcaoInicial == 1)
                {
                    ExecutarTodosArquivosDIMACS();
                    Console.WriteLine("\nPrograma finalizado. Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    return;
                }
                else if (opcaoInicial == 2)
                {
                    Console.WriteLine("\nInforme o caminho do arquivo DIMACS:");
                    Console.WriteLine("Exemplo: grafos_dimacs/grafo01.dimacs");
                    string? caminho = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(caminho))
                    {
                        Console.WriteLine("Caminho vazio. Processando todos os arquivos automaticamente...");
                        ExecutarTodosArquivosDIMACS();
                        Console.WriteLine("\nPrograma finalizado. Pressione qualquer tecla para sair...");
                        Console.ReadKey();
                        return;
                    }

                    string caminhoCompleto = ObterCaminhoArquivo(caminho);
                    grafo = new Grafo(caminhoCompleto);
                    arquivoAtual = caminho;
                    Console.WriteLine($"\nGrafo carregado do arquivo: {caminhoCompleto}");
                    Console.WriteLine($"Grafo carregado com {grafo.NumVertices} vértices e {grafo.NumArestas} arestas.");

                    int opcao;
                    do
                    {
                        ExibirMenu();
                        Console.Write("Escolha uma opção: ");
                        if (!int.TryParse(Console.ReadLine(), out opcao))
                        {
                            Console.WriteLine("Por favor, insira um número válido.");
                            continue;
                        }
                        ProcessarEscolha(opcao);

                    } while (opcao != 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro fatal: {ex.Message}");
                SalvarLog("Erro fatal: " + ex.Message);
            }

            Console.WriteLine("Programa finalizado. Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        /// <summary>
        /// Exibe o menu principal de opções para o usuário.
        /// </summary>
        /// <summary>
        /// Exibe o menu principal de opções para o usuário.
        /// </summary>
        private static void ExibirMenu()
        {
            Console.WriteLine("\n===== MENU DE ANÁLISE DE GRAFOS =====");
            Console.WriteLine("1 - Roteamento de menor custo (Dijkstra/Bellman-Ford)");
            Console.WriteLine("2 - Fluxo máximo (Ford-Fulkerson/Edmonds-Karp)");
            Console.WriteLine("3 - Árvore geradora mínima (Kruskal/Prim/Boruvka)");
            Console.WriteLine("4 - Coloração de vértices (Gulosa/DSATUR/Welsh-Powell)");
            Console.WriteLine("5 - Ciclo Euleriano");
            Console.WriteLine("6 - Ciclo Hamiltoniano");
            Console.WriteLine("7 - Executar todas as análises (grafo atual)");
            Console.WriteLine("8 - Gerar relatório completo");
            Console.WriteLine("9 - Informações do grafo atual");
            Console.WriteLine("10 - Executar todas as análises em TODOS os arquivos DIMACS");
            Console.WriteLine("0 - Sair");
        }

        /// <summary>
        /// Processa a escolha do usuário a partir do menu.
        /// </summary>
        /// <param name="opcao">A opção escolhida pelo usuário.</param>
        /// <summary>
        /// Processa a escolha do usuário a partir do menu.
        /// </summary>
        /// <param name="opcao">A opção escolhida pelo usuário.</param>
        private static void ProcessarEscolha(int opcao)
        {
            try
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
                    case 8:
                        GerarRelatorioCompleto();
                        break;
                    case 9:
                        ExibirInformacoesGrafo();
                        break;
                    case 10:
                        ExecutarTodosArquivosDIMACS();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                SalvarLog("Erro ao executar opção: " + ex.Message);
                Console.WriteLine("Erro durante a execução. Consulte o log.");
            }
        }

        /// <summary>
        /// Executa a análise de roteamento de menor custo.
        /// </summary>
        /// <summary>
        /// Executa a análise de roteamento de menor custo.
        /// </summary>
        private static void ExecutarRoteamentoMenorCusto()
        {
            Console.Write("Vértice de origem: ");
            int origem = int.Parse(Console.ReadLine());

            Console.Write("Vértice de destino: ");
            int destino = int.Parse(Console.ReadLine());

            Console.Write("Escolha o algoritmo (1-Dijkstra, 2-Bellman-Ford): ");
            int escolha = int.Parse(Console.ReadLine());

            var caminhoMinimo = new AlgoritmoCaminhoMinimo(grafo);
            ResultadoCaminho resultado = null;

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            if (escolha == 1)
            {
                resultado = caminhoMinimo.Dijkstra(origem, destino);
            }
            else if (escolha == 2)
            {
                resultado = caminhoMinimo.BellmanFord(origem, destino);
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                SalvarLog("Tentativa de executar roteamento com algoritmo inválido.");
                return;
            }

            medidor.Parar();

            Console.WriteLine("\n--- Resultado do Roteamento ---");
            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao} ms");
            Console.WriteLine($"Tempo total (incluindo entrada): {medidor.ObterTempoDecorrido()} ms");

            if (resultado.CaminhoEncontrado)
            {
                Console.WriteLine($"Custo total de {origem} para {destino}: {resultado.CustoTotal}");
                Console.WriteLine($"Caminho: {string.Join(" -> ", resultado.Caminho)}");
            }
            else
            {
                Console.WriteLine($"Não há caminho do vértice {origem} para o vértice {destino}");
            }

            SalvarLog($"Roteamento de menor custo executado. Algoritmo: {resultado.AlgoritmoUsado}, Origem: {origem}, Destino: {destino}, Encontrado: {resultado.CaminhoEncontrado}");
        }

        /// <summary>
        /// Executa a análise de fluxo máximo.
        /// </summary>
        /// <summary>
        /// Executa a análise de fluxo máximo.
        /// </summary>
        private static void ExecutarFluxoMaximo()
        {
            Console.Write("Vértice origem: ");
            int origem = int.Parse(Console.ReadLine());

            Console.Write("Vértice destino: ");
            int destino = int.Parse(Console.ReadLine());

            Console.Write("Escolha o algoritmo (1-Ford-Fulkerson, 2-Edmonds-Karp): ");
            int escolha = int.Parse(Console.ReadLine());

            var fluxoAlgoritmo = new AlgoritmoFluxoMaximo(grafo);
            ResultadoFluxo resultado = null;

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            if (escolha == 1)
            {
                resultado = fluxoAlgoritmo.FordFulkerson(origem, destino);
            }
            else if (escolha == 2)
            {
                resultado = fluxoAlgoritmo.EdmondsKarp(origem, destino);
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                SalvarLog("Tentativa de executar fluxo máximo com algoritmo inválido.");
                return;
            }

            medidor.Parar();

            Console.WriteLine($"\n--- Resultado do Fluxo Máximo ---");
            Console.WriteLine($"Algoritmo: {(escolha == 1 ? "Ford-Fulkerson" : "Edmonds-Karp")}");
            Console.WriteLine($"Fluxo máximo de {origem} para {destino}: {resultado.FluxoMaximo}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao} ms");
            Console.WriteLine($"Tempo total (incluindo entrada): {medidor.ObterTempoDecorrido()} ms");

            if (resultado.CorteMinimo != null && resultado.CorteMinimo.Count > 0)
            {
                Console.WriteLine($"\nCorte mínimo (capacidade: {resultado.FluxoMaximo}):");
                foreach (var aresta in resultado.CorteMinimo)
                {
                    Console.WriteLine($"  {aresta.Origem} -> {aresta.Destino}");
                }
            }

            if (resultado.FluxoPorAresta != null && resultado.FluxoPorAresta.Count > 0)
            {
                Console.WriteLine($"\nFluxo por aresta:");
                foreach (var fluxoAresta in resultado.FluxoPorAresta)
                {
                    if (fluxoAresta.Value > 0)
                    {
                        Console.WriteLine($"  {fluxoAresta.Key.Item1} -> {fluxoAresta.Key.Item2}: {fluxoAresta.Value:F2}");
                    }
                }
            }

            SalvarLog($"Fluxo máximo executado. Algoritmo: {(escolha == 1 ? "Ford-Fulkerson" : "Edmonds-Karp")}, Origem: {origem}, Destino: {destino}, Fluxo: {resultado.FluxoMaximo}");
        }

        /// <summary>
        /// Executa a análise de árvore geradora mínima.
        /// </summary>
        /// <summary>
        /// Executa a análise de árvore geradora mínima.
        /// </summary>
        private static void ExecutarArvoreGeradora()
        {
            Console.WriteLine("Escolha o algoritmo:");
            Console.WriteLine("1 - Kruskal");
            Console.WriteLine("2 - Prim (precisa de vértice inicial)");
            Console.WriteLine("3 - Boruvka");
            Console.Write("Opção: ");
            int escolha = int.Parse(Console.ReadLine());

            var arvoreAlgoritmo = new AlgoritmoArvoreGeradora(grafo);
            ResultadoArvore resultado = null;

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            if (escolha == 1)
            {
                resultado = arvoreAlgoritmo.Kruskal();
            }
            else if (escolha == 2)
            {
                Console.Write("Vértice inicial: ");
                int verticeInicial = int.Parse(Console.ReadLine());
                resultado = arvoreAlgoritmo.Prim(verticeInicial);
            }
            else if (escolha == 3)
            {
                resultado = arvoreAlgoritmo.Boruvka();
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                SalvarLog("Tentativa de executar algoritmo de árvore geradora com opção inválida.");
                return;
            }

            medidor.Parar();

            Console.WriteLine($"\n--- Resultado da Árvore Geradora Mínima ---");
            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Árvore encontrada: {resultado.ArvoreEncontrada}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao} ms");
            Console.WriteLine($"Tempo total (incluindo entrada): {medidor.ObterTempoDecorrido()} ms");

            if (resultado.ArvoreEncontrada && resultado.Arestas != null)
            {
                Console.WriteLine($"\nCusto total: {resultado.CustoTotal:F2}");
                Console.WriteLine($"Número de arestas: {resultado.Arestas.Count}");

                Console.WriteLine("\nArestas da árvore geradora mínima:");
                foreach (var aresta in resultado.Arestas)
                {
                    Console.WriteLine($"  {aresta.Origem} -> {aresta.Destino} (peso {aresta.Peso:F2})");
                }

                Console.WriteLine($"\nRepresentação completa:");
                Console.WriteLine(resultado.ToString());
            }
            else
            {
                Console.WriteLine("\nNão foi possível encontrar uma árvore geradora mínima.");
                Console.WriteLine("O grafo pode não ser conexo ou ter outros problemas.");
            }

            SalvarLog($"Árvore geradora mínima executada. Algoritmo: {resultado.AlgoritmoUsado}, " +
                      $"Encontrada: {resultado.ArvoreEncontrada}, Custo: {resultado.CustoTotal:F2}");
        }

        /// <summary>
        /// Executa a análise de coloração de vértices.
        /// </summary>
        private static void ExecutarColoracao()
        {
            Console.WriteLine("Escolha o algoritmo de coloração:");
            Console.WriteLine("1 - Coloração Gulosa");
            Console.WriteLine("2 - DSATUR");
            Console.WriteLine("3 - Welsh-Powell");
            Console.Write("Opção: ");
            int escolha = int.Parse(Console.ReadLine());

            var algoritmoColoracao = new AlgoritmoColoracao(grafo);
            ResultadoColoracao resultado = null;

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            switch (escolha)
            {
                case 1:
                    resultado = algoritmoColoracao.ColoracaoGulosa();
                    break;
                case 2:
                    resultado = algoritmoColoracao.ColoracaoDSATUR();
                    break;
                case 3:
                    resultado = algoritmoColoracao.ColoracaoWelshPowell();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    SalvarLog("Tentativa de executar coloração com algoritmo inválido.");
                    return;
            }

            medidor.Parar();

            Console.WriteLine($"\n--- Resultado da Coloração ---");
            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Número de cores utilizadas: {resultado.NumeroTurnos}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao} ms");
            Console.WriteLine($"Tempo total (incluindo entrada): {medidor.ObterTempoDecorrido()} ms");

            Console.WriteLine("\nColoração dos vértices por cor:");

            foreach (var grupo in resultado.GruposPorCor)
            {
                Console.Write($"Cor {grupo.Key}: ");
                Console.WriteLine(string.Join(", ", grupo.Value.Select(v => v + 1))); // +1 se vértices começam em 1
            }

            Console.WriteLine("\nColoração individual dos vértices:");
            foreach (var item in resultado.CorPorVertice)
            {
                Console.WriteLine($"Vértice {item.Key + 1}: cor {item.Value}"); // +1 se vértices começam em 1
            }

            Console.WriteLine($"\nRepresentação completa:");
            Console.WriteLine(resultado.ToString());

            SalvarLog($"Coloração executada. Algoritmo: {resultado.AlgoritmoUsado}, " +
                      $"Número de cores: {resultado.NumeroTurnos}");
        }

        /// <summary>
        /// Executa a análise de ciclo Euleriano.
        /// </summary>
        /// <summary>
        /// Executa a análise de ciclo Euleriano.
        /// </summary>
        private static void ExecutarCicloEuleriano()
        {
            var algoritmoCiclo = new AlgoritmoCiclo(grafo);

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = algoritmoCiclo.VerificarCicloEuleriano();
            resultado.TipoCiclo = "Euleriano";
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();

            if (!resultado.ExisteCiclo || resultado.Sequencia == null || resultado.Sequencia.Count == 0)
            {
                Console.WriteLine("O grafo NÃO possui ciclo Euleriano.");
                Console.WriteLine("Condição: todos os vértices devem ter grau par.");
            }
            else
            {
                Console.WriteLine("Ciclo Euleriano encontrado!");

                var cicloFormatado = resultado.Sequencia.Select(v => v + 1).ToList(); // +1 se vértices começam em 1

                Console.WriteLine("Ciclo Euleriano:");
                Console.WriteLine(string.Join(" -> ", cicloFormatado));

                Console.WriteLine($"\nNúmero de arestas percorridas: {resultado.NumeroArestasPercorridas}");
                Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao:F2} ms");
            }

            SalvarLog($"Verificação de ciclo Euleriano executada. Encontrado: {resultado.ExisteCiclo}");
        }

        /// <summary>
        /// Executa a análise de ciclo Hamiltoniano.
        /// </summary>
        /// <summary>
        /// Executa a análise de ciclo Hamiltoniano.
        /// </summary>
        private static void ExecutarCicloHamiltoniano()
        {
            var algoritmoCiclo = new AlgoritmoCiclo(grafo);

            var medidor = new MedidorPerformance();
            medidor.Iniciar();

            var resultado = algoritmoCiclo.VerificarCicloHamiltoniano();
            resultado.TipoCiclo = "Hamiltoniano";
            resultado.TempoExecucao = medidor.ObterTempoDecorrido();

            if (!resultado.ExisteCiclo || resultado.Sequencia == null || resultado.Sequencia.Count == 0)
            {
                Console.WriteLine("O grafo NÃO possui ciclo Hamiltoniano.");
            }
            else
            {
                Console.WriteLine("Ciclo Hamiltoniano:");

                var cicloFormatado = resultado.Sequencia.Select(v => v + 1).ToList();

                if (cicloFormatado.Count > 0)
                {
                    cicloFormatado.Add(cicloFormatado[0]);
                }

                Console.WriteLine(string.Join(" -> ", cicloFormatado));

                Console.WriteLine($"\nDetalhes:");
                Console.WriteLine($"- Número de vértices: {cicloFormatado.Count - 1}"); 
                Console.WriteLine($"- Tempo de execução: {resultado.TempoExecucao:F2} ms");
            }

            SalvarLog("Verificação de ciclo Hamiltoniano executada.");
        }

        /// <summary>
        /// Executa todas as análises disponíveis em sequência.
        /// </summary>
        /// <summary>
        /// Executa todas as análises disponíveis em sequência.
        /// </summary>
        private static void ExecutarTodasAnalises()
        {
            try
            {
                Console.WriteLine("\n=== EXECUTANDO TODAS AS ANÁLISES ===\n");

                if (grafo == null || grafo.NumVertices == 0)
                {
                    Console.WriteLine("Erro: Grafo não carregado ou vazio.");
                    SalvarLog("Tentativa de executar análises sem grafo carregado.");
                    return;
                }

                var medidorTotal = new MedidorPerformance();
                medidorTotal.Iniciar();

                // 1. Roteamento de menor custo (do primeiro ao último vértice)
                if (grafo.NumVertices >= 2)
                {
                    Console.WriteLine("1. Executando roteamento de menor custo (vértice 1 -> vértice " + grafo.NumVertices + ")...");
                    ExecutarRoteamentoMenorCustoAutomatico(1, grafo.NumVertices);
                    Console.WriteLine();
                }

                // 2. Fluxo máximo (do primeiro ao último vértice)
                if (grafo.NumVertices >= 2)
                {
                    Console.WriteLine("2. Executando fluxo máximo (vértice 1 -> vértice " + grafo.NumVertices + ")...");
                    ExecutarFluxoMaximoAutomatico(1, grafo.NumVertices);
                    Console.WriteLine();
                }

                // 3. Árvore geradora mínima (Kruskal)
                Console.WriteLine("3. Executando árvore geradora mínima (Kruskal)...");
                ExecutarArvoreGeradoraAutomatico(1); // Kruskal
                Console.WriteLine();

                // 4. Coloração (Gulosa)
                Console.WriteLine("4. Executando coloração de vértices (Gulosa)...");
                ExecutarColoracaoAutomatico(1); // Gulosa
                Console.WriteLine();

                // 5. Ciclo Euleriano
                Console.WriteLine("5. Executando verificação de ciclo Euleriano...");
                ExecutarCicloEuleriano();
                Console.WriteLine();

                // 6. Ciclo Hamiltoniano
                Console.WriteLine("6. Executando verificação de ciclo Hamiltoniano...");
                ExecutarCicloHamiltoniano();
                Console.WriteLine();

                medidorTotal.Parar();
                Console.WriteLine($"=== TODAS AS ANÁLISES CONCLUÍDAS ===");
                Console.WriteLine($"Tempo total: {medidorTotal.ObterTempoDecorrido():F2} ms");

                SalvarLog($"Todas as análises executadas. Tempo total: {medidorTotal.ObterTempoDecorrido():F2} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar todas as análises: {ex.Message}");
                SalvarLog($"ERRO em ExecutarTodasAnalises: {ex.Message}");
            }
        }

        /// <summary>
        /// Executa roteamento de menor custo automaticamente.
        /// </summary>
        /// <summary>
        /// Executa roteamento de menor custo automaticamente.
        /// </summary>
        private static void ExecutarRoteamentoMenorCustoAutomatico(int origem, int destino)
        {
            var caminhoMinimo = new AlgoritmoCaminhoMinimo(grafo);
            var resultado = caminhoMinimo.Dijkstra(origem, destino);

            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao:F2} ms");

            if (resultado.CaminhoEncontrado)
            {
                Console.WriteLine($"Custo total de {origem} para {destino}: {resultado.CustoTotal:F2}");
                Console.WriteLine($"Caminho: {string.Join(" -> ", resultado.Caminho)}");
            }
            else
            {
                Console.WriteLine($"Não há caminho do vértice {origem} para o vértice {destino}");
            }

            SalvarLog($"Roteamento automático: {origem} -> {destino}, Encontrado: {resultado.CaminhoEncontrado}");
        }

        /// <summary>
        /// Executa fluxo máximo automaticamente.
        /// </summary>
        /// <summary>
        /// Executa fluxo máximo automaticamente.
        /// </summary>
        private static void ExecutarFluxoMaximoAutomatico(int origem, int destino)
        {
            var fluxoAlgoritmo = new AlgoritmoFluxoMaximo(grafo);
            var resultado = fluxoAlgoritmo.EdmondsKarp(origem, destino);

            Console.WriteLine($"Algoritmo: Edmonds-Karp");
            Console.WriteLine($"Fluxo máximo de {origem} para {destino}: {resultado.FluxoMaximo:F2}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao:F2} ms");

            if (resultado.CorteMinimo != null && resultado.CorteMinimo.Count > 0)
            {
                Console.WriteLine($"Corte mínimo: {resultado.CorteMinimo.Count} arestas");
            }

            SalvarLog($"Fluxo máximo automático: {origem} -> {destino}, Fluxo: {resultado.FluxoMaximo:F2}");
        }

        /// <summary>
        /// Executa árvore geradora automaticamente.
        /// </summary>
        /// <summary>
        /// Executa árvore geradora automaticamente.
        /// </summary>
        private static void ExecutarArvoreGeradoraAutomatico(int algoritmo)
        {
            var arvoreAlgoritmo = new AlgoritmoArvoreGeradora(grafo);
            ResultadoArvore resultado = null;

            if (algoritmo == 1)
            {
                resultado = arvoreAlgoritmo.Kruskal();
            }
            else if (algoritmo == 2)
            {
                resultado = arvoreAlgoritmo.Prim(1);
            }
            else
            {
                resultado = arvoreAlgoritmo.Boruvka();
            }

            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Árvore encontrada: {resultado.ArvoreEncontrada}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao:F2} ms");

            if (resultado.ArvoreEncontrada && resultado.Arestas != null)
            {
                Console.WriteLine($"Custo total: {resultado.CustoTotal:F2}");
                Console.WriteLine($"Número de arestas: {resultado.Arestas.Count}");
            }

            SalvarLog($"Árvore geradora automática: {resultado.AlgoritmoUsado}, Custo: {resultado.CustoTotal:F2}");
        }

        /// <summary>
        /// Executa coloração automaticamente.
        /// </summary>
        /// <summary>
        /// Executa coloração automaticamente.
        /// </summary>
        private static void ExecutarColoracaoAutomatico(int algoritmo)
        {
            var algoritmoColoracao = new AlgoritmoColoracao(grafo);
            ResultadoColoracao resultado = null;

            if (algoritmo == 1)
            {
                resultado = algoritmoColoracao.ColoracaoGulosa();
            }
            else if (algoritmo == 2)
            {
                resultado = algoritmoColoracao.ColoracaoDSATUR();
            }
            else
            {
                resultado = algoritmoColoracao.ColoracaoWelshPowell();
            }

            Console.WriteLine($"Algoritmo: {resultado.AlgoritmoUsado}");
            Console.WriteLine($"Número de cores utilizadas: {resultado.NumeroTurnos}");
            Console.WriteLine($"Tempo de execução: {resultado.TempoExecucao:F2} ms");

            SalvarLog($"Coloração automática: {resultado.AlgoritmoUsado}, Cores: {resultado.NumeroTurnos}");
        }

        /// <summary>
        /// Gera um relatório completo com os resultados de todas as análises.
        /// </summary>
        /// <summary>
        /// Gera um relatório completo com os resultados de todas as análises.
        /// </summary>
        private static void GerarRelatorioCompleto()
        {
            try
            {
                Console.WriteLine("\n=== GERANDO RELATÓRIO COMPLETO ===\n");

                var medidor = new MedidorPerformance();
                medidor.Iniciar();

                var gerador = new GeradorRelatorio("relatorio_grafos");

                gerador.AdicionarSecao("INFORMAÇÕES GERAIS",
                    $"Data e hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

                if (grafo != null)
                {
                    gerador.AdicionarSecao("CARACTERÍSTICAS DO GRAFO", grafo.ToString());
                }

                gerador.AdicionarSecao("RESULTADOS DAS ANÁLISES",
                    "Execute as análises individualmente (opções 1-6) para gerar resultados.");

                var estatisticas = new List<List<string>>
                {
                    new List<string> { "Análise", "Status", "Observações" },
                    new List<string> { "Roteamento Menor Custo", "Disponível", "Executar opção 1" },
                    new List<string> { "Fluxo Máximo", "Disponível", "Executar opção 2" },
                    new List<string> { "Árvore Geradora", "Disponível", "Executar opção 3" },
                    new List<string> { "Coloração", "Disponível", "Executar opção 4" },
                    new List<string> { "Ciclo Euleriano", "Disponível", "Executar opção 5" },
                    new List<string> { "Ciclo Hamiltoniano", "Disponível", "Executar opção 6" }
                };

                gerador.AdicionarTabela("ESTATÍSTICAS DAS ANÁLISES", estatisticas);

                string caminhoArquivo = gerador.SalvarRelatorio();

                medidor.Parar();

                Console.WriteLine("Relatório completo gerado com sucesso!");
                Console.WriteLine($"Tempo para gerar relatório: {medidor.ObterTempoDecorrido():F2} ms");
                Console.WriteLine($"Arquivo salvo em: {caminhoArquivo}");


                Console.WriteLine("\nO relatório contém informações básicas do grafo.");
                Console.WriteLine("Para resultados de análises, execute-as primeiro.");

                SalvarLog($"Relatório completo gerado. Arquivo: {caminhoArquivo}, Tempo: {medidor.ObterTempoDecorrido():F2} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório: {ex.Message}");
                SalvarLog($"ERRO em GerarRelatorioCompleto: {ex.Message}");
            }
        }

        /// <summary>
        /// Tenta encontrar o arquivo DIMACS em diferentes locais possíveis.
        /// </summary>
        /// <param name="caminho">Caminho fornecido pelo usuário.</param>
        /// <returns>Caminho completo do arquivo encontrado.</returns>
        /// <summary>
        /// Tenta encontrar o arquivo DIMACS em diferentes locais possíveis.
        /// </summary>
        /// <param name="caminho">Caminho fornecido pelo usuário.</param>
        /// <returns>Caminho completo do arquivo encontrado.</returns>
        private static string ObterCaminhoArquivo(string caminho)
        {
            // Se o caminho já é absoluto e existe, retorna
            if (Path.IsPathRooted(caminho) && File.Exists(caminho))
            {
                return caminho;
            }

            string diretorioAtual = Directory.GetCurrentDirectory();
            string nomeArquivo = Path.GetFileName(caminho);
            
            // Lista de possíveis caminhos a tentar
            var caminhosPossiveis = new List<string>
            {
                caminho, // Caminho original (relativo ao diretório atual)
                Path.Combine(diretorioAtual, caminho), // Relativo ao diretório atual
                Path.Combine(diretorioAtual, "grafos_dimacs", nomeArquivo), // Se executado de TP_Grafos
                Path.Combine(diretorioAtual, "TP_Grafos", "grafos_dimacs", nomeArquivo), // Se executado da raiz
                Path.Combine("grafos_dimacs", nomeArquivo), // Relativo simples
            };

            // Se o caminho contém "TP_Grafos", remove essa parte e tenta
            if (caminho.Contains("TP_Grafos/") || caminho.Contains("TP_Grafos\\"))
            {
                string caminhoLimpo = caminho.Replace("TP_Grafos/", "").Replace("TP_Grafos\\", "");
                caminhosPossiveis.Add(Path.Combine(diretorioAtual, caminhoLimpo));
                caminhosPossiveis.Add(caminhoLimpo);
            }

            // Tentar cada caminho
            foreach (var caminhoTeste in caminhosPossiveis.Distinct())
            {
                try
                {
                    if (File.Exists(caminhoTeste))
                    {
                        return Path.GetFullPath(caminhoTeste);
                    }
                }
                catch
                {
                    // Ignorar erros de caminho inválido
                }
            }

            // Se nenhum caminho funcionou, lança exceção com mensagem útil
            throw new FileNotFoundException(
                $"Arquivo DIMACS não encontrado: {caminho}\n" +
                $"Diretório atual: {diretorioAtual}\n" +
                $"Tente usar: grafos_dimacs/{nomeArquivo}",
                caminho);
        }

        /// <summary>
        /// Exibe informações sobre o grafo atual.
        /// </summary>
        /// <summary>
        /// Exibe informações sobre o grafo atual.
        /// </summary>
        private static void ExibirInformacoesGrafo()
        {
            if (grafo == null)
            {
                Console.WriteLine("Nenhum grafo carregado.");
                return;
            }

            Console.WriteLine("\n=== INFORMAÇÕES DO GRAFO ===");
            Console.WriteLine($"Número de vértices: {grafo.NumVertices}");
            Console.WriteLine($"Número de arestas: {grafo.NumArestas}");
            Console.WriteLine($"Grafo conexo: {(grafo.EhConexo() ? "Sim" : "Não")}");
            Console.WriteLine($"Arquivo: {arquivoAtual ?? "Grafo criado manualmente"}");
            
            Console.WriteLine("\nGraus dos vértices:");
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                int grauEntrada = grafo.ObterGrauEntrada(i);
                int grauSaida = grafo.ObterGrauSaida(i);
                Console.WriteLine($"  Vértice {i}: Grau entrada={grauEntrada}, Grau saída={grauSaida}, Total={grauEntrada + grauSaida}");
            }

            SalvarLog("Informações do grafo exibidas.");
        }

        /// <summary>
        /// Executa todas as análises em todos os arquivos DIMACS da pasta grafos_dimacs.
        /// </summary>
        /// <summary>
        /// Executa todas as análises em todos os arquivos DIMACS da pasta grafos_dimacs.
        /// </summary>
        private static void ExecutarTodosArquivosDIMACS()
        {
            try
            {
                Console.WriteLine("\n=== EXECUTANDO ANÁLISES EM TODOS OS ARQUIVOS DIMACS ===\n");

                string pastaGrafos = ObterPastaGrafosDimacs();
                
                if (!Directory.Exists(pastaGrafos))
                {
                    Console.WriteLine($"Erro: Pasta não encontrada: {pastaGrafos}");
                    SalvarLog($"Pasta grafos_dimacs não encontrada: {pastaGrafos}");
                    return;
                }

                var arquivos = Directory.GetFiles(pastaGrafos, "*.dimacs")
                    .OrderBy(f => Path.GetFileName(f))
                    .ToList();

                if (arquivos.Count == 0)
                {
                    Console.WriteLine($"Nenhum arquivo .dimacs encontrado em: {pastaGrafos}");
                    SalvarLog($"Nenhum arquivo .dimacs encontrado em: {pastaGrafos}");
                    return;
                }

                Console.WriteLine($"Encontrados {arquivos.Count} arquivo(s) DIMACS:\n");
                foreach (var arquivo in arquivos)
                {
                    Console.WriteLine($"  - {Path.GetFileName(arquivo)}");
                }
                Console.WriteLine();

                var geradorRelatorio = new GeradorRelatorio("relatorio_todos_grafos");
                geradorRelatorio.AdicionarSecao("RELATÓRIO COMPLETO - TODOS OS GRAFOS",
                    $"Data e hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                    $"Total de arquivos processados: {arquivos.Count}");

                int arquivoNum = 1;
                foreach (var caminhoArquivo in arquivos)
                {
                    string nomeArquivo = Path.GetFileName(caminhoArquivo);
                    Console.WriteLine($"\n{new string('=', 70)}");
                    Console.WriteLine($"PROCESSANDO ARQUIVO {arquivoNum}/{arquivos.Count}: {nomeArquivo}");
                    Console.WriteLine($"{new string('=', 70)}\n");

                    try
                    {
                        grafo = new Grafo(caminhoArquivo);
                        arquivoAtual = nomeArquivo;

                        Console.WriteLine($"Grafo carregado: {grafo.NumVertices} vértices, {grafo.NumArestas} arestas\n");

                        var medidorTotal = new MedidorPerformance();
                        medidorTotal.Iniciar();

                        if (grafo.NumVertices >= 2)
                        {
                            Console.WriteLine("1. Roteamento de menor custo...");
                            ExecutarRoteamentoMenorCustoAutomatico(1, grafo.NumVertices);
                            Console.WriteLine();
                        }

                        if (grafo.NumVertices >= 2)
                        {
                            Console.WriteLine("2. Fluxo máximo...");
                            ExecutarFluxoMaximoAutomatico(1, grafo.NumVertices);
                            Console.WriteLine();
                        }

                        Console.WriteLine("3. Árvore geradora mínima...");
                        ExecutarArvoreGeradoraAutomatico(1);
                        Console.WriteLine();

                        Console.WriteLine("4. Coloração de vértices...");
                        ExecutarColoracaoAutomatico(1);
                        Console.WriteLine();

                        Console.WriteLine("5. Ciclo Euleriano...");
                        ExecutarCicloEuleriano();
                        Console.WriteLine();

                        Console.WriteLine("6. Ciclo Hamiltoniano...");
                        ExecutarCicloHamiltoniano();
                        Console.WriteLine();

                        medidorTotal.Parar();

                        geradorRelatorio.AdicionarSecao($"GRAFO: {nomeArquivo}",
                            $"Vértices: {grafo.NumVertices}, Arestas: {grafo.NumArestas}\n" +
                            $"Tempo total: {medidorTotal.ObterTempoDecorrido():F2} ms");

                        SalvarLog($"Arquivo {nomeArquivo} processado com sucesso. Tempo: {medidorTotal.ObterTempoDecorrido():F2} ms");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERRO ao processar {nomeArquivo}: {ex.Message}");
                        geradorRelatorio.AdicionarSecao($"GRAFO: {nomeArquivo} - ERRO",
                            $"Erro ao processar: {ex.Message}");
                        SalvarLog($"ERRO ao processar {nomeArquivo}: {ex.Message}");
                    }

                    arquivoNum++;
                }

                string caminhoRelatorio = geradorRelatorio.SalvarRelatorio();
                Console.WriteLine($"\n{new string('=', 70)}");
                Console.WriteLine("=== PROCESSAMENTO CONCLUÍDO ===");
                Console.WriteLine($"Relatório salvo em: {caminhoRelatorio}");
                Console.WriteLine($"{new string('=', 70)}\n");

                SalvarLog($"Processamento de todos os arquivos concluído. Relatório: {caminhoRelatorio}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar análises em todos os arquivos: {ex.Message}");
                SalvarLog($"ERRO em ExecutarTodosArquivosDIMACS: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém o caminho da pasta grafos_dimacs.
        /// </summary>
        /// <returns>Caminho da pasta grafos_dimacs.</returns>
        /// <summary>
        /// Obtém o caminho da pasta grafos_dimacs.
        /// </summary>
        /// <returns>Caminho da pasta grafos_dimacs.</returns>
        private static string ObterPastaGrafosDimacs()
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            
            var caminhosPossiveis = new List<string>
            {
                Path.Combine(diretorioAtual, "grafos_dimacs"), // Se executado de TP_Grafos
                Path.Combine(diretorioAtual, "TP_Grafos", "grafos_dimacs"), // Se executado da raiz
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grafos_dimacs"), // Relativo ao executável
                "grafos_dimacs", // Relativo simples
            };

            foreach (var caminho in caminhosPossiveis)
            {
                if (Directory.Exists(caminho))
                {
                    return Path.GetFullPath(caminho);
                }
            }

            return caminhosPossiveis[0];
        }

        /// <summary>
        /// Salva uma mensagem no arquivo de log.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser salva.</param>
        /// <summary>
        /// Salva uma mensagem no arquivo de log.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser salva.</param>
        private static void SalvarLog(string mensagem)
        {
            try
            {
                var log = new ArquivoLog(arquivoLogAtual);

                try
                {
                    log.Escrever(mensagem);
                }
                catch
                {
                    log.EscreverInfo(mensagem);
                }

                log.Fechar();

                Console.WriteLine($"[LOG] {mensagem}");
            }
            catch (Exception ex)
            {
                try
                {
                    // Fallback 1: Cria um arquivo de log alternativo
                    string linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}";
                    File.AppendAllText("fallback_log.txt", linha + Environment.NewLine);
                    Console.WriteLine($"[LOG FALLBACK] {mensagem}");
                }
                catch
                {
                    // Fallback 2: Apenas mostra no console
                    Console.WriteLine($"[LOG CONSOLE] {DateTime.Now:HH:mm:ss} - {mensagem}");
                }
            }
        }
    }
}
