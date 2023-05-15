namespace Spells.Abstract_Types.Scriptable_Objects
{
    public interface IImplementationObjectProvider<out TImplementation>
    {
        TImplementation GetImplementationObject();
    }
}