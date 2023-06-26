using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeapSort;
namespace HeapSort
{
    class Node       //class node
    {
        public int data;
        public Node parent;
        public Node left;
        public Node right;

        public Node(int data)   //constructor for class Node
        {
            this.data = data;
            this.left = null;
            this.right = null;
        }
        public Node(Node parent, Node left, Node right)     //constructor for class Node
        {
            this.parent = parent;
            this.left = left;
            this.right = right;
        }

    
    }
    class Heap       //class Heap
    {
        public int heapsize;
        int[] Array;
        public int Parent(int i)    //Parent Function
        {
            return i / 2;
        }
        public int Left(int i)      //Left Function
        {
            return 2*i;
        }
        public int Right(int i)     //Right Function
        {
            return 2*i + 1;
        }
        public Heap(int heapsize)   //constructor
        {
            Array = new int[heapsize-1];    
            this.heapsize = heapsize;
        }

    }
    class MaxHeap : Heap    //class MaxHeap
    {
        public Node root;
        public int[] A;

        public MaxHeap(int heapsize, Node root): base(heapsize)   //constructor for class MaxHeap
        {
            this.root = root;
        }
        public void Swap(int a, int b)  //For exchange two nodes
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public int RemoveMax()              //It used in Print Kth Biggest Number In MaxHeap
        {
            if (heapsize == 0)
            {
                throw new Exception("MaxHeap is empty.");
            }
            int max = A[1];
            A[1] = A[heapsize];
            heapsize--;
            int parentIndex = 1;
            while (true)
            {
                int l=Left(parentIndex);
                int r=Right(parentIndex);
                if (l > heapsize)
                {
                    break;
                }
                int maxIndex = l;
                if (r <= heapsize && A[r] > A[l])
                {
                    maxIndex = r;
                }
                if (A[parentIndex]< A[maxIndex])
                {
                    Swap(parentIndex, maxIndex);
                    parentIndex = maxIndex;
                }
   
            }
            return max;
        }
        

        public void MaxHeapify(int[] A, int i)  
        {


            int l, r;                       //left child and right child
            int largest;     
            l= Left(i);                     //Find the left child
            r = Right(i);                   //Find the right child
            if (l <= A.Length-1 && A[l]> A[i])
                largest = l;
            else
                largest = i;
            if (r <= A.Length-1 && A[r] > A[largest])
                largest = r;
            if (largest !=i)
            {
                Swap(A[i], A[largest]);
                MaxHeapify(A, largest);
            }
        }

