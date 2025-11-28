using System.Collections.Generic;

namespace TP_Grafos
{
    /// <summary>
    /// Contém algoritmos para a coloração de vértices de um grafo.
    /// </summary>
    public class AlgoritmoColoracao
    {
        /// <summary>
        /// O grafo no qual o algoritmo será executado.
        /// </summary>
        private Grafo grafo;

        /// <summary>
        /// Array para armazenar a cor de cada vértice.
        /// </summary>
        private int[] cores;

        /// <summary>
        /// O número de cores utilizadas.
        /// </summary>
        private int numCores;

        /// <summary>
        /// Ordem dos vértices a serem coloridos.
        /// </summary>
        private List<int> ordemVertices;

        /// <summary>
        /// Grau de saturação de cada vértice (para DSATUR).
        /// </summary>
        private int[] grauSaturacao;

        /// <summary>
        /// Construtor que recebe o grafo.
        /// </summary>
        /// <param name="grafo">O grafo.</param>
        public AlgoritmoColoracao(Grafo grafo)
        {
        }

        /// <summary>
        /// Executa um algoritmo de coloração guloso.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoGulosa()
        {
            return null;
        }

        /// <summary>
        /// Executa o algoritmo de coloração DSATUR.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoDSATUR()
        {
            return null;
        }

        /// <summary>
        /// Executa o algoritmo de coloração de Welsh-Powell.
        /// </summary>
        /// <returns>O resultado da coloração.</returns>
        public ResultadoColoracao ColoracaoWelshPowell()
        {
            return null;
        }

        /// <summary>
        /// Encontra o próximo vértice a ser colorido pelo algoritmo DSATUR.
        /// </summary>
        /// <returns>O índice do próximo vértice.</returns>
        private int ProximoVerticeDSATUR()
        {
            return 0;
        }

        /// <summary>
        /// Calcula o grau de saturação de um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>O grau de saturação.</returns>
        private int CalcularGrauSaturacao(int vertice)
        {
            return 0;
        }

        /// <summary>
        /// Verifica se um vértice pode ser colorido com uma determinada cor.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <param name="cor">A cor.</param>
        /// <returns>True se a coloração for possível, false caso contrário.</returns>
        private bool PodeColorir(int vertice, int cor)
        {
            return false;
        }

        /// <summary>
        /// Obtém a primeira cor disponível para um vértice.
        /// </summary>
        /// <param name="vertice">O vértice.</param>
        /// <returns>A primeira cor disponível.</returns>
        private int ObterPrimeiraCor(int vertice)
        {
            return 0;
        }

        /// <summary>
        /// Ordena os vértices por grau decrescente (para Welsh-Powell).
        /// </summary>
        private void OrdenarPorGrauDecrescente()
        {
        }
    }
}
