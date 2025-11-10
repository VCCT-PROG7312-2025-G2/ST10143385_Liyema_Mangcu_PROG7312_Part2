namespace MunicipalForms.DataStructures
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public int Priority { get; set; } // Lower = more urgent
        public string Status { get; set; } = "";
    }

    public class MinHeap
    {
        private List<ServiceRequest> heap = new();

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public ServiceRequest? ExtractMin()
        {
            if (heap.Count == 0) return null;

            var root = heap[0];
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return root;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[index].Priority >= heap[parent].Priority) break;
                (heap[index], heap[parent]) = (heap[parent], heap[index]);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left <= lastIndex && heap[left].Priority < heap[smallest].Priority)
                    smallest = left;
                if (right <= lastIndex && heap[right].Priority < heap[smallest].Priority)
                    smallest = right;

                if (smallest == index) break;
                (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                index = smallest;
            }
        }

        public List<ServiceRequest> GetAll() => heap;
    }
}
