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

## Contato
Trabalho acadêmico da disciplina Algoritmos em Grafos da PUC Minas (período 02/2025). Em caso de dúvidas, consulte os membros do grupo pelo Canvas.
