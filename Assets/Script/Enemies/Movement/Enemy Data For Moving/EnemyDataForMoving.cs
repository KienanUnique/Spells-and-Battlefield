using UnityEngine;

namespace Enemies.Movement.Enemy_Data_For_Moving
{
    [CreateAssetMenu(fileName = "Data For Moving",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Data For Moving", order = 0)]
    public class EnemyDataForMoving : ScriptableObject, IEnemyDataForMoving
    {
        [SerializeField] private float _needDistanceFromTarget = 0.5f;

        public float NeedDistanceFromTarget => _needDistanceFromTarget;
    }
}