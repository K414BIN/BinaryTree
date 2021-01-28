using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTestConsoleApplication
{
    public class BinaryTree<T>
    {
        
       public  class BinaryTreeNode
        {
            public BinaryTreeNode Left { get; set; }

            public BinaryTreeNode Right { get; set; }

            public BinaryTreeNode Parent { get; set; }

            public T Data { get; set; }


        public BinaryTreeNode()
            {
                Left = null;
                Right = null;
                Parent = null;
            }
        }

        BinaryTreeNode Root;
        Comparison<T> CompareFunction;
        public object Right;
        public object Left;
        public T Data;

        

        public BinaryTree(Comparison<T> theCompareFunction)
        {
            Root = null;
            CompareFunction = theCompareFunction;
        }

        // Вариант с деревом из числовых значений типа int
        public static int CompareFunction_Int(int left, int right)
        {
            return left - right;
        }
        // Вариант с деревом из значений типа string
        public static int CompareFunction_String(string left, string right)
        {
            return left.CompareTo(right);
        }

        // добавление значений в дерево без рекурсии, ЧТОБЫ ХОТЬ КАК-ТО РАБОТАЛО!
        public void Add(T Value)
        {
            BinaryTreeNode child = new BinaryTreeNode();
            child.Data = Value;

            // Is the tree empty? Make the root the new child
            if (Root == null)
            {
                Root = child;
            }
            else
            {
                // Выбор "корня"
                BinaryTreeNode Iterator = Root;
                while (true)
                {
                    // сравнение значений , одинаковые разрешены
                    int Compare = CompareFunction(Value, Iterator.Data);
                    // значения меньше или равные на левую сторону
                    if (Compare <= 0)
                        if (Iterator.Left != null)
                        {
                            // Пошли налево
                            Iterator = Iterator.Left;
                            continue;
                        }
                        else
                        {
                            // ничего нет - добавляем сына
                            Iterator.Left = child;
                            child.Parent = Iterator;
                            break;
                        }
                    if (Compare > 0)
                        if (Iterator.Right != null)
                        {
                            // Пошли направо
                            Iterator = Iterator.Right;
                            continue;
                        }
                        else
                        {
                            // Добавляем сына справа
                            Iterator.Right = child;
                            child.Parent = Iterator;
                            break;
                        }
                }

            }

        }

     // Поиск значения
        public bool Find(T Value)
        {
            string str = "\t";
            string str1 = "                                           ";
            BinaryTreeNode Iterator = Root;
            while (Iterator != null)
            {
                int Compare = CompareFunction(Value, Iterator.Data);
                // Нашли значение ?
                if (Compare == 0)
                {
                    Console.Write(str1);

                    return true;
                }
                if (Compare < 0)
                {
                    // Пошли налево
                    Iterator = Iterator.Left;
                        Console.Write(str);
                  
                    continue;
                }
                
                // Пошли направо
                Iterator = Iterator.Right;
                        Console.Write(str);
            }
            Console.WriteLine();
            return false;
        }

        // найти ветку к которой прицепится следующаа 
        BinaryTreeNode FindMostLeft(BinaryTreeNode start)
        {
            BinaryTreeNode node = start;
            while (true)
            {
                if (node.Left != null)
                {
                    node = node.Left;
                    continue;
                }
                break;
            }
            return node;
        }

        // Вообще не знаю как это работает 
        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator(this);
        }

        // Позволяет сбалансировать дерево
        class BinaryTreeEnumerator : IEnumerator<T>
        {
            BinaryTreeNode current;
            BinaryTree<T> theTree;

            public BinaryTreeEnumerator(BinaryTree<T> tree)
            {
                theTree = tree;
                current = null;
            }

            // Помогает отсортировать значения
            public bool MoveNext()
            {
                // Первая запись - всегда самое малое значение
                if (current == null)
                    current = theTree.FindMostLeft(theTree.Root);
                else
                {
                    // Куда поворачивать?
                    if (current.Right != null)
                        current = theTree.FindMostLeft(current.Right);
                    else
                    {
                        // Нашли значения
                        T CurrentValue = current.Data;
                        // Поднимаемся  по дереву - от большего к большему значению
                        while (current != null)
                        {
                            current = current.Parent;
                            if (current != null)
                            {
                                int Compare = theTree.CompareFunction(current.Data, CurrentValue);
                                if (Compare < 0) continue;
                            }
                            break;
                        }

                    }
                }
                return (current != null);
            }

            public T Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException();
                    return current.Data;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException();
                    return current.Data;
                }
            }

            public void Dispose() { }
            public void Reset() { current = null; }
                       
        }
        // добавляет свободный узел
        public static BinaryTree<int> GetFreeNode(int value, BinaryTree<int> parent)
        {
            var newNode = new BinaryTree<int>(BinaryTree<int>.CompareFunction_Int);
            newNode.Add(value);
            return newNode;
        }
        //Вставка значений
        public static BinaryTree<int> Insert(BinaryTree<int> head, int value)
        {
            
            var tmp = new BinaryTree<int>(BinaryTree<int>.CompareFunction_Int);
 
            if (head == null)
            {
                head = GetFreeNode(value, null);
                return head;
            }
            tmp = head;
            while (tmp != null)
            {
                if (value > tmp.Data)
                {
                    if (tmp.Right != null)
                    {
                        tmp = (BinaryTree<int>)tmp.Right;
                        continue;
                    }
                    else
                    {
                        tmp.Right = GetFreeNode(value, tmp);
                        return head;
                    }
                }
                else if (value < tmp.Data)
                {
                    if (tmp.Left != null)
                    {
                        tmp = (BinaryTree<int>)tmp.Left;
                        continue;
                    }
                    else
                    {
                        tmp.Left = GetFreeNode(value, tmp);
                        return head;
                    }
                }
                else
                {
                    throw new Exception("Wrong tree state");
                }

            }
            return head;
        }
    }
}
