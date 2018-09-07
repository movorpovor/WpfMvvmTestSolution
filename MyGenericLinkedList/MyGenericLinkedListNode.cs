using System;

namespace MyGenericLinkedList
{
    public class MyGenericLinkedListNode<T> : IDisposable
    {
        public MyGenericLinkedListNode<T> PrevItem { get; set; }
        public MyGenericLinkedListNode<T> NextItem { get; set; }
        public T Value { get; set; }

        public void Dispose()
        {
            PrevItem = null;
            NextItem = null;
        }
    }
}