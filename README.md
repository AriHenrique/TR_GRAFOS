# 🚀 Trabalho Prático de Algoritmos em Grafos
**Pontifícia Universidade Católica de Minas Gerais**
**Curso**: Algoritmos em Grafos
**Período**: 02/2025

---

## 📋 Sobre o Projeto

Este repositório contém a implementação completa de **dois trabalhos práticos** da disciplina de Algoritmos em Grafos, desenvolvidos em **C# (.NET 8.0)**:

### 🔹 **Trabalho 1: Sistema de Otimização de Rotas Logísticas (SORL)**
Sistema completo que resolve 5 problemas clássicos de grafos aplicados ao contexto logístico da empresa fictícia "Entrega Máxima Logística S.A.":

1. **Roteamento de Menor Custo** - Dijkstra e Bellman-Ford
2. **Capacidade Máxima de Escoamento** - Ford-Fulkerson e Edmonds-Karp
3. **Expansão da Rede de Comunicação** - Kruskal, Prim e Boruvka
4. **Agendamento de Manutenções sem Conflito** - Coloração Gulosa, DSATUR e Welsh-Powell
5. **Rota Única de Inspeção** - Ciclos Euleriano e Hamiltoniano

### 🔹 **Trabalho 2: Ilha do Tesouro (Beecrowd 2098)**
Solução para o problema competitivo de grafos "Ilha do Tesouro", que utiliza **Busca Binária + BFS** para encontrar o tempo máximo de coleta de tesouros considerando uma névoa mortal que sobe progressivamente.

---

## 🚀 Instalação e Execução

### Pré-requisitos

- **.NET SDK 8.0** ou superior
- Sistema operacional: macOS, Linux ou Windows

### Instalação do .NET SDK

**macOS (via Homebrew):**
```bash
brew install --cask dotnet-sdk
```

