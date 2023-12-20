using System.Collections;
using UnityEngine;
public partial class FigureScript : MonoBehaviour
{
    IEnumerator MoveDownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Transform currentTransform = _currentFigure.transform;
            int childCount = currentTransform.childCount;
            iters++;
            bool isBottom = false;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = currentTransform.GetChild(i);
                Vector3 nextStep = new(
                    child.transform.localPosition.x,
                    child.transform.localPosition.y - 1f,
                    child.transform.localPosition.z
                );
                Vector3 postionOnTetris = currentTransform.localPosition + nextStep;
                string keyPosition = postionOnTetris.ToString();
                if (
                    positionsDict.ContainsKey(keyPosition) ||
                    (currentTransform.localPosition.y + child.localPosition.y - 1f) < 0f
                )
                {
                    isBottom = true;
                    break;
                }
            }
            if (isBottom)
            {
                BottomActions(currentTransform, childCount);
            }
            else
            {
                currentTransform.localPosition = new Vector3(
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
            GameObject childsCopy = Instantiate(
                child,
                child.transform.position,
                child.transform.rotation,
                transform
            );
            Vector3 postionOnTetris = currentTransform.localPosition + child.transform.localPosition;
            Debug.Log("Iterations: " + iters + " \nobject is " + childsCopy);
            positionsDict[postionOnTetris.ToString()] = childsCopy;
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
        int current_number = (nextFigureNumber == -1) ? UnityEngine.Random.Range(0, figures.Length - 1) : nextFigureNumber;
        int next_number = UnityEngine.Random.Range(0, figures.Length - 1);
        GameObject chosenFigure = figures[current_number];
        GameObject nextChangedFigure = figures[next_number];
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
    }
}