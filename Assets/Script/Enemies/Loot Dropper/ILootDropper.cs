using UnityEngine;

namespace Enemies.Loot_Dropper
{
    public interface ILootDropper
    {
        public void DropLoot(Vector3 priorityDropDirection);
    }
}