namespace LeanCommandUnframework
{
    public interface IHandler<in T>
    {
        object Handle(T command);
    }
}