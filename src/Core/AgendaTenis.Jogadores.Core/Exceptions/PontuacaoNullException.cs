using System.Runtime.Serialization;

namespace AgendaTenis.Jogadores.Core.Exceptions;

internal class PontuacaoNullException : Exception
{
    public PontuacaoNullException()
    {
    }

    public PontuacaoNullException(string? message) : base(message)
    {
    }

    public PontuacaoNullException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PontuacaoNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
