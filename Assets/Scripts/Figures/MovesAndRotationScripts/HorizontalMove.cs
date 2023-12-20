using System.Collections;
using UnityEngine;
public partial class FigureScript : MonoBehaviour
{
    void HorizontalMove()
    {
        Transform child = currentTransform.GetChild(i);

        // Получаем позицию на поле
        Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
        // Рассчитываем следующий шаг
        Vector3 nextStep = new(
            currentStep.x,
            currentStep.y - 1f,
            currentStep.z
        );
    }
}