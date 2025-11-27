using System;
using System.Collections.Generic;

public class ArvoreBinaria
{
    private No? _raiz;

    public class No
    {
        public int Valor { get; set; }
        public No? Esquerda { get; set; }
        public No? Direita { get; set; }

        public No(int valor)
        {
            Valor = valor;
            Esquerda = null;
            Direita = null;
        }
    }

    public void Inserir(int valor)
    {
        if (_raiz == null)
        {
            _raiz = new No(valor);
            return;
        }

        InserirRecursivo(_raiz, valor);
    }

    private void InserirRecursivo(No? no, int valor)
    {
        if (no == null) return;

        if (valor == no.Valor) return;

        if (valor < no.Valor)
        {
            if (no.Esquerda == null)
                no.Esquerda = new No(valor);
            else
                InserirRecursivo(no.Esquerda, valor);
        }
        else
        {
            if (no.Direita == null)
                no.Direita = new No(valor);
            else
                InserirRecursivo(no.Direita, valor);
        }
    }

    public bool Contem(int valor)
    {
        return ContemRecursivo(_raiz, valor);
    }

    private bool ContemRecursivo(No? no, int valor)
    {
        if (no == null) return false;
        if (valor == no.Valor) return true;
        
        return valor < no.Valor 
            ? ContemRecursivo(no.Esquerda, valor)
            : ContemRecursivo(no.Direita, valor);
    }

    public void ImprimirEmOrdem()
    {
        ImprimirEmOrdemRecursivo(_raiz);
        Console.WriteLine();
    }

    private void ImprimirEmOrdemRecursivo(No? no)
    {
        if (no == null) return;
        
        ImprimirEmOrdemRecursivo(no.Esquerda);
        Console.Write(no.Valor + " ");
        ImprimirEmOrdemRecursivo(no.Direita);
    }

    public int Altura()
    {
        return AlturaRecursivo(_raiz);
    }

    private int AlturaRecursivo(No? no)
    {
        if (no == null) return 0;
        
        int alturaEsquerda = AlturaRecursivo(no.Esquerda);
        int alturaDireita = AlturaRecursivo(no.Direita);
        
        return 1 + Math.Max(alturaEsquerda, alturaDireita);
    }

    public int ContarNos()
    {
        return ContarNosRecursivo(_raiz);
    }

    private int ContarNosRecursivo(No? no)
    {
        if (no == null) return 0;
        
        return 1 + ContarNosRecursivo(no.Esquerda) + ContarNosRecursivo(no.Direita);
    }

    public int ContarFolhas()
    {
        return ContarFolhasRecursivo(_raiz);
    }

    private int ContarFolhasRecursivo(No? no)
    {
        if (no == null) return 0;
        if (no.Esquerda == null && no.Direita == null) return 1;
        
        return ContarFolhasRecursivo(no.Esquerda) + ContarFolhasRecursivo(no.Direita);
    }

    public bool Remover(int valor)
    {
        if (_raiz == null) return false;
        
        bool removido = false;
        RemoverRecursivo(ref _raiz, valor, ref removido);
        return removido;
    }

    private void RemoverRecursivo(ref No? no, int valor, ref bool removido)
    {
        if (no == null) return;

        if (valor < no.Valor)
        {
            var esquerda = no.Esquerda;
            RemoverRecursivo(ref esquerda, valor, ref removido);
            no.Esquerda = esquerda;
        }
        else if (valor > no.Valor)
        {
            var direita = no.Direita;
            RemoverRecursivo(ref direita, valor, ref removido);
            no.Direita = direita;
        }
        else
        {
            if (no.Esquerda == null && no.Direita == null)
            {
                no = null;
                removido = true;
            }
        }
    }

    public void ImprimirPorLargura()
    {
        if (_raiz == null)
        {
            Console.WriteLine("Árvore vazia");
            return;
        }

        Queue<No?> fila = new Queue<No?>();
        fila.Enqueue(_raiz);

        while (fila.Count > 0)
        {
            No? atual = fila.Dequeue();
            if (atual == null) continue;

            Console.Write(atual.Valor + " ");

            if (atual.Esquerda != null)
                fila.Enqueue(atual.Esquerda);
            if (atual.Direita != null)
                fila.Enqueue(atual.Direita);
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Exercício 1 - Criação do nó ===");
        ArvoreBinaria.No noTeste = new ArvoreBinaria.No(5);
        Console.WriteLine($"Valor do nó: {noTeste.Valor}");
        Console.WriteLine($"Esquerda é nula: {noTeste.Esquerda == null}");
        Console.WriteLine($"Direita é nula: {noTeste.Direita == null}");

        Console.WriteLine("\n=== Exercício 2 - Inserção do primeiro elemento ===");
        ArvoreBinaria arvore = new ArvoreBinaria();
        arvore.Inserir(10);
        Console.WriteLine("Raiz inserida com valor 10");

        Console.WriteLine("\n=== Exercício 3 - Inserção ordenada ===");
        arvore.Inserir(5);
        arvore.Inserir(12);
        arvore.Inserir(3);
        Console.WriteLine("Valores 10, 5, 12, 3 inseridos");
        Console.Write("Em ordem: ");
        arvore.ImprimirEmOrdem();

        Console.WriteLine("\n=== Exercício 4 - Busca recursiva ===");
        Console.WriteLine($"Contém 12: {arvore.Contem(12)}");
        Console.WriteLine($"Contém 7: {arvore.Contem(7)}");

        Console.WriteLine("\n=== Exercício 5 - Percurso em-ordem ===");
        Console.Write("Em ordem: ");
        arvore.ImprimirEmOrdem();

        Console.WriteLine("\n=== Exercício 6 - Cálculo da altura ===");
        Console.WriteLine($"Altura: {arvore.Altura()}");

        Console.WriteLine("\n=== Exercício 7 - Contagem total de nós ===");
        Console.WriteLine($"Total de nós: {arvore.ContarNos()}");

        Console.WriteLine("\n=== Exercício 8 - Contagem de folhas ===");
        Console.WriteLine($"Folhas: {arvore.ContarFolhas()}");

        Console.WriteLine("\n=== Exercício 9 - Remoção de nó folha ===");
        Console.WriteLine($"Remover 3: {arvore.Remover(3)}");
        Console.Write("Em ordem após remoção: ");
        arvore.ImprimirEmOrdem();

        Console.WriteLine("\n=== Exercício 10 - Percurso em largura ===");
        Console.Write("Por largura: ");
        arvore.ImprimirPorLargura();

        Console.WriteLine("\n=== Testes adicionais ===");
        arvore.Inserir(7);
        arvore.Inserir(15);
        arvore.Inserir(1);
        Console.Write("Em ordem após novas inserções: ");
        arvore.ImprimirEmOrdem();
        Console.WriteLine($"Altura: {arvore.Altura()}");
        Console.WriteLine($"Total de nós: {arvore.ContarNos()}");
        Console.WriteLine($"Folhas: {arvore.ContarFolhas()}");
        Console.Write("Por largura: ");
        arvore.ImprimirPorLargura();
    }
}