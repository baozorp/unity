using System.Collections.Generic;
using UnityEngine;
public partial class Figure : MonoBehaviour
{
    public void HorizontalMove(Vector3 vector,
                                Dictionary<string, GameObject> positionsDict,
                                float _minX,
                                float _maxX,
                                float _minZ,
                                float _maxZ)
    {
        int childCount = transform.childCount;
        bool isMovePossible = true;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            // Получаем позицию на поле
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            Vector3 nextStep = currentStep + vector;
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
            transform.localPosition = transform.localPosition + vector;
        }
    }

}