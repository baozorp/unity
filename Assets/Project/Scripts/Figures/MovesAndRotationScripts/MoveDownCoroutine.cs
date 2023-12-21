using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public partial class FigureScript : MonoBehaviour
{
    IEnumerator MoveDownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_currentSpeed);
            if (_isPause)
            {
                continue;
            }
            if (_currentFigure == null)
            {
                break;
            }
            TryMoveDown();
        }
    }

    void TryMoveDown()
    {
        Transform currentTransform = _currentFigure.transform;
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
                currentStep.y < _minY
            )
            {
                isBottom = true;
                BottomActions(currentTransform, childCount);
                break;
            }
        }
        if (!isBottom)
        {
            currentTransform.localPosition = new(
                currentTransform.localPosition.x,
                currentTransform.localPosition.y - 1f,
                currentTransform.localPosition.z);
        };
    }



    void BottomActions(Transform currentTransform, int childCount)
    {
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = currentTransform.GetChild(i).gameObject;
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.transform.position);
            GameObject childsCopy = Instantiate(
                child,
                child.transform.position,
                child.transform.rotation,
                transform
            );
            // Заносим занятые позиции в словарь
            positionsDict[currentStep.ToString()] = childsCopy;
        }
        Dictionary<float, int> a = new Dictionary<float, int>();
        foreach (KeyValuePair<string, GameObject> entry in positionsDict)
        {
            float yPosition = entry.Value.transform.position.y;
            if (a.ContainsKey(yPosition))
            {
                a[yPosition]++;
            }
            else
            {
                a[yPosition] = 1;
            }
        }

        foreach (KeyValuePair<float, int> coordinate in a)
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
        Destroy(currentTransform.gameObject);
        ChoseNewFigures();
        return;
    }

    void ChoseNewFigures()
    {
        if (_nextFigure != null)
        {
            Destroy(_nextFigure);
        }
        int currentNumber = (_nextFigureNumber == -1) ? Random.Range(0, figures.Count - 1) : _nextFigureNumber;
        _nextFigureNumber = Random.Range(0, figures.Count - 1);
        GameObject chosenFigure = figures[currentNumber];
        GameObject nextChangedFigure = figures[_nextFigureNumber];
        _currentFigure = Instantiate(
            chosenFigure,
            chosenFigure.transform.position,
            chosenFigure.transform.rotation,
            chosenFigure.transform.parent
        );
        _nextFigure = Instantiate(
            nextChangedFigure,
            nextField.transform.position,
            nextChangedFigure.transform.rotation,
            nextChangedFigure.transform.parent);
        _currentFigure.SetActive(true);
        _nextFigure.SetActive(true);
        if (TestGenerateHasCollision())
        {
            StopAllCoroutines();
            StartCoroutine(GameOver());
        };
    }


    bool TestGenerateHasCollision()
    {
        int childCount = _currentFigure.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _currentFigure.transform.GetChild(i);
            // Получаем позицию на поле
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            if (
                positionsDict.ContainsKey(currentStep.ToString()))
            {
                return true;
            }
        }
        return false;
    }
}