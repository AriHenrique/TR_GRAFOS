# Ilha do Tesouro (Beecrowd 2098)

## Problema
- A ilha é uma grade R×C com alturas Hij.
- O tesouro está em (1,1) e o barco em (R,C).
- Após coletar o tesouro, a névoa sobe 1 unidade de altura por segundo; depois de t segundos não é possível pisar em células com altura ≤ t.
- Objetivo: descobrir o maior tempo de coleta possível que ainda permita alcançar o barco.

## Abordagem
- **Busca binária** sobre o tempo de coleta para encontrar o maior valor viável.
- **BFS** para validar cada tempo: no segundo k do trajeto a névoa está em t+k e a célula visitada precisa ter altura > t+k.
- **Complexidade**: tempo O(log(MAX_ALTURA) × R × C); espaço O(R × C).

## Como executar
```bash
dotnet build
dotnet run
```

### Entrada de exemplo
```bash
echo "3 3
2 3 4
3 4 5
4 5 6" | dotnet run
```

## Submissão no Beecrowd
- Abra `Program.cs` e copie todo o conteúdo para o problema 2098 em https://judge.beecrowd.com/pt/problems/view/2098.
- Linguagem: C# (mono 6.8).
- Para mais detalhes, consulte `INSTRUCOES_BEECROWD.md`.

## Testes
| Caso | Descrição | Saída esperada |
| --- | --- | --- |
| 1 | 3×3 com alturas 2–6 | 1 |
| 2 | 3×3 com alturas 1–5 | -1 |
| 3 | 3×2 com alturas altas (inclui 314) | 310 |
