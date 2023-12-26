using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
public partial class Generator : MonoBehaviour
{

    IEnumerator MoveDownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_currentSpeed);
            if (_currentFigure == null)
            {
                break;
            }
            bool is_bottom = _currentFigure.TryMoveDown(positionsDict, _minY);
            if (is_bottom)
            {
                Destroy(_currentFigure.transform.gameObject);
                Destroy(_nextFigure.transform.gameObject);
                BottomActions();
                (_currentFigure, _nextFigure) = FiguresGenerator.GetInstance().ChoseNewFigures(transform, nextField);
            }
            if (TestGenerateHasCollision())
            {
                StopAllCoroutines();
                StartCoroutine(GameOver());
            }
        }
    }

    void BottomActions()
    {
        Dictionary<float, int> YAxeAggregation = new Dictionary<float, int>();
        foreach (KeyValuePair<string, GameObject> entry in positionsDict)
        {
            float yPosition = entry.Value.transform.position.y;
            if (YAxeAggregation.ContainsKey(yPosition))
            {
                YAxeAggregation[yPosition]++;
            }
            else
            {
                YAxeAggregation[yPosition] = 1;
            }
        }

        foreach (KeyValuePair<float, int> coordinate in YAxeAggregation)
        {
            if (coordinate.Value >= 20)
            {
                _score++;
                TextMeshPro _scoreFieldTextMesh = scoreField.GetComponent<TextMeshPro>();
                _scoreFieldTextMesh.text = _score.ToString();
                List<string> keysToRemove = new List<string>();
                Dictionary<string, GameObject> updatedElements = new Dictionary<string, GameObject>();

                foreach (KeyValuePair<string, GameObject> entry in positionsDict)
                {
                    float yPosition = entry.Value.transform.position.y;

                    if (yPosition == coordinate.Key)
                    {
                        // Удалить объект, если его значение равно 20 и его y-координата совпадает с coordinate.Key
                        keysToRemove.Add(entry.Key);
                        Destroy(entry.Value);
                    }
                    else if (yPosition > coordinate.Key)
                    {
                        // Сдвинуть объект вниз, если его y-координата больше coordinate.Key
                        entry.Value.transform.localPosition = new Vector3(
                            entry.Value.transform.localPosition.x,
                            entry.Value.transform.localPosition.y - 1f,
                            entry.Value.transform.localPosition.z
                        );
                        // Сохранить новый ключ и значение после сдвига
                        Vector3 movedElementPosition = transform.parent.InverseTransformPoint(entry.Value.transform.position);
                        string newKey = movedElementPosition.ToString();
                        updatedElements[newKey] = entry.Value;
                        // Очистить старое значение словаря
                        keysToRemove.Add(entry.Key);
                    }
                }

                // Заменить старые ключи на новые в словаре
                foreach (string keyToRemove in keysToRemove)
                {
                    positionsDict.Remove(keyToRemove);
                }

                foreach (KeyValuePair<string, GameObject> updatedElement in updatedElements)
                {
                    positionsDict[updatedElement.Key] = updatedElement.Value;
                }
            }
        }
        return;
    }
}