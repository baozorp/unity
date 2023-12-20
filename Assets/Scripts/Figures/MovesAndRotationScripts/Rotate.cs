using System;
using UnityEngine;
public partial class FigureScript : MonoBehaviour
{
    void RotateByZ()
    {
        // Проверяем, можем ли повернуться по оси
        // Поворачиваемся
        _currentFigure.transform.Rotate(Vector3.left, 90, Space.World);
        // Смотрим, не возникает ли коллизий дочерних элементов
        int childCount = _currentFigure.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _currentFigure.transform.GetChild(i);
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            string keyForElement = currentStep.ToString();
            // Если возникает или вылезает за поле – поворачиваеся назад
            if (
                positionsDict.ContainsKey(keyForElement) ||
                _currentFigure.transform.localPosition.z < _minZ ||
                _currentFigure.transform.localPosition.z > _maxZ)
            {
                _currentFigure.transform.Rotate(Vector3.left, -90, Space.World);
                break;
            }
        }
    }
    // void RotateByX()
    // {
    //     // Проверяем, можем ли повернуться по оси
    //     // Поворачиваемся
    //     _currentFigure.transform.Rotate(Vector3.forward, 90, Space.World);
    //     // Смотрим, не возникает ли коллизий дочерних элементов
    //     int childCount = _currentFigure.transform.childCount;
    //     for (int i = 0; i < childCount; i++)
    //     {
    //         Transform child = _currentFigure.transform.GetChild(i);
    //         Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
    //         string keyForElement = currentStep.ToString();
    //         // Если возникает или вылезает за поле – поворачиваеся назад
    //         if (
    //             positionsDict.ContainsKey(keyForElement) ||
    //             _currentFigure.transform.localPosition.x > _minX ||
    //             _currentFigure.transform.localPosition.x < _maxX)
    //         {
    //             _currentFigure.transform.Rotate(Vector3.forward, -90, Space.World);
    //             break;
    //         }
    //     }
    // }
}