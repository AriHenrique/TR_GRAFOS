# Trabalhos de Algoritmos em Grafos

Repositório com dois trabalhos práticos da disciplina de Algoritmos em Grafos, implementados em C# (.NET 8.0).

- **TP_Grafos**: sistema de otimização logística que processa grafos no formato DIMACS e executa análises clássicas (caminho mínimo, fluxo máximo, AGM, coloração e ciclos).
- **IlhaTesouro**: solução do problema Beecrowd 2098 utilizando busca binária e BFS.

## Requisitos
- .NET SDK 8.0 ou superior
- macOS, Linux ou Windows

## Execução rápida

### Trabalho 1 (TP_Grafos)
```bash
cd TP_Grafos
dotnet build
dotnet run
```

- **Modo automático**: pressione ENTER ao iniciar para processar todos os arquivos DIMACS em `grafos_dimacs/` e gerar um relatório consolidado.
- **Modo interativo**: escolha a opção 2 para carregar um arquivo específico e acessar o menu de análises (caminho mínimo, fluxo, AGM, coloração, ciclos, relatório e informações do grafo).

### Trabalho 2 (IlhaTesouro)
```bash
cd IlhaTesouro
dotnet build
dotnet run
```

Entrada de exemplo:
```bash
echo "3 3
2 3 4
3 4 5
4 5 6" | dotnet run
```
Saída esperada: `1`

## Estrutura
```
TR_GRAFOS/
├── TP_Grafos/            # Código do sistema logístico
├── IlhaTesouro/          # Código do problema 2098
├── trabalho1.md          # Enunciado resumido do TP de logística
├── trabalho2.md          # Enunciado resumido do desafio Beecrowd
└── uml.mmd               # Diagrama em Mermaid
```

## Algoritmos utilizados
- Caminho mínimo: Dijkstra, Bellman-Ford
- Fluxo máximo: Ford-Fulkerson, Edmonds-Karp
- Árvore geradora mínima: Kruskal, Prim, Boruvka
- Coloração: heurística gulosa, DSATUR, Welsh-Powell
- Ciclos: Hierholzer (Euler), backtracking (Hamilton)
- Ilha do Tesouro: busca binária para tempo máximo + BFS para validação do percurso

## Formato DIMACS adotado
```
<num_vertices> <num_arestas>
<origem> <destino> <peso> <capacidade>
...
```
Exemplo:
```
6 12
1 2 2.5 10
1 3 3.0 7
2 4 1.5 12
3 4 2.0 8
```

## Respostas solicitadas (execução de 08/12/2025)
- O relatório `relatorio_todos_grafos_20251208_212631.txt` confirmou o processamento dos sete grafos DIMACS com tempos entre 2,06 ms e 14,52 ms.
- O grafo mais denso é o **GRAFO01.DIMACS** (6 vértices, 12 arestas), com densidade aproximada de 0,40, recomendando matriz de adjacência; ele também teve o maior tempo total (14,52 ms) pela execução completa de todos os algoritmos.
- O grafo mais esparso é o **GRAFO07.DIMACS** (100 vértices, 400 arestas), com densidade próxima de 0,04; a lista de adjacência é a melhor escolha e o tempo total foi de 4,99 ms mesmo com o maior número de vértices.
- Síntese por grafo com densidade estimada e estrutura sugerida:
  - **GRAFO01.DIMACS** – 6 vértices, 12 arestas, 14,52 ms, densidade ≈ 0,40 → matriz.
  - **GRAFO02.DIMACS** – 5 vértices, 6 arestas, 2,23 ms, densidade ≈ 0,30 → matriz se for necessário acesso direto; lista atende bem para inserções.
  - **GRAFO03.DIMACS** – 8 vértices, 10 arestas, 3,72 ms, densidade ≈ 0,18 → lista.
  - **GRAFO04.DIMACS** – 10 vértices, 15 arestas, 2,06 ms, densidade ≈ 0,17 → lista.
  - **GRAFO05.DIMACS** – 10 vértices, 30 arestas, 2,62 ms, densidade ≈ 0,33 → matriz.
  - **GRAFO06.DIMACS** – 50 vértices, 200 arestas, 2,25 ms, densidade ≈ 0,08 → lista.
  - **GRAFO07.DIMACS** – 100 vértices, 400 arestas, 4,99 ms, densidade ≈ 0,04 → lista.

## Contato
Trabalho acadêmico da disciplina Algoritmos em Grafos da PUC Minas (período 02/2025). Em caso de dúvidas, consulte os membros do grupo pelo Canvas.
