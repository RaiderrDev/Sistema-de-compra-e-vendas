public class CategoriaNaoEncontradaException : Exception
{
    public CategoriaNaoEncontradaException() { }
    public CategoriaNaoEncontradaException(string message) : base(message) { }
    public CategoriaNaoEncontradaException(string message, System.Exception inner) : base(message, inner) { }
}