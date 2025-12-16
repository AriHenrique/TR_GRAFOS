using System;
using System.Collections.Generic;

class URI {

    static void Main(string[] args) {

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

        int resultado = EncontrarTempoMaximo(R, C, alturas);

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
