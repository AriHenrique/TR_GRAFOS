# Instruções para submissão no Beecrowd

## Problema 2098 — Ilha do Tesouro

### Código para envio
Envie exatamente o conteúdo abaixo no arquivo `Program.cs` (classe deve se chamar `URI` e não deve ter namespace):

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

### Passo a passo de envio
1) Acesse https://judge.beecrowd.com/pt/problems/view/2098 e faça login.
2) Clique em "Enviar/Submit" e escolha a linguagem **C# (mono 6.8)**.
3) Cole todo o código acima (desde `using System;` até o último `}`) e envie.

---

### Resultados esperados

| Teste | Entrada (resumo) | Saída | Status |
|-------|------------------|-------|--------|
| 1 | 3×3 com alturas 2–6 | 1 | Aceito |
| 2 | 3×3 começando em 1 | -1 | Aceito |
| 3 | 3×2 com 314–1.000.000 | 310 | Aceito |

---

### Resumo do algoritmo
- Busca binária no tempo de coleta (0 a 2.000.000).
- Para cada tempo, roda BFS do canto superior esquerdo ao inferior direito.
- Uma célula é válida se `altura > tempoInicial + passos`.
- Complexidade: O(log 2.000.000 × R × C) em tempo e O(R × C) em espaço.

---

### Casos de teste locais
**Teste 1**
```
3 3
2 3 4
3 4 5
4 5 6
```
Saída: `1`

**Teste 2**
```
3 3
1 2 3
2 2 3
2 4 5
```
Saída: `-1`

**Teste 3**
```
3 2
1000000 1000000
1000000 1000000
1000000 314
```
Saída: `310`

---

### Pontos de atenção
- Classe deve se chamar `URI` e não usar namespace.
- Use apenas `Console.ReadLine()` e `Split()` para a entrada.
- Imprima somente o número calculado com `Console.WriteLine()`.
- Início do caminho é (0,0) e fim é (R-1, C-1).