**Linux/Windows:** Acesse [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

---

## 📦 Trabalho 1: Sistema de Otimização de Rotas Logísticas

### Como Executar

```bash
cd TP_Grafos
dotnet build
dotnet run
```

### Modos de Execução

#### 🔹 Modo Automático (Recomendado)
Ao executar, pressione **ENTER** para processar todos os grafos DIMACS automaticamente:
- Processa todos os arquivos `.dimacs` da pasta `grafos_dimacs/`
- Executa as 6 análises para cada grafo
- Gera relatório consolidado: `relatorio_todos_grafos_YYYYMMDD_HHmmss.txt`

#### 🔹 Modo Interativo
Escolha a opção **2** para carregar um arquivo específico e acessar o menu:

```
1  - Caminho de Menor Custo (Dijkstra/Bellman-Ford)
2  - Fluxo Máximo (Ford-Fulkerson/Edmonds-Karp)
3  - Árvore Geradora Mínima (Kruskal/Prim/Boruvka)
4  - Coloração de Vértices (Gulosa/DSATUR/Welsh-Powell)
5  - Ciclo Euleriano (Hierholzer)
6  - Ciclo Hamiltoniano (Backtracking)
7  - Executar todas as análises (grafo atual)
8  - Gerar relatório completo
9  - Informações do grafo
10 - Processar TODOS os arquivos DIMACS
```

---

## 🎯 Trabalho 2: Ilha do Tesouro (Beecrowd 2098)

### Como Executar

```bash
cd IlhaTesouro
dotnet build
dotnet run
```

### Entrada de Teste

```bash
echo "3 3
2 3 4
3 4 5
4 5 6" | dotnet run
```

**Saída esperada:** `1`

### Submeter ao Beecrowd
Copie o conteúdo de `IlhaTesouro/Program.cs` e submeta na plataforma [Beecrowd](https://judge.beecrowd.com/pt/problems/view/2098).

---

## 📁 Estrutura do Projeto

```
TR_GRAFOS/
│
├── TP_Grafos/                          # 📦 Trabalho 1: Sistema de Logística
│   ├── grafos_dimacs/                  # Arquivos de teste DIMACS
│   │   ├── grafo01.dimacs
│   │   ├── grafo02.dimacs
│   │   └── ... (até grafo07.dimacs)
│   │
│   ├── Core/                           # Núcleo do Grafo
│   │   ├── Grafo.cs                   # Estrutura de dados principal
│   │   ├── Aresta.cs                  # Representação de arestas
│   │   └── UnionFind.cs               # Estrutura para Kruskal/Boruvka
│   │
│   ├── Algoritmos/                     # Implementações de Algoritmos
│   │   ├── AlgoritmoCaminhoMinimo.cs  # Dijkstra e Bellman-Ford
│   │   ├── AlgoritmoFluxoMaximo.cs    # Ford-Fulkerson e Edmonds-Karp
│   │   ├── AlgoritmoArvoreGeradora.cs # Kruskal, Prim e Boruvka
│   │   ├── AlgoritmoColoracao.cs      # Gulosa, DSATUR, Welsh-Powell
│   │   └── AlgoritmoCiclo.cs          # Euleriano e Hamiltoniano
│   │
│   ├── Resultados/                     # Classes de Resultados
│   │   ├── ResultadoCaminho.cs
│   │   ├── ResultadoFluxo.cs
│   │   ├── ResultadoArvore.cs
│   │   ├── ResultadoColoracao.cs
│   │   └── ResultadoCiclo.cs
│   │
│   ├── Utilitarios/                    # Ferramentas Auxiliares
│   │   ├── MedidorPerformance.cs      # Medição de tempo
│   │   ├── ValidadorGrafo.cs          # Validações
│   │   ├── ArquivoLog.cs              # Sistema de logs
│   │   └── GeradorRelatorio.cs        # Geração de relatórios
│   │
│   ├── Program.cs                      # Interface principal
│   └── TP_Grafos.csproj               # Projeto .NET
│
├── IlhaTesouro/                        # 🎯 Trabalho 2: Beecrowd 2098
│   ├── Program.cs                      # Solução do problema
│   ├── IlhaTesouro.csproj             # Projeto .NET
│   └── README.md                       # Documentação específica
│
├── trabalho1.md                        # Enunciado do Trabalho 1
├── trabalho2.md                        # Enunciado do Trabalho 2
└── README.md                           # Este arquivo
```

---

## 🧮 Algoritmos Implementados

### 1️⃣ Caminho Mínimo
| Algoritmo | Complexidade | Uso |
|-----------|--------------|-----|
| **Dijkstra** | O((V+E) log V) | Pesos não-negativos |
| **Bellman-Ford** | O(V×E) | Permite pesos negativos |

### 2️⃣ Fluxo Máximo
| Algoritmo | Complexidade | Característica |
|-----------|--------------|----------------|
| **Ford-Fulkerson** | O(E×f) | DFS, mais simples |
| **Edmonds-Karp** | O(V×E²) | BFS, polinomial |

### 3️⃣ Árvore Geradora Mínima
| Algoritmo | Complexidade | Estratégia |
|-----------|--------------|------------|
| **Kruskal** | O(E log E) | Ordena arestas, Union-Find |
| **Prim** | O((V+E) log V) | Cresce árvore a partir de vértice |
| **Boruvka** | O(E log V) | Paralelizável, histórico |

### 4️⃣ Coloração de Vértices
| Algoritmo | Complexidade | Abordagem |
|-----------|--------------|-----------|
| **Gulosa** | O(V+E) | Sequencial simples |
| **DSATUR** | O(V²) | Grau de saturação |
| **Welsh-Powell** | O(V²) | Ordena por grau |

### 5️⃣ Ciclos
| Tipo | Algoritmo | Complexidade |
|------|-----------|--------------|
| **Euleriano** | Hierholzer | O(E) |
| **Hamiltoniano** | Backtracking | O(V!) - NP-Completo |

---

## 📊 Formato dos Arquivos DIMACS

### Estrutura
```
<num_vertices> <num_arestas>
<origem> <destino> <peso> <capacidade>
<origem> <destino> <peso> <capacidade>
...
```

### Exemplo Completo
```
6 12
1 2 2.5 10
1 3 3.0 7
2 4 1.5 12
3 4 2.0 8
...
```

**Campos**:
- Linha 1: Número de vértices e arestas
- Linhas seguintes: Origem, Destino, Peso (custo), Capacidade (toneladas)

---

## 📝 Relatórios Gerados

### 🔹 Logs de Execução
Salvos como: `log_execucao_YYYYMMDD_HHmmss.txt`

Contém:
- Horário de cada operação
- Algoritmo executado
- Resultados obtidos
- Tempo de execução

### 🔹 Relatórios Consolidados
Salvos como: `relatorio_todos_grafos_YYYYMMDD_HHmmss.txt`

Inclui:
- Análise de todos os 7 grafos
- Comparação de desempenho dos algoritmos
- Estatísticas consolidadas
- Observações sobre conectividade e viabilidade

---

## ✅ Testes e Validação

### Trabalho 1: Sistema de Logística
✅ Testado com 7 grafos DIMACS (2-500 vértices)
✅ Todas as análises executam sem erros
✅ Relatórios gerados automaticamente
✅ Validação de estruturas de dados (matriz vs lista)

### Trabalho 2: Ilha do Tesouro
| Teste | Grid | Saída Esperada | Resultado | Status |
|-------|------|----------------|-----------|--------|
| 1 | 3×3 (alturas 2-6) | 1 | 1 | ✅ |
| 2 | 3×3 (alturas 1-5) | -1 | -1 | ✅ |
| 3 | 3×2 (alturas 314-1M) | 310 | 310 | ✅ |

---

## 👥 Equipe e Divisão de Tarefas

### 🔹 Aristides Cruz
**Responsabilidades**:
- Estruturas de Dados Especializadas
  - `UnionFind.cs` - Disjoint Set para Kruskal/Boruvka
- Algoritmos de Árvore Geradora Mínima
  - `AlgoritmoArvoreGeradora.cs` - Kruskal, Prim, Boruvka
  - `ResultadoArvore.cs`
- Algoritmos de Fluxo Máximo
  - `AlgoritmoFluxoMaximo.cs` - Ford-Fulkerson, Edmonds-Karp
  - `ResultadoFluxo.cs`
- Trabalho 2: Ilha do Tesouro
  - Solução completa Beecrowd 2098

### 🔹 Vinicius Dumont
**Responsabilidades**:
- Núcleo do Grafo
  - `Grafo.cs` - Estrutura principal
  - `Aresta.cs` - Representação de arestas
- Algoritmo de Caminho Mínimo
  - `AlgoritmoCaminhoMinimo.cs` - Dijkstra, Bellman-Ford
  - `ResultadoCaminho.cs`

### 🔹 Outros Integrantes
**Responsabilidades**:
- Algoritmos Adicionais
  - `AlgoritmoColoracao.cs` - Gulosa, DSATUR, Welsh-Powell
  - `AlgoritmoCiclo.cs` - Euleriano, Hamiltoniano
- Utilitários e Apresentação
  - `MedidorPerformance.cs`
  - `ValidadorGrafo.cs`
  - `GeradorRelatorio.cs`
  - `Program.cs` - Interface do usuário

---

## 📚 Documentação e Referências

### 📖 Arquivos de Documentação
- **`trabalho1.md`** - Enunciado completo do Sistema de Logística
- **`trabalho2.md`** - Enunciado do problema Ilha do Tesouro
- **`IlhaTesouro/README.md`** - Documentação técnica da solução Beecrowd

### 📘 Conceitos Utilizados
- Grafos Direcionados e Ponderados
- Representação Mista (Lista/Matriz de Adjacência)
- Algoritmos Gulosos (Dijkstra, Prim, Kruskal)
- Programação Dinâmica (Bellman-Ford)
- Busca em Grafos (BFS, DFS)
- Fluxo em Redes
- Coloração de Grafos
- Ciclos em Grafos
- Busca Binária + BFS

---

## 🎓 Informações Acadêmicas

**Instituição**: Pontifícia Universidade Católica de Minas Gerais (PUC Minas)
**Disciplina**: Algoritmos em Grafos
**Período**: 02/2025
**Linguagem**: C# (.NET 8.0)

---

## 📧 Contato e Suporte

Para dúvidas sobre o projeto, entre em contato com os membros da equipe através do Canvas ou durante as apresentações.

---

## 📄 Licença

Este projeto foi desenvolvido exclusivamente para fins acadêmicos como parte da disciplina de Algoritmos em Grafos da PUC Minas.

**⚠️ Aviso**: Trabalhos copiados, parcialmente ou integralmente, serão avaliados com nota zero, conforme estabelecido nas instruções do trabalho prático.

---

**Última Atualização**: Dezembro de 2025
**Status do Projeto**: ✅ Concluído e Testado
