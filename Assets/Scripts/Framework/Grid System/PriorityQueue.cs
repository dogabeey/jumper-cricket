﻿using System.Collections.Generic;

namespace Lionsfall
{
    public class PriorityQueue<T>
    {
        private readonly List<(T item, int priority)> elements = new();

        public int Count => elements.Count;

        public void Enqueue(T item, int priority)
        {
            elements.Add((item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;
            int bestPriority = elements[0].priority;

            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].priority < bestPriority)
                {
                    bestPriority = elements[i].priority;
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].item;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }

        public bool Contains(T item)
        {
            return elements.Exists(e => EqualityComparer<T>.Default.Equals(e.item, item));
        }
    }

}