namespace Common.Collider_With_Disabling
{
    public interface IColliderWithDisabling : IReadonlyColliderWithDisabling
    {
        public void EnableCollider();
        public void DisableCollider();
    }
}