using UnityEngine;
public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionDistance;
    [SerializeField] private TypeOfComparison _typeOfComparison;
    private Transform _localTransform;

    protected override void CheckConditions()
    {
        var calculatedDistance = Vector3.Distance(_localTransform.position, Target.MainTransform.position);
        switch (_typeOfComparison)
        {
            case TypeOfComparison.isMore:
                if (calculatedDistance > _transitionDistance)
                {
                    NeedTransit = true;
                }
                break;
            case TypeOfComparison.isLess:
                if (calculatedDistance < _transitionDistance)
                {
                    NeedTransit = true;
                }
                break;
        }
    }

    private void Awake()
    {
        _localTransform = transform;
    }

    private enum TypeOfComparison
    {
        isMore,
        isLess
    }
}