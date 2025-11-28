# TP_Grafos - Análise de Algoritmos em Grafos

Este projeto implementa e analisa diversos algoritmos em grafos, oferecendo uma ferramenta de linha de comando para executar análises como caminho mínimo, fluxo máximo, coloração, entre outros.

## Divisão de Tarefas

A implementação do projeto foi dividida entre três integrantes, agrupando as classes por afinidade para minimizar dependências e facilitar o desenvolvimento paralelo.

---

### **<Nome>: Núcleo do Grafo e Algoritmos Base**

Responsável pela estrutura de dados central do projeto e por um conjunto inicial de algoritmos e validações.

-   `Aresta.cs`
-   `Grafo.cs`
-   `ValidadorGrafo.cs`
-   `AlgoritmoCaminhoMinimo.cs`
-   `ResultadoCaminho.cs`

---

### **Aristides: Algoritmos Especializados e Estruturas de Dados Auxiliares**

Focado na implementação de algoritmos mais complexos que, em sua maioria, dependem apenas da estrutura base do grafo.

-   `UnionFind.cs`
-   `AlgoritmoArvoreGeradora.cs`
-   `ResultadoArvore.cs`
-   `AlgoritmoFluxoMaximo.cs`
-   `ResultadoFluxo.cs`

---

### **<Nome>: Algoritmos Adicionais, Utilitários e Camada de Apresentação**

Encarregado dos algoritmos restantes e de toda a camada de interação com o usuário, medição de performance e geração de relatórios, integrando o trabalho de toda a equipe.

-   `AlgoritmoColoracao.cs`
-   `ResultadoColoracao.cs`
-   `AlgoritmoCiclo.cs`
-   `ResultadoCiclo.cs`
-   `MedidorPerformance.cs`
-   `ArquivoLog.cs`
-   `GeradorRelatorio.cs`
-   `Program.cs`
