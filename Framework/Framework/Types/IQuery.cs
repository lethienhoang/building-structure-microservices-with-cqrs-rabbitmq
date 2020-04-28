namespace Framework.Types
{
    public interface IQuery : IMessage
    {
    }

    public interface IQuery<T> : IQuery, IMessage
    {
    }
}
