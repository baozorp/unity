using System.Collections.Generic;
using UnityEngine;

class FiguresGenerator : MonoBehaviour
{
    private static FiguresGenerator instance;
    private FiguresGenerator() { }
    int _nextFigureNumber = -1;
    int _holdNumber = -1;

    public List<GameObject> figures;
    public (Figure, Figure) ChoseNewFigures(Transform parent, Transform nextField)
    {
        int countOfFigures = figures.Count;
        if (countOfFigures == 0)
        {
            Debug.Log("List of figures is empty!");
        }
        int currentNumber = (_nextFigureNumber == -1) ? Random.Range(0, figures.Count - 1) : _nextFigureNumber;
        _nextFigureNumber = Random.Range(0, figures.Count - 1);
        GameObject chosenGameObject = figures[currentNumber];
        GameObject nextChosenGameObject = figures[_nextFigureNumber];
        GameObject currentGameObject = Instantiate(
            chosenGameObject,
            parent.position,
            parent.rotation,
            parent.transform
        );
        currentGameObject.transform.localPosition = new Vector3(
            chosenGameObject.transform.localPosition.x,
            currentGameObject.transform.localPosition.y + chosenGameObject.transform.localPosition.y,
            currentGameObject.transform.localPosition.z + chosenGameObject.transform.localPosition.z
        );
        GameObject nextGameObject = Instantiate(
            nextChosenGameObject,
            nextField.position,
            parent.rotation,
            parent);
        var currentFigure = currentGameObject.GetComponent<Figure>();
        var nextFigure = nextGameObject.GetComponent<Figure>();

        return (currentFigure, nextFigure);
    }



    public Figure GenerateFigure(GameObject parent, Transform nextField)
    {
        _holdNumber = _nextFigureNumber;
        _nextFigureNumber = Random.Range(0, figures.Count - 1);
        GameObject nextChangedFigure = figures[_nextFigureNumber];
        GameObject figure = Instantiate(
            nextChangedFigure,
            nextField.position,
            parent.transform.rotation,
            parent.transform);
        figure.SetActive(true);
        var b = figure.GetComponent<Figure>();
        return b;
    }

    public void ReverseHoldAndNextNumbers()
    {
        (_holdNumber, _nextFigureNumber) = (_nextFigureNumber, _holdNumber);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static FiguresGenerator GetInstance()
    {
        return instance;
    }
}