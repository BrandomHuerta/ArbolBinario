using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBinario
{
    public class ArbolBinarioBusqueda
    {
        public Nodo Raiz;

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        public void Insertar(int valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);

            return nodo;
        }

        public bool Buscar(int valor)
        {
            return BuscarRec(Raiz, valor);
        }

        private bool BuscarRec(Nodo nodo, int valor)
        {
            if (nodo == null) return false;
            if (nodo.Valor == valor) return true;

            return valor < nodo.Valor
                ? BuscarRec(nodo.Izquierdo, valor)
                : BuscarRec(nodo.Derecho, valor);
        }

        public void InOrden(Nodo nodo, List<int> lista)
        {
            if (nodo != null)
            {
                InOrden(nodo.Izquierdo, lista);
                lista.Add(nodo.Valor);
                InOrden(nodo.Derecho, lista);
            }
        }

        public List<int> PorNiveles()
        {
            List<int> resultado = new List<int>();
            Queue<Nodo> cola = new Queue<Nodo>();

            if (Raiz != null)
                cola.Enqueue(Raiz);

            while (cola.Count > 0)
            {
                Nodo actual = cola.Dequeue();
                resultado.Add(actual.Valor);

                if (actual.Izquierdo != null)
                    cola.Enqueue(actual.Izquierdo);
                if (actual.Derecho != null)
                    cola.Enqueue(actual.Derecho);
            }

            return resultado;
        }

        public int ContarHojas(Nodo nodo)
        {
            if (nodo == null) return 0;
            if (nodo.Izquierdo == null && nodo.Derecho == null)
                return 1;

            return ContarHojas(nodo.Izquierdo) + ContarHojas(nodo.Derecho);
        }

        public int Altura(Nodo nodo)
        {
            if (nodo == null) return 0;

            int izq = Altura(nodo.Izquierdo);
            int der = Altura(nodo.Derecho);

            return Math.Max(izq, der) + 1;
        }
    }
}