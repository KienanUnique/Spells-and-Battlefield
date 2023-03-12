using UnityEngine;

[CreateAssetMenu(fileName = "Rotating Around Caster Movement", menuName = "Spells and Battlefield/Spell System/Movement/Rotating Around Caster Movement", order = 0)]
public class RotatingAroundCasterMovement : SpellMovementScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    public override ISpellMovement GetImplementationObject() => new RotatingAroundCasterMovementImplementation(_speed, _radius);

    private class RotatingAroundCasterMovementImplementation : SpellMovementImplementationBase
    {
        private float _speed;
        private float _radius;

        public RotatingAroundCasterMovementImplementation(float speed, float radius)
        {
            _speed = speed;
            _radius = radius;
        }
#nullable enable
        public override void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize)
        {
            if (fromCastObjectTransform == null)
            {
                return;
            }

            var fromCastObjectPosition = fromCastObjectTransform.position;
            var spellRigidbodyTransform = spellRigidbody.transform;
            if (!Mathf.Approximately(_radius, Vector3.Distance(fromCastObjectPosition, spellRigidbodyTransform.position)))
            {
                var direction = (spellRigidbodyTransform.position - fromCastObjectPosition).normalized;
                spellRigidbodyTransform.position = fromCastObjectPosition + direction * _radius;
                spellRigidbodyTransform.position = new Vector3(spellRigidbodyTransform.position.x, fromCastObjectPosition.y, spellRigidbodyTransform.position.z);
            }

            spellRigidbodyTransform.RotateAround(fromCastObjectPosition, spellRigidbodyTransform.up, _speed * Time.fixedDeltaTime);
        }
#nullable disable
    }
}
