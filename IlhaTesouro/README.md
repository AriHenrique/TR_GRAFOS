# Ilha do Tesouro - Beecrowd 2098

## 📋 Descrição do Problema

Um pirata encontrou um tesouro em uma ilha representada por uma grade R×C, onde cada célula tem uma altura Hij. O tesouro está na posição (1,1) e o barco está em (R,C).

Quando o tesouro é encontrado, uma **névoa mortal** começa a subir do nível do mar a uma taxa de **1 unidade de altura por segundo**. Após t segundos, não é possível estar em nenhuma célula com altura ≤ t.

O objetivo é encontrar o **tempo máximo** que se pode coletar o tesouro e ainda conseguir retornar ao barco.

## 🎯 Estratégia de Solução

### Algoritmo Utilizado: **Busca Binária + BFS**

1. **Busca Binária no Tempo de Coleta**:
   - Testamos diferentes tempos de coleta (0 a 2.000.000 segundos)
   - Para cada tempo t, verificamos se é possível chegar ao barco

2. **BFS (Busca em Largura) para Validação**:
   - Para um tempo t de coleta, simulamos o caminho de (1,1) a (R,C)
   - No passo k da jornada (k segundos desde que começamos a nos mover):
     - A névoa está na altura t+k
     - A célula atual deve ter altura > t+k
   - BFS garante que encontramos o caminho mais curto

### Complexidade

- **Tempo**: O(log(MAX_ALTURA) × R × C)
  - Busca binária: O(log 2.000.000) ≈ 21 iterações
  - BFS por iteração: O(R × C)

- **Espaço**: O(R × C) para o array de visitados

## 📊 Exemplos de Teste

### Teste 1
**Entrada:**
```
3 3
2 3 4
3 4 5
4 5 6
```
**Saída:** `1`

**Explicação**: Se coletar por 1 segundo, ainda é possível chegar ao barco seguindo:
- (0,0) tempo=1, altura=2 > 1 ✓
- (0,1) tempo=2, altura=3 > 2 ✓
- (0,2) tempo=3, altura=4 > 3 ✓
- (1,2) tempo=4, altura=5 > 4 ✓
- (2,2) tempo=5, altura=6 > 5 ✓

### Teste 2
**Entrada:**
```
3 3
1 2 3
2 2 3
2 4 5
```
**Saída:** `-1`

**Explicação**: Não é possível chegar ao barco, pois há células com altura muito baixa que bloqueiam todos os caminhos.

### Teste 3
**Entrada:**
```
3 2
1000000 1000000
1000000 1000000
1000000 314
```
**Saída:** `310`

**Explicação**: Com alturas muito altas, é possível coletar o tesouro por 310 segundos. O destino (2,1) tem altura 314, e leva 4 passos para chegar lá, então 310+4=314.

## 🚀 Como Executar

### Compilar e Testar Localmente:
```bash
dotnet build
dotnet run
```

### Executar com entrada de teste:
```bash
echo "3 3
2 3 4
3 4 5
4 5 6" | dotnet run
```

### 📝 Submeter ao Beecrowd:

**Código Pronto para Submissão** ⭐:
1. Abra o arquivo **`Program.cs`**
2. Copie **TODO** o conteúdo (desde `using System;` até o último `}`)
3. Acesse: https://judge.beecrowd.com/pt/problems/view/2098
4. Selecione linguagem: **C# (mono 6.8)**
5. Cole o código e clique em **Enviar**

**Instruções Detalhadas**:
- Consulte `INSTRUCOES_BEECROWD.md` para passo a passo completo
- Explicação do algoritmo, casos de teste e troubleshooting

## 📝 Observações

- O problema usa **grafos implícitos**: cada célula é um vértice, e movimentos adjacentes são arestas
- A restrição temporal transforma isso em um problema de **caminho mais curto com restrições dinâmicas**
- A busca binária é crucial para eficiência, já que testar todos os tempos seria muito lento

## ✅ Resultados dos Testes

| Teste | Entrada      | Saída Esperada | Saída Obtida | Status |
|-------|--------------|----------------|--------------|--------|
| 1     | 3×3 (2-6)    | 1              | 1            | ✅     |
| 2     | 3×3 (1-5)    | -1             | -1           | ✅     |
| 3     | 3×2 (314-1M) | 310            | 310          | ✅     |
