using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TP_Grafos
{
    public static class ValidadorGrafo
    {
        private static List<string> erros = new List<string>();

        public static bool ValidarFormatoDIMACS(string arquivo)
        {
            erros.Clear();
            if (!File.Exists(arquivo))
            {
                erros.Add("Arquivo não encontrado.");
                return false;
            }

            var linhas = File.ReadAllLines(arquivo);
            bool temCabecalho = false;
            foreach (var linha in linhas)
            {
                if (linha.StartsWith("p "))
                {
                    temCabecalho = true;
                    break;
                }
            }

            if (!temCabecalho)
                erros.Add("Cabeçalho 'p' do formato DIMACS não encontrado.");

            return erros.Count == 0;
        }

        public static bool ValidarConectividade(Grafo grafo)
        {
            bool conexo = grafo.EhConexo();
            if (!conexo) erros.Add("O grafo não é conexo.");
            return conexo;
        }

        public static bool ValidarPesosPositivos(Grafo grafo)
        {
            var arestas = grafo.ObterTodasArestas();
            bool valido = arestas.All(a => a.Peso >= 0);
            if (!valido) erros.Add("Existem arestas com pesos negativos.");
            return valido;
        }

        public static bool ValidarCapacidades(Grafo grafo)
        {
            var arestas = grafo.ObterTodasArestas();
            bool valido = arestas.All(a => a.Capacidade >= 0);
            if (!valido) erros.Add("Existem arestas com capacidades negativas.");
            return valido;
        }

        public static bool ValidarGrafoEuleriano(Grafo grafo)
        {
            // Um grafo conexo é Euleriano se todos os vértices têm grau par (Ciclo)
            // Ou se tem exatamente 0 ou 2 vértices de grau ímpar (Caminho/Trilha)
            // Assumindo verificação para Ciclo Euleriano (Teorema de Euler)
            if (!grafo.EhConexo()) return false;

            int impares = 0;
            for (int i = 1; i <= grafo.NumVertices; i++)
            {
                if (grafo.ObterGrauVertice(i) % 2 != 0)
                    impares++;
            }
            
            bool euleriano = (impares == 0);
            if (!euleriano) erros.Add($"Grafo não é Euleriano. Número de vértices com grau ímpar: {impares}");
            return euleriano;
        }

        public static bool ValidarGrafoHamiltoniano(Grafo grafo)
        {
            // Verificar se é Hamiltoniano é NP-Completo.
            // Aqui verificamos a condição necessária (mas não suficiente) do Teorema de Dirac
            // Se n >= 3 e grau(v) >= n/2 para todo v, então é Hamiltoniano.
            
            int n = grafo.NumVertices;
            if (n < 3) return true; // Trivial

            bool atendeDirac = true;
            for (int i = 1; i <= n; i++)
            {
                if (grafo.ObterGrauVertice(i) < n / 2.0)
                {
                    atendeDirac = false;
                    break;
                }
            }

            if (!atendeDirac)
                erros.Add("O grafo não atende a condição suficiente de Dirac para ser Hamiltoniano (não garante que não seja).");
            
            return atendeDirac;
        }

        public static List<string> ObterErros()
        {
            return erros;
        }

        public static string GerarRelatorioValidacao(Grafo grafo)
        {
            erros.Clear();
            ValidarConectividade(grafo);
            ValidarPesosPositivos(grafo);
            return string.Join(Environment.NewLine, erros);
        }
    }
}