using System;
using System.Collections.Generic;
using MunicipalForms.Models;

namespace MunicipalForms.Data
{
    // MinHeap prioritizes the smallest pritority value 
    public class MinHeap
    {
        private readonly List<ServiceRequest> _heap;

        public MinHeap()
        {
            _heap = new List<ServiceRequest>();
        }

        // insert new request and reorder based on its priority
        public int Count => _heap.Count;

        public void Insert(ServiceRequest request)
        {
            _heap.Add(request);
            HeapifyUp(_heap.Count - 1);
        }

        // extract and remove the service request with the highest priority
        public ServiceRequest ExtractMin()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("The heap is empty.");

            ServiceRequest min = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

        // everything below helps maintain the correct order 
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_heap[index].Priority >= _heap[parentIndex].Priority)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = _heap.Count - 1;

            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left <= lastIndex && _heap[left].Priority < _heap[smallest].Priority)
                    smallest = left;

                if (right <= lastIndex && _heap[right].Priority < _heap[smallest].Priority)
                    smallest = right;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            ServiceRequest temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }
    }
}

// Min Heap structure and operations adapted from GeeksforGeeks (2024).