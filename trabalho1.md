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
