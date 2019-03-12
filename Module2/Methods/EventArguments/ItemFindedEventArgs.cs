namespace Methods.EventArguments
{
    public class ItemFindedEventArgs<T> : System.EventArgs 
        where T : System.IO.FileSystemInfo
    {
        public T FindedItem { get; set; }
        public ActionType ActionType { get; set; }
    }
}
