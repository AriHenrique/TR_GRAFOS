PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS
Algoritmos em Grafos – Trabalho Prático (Parte 2) 02/2025 – 5 pts

Instruções básicas
- Mesmo trio do TP de logística, implementação em C#.
- Código apresentado na data do Canvas, entregue junto com a parte 1.
- A solução deve rodar na plataforma Beecrowd.

1. Descrição geral
Cada grupo recebe um único problema da lista abaixo para resolver com técnicas de grafos. Entregue as duas partes do trabalho em um único pacote.

Problemas sorteados (referência Beecrowd):
1. 3350 – A casa das 7 mulheres
2. 3220 – Planejamento de Voo
3. 1384 – Sapo Preguiçoso
4. 1417 – Liga da Justiça
5. 3221 – Faróis
6. 3314 – Konfusa, a Colmeia!
7. 2098 – Ilha do Tesouro
8. 2870 – Jogo do Mapa
9. 1082 – Componentes Conexos
10. 1389 – O problema do Sapateiro Viajante
11. 2566 – Viagem para BH
12. 1592 – Elias e Golias
13. 1562 – Escolhendo as Duplas
14. 1782 – Honorável Presente

2. Resumo do problema 2098 (Ilha do Tesouro)
- A ilha é uma grade R × C, com altura Hij em cada célula.
- O tesouro está em (1,1) e o barco em (R,C).
- A névoa sobe uma unidade de altura por segundo; após t segundos, células com altura ≤ t ficam inviáveis.
- É possível mover apenas para cima/baixo/esquerda/direita, um segundo por movimento.
- Objetivo: determinar o maior tempo de coleta possível antes de iniciar o retorno sem ser alcançado pela névoa. Se for impossível voltar mesmo iniciando imediatamente, a saída é -1.

Entrada
```
R C
H11 H12 ... H1C
...
HR1 HR2 ... HRC
```

Saída
Inteiro com o tempo máximo em segundos ou -1.

Exemplos
| Entrada                                     | Saída |
|---------------------------------------------|-------|
| 3 3\n2 3 4\n3 4 5\n4 5 6                   | 1     |
| 3 3\n1 2 3\n2 2 3\n2 4 5                   | -1    |
| 3 2\n1000000 1000000\n1000000 1000000\n1000000 314 | 310   |
