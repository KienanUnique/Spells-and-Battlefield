namespace Common.Providers
{
    public interface IImplementationObjectProvider<out TImplementation>
    {
        TImplementation GetImplementationObject();
    }
}