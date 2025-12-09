PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS
Algoritmos em Grafos
Trabalho Prático 02/2025

Instruções:
I. O trabalho deverá ser feito em trio.
II. O trabalho deverá ser realizado usando a linguagem de programação C#.
III. Deverão usar os conceitos aprendidos na disciplina Algoritmos em Grafos, levando 
em consideração os algoritmos que resolvem cada um dos problemas mencionados 
no trabalho prático.
IV. Somente poderão ser utilizados algoritmos estudados em sala de aula.
V. A avaliação do trabalho será por meio de apresentação do código.
VI. O trabalho deverá ser entregue conforme data estabelecida no Canvas.
VII. Para testarem o código de vocês, poderão utilizar os grafos no formato DIMACS1
que serão disponibilizados junto com a tarefa no Canvas.
VIII. Comece a fazer este trabalho logo, enquanto o problema está fresco na memória e 
o prazo para terminá-lo está tão longe quanto jamais poderá estar;

1 – Contexto e Descrição do trabalho

A Entrega Máxima Logística S.A. é uma transportadora nacional que atua no 
escoamento de mercadorias entre centros de distribuição, portos e polos industriais. Com 
o aumento da demanda, a empresa vem enfrentando atrasos, custos elevados e gargalos 
operacionais em determinadas regiões.

A diretoria, preocupada com a eficiência da malha logística, solicitou o 
desenvolvimento de um Sistema de Otimização de Rotas Logísticas (SORL) capaz de 
analisar e otimizar diferentes aspectos da rede de transporte, representada por meio de um 
grafo ponderado e direcionado.

O sistema deverá auxiliar o departamento de planejamento a responder perguntas 
estratégicas como:
I. Qual é o trajeto mais econômico para enviar cargas entre dois centros?
II. Qual é o limite diário de escoamento de mercadorias da empresa?
III. Como interligar todos os centros de distribuição ao menor custo?
IV. Como planejar manutenções sem conflitos de recurso?
V. É possível criar um percurso único de inspeção pelas rotas e centros?

Essas perguntas serão traduzidas em cinco problemas clássicos de grafos, cada um 
correspondendo a um desafio real enfrentado pela empresa.

2 – Modelagem da Rede Logística

A rede logística da Entrega Máxima será representada computacionalmente como 
um Grafo Direcionado e Ponderado, construído a partir de um arquivo de dados.

I. Cada vértice representa um Centro de Distribuição (Hub) ou Ponto de Entrega da 
empresa.
II. Cada aresta representa uma Rota Rodoviária ou Ligação Viária entre dois hubs.
III. O peso da aresta indica o custo financeiro (em R$) para transportar uma unidade de 
carga por aquela rota — considerando distância, pedágio, combustível, tempo, entre 
outros fatores.
IV. A capacidade da aresta representa o limite máximo diário (em toneladas) que pode 
ser transportado pela rota, levando em conta restrições de infraestrutura e tráfego.

O sistema deve permitir a leitura, armazenamento e manipulação dessas 
informações, mantendo a flexibilidade para futuras expansões (como a inclusão de novos 
hubs e rotas).

3 – Problemas enfrentados pela Máxima Logística S.A.

O sistema deverá apresentar um menu de análises, no qual o usuário poderá 
executar as seguintes otimizações logísticas:

I. Roteamento de Menor Custo
• O gerente de operações precisa descobrir a sequência de hubs mais econômica 
para enviar uma carga de um hub de origem até um hub de destino informado.
• O custo total deve ser o somatório dos custos das rotas percorridas.
• O objetivo aqui é determinar o caminho de menor custo entre dois vértices do 
grafo, considerando os pesos das arestas.

II. Capacidade Máxima de Escoamento
• O setor de planejamento logístico deseja saber qual é o volume máximo diário de 
mercadorias (em toneladas) que pode ser transportado do hub central de 
distribuição (S) até o terminal de destino (T).
• As rotas têm limites de capacidade que não podem ser excedidos.
• Deseja-se calcular o fluxo máximo entre dois vértices do grafo e identificar o 
conjunto mínimo de arestas cujo bloqueio causaria o estrangulamento da rede 
(corte mínimo).

III. Expansão da Rede de Comunicação
• A empresa pretende instalar uma rede de fibra óptica para interligar todos os seus 
centros de distribuição. O projeto precisa garantir conectividade total entre os 
hubs com o menor custo possível de instalação.
• Objetiva-se identificar o conjunto de rotas que conecta todos os vértices com 
custo mínimo total, sem criar ciclos desnecessários.

IV. Agendamento de Manutenções sem Conflito
• Durante a manutenção preventiva, algumas rotas não podem ser interditadas 
simultaneamente, pois compartilham recursos logísticos (como pátios, oficinas 
ou equipamentos de inspeção).
• O setor de manutenção precisa elaborar um cronograma que minimize o número 
do turnos.
• Almeja-se determinar o número mínimo de turnos (ou dias) necessários para 
realizar as manutenções de todas as rotas sem conflitos de recurso, e sugerir a 
alocação de rotas por turno.

