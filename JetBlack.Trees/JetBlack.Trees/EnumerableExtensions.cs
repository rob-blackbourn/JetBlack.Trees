using System;
using System.Collections.Generic;
using System.Linq;

namespace JetBlack.Trees
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// A depth first traversal of a tree structure.
        /// </summary>
        /// <param name="item">The item node of the tree</param>
        /// <param name="childSelector">A function which returns the childSelector of an item.</param>
        /// <typeparam name="T">The type of the nodes in the tree.</typeparam>
        /// <returns>An enumeration of the nodes</returns>
        public static IEnumerable<T> DepthFirstTraverse<T>(this T item, Func<T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>();
            stack.Push(item);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next).Reverse())
                    stack.Push(child);
            }
        }

        /// <summary>
        /// A breadth first traversal of a tree structure
        /// </summary>
        /// <param name="item">The item node of the tree</param>
        /// <param name="childSelector">A function which returns the childSelector of an item.</param>
        /// <typeparam name="T">The type of the nodes in the tree.</typeparam>
        /// <returns>An enumeration of the nodes</returns>
        public static IEnumerable<T> BreadthFirstTraversal<T>(this T item, Func<T, IEnumerable<T>> childSelector)
        {
            var q = new Queue<T>();
            q.Enqueue(item);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                yield return current;
                foreach (var child in childSelector(current))
                    q.Enqueue(child);
            }
        }
    }
}