        public void BuildMaxHeap(int[] A)   //Creat MaxHeap
        {
            int n = A.Length;
            for (int i = n / 2; i >= 0; i--)
            {
                MaxHeapify(A, i);
            }
            Console.Write("Nodes of the heap: ");       // Print the nodes of the heap
            for (int i = 0; i < n; i++)
            {
                Console.Write(A[i] + " ");
            }
            Console.WriteLine();
        }
        //  (1)
        public void InsertToMaxHeap(int x)     //Add to MaxHeap
        {

            heapsize = ++A[0];  
            heapsize++;
            A[heapsize] = x;    
            int child = heapsize;   
            int parent = child / 2; 
            while (A[parent]< A[child] && parent>=1)  
            {
                Swap(A[parent], A[child]);
                child = parent;     
                parent = child / 2;     
            }
        }
        //  (2)
        public bool IsMaxHeap(int[] A)                  //Is MaxHeap
        {
            for (int i = 0; i<= (A.Length / 2)-1; i++)
            {
                int l = Left(i);
                int r = Right(i);
                if (l < A.Length && A[l] > A[i])
                    return false;
                if (r < A.Length && A[r] > A[i])
                    return false;
            }
            return true;
        }
        //  (3)
        public void FindAndDeleteFromHeap(int[] A,int x)        //Delete From MaxHeap
        {
            int index = -1;     
            for (int i = 0; i < heapsize - 1; i++)      
            {
                if (x == A[i])
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)            
            {
                Console.WriteLine("Element not found in Heap!");
            }
            Swap(A[index], A[heapsize - 1]);  
            heapsize--;    
            MaxHeapify(A, index);   
        }
        //  (4)
        public void PrintSortedMaxHeap(int[] A)             //Print sorted MaxHeap
        {
            int[]sortedArray = new int[A.Length];   
            for (int i = A.Length/2 - 1; i >= 0; i--)
            {
                MaxHeapify(A, A.Length);        
            }
            for (int i = A.Length - 1; i >=0 ; i--)
            {
                sortedArray[i] = A[i];
                Swap(A[0], A[i]);
                MaxHeapify(A, i);
            }
            Console.WriteLine("Sorted MaxHeap: ");
            foreach (int item in sortedArray)
            {
                Console.Write(item + " ");
            }
        }
        //  (5)
        public void MergeHeaps(int[] fisrtArray, int[] secondArray, int sizeOffirstArray, int sizeOfsecondArray )       //Merge two MaxHeaps
        {
            int[] mergedArray= new int[sizeOfsecondArray + sizeOffirstArray];
            for (int i = 0; i < sizeOffirstArray; i++)
            {
                mergedArray[i] = fisrtArray[i];
            }
            for (int i = 0;i < sizeOfsecondArray; i++)
            {
                mergedArray[sizeOffirstArray + i] = secondArray[i];
            }
            for(int i = (sizeOffirstArray + sizeOfsecondArray)/2 - 1; i>= 0;i--)
            {
                MaxHeapify(mergedArray, i);
            }
            foreach (int item in mergedArray)
            {
                Console.Write(item + " ");
            }

        }
        //  (6)
        public int PrintKthBiggestNumInMaxHeap(int k)       //Print kth biggest number in MaxHeap
        {
            if (k > heapsize)
            {
                throw new Exception("K is larger than heap size.");
            }
            int max = 0;
            for (int i = 0; i < k; i++)
            {
                max = RemoveMax();
            }

            return max;
        }
        //   (7)
        public Node Insert(Node root, int data)     //It used in MaxHeapToBST
        {
            if (root == null)
            {
                root = new Node(data);
            }
            else if (data <= root.data)
            {
                root.left = Insert(root.left, data);
            }
            else
            {
                root.right = Insert(root.right, data);
            }
            return root;
        }
        public void AddChildsToHeap(int[] A, int i, int n, Node root)           //It used in MaxHeapToBST
        {
            
            if (i < n)
            {
                int l = Left(i);
                int r = Right(i);
                root = Insert(root, A[i]);
                AddChildsToHeap(A, r, n, root.left);
                AddChildsToHeap(A, l, n, root.right);
            }
        }
        public Node MaxHeapToBST(int[] A, int n)        //Convert MaxHeap to BST
        {
            Node root = null;
            AddChildsToHeap(A, 0, n, root);
            return root;
        }
       



    }
        

}
  


class Program
{
    public static void Main()
    {
        //----------------------------------------------------> //Creat an object of the classes
        Node rootNode = new Node(15);
        MaxHeap maxHeap = new MaxHeap(7, rootNode);

        //--------(1)--------
        maxHeap.InsertToMaxHeap(7);

        //--------(2)--------
        int[] B = { 15, 14, 10, 7, 3, 18, 1 };
        Console.WriteLine(maxHeap.IsMaxHeap(B));

        //-------- (3) --------
        int[] C = { 15, 14, 8, 7, 11, 3, 1 };
        maxHeap.BuildMaxHeap(C);
        maxHeap.FindAndDeleteFromHeap(C, 7);
        maxHeap.PrintSortedMaxHeap(C);

        //-------- (4) --------
        int[] D = { 16, 13, 10, 5, 3, 1 };
        maxHeap.PrintSortedMaxHeap(D);

        //-------- (5) --------
        int[] firstArray = { 10, 5, 6, 2 };
        int[] secondArray = { 12, 7, 9 };
        maxHeap.MergeHeaps(firstArray, secondArray, 4, 3);

        //--------(6)--------
        maxHeap.PrintKthBiggestNumInMaxHeap(3);

        //--------(7)--------
        int[] E = { 90, 15, 10, 7, 12, 2, 7, 3 };
        maxHeap.MaxHeapToBST(E, 8);


        Console.ReadKey();
    }
}