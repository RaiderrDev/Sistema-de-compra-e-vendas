public class LojaNaoEncontradaException : Exception
{
    public LojaNaoEncontradaException() { }
    public LojaNaoEncontradaException(string message) : base(message) { }
    public LojaNaoEncontradaException(string message, System.Exception inner) : base(message, inner) { }

}

