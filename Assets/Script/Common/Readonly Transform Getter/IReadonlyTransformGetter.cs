using Common.Readonly_Transform;

namespace Common.Readonly_Transform_Getter
{
    public interface IReadonlyTransformGetter
    {
        IReadonlyTransform ReadonlyTransform { get; }
    }
}