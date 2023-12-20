using UnityEngine;
public partial class GeneratorScript : MonoBehaviour
{
    void HorizontalMove(string direction)
    {
        if (_currentFigure == null)
        {
            return;
        }
        Transform currentTransform = _currentFigure.transform;
        int childCount = currentTransform.childCount;
        bool isMovePossible = true;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _currentFigure.transform.GetChild(i);
            // Получаем позицию на поле
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            Vector3 nextStep = SwitchDirection(direction, currentStep);
            if (
                positionsDict.ContainsKey(nextStep.ToString()) ||
                nextStep.x < _minX ||
                nextStep.x > _maxX ||
                nextStep.z < _minZ ||
                nextStep.z > _maxZ)
            {
                isMovePossible = false;
                break;
            }
        }
        if (isMovePossible)
        {
            _currentFigure.transform.localPosition = SwitchDirection(direction, _currentFigure.transform.localPosition);
        }
    }

    private Vector3 SwitchDirection(string direction, Vector3 currentStep)
    {
        return direction switch
        {
            "forward" => new(
                            currentStep.x + 1f,
                            currentStep.y,
                            currentStep.z
                            ),
            "back" => new(
                            currentStep.x - 1f,
                            currentStep.y,
                            currentStep.z
                            ),
            "left" => new(
                            currentStep.x,
                            currentStep.y,
                            currentStep.z + 1f
                            ),
            "right" => new(
                            currentStep.x,
                            currentStep.y,
                            currentStep.z - 1f
                            ),
            _ => Vector3.zero,
        };
    }
}