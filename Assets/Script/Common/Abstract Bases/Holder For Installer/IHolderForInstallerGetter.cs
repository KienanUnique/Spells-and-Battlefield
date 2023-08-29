namespace Common.Abstract_Bases.Holder_For_Installer
{
    public interface IHolderForInstallerGetter<out T>
    {
        T ItemToHold { get; }
    }
}