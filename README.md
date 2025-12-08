# TP_Grafos - Sistema de Otimização de Rotas Logísticas (SORL)

Este projeto implementa um Sistema de Otimização de Rotas Logísticas (SORL) que analisa e otimiza diferentes aspectos de uma rede de transporte representada por grafos direcionados e ponderados.

## 📋 Sobre o Projeto

O sistema resolve 5 problemas clássicos de grafos:
1. **Roteamento de Menor Custo** - Caminho mais econômico entre dois centros (Dijkstra/Bellman-Ford)
2. **Capacidade Máxima de Escoamento** - Fluxo máximo entre origem e destino (Ford-Fulkerson/Edmonds-Karp)
3. **Expansão da Rede de Comunicação** - Árvore geradora mínima (Kruskal/Prim/Boruvka)
4. **Agendamento de Manutenções sem Conflito** - Coloração de vértices (Gulosa/DSATUR/Welsh-Powell)
5. **Rota Única de Inspeção** - Ciclo Euleriano e Hamiltoniano

## 🚀 Instalação e Execução

### Pré-requisitos

- .NET SDK 8.0 ou superior
- macOS, Linux ou Windows

### Instalação do .NET SDK (se necessário)

Instale via Homebrew:
```bash
brew install --cask dotnet-sdk
```

### Compilar o Projeto

```bash
cd TP_Grafos
dotnet build
```

### Executar o Programa

```bash
cd TP_Grafos
dotnet run
```

**Por padrão**, o programa processa automaticamente todos os arquivos `.dimacs` da pasta `grafos_dimacs/` e gera um relatório consolidado.

## 📁 Estrutura do Projeto

```
TP_Grafos/
├── grafos_dimacs/          # Arquivos de teste no formato DIMACS
│   ├── grafo01.dimacs
│   ├── grafo02.dimacs
│   └── ...
├── Algoritmo*.cs           # Implementações dos algoritmos
├── Resultado*.cs           # Classes de resultado
├── Grafo.cs                # Estrutura de dados do grafo
├── Program.cs              # Interface principal
└── TP_Grafos.csproj        # Arquivo de projeto
```

## 🎯 Como Usar

### Modo Automático (Recomendado)

Ao executar `dotnet run`, pressione **ENTER** para processar todos os arquivos automaticamente:

1. O programa encontra todos os arquivos `.dimacs` na pasta `grafos_dimacs/`
2. Processa cada arquivo sequencialmente
3. Executa todas as 6 análises para cada grafo
4. Gera um relatório consolidado: `relatorio_todos_grafos_*.txt`

### Modo Interativo

Escolha a opção 2 para carregar um arquivo específico e acessar o menu completo:

- `1` - Roteamento de menor custo
- `2` - Fluxo máximo
- `3` - Árvore geradora mínima
- `4` - Coloração de vértices
- `5` - Ciclo Euleriano
- `6` - Ciclo Hamiltoniano
- `7` - Executar todas as análises (grafo atual)
- `8` - Gerar relatório completo
- `9` - Informações do grafo atual
- `10` - Executar todas as análises em TODOS os arquivos DIMACS

## 📊 Formato dos Arquivos DIMACS

Os arquivos seguem o formato:
```
<num_vertices> <num_arestas>
<origem> <destino> <peso> <capacidade>
...
```

Exemplo:
```
6 12
1 2 2 10
1 3 3 7
...
```

## 📝 Relatórios

O sistema gera automaticamente:
- **Logs de execução**: `log_execucao_*.txt`
- **Relatórios consolidados**: `relatorio_todos_grafos_*.txt`

## 👥 Divisão de Tarefas

### **Aristides: Algoritmos Especializados e Estruturas de Dados Auxiliares**
- `UnionFind.cs`
- `AlgoritmoArvoreGeradora.cs`
- `ResultadoArvore.cs`
- `AlgoritmoFluxoMaximo.cs`
- `ResultadoFluxo.cs`

### **Outros Integrantes:**
- Núcleo do Grafo e Algoritmos Base
- Algoritmos Adicionais, Utilitários e Camada de Apresentação

## 📚 Documentação Adicional

- `trabalho1.md` - Enunciado completo do trabalho prático
- `trabalho2.md` - Documentação adicional (se houver)