V. Rota Única de Inspeção
• Um inspetor deve verificar as condições de segurança de todas as estradas da 
malha logística.
• Há duas análises a serem realizadas:
  ▪ Cenário A – Percurso de Rotas: É possível que o inspetor percorra todas 
  as rotas (arestas) exatamente uma vez e retorne ao ponto de partida?
  ▪ Cenário B – Percurso de Hubs: É possível que o inspetor visite todos os 
  hubs (vértices) exatamente uma vez, retornando ao hub inicial?
• Deseja-se verificar a existência e viabilidade dos percursos propostos, e listar a 
sequência de visita quando possível.

4 – Critérios de Avaliação

I. Implementação da Estrutura de Dados
• Criação da classe Grafo em C#, com suporte a pesos e capacidades.
• Representação por lista ou matriz de adjacência, de acordo com a densidade do 
grafo.
• Como podem haver alterações no grafo, seu código deve identificar a densidade 
do grafo e somente depois selecionar qual a estrutura de dados seria melhor para 
representar o grafo.

II. Resolução dos Problemas
• Cada análise deve ser resolvida com um algoritmo de grafos adequado ao 
problema identificado.
• O aluno deve justificar a escolha algorítmica no relatório.

III. Log de Execução
• Os resultados (caminhos, fluxos, custos, turnos, percursos) devem ser salvos 
em um arquivo de log, para os 7 grafos disponibilizados.

IV. Relatório Técnico
• Descrever a modelagem da rede, as decisões tomadas e as implicações dos 
resultados no contexto logística.

5 – Considerações Finais

O trabalho deve ser feito em trio. O trabalho deverá ser apresentado ao professor da 
disciplina no final do semestre (será elaborado e disponibilizado um cronograma de 
apresentação). Trabalhos copiados, parcialmente ou integralmente, serão avaliados com 
nota zero, sem direito a contestação. Ressaltando que deverão usar apenas algoritmos 
estudados em sala de aula.

Exemplo de formato DIMACS:
1 - Formato padrão DIMACS (Exemplo, o grafo a seguir tem a seguinte representação 
DIMACS):

Abaixo está **a representação completa por escrito da imagem**, incluindo:

1. **Descrição textual detalhada do grafo**
2. **A lista de arestas com pesos**, conforme aparece no quadro
3. **Representações formais do grafo**: lista de arestas, lista de adjacência e matriz de adjacência (opcional, mas útil caso você queira usar depois em um exercício)

---

# ✅ **1. Descrição textual do grafo da imagem**

O grafo possui **5 vértices** (1, 2, 3, 4 e 5) conectados por **arestas direcionadas e ponderadas**.
As arestas representadas visualmente são:

* Uma seta de **1 → 2** com peso **2**
* Uma seta de **1 → 4** com peso **2**
* Uma seta de **1 → 5** com peso **4**
* Uma seta de **2 → 3** com peso **5**
* Uma seta de **4 → 3** com peso **1**
* Uma seta de **4 → 5** com peso **3**

O grafo indica claramente a direção das setas e os pesos aparecem próximos às arestas.

---

# ✅ **2. Representação escrita exata (como está no quadro da imagem)**

```
5 6
1 2 2
1 4 2
2 3 5
3 4 1
4 5 3
5 1 4
```

Interpretação:

* **5 6** → 5 vértices, 6 arestas
* Cada linha após isso representa:
  **origem  destino  peso**

---

# ✅ **3. Lista de Arestas (Edge List)**

(igual ao formato acima, só explicado)

* (1 → 2, peso 2)
* (1 → 4, peso 2)
* (2 → 3, peso 5)
* (3 → 4, peso 1)
* (4 → 5, peso 3)
* (5 → 1, peso 4)

---

# ✅ **4. Lista de Adjacência**

```
1: (2,2), (4,2)
2: (3,5)
3: (4,1)
4: (5,3)
5: (1,4)
```

---

# ✅ **5. Matriz de Adjacência (direcionada e ponderada)**

Zeros indicam ausência de aresta.

```
    1  2  3  4  5
1 [ 0  2  0  2  0 ]
2 [ 0  0  5  0  0 ]
3 [ 0  0  0  1  0 ]
4 [ 0  0  0  0  3 ]
5 [ 4  0  0  0  0 ]
```

---

Onde:
Na primeira linha, o primeiro valor representa o número de vértices, e o segundo o número 
de arestas;
Nas demais linhas, o primeiro valor representa o vértice de origem, o segundo valor o vértice 
de destino e o terceiro valor o peso da aresta;
Nos grafos disponibilizados, a partir da segunda linha, há uma informação adicional (último 
valor de cada linha) que representa a capacidade daquela aresta.
