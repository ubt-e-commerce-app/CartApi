using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class KafkaExceptions : Exception
{
    public KafkaExceptions() { }

    public KafkaExceptions(string message) : base(message) { }

    public KafkaExceptions(string message, Exception? innerException) : base(message, innerException) { }

    protected KafkaExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
