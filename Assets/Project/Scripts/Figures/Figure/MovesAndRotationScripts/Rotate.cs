using System;
using System.Collections.Generic;
using UnityEngine;
public partial class Figure : MonoBehaviour
{
    public void RotateByZ(Dictionary<string, GameObject> positionsDict, float _minZ, float _maxZ)
    {
        // Проверяем, можем ли повернуться по оси
        // Поворачиваемся
        transform.Rotate(Vector3.left, 90, Space.World);
        // Смотрим, не возникает ли коллизий дочерних элементов
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            string keyForElement = currentStep.ToString();
            // Если возникает или вылезает за поле – поворачиваеся назад
            if (
                positionsDict.ContainsKey(keyForElement) ||
                currentStep.z < _minZ ||
                currentStep.z > _maxZ)
            {
                transform.Rotate(Vector3.left, -90, Space.World);
                break;
            }
        }
    }
}