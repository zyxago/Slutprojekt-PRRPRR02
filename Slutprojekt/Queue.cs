using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public class Queue<T>
    {
        T[] queue;
        public int Count { get; private set; } = 0;

        public Queue()
        {
            queue = new T[16];
        }

        public Queue(Queue<T> TempQueue)
        {
            Count = TempQueue.Count;
            queue = new T[TempQueue.queue.Length];
            TempQueue.queue.CopyTo(queue, 0);
        }
        /// <summary>
        /// Add object to queue
        /// </summary>
        /// <param name="data">Data or object you want to add to queue</param>
        public void Enqueue(T data)
        {
            if(queue.Length == Count)
            {
                T[] temp = queue;
                queue = new T[Count + 16];
                temp.CopyTo(queue, 0);
            }
            queue[Count++] = data;
        }
        /// <summary>
        /// Return first object in queue and then remove it from the queue
        /// </summary>
        /// <returns>Returns first object from queue</returns>
        public T Dequeue()
        {
            T value = queue[0];
            T[] temp = new T[queue.Length-1];
            for(int i = 0; i < temp.Length; i++)
            {
                temp[i] = queue[i+1];
            }
            queue = temp;
            Count--;
            return value;
        }
        /// <summary>
        /// Return first object in queue 
        /// </summary>
        /// <returns>Returns first object from queue</returns>
        public T Peek()
        {
            return queue[0];
        }
        /// <summary>
        /// Checks if queue is empty
        /// Returns TRUE if empty, FALSE if not empty
        /// </summary>
        /// <returns>TRUE if empty, FALSE if not empty</returns>
        public bool IsEmpty()
        {
            if (Count == 0)
                return true;
            else
                return false;
        }
    }
}
