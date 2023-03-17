using UnityEngine;
public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread = 0f;
    private Transform _localTransform;

    protected override void CheckConditions()
    {
        if (Vector3.Distance(_localTransform.position, Target.MainTransform.position) <= _transitionRange)
        {
            NeedTransit = true;
        }
    }

    private void Awake()
    {
        _localTransform = transform;
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
    }
}