PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS
Algoritmos em Grafos
Trabalho Prático – Parte 2 - 02/2025 (5 pts)

Instruções:
I. O trabalho deverá ser feito em trio, o mesmo trio do TP do sistema de Logística.
II. O trabalho deverá ser realizado usando a linguagem de programação C#.
III. A avaliação do trabalho será por meio de apresentação do código.
IV. O trabalho deverá ser entregue conforme data estabelecida no Canvas, na mesma 
tarefa que o TP de Logística.
V. O código deverá ser executado no Beecrowd, de acordo com os requisitos da 
plataforma.
VI. Comece a fazer este trabalho logo, enquanto o problema está fresco na memória e 
o prazo para terminá-lo está tão longe quanto jamais poderá estar;

1 – Descrição do trabalho

Este desafio constitui-se de algumas questões/problemas onde Algoritmos em Grafos 
podem ser utilizados para chegar na solução. A dinâmica deste desafio será apresentada a 
seguir:

Cada grupo, receberá apenas uma questão para resolver. 

Foram selecionados e sorteados um problema para cada grupo, conforme consta a seguir. 
Desta forma, o grupo 1 ficará com o problema 1 a seguir, o grupo 2 com o problema 2 e 
assim sucessivamente.

1. https://judge.beecrowd.com/pt/problems/view/3350 - A casa das 7 mulheres
2. https://judge.beecrowd.com/pt/problems/view/3220 - Planejamento de Voo
3. https://judge.beecrowd.com/pt/problems/view/1384 - Sapo Preguiçoso
4. https://judge.beecrowd.com/pt/problems/view/1417 - Liga da Justiça
5. https://judge.beecrowd.com/pt/problems/view/3221 - Faróis
6. https://judge.beecrowd.com/pt/problems/view/3314 - Konfusa, a Colmeia!
7. https://judge.beecrowd.com/pt/problems/view/2098 - Ilha do Tesouro
8. https://judge.beecrowd.com/pt/problems/view/2870 - Jogo do Mapa
9. https://judge.beecrowd.com/pt/problems/view/1082 - Componentes Conexos
10. https://judge.beecrowd.com/pt/problems/view/1389 - O problema do Sapateiro Viajante
11. https://judge.beecrowd.com/pt/problems/view/2566 - Viagem para BH
12. https://judge.beecrowd.com/pt/problems/view/1592 - Elias e Golias
13. https://judge.beecrowd.com/pt/problems/view/1562 - Escolhendo as Duplas
14. https://judge.beecrowd.com/pt/problems/view/1782 - Honorável Presente

A entrega da solução deste desafio, deverá ser feita juntamente com a parte 1 do trabalho. 
Assim, compacte uma pasta contendo nela as duas soluções, tanto da parte 1 (Sistema de 
Logística), quanto da parte 2 do trabalho prático.

Importante, para este desafio, ele deve ser executado na plataforma Beecrowd. A 
apresentação desta parte do Trabalho se dará com a execução na plataforma referida.



beecrowd | 2098
Ilha do Tesouro
Por Fidel I. Schaposnik, Universidad Nacional de La Plata AR Argentina

Timelimit: 2
Encontrar os tesouros escondidos há séculos pelos piratas das ilhas do Caribe não é tarefa fácil, mais difícil ainda é viver para contar a história. Isto porque, como todo mundo sabe, os piratas tinham poderes sobrenaturais que eles usavam para amaldiçoar a pessoa que levou o seu tesouro sem autorização.

Uma maldição muito comum entre os mais poderosos dos piratas, e para a qual é sempre uma boa ideia estar preparado, é hoje conhecida como a névoa mortal. Sempre que o tesouro de um pirata for encontrado, esta maldição vai fazer com que a névoa venenosa suba do chão até que toda a ilha fique coberta por ela. Qualquer criatura viva que é tocado pela névoa vai morrer instantaneamente, algo especialmente indesejável para quem acabou de encontrar um tesouro. A única maneira de se salvar é, em seguida, retornar para o seu barco, sempre passando por áreas que ainda não foram cobertas pela névoa, e, assim, fugir com a parte do tesouro que pode ter sido resgatada. Neste problema estamos interessados em saber qual é a quantidade máxima de tempo que uma pessoa pode recolher o tesouro e ser capaz de voltar para o barco vivo.

Para simplificar o problema, vamos considerar que uma ilha pode ser representada por uma grade com R linhas e C colunas, em que a célula na linha i-th e coluna j-th tem altura Hij acima do nível do mar. Além disso, vamos supor que o tesouro está sempre escondido na célula de linha 1 e coluna 1, porque esta é a mais distante do único lugar onde o barco pode ancorar, que é a célula da linha R e coluna C. A névoa mortal aparece no nível do mar no mesmo instante que o tesouro é encontrado, em seguida, levanta-se em toda a ilha, a uma taxa de uma unidade de altura por segundo, para que depois de t segundos não se pode estar em qualquer célula de altura menor ou igual a t. A fim de voltar para o barco, você pode ir de uma célula para outra somente se elas compartilham um lado, de modo que, se você estiver em uma determinada célula você só pode mover horizontalmente para a célula antes ou depois da mesma linha, ou verticalmente para a célula antes ou depois, na mesma coluna, mas você não pode se mover na diagonal ou cruzar as fronteiras da ilha. Cada um desses movimentos de uma célula para outra leva exatamente um segundo.

Entrada
A primeira linha contém dois números inteiros R e C, representado, respectivamente, o número de linhas e colunas da grade que representa a ilha, constituído por pelo menos duas células (1 ≤ R, C e R ≤ 500 × C ≥ 2). Cada uma das seguintes R linhas contém C valores. No i-ésimo destas R linhas, o valor j-ésimo é um número inteiro Hij que representa a altura da célula da linha i e coluna j (1 ≤ Hij ≤ 106 para i = 1, ..., R e j = 1, ..., C).

Saída
Imprimir uma única linha contendo um número inteiro que representa a quantidade máxima de tempo, em segundos, que se pode recolher o tesouro, de modo a ser capaz de retornar para o barco sem ser atingido pela névoa mortal. Imprimir o número -1 se for impossível voltar para o barco, mesmo quando se inicia o caminho de volta assim que o tesouro é descoberto.

| Exemplos de Entrada                                      | Exemplos de Saída |
|----------------------------------------------------------|-------------------|
| 3 3<br>2 3 4<br>3 4 5<br>4 5 6                           | 1                 |
| 3 3<br>1 2 3<br>2 2 3<br>2 4 5                           | -1                |
| 3 2<br>1000000 1000000<br>1000000 1000000<br>1000000 314 | 310               |
