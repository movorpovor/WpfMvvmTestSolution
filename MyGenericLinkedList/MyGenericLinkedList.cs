namespace MyGenericLinkedList
{
    public class MyGenericLinkedList<T>
    {
        private MyGenericLinkedListNode<T> _firstNode;
        private MyGenericLinkedListNode<T> _lastNode;

        public event ListChangedEventHandler ListChanged;

        public T FirstItem
        {
            get => _firstNode == null ? default(T) : _firstNode.Value;
            private set
            {
                _firstNode = new MyGenericLinkedListNode<T>
                {
                    Value = value
                };

                _lastNode = _firstNode;

                ListChanged?.Invoke(new ListChangedEventArgs()
                {
                    Action = ListChangedAction.CollectionReset
                });
            }
        }
        public T LastItem => _lastNode == null ? default(T) : _lastNode.Value;
        public T PenultimateItem => (_lastNode == _firstNode) ? default(T) : _lastNode.PrevItem.Value;

        public void Add(T value)
        {
            if (_firstNode == null)
                FirstItem = value;
            else
            {
                var node = new MyGenericLinkedListNode<T>()
                {
                    PrevItem = _lastNode,
                    Value = value
                };

                _lastNode = node;
            }

            ListChanged?.Invoke(new ListChangedEventArgs()
            {
                Action = ListChangedAction.ItemAdded,
                LastItem = _lastNode.Value
            });
        }

        public void RemoveLast()
        {
            var oldItem = _lastNode;
            _lastNode = _lastNode.PrevItem;

            if (_lastNode != null)
                _lastNode.NextItem = null;
            else
                _firstNode = null;

            ListChanged?.Invoke(new ListChangedEventArgs()
            {
                Action = ListChangedAction.ItemRemoved,
                LastItem = _lastNode == null ? default(T) : _lastNode.Value
            });
            
            oldItem.Dispose();
        }
    }
}