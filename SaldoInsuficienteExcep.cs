public class SaldoInsuficienteExcepException : Exception
{
    public SaldoInsuficienteExcepException() { }
    public SaldoInsuficienteExcepException(string message) : base(message) { }
    public SaldoInsuficienteExcepException(string message, System.Exception inner) : base(message, inner) { }
}
