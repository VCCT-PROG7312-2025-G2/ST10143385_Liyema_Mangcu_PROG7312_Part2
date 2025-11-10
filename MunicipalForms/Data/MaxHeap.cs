using System;
using System.Collections.Generic;

namespace MunicipalForms.DataStructures
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private List<T> heap;

        public MaxHeap()
        {
            heap = new List<T>();
        }

        public int Size => heap.Count;

        public void Insert(T value)
        {
            heap.Add(value);
            HeapifyUp(heap.Count - 1);
        }

        public T ExtractMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            T max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return max;
        }

        public T PeekMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].CompareTo(heap[parentIndex]) > 0)
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int largest = index;

            if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[largest]) > 0)
                largest = leftChild;

            if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[largest]) > 0)
                largest = rightChild;

            if (largest != index)
            {
                Swap(index, largest);
                HeapifyDown(largest);
            }
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}