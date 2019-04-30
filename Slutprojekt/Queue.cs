﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    class Queue<T>
    {
        T[] queue;
        public int Count = 0;
        public Queue()
        {
            queue = new T[16];
        }

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

        public T Peek()
        {
            return queue[0];
        }
    }
}
