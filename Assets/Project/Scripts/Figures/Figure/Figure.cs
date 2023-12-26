using System.Collections.Generic;
using UnityEngine;

public partial class Figure : MonoBehaviour
{
    public bool TryMoveDown(Dictionary<string, GameObject> positionsDict, float _minY)
    {
        Transform currentTransform = transform;
        int childCount = currentTransform.childCount;
        bool isBottom = false;
        for (int i = 0; i < childCount; i++)
        {
            //Получаем дочерний объект
            Transform child = currentTransform.GetChild(i);

            // Получаем позицию на поле
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            // Рассчитываем следующий шаг
            Vector3 nextStep = new(
                currentStep.x,
                currentStep.y - 1f,
                currentStep.z
            );
            // Получаем ключ для словаря позиций
            string keyForElement = nextStep.ToString();

            if (
                positionsDict.ContainsKey(keyForElement) ||
                nextStep.y < _minY
            )
            {
                isBottom = true;
                // BottomActions(currentTransform, childCount);
                break;

            }
        }
        if (isBottom)
        {
            for (int i = 0; i < childCount; i++)
            {
                GameObject child = currentTransform.GetChild(i).gameObject;
                Vector3 currentStep = transform.parent.InverseTransformPoint(child.transform.position);
                GameObject childsCopy = Instantiate(
                    child,
                    child.transform.position,
                    child.transform.rotation,
                    transform.parent
                );
                // Заносим занятые позиции в словарь
                positionsDict[currentStep.ToString()] = childsCopy;
            }
        }
        else
        {
            currentTransform.localPosition = new(
                currentTransform.localPosition.x,
                currentTransform.localPosition.y - 1f,
                currentTransform.localPosition.z);
        };
        return isBottom;
    }
}
