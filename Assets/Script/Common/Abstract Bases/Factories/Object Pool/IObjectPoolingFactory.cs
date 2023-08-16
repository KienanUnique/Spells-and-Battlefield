using Common.Abstract_Bases.Disableable;

namespace Common.Abstract_Bases.Factories.Object_Pool
{
    public interface IObjectPoolingFactory: IDisableable
    {
        public void FillPool();
    }
}