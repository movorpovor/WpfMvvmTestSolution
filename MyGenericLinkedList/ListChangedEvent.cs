namespace MyGenericLinkedList
{
    public delegate void ListChangedEventHandler(ListChangedEventArgs args);

    public class ListChangedEventArgs
    {
        public ListChangedAction Action { get; set; }
        public object LastItem { get; set; }
    }

    public enum ListChangedAction
    {
        ItemAdded,
        ItemRemoved,
        CollectionReset
    }
}