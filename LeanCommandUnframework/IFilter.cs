namespace LeanCommandUnframework
{
    public interface IFilter<in T>
    {
        void OnHandling(T command);
        void OnHandled(T command);
    }
}