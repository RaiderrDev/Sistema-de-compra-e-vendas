public class ClienteNaoEncontradoException : Exception
{
    public ClienteNaoEncontradoException() { }
    public ClienteNaoEncontradoException(string message) : base(message) { }
    public ClienteNaoEncontradoException(string message, System.Exception inner) : base(message, inner) { }
}