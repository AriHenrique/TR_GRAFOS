# 📝 Instruções para Submissão no Beecrowd

## 🎯 Problema: 2098 - Ilha do Tesouro

### 📋 Código para Submissão

**Arquivo**: `Program.cs` ⭐

Abra o arquivo `Program.cs` do projeto e copie TODO o conteúdo, ou use o código abaixo:

```csharp
using System;
using System.Collections.Generic;

class URI {

    static void Main(string[] args) {

        // Leitura da entrada
        string[] primeira = Console.ReadLine().Split();
        int R = int.Parse(primeira[0]);
        int C = int.Parse(primeira[1]);

        int[,] alturas = new int[R, C];

        for (int i = 0; i < R; i++)
        {
            string[] linha = Console.ReadLine().Split();
            for (int j = 0; j < C; j++)
            {
                alturas[i, j] = int.Parse(linha[j]);
            }
        }

        // Processamento - Busca Binária
        int resultado = EncontrarTempoMaximo(R, C, alturas);

        // Saída
        Console.WriteLine(resultado);
    }

    static int EncontrarTempoMaximo(int R, int C, int[,] alturas)
    {
        int esquerda = 0;
        int direita = 2000000;
        int resultado = -1;

        while (esquerda <= direita)
        {
            int meio = esquerda + (direita - esquerda) / 2;

            if (PodeChegarAoBarco(R, C, alturas, meio))
            {
                resultado = meio;
                esquerda = meio + 1;
            }
            else
            {
                direita = meio - 1;
            }
        }

        return resultado;
    }

    static bool PodeChegarAoBarco(int R, int C, int[,] alturas, int tempoColeta)
    {
        if (alturas[0, 0] <= tempoColeta)
            return false;

        var fila = new Queue<(int r, int c, int tempo)>();
        var visitado = new bool[R, C];

        fila.Enqueue((0, 0, tempoColeta));
        visitado[0, 0] = true;

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        while (fila.Count > 0)
        {
            var (r, c, tempo) = fila.Dequeue();

            if (r == R - 1 && c == C - 1)
                return true;

            for (int i = 0; i < 4; i++)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];
                int novoTempo = tempo + 1;

                if (nr >= 0 && nr < R && nc >= 0 && nc < C &&
                    !visitado[nr, nc] && alturas[nr, nc] > novoTempo)
                {
                    visitado[nr, nc] = true;
                    fila.Enqueue((nr, nc, novoTempo));
                }
            }
        }

        return false;
    }
}
```

---

## 🚀 Passo a Passo para Submissão

### 1️⃣ Acessar o Problema
- URL: https://judge.beecrowd.com/pt/problems/view/2098
- Faça login na sua conta Beecrowd

### 2️⃣ Selecionar Linguagem
- Clique em **"Enviar"** ou **"Submit"**
- Selecione **"C# (mono 6.8)"** na lista de linguagens

### 3️⃣ Colar o Código
- Copie **TODO** o código acima (desde `using System;` até o último `}`)
- Cole na área de texto do Beecrowd
- **IMPORTANTE**: Use exatamente como está, incluindo `class URI`

### 4️⃣ Submeter
- Clique em **"Enviar"** / **"Submit"**
- Aguarde o julgamento

---

## ✅ Resultados Esperados

| Teste | Entrada | Saída Esperada | Status |
|-------|---------|----------------|--------|
| 1 | 3×3 (2-6) | 1 | ✅ |
| 2 | 3×3 (1-5) | -1 | ✅ |
| 3 | 3×2 (314-1M) | 310 | ✅ |

---

## 🧮 Explicação do Algoritmo

### Estratégia: Busca Binária + BFS

1. **Busca Binária no Tempo de Coleta** (0 a 2.000.000 segundos)
   - Para cada tempo `t`, verifica se é possível chegar ao barco

2. **BFS (Busca em Largura) para Validação**
   - Simula o caminho de (0,0) até (R-1, C-1)
   - No passo `k`: célula deve ter altura > `t + k`
   - Névoa sobe 1 unidade por segundo

### Complexidade
- **Tempo**: O(log(2.000.000) × R × C) ≈ O(21 × R × C)
- **Espaço**: O(R × C)

---

## 📊 Casos de Teste Locais

### Teste 1 (Exemplo Básico)
**Entrada:**
```
3 3
2 3 4
3 4 5
4 5 6
```
**Saída:** `1`

### Teste 2 (Impossível)
**Entrada:**
```
3 3
1 2 3
2 2 3
2 4 5
```
**Saída:** `-1`

### Teste 3 (Alturas Grandes)
**Entrada:**
```
3 2
1000000 1000000
1000000 1000000
1000000 314
```
**Saída:** `310`

---

## ⚠️ Pontos de Atenção

1. **Nome da Classe**: DEVE ser `URI` (exigência do Beecrowd)
2. **Namespace**: Não usar namespace
3. **Formato de Entrada**: Usar `Console.ReadLine()` e `.Split()`
4. **Saída**: Apenas o número resultado com `Console.WriteLine()`
5. **Índices**: Grid começa em (0,0) e termina em (R-1, C-1)

---

## 🐛 Solução de Problemas

### Erro de Compilação
- Verifique se copiou TODO o código
- Confirme que a classe se chama `URI`
- Não adicione namespace

### Wrong Answer
- Teste localmente com os 3 exemplos
- Verifique os limites da busca binária
- Confirme a lógica do BFS

### Time Limit Exceeded
- A complexidade está otimizada
- Provavelmente não é o caso com este algoritmo

---

## 📚 Referências

- **Problema**: https://judge.beecrowd.com/pt/problems/view/2098
- **Algoritmo**: Busca Binária + BFS (Breadth-First Search)
- **Categoria**: Grafos, Busca em Malha
- **Dificuldade**: Média-Alta

---

**Boa sorte! 🍀**
