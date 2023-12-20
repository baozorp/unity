using System.Collections;
using UnityEngine;
public partial class FigureScript : MonoBehaviour
{
    IEnumerator MoveDownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_currentFigure == null)
            {
                break;
            }
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
            positionsDict[currentStep.ToString()] = childsCopy;
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
        int current_number = (this._nextFigureNumber == -1) ? UnityEngine.Random.Range(0, figures.Count - 1) : this._nextFigureNumber;
        int _nextFigureNumber = UnityEngine.Random.Range(0, figures.Count - 1);
        GameObject chosenFigure = figures[current_number];
        GameObject nextChangedFigure = figures[_nextFigureNumber];
        _currentFigure = Instantiate(
            chosenFigure,
            chosenFigure.transform.position,
            chosenFigure.transform.rotation,
            chosenFigure.transform.parent
        );
        int childCount = _currentFigure.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _currentFigure.transform.GetChild(i);
            // Получаем позицию на поле
            Vector3 currentStep = transform.parent.InverseTransformPoint(child.position);
            if (
                positionsDict.ContainsKey(currentStep.ToString()))
            {
                Destroy(_currentFigure);
                Destroy(_nextFigure);
                int count = transform.parent.childCount;
                for (int childOfField = 0; childOfField < count; childOfField++)
                {
                    var childToDel = transform.parent.GetChild(childOfField).gameObject;
                    var childToDelName = childToDel.name;
                    if (childToDelName.Contains("Clone"))
                    {
                        Destroy(childToDel);
                    }
                }
                return;
            }
        }
        _nextFigure = Instantiate(
            nextChangedFigure,
            nextField.transform.position,
            nextChangedFigure.transform.rotation,
            nextChangedFigure.transform.parent);
        _currentFigure.SetActive(true);
        _nextFigure.SetActive(true);
    }
}