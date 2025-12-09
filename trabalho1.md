PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS
Algoritmos em Grafos – Trabalho Prático 02/2025

Instruções básicas
- Trabalho em trio, implementado em C#.
- Utilize apenas algoritmos estudados na disciplina.
- Apresente o código na data indicada no Canvas.
- Teste com os grafos DIMACS fornecidos.

1. Contexto
A Entrega Máxima Logística S.A. precisa de um Sistema de Otimização de Rotas Logísticas (SORL) para avaliar sua malha de transporte modelada como grafo direcionado e ponderado.

2. Modelagem
- Vértices: centros de distribuição ou pontos de entrega.
- Arestas: rotas rodoviárias com peso (custo) e capacidade (toneladas por dia).
- O sistema deve ler, armazenar e atualizar o grafo conforme a densidade para escolher lista ou matriz de adjacência.

3. Análises exigidas
- Roteamento de menor custo entre origem e destino.
- Fluxo máximo e corte mínimo entre dois vértices.
- Árvore geradora mínima para interligar todos os hubs ao menor custo.
- Coloração de vértices para planejar manutenções em turnos sem conflito.
- Verificação de ciclo Euleriano (arestas) e caminho Hamiltoniano (vértices) para inspeções únicas.

4. Critérios de avaliação
- Estrutura do grafo em C# com pesos e capacidades, adaptando a estrutura de dados à densidade.
- Implementação dos algoritmos adequados a cada problema, com justificativa no relatório.
- Registro dos resultados para os sete grafos DIMACS em arquivo de log.
- Relatório técnico com modelagem e interpretação dos resultados.

6. Respostas solicitadas (execução de 08/12/2025)
- O relatório `relatorio_todos_grafos_20251208_212631.txt` confirmou o processamento dos sete grafos DIMACS com tempos entre 2,06 ms e 14,52 ms.
- O grafo mais denso é o **GRAFO01.DIMACS** (6 vértices, 12 arestas), cuja densidade aproximada de 0,40 recomenda o uso de matriz de adjacência; ele também apresentou o maior tempo total de execução (14,52 ms), possivelmente pelo impacto do cálculo completo de todos os algoritmos em uma estrutura mais densa.
- O grafo mais esparso é o **GRAFO07.DIMACS** (100 vértices, 400 arestas), com densidade próxima de 0,04; aqui, a lista de adjacência é a escolha adequada e o tempo total foi de 4,99 ms mesmo com o maior número de vértices.
- Abaixo, a síntese por grafo com densidade estimada e estrutura sugerida:
  - **GRAFO01.DIMACS** – 6 vértices, 12 arestas, 14,52 ms, densidade ≈ 0,40 → matriz.
  - **GRAFO02.DIMACS** – 5 vértices, 6 arestas, 2,23 ms, densidade ≈ 0,30 → matriz se for necessário acesso direto; lista atende bem para inserções.
  - **GRAFO03.DIMACS** – 8 vértices, 10 arestas, 3,72 ms, densidade ≈ 0,18 → lista.
  - **GRAFO04.DIMACS** – 10 vértices, 15 arestas, 2,06 ms, densidade ≈ 0,17 → lista.
  - **GRAFO05.DIMACS** – 10 vértices, 30 arestas, 2,62 ms, densidade ≈ 0,33 → matriz.
  - **GRAFO06.DIMACS** – 50 vértices, 200 arestas, 2,25 ms, densidade ≈ 0,08 → lista.
  - **GRAFO07.DIMACS** – 100 vértices, 400 arestas, 4,99 ms, densidade ≈ 0,04 → lista.

5. Formato DIMACS
```
<num_vertices> <num_arestas>
<origem> <destino> <peso> <capacidade>
...
```
Exemplo:
```
5 6
1 2 2
1 4 2
2 3 5
3 4 1
4 5 3
5 1 4
```

Lista de adjacência correspondente:
```
1: (2,2), (4,2)
2: (3,5)
3: (4,1)
4: (5,3)
5: (1,4)
```

Matriz de adjacência (0 indica ausência de aresta):
```
    1  2  3  4  5
1 [ 0  2  0  2  0 ]
2 [ 0  0  5  0  0 ]
3 [ 0  0  0  1  0 ]
4 [ 0  0  0  0  3 ]
5 [ 4  0  0  0  0 ]
```
