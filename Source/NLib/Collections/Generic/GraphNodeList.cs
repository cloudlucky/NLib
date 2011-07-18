namespace NLib.Collections.Generic
{
    using System.Collections.ObjectModel;

    public class GraphNodeList<T> : Collection<GraphNode<T>>
    {
        public GraphNodeList()
        {
        }

        /// <summary>
        /// Searches the NodeList for a Node containing a particular value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The Node in the NodeList, if it exists; null otherwise.</returns>
        public GraphNode<T> FindByValue(T value)
        {
            // search the list for the value
            foreach (GraphNode<T> node in Items)
                if (node.Value.Equals(value))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }


    }
}
