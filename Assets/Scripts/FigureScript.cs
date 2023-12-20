using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FigureScript : MonoBehaviour
{


    // Задаем размеры поля
    // private int _minZ = -10;
    // private int _minY = -20;
    // private int _maxX = 1;
    private int iters = 0;

    // Задаем фигуры
    public List<GameObject> figures;
    private GameObject _currentFigure;
    private GameObject _nextFigure;

    public Transform next_field;
    public Transform hold_field;
    private Dictionary<string, GameObject> positionsDict;

    void Start()
    {
        if (figures.Count == 0)
        {
            Debug.Log("List of figures is empty!");
            return;
        }
        positionsDict = new();
        ChoseNewFigures();
        StartCoroutine(MoveDownCoroutine());
    }
    void Update()
    {
    }

    IEnumerator MoveDownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            IterativeMoveDown();
        }
    }

    void IterativeMoveDown()
    {
        Transform currentTransform = _currentFigure.transform;
        int childCount = currentTransform.childCount;
        iters++;
        bool isBottom = false;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = currentTransform.GetChild(i);
            if (currentTransform.localPosition.y + child.localPosition.y - 1f < 0f)
            {
                isBottom = true;
                break;
            }
        }
        if (isBottom)
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
        else
        {
            currentTransform.localPosition = new Vector3(
                currentTransform.localPosition.x,
                currentTransform.localPosition.y - 1f,
                currentTransform.localPosition.z);
        };
    }

    void ChoseNewFigures()
    {
        int current_number = UnityEngine.Random.Range(0, figures.Count - 1);
        int next_number = UnityEngine.Random.Range(0, figures.Count - 1);
        GameObject chosen_figure = figures[current_number];
        GameObject next_changed_figure = figures[next_number];
        if (_nextFigure == null)
        {
            _currentFigure = Instantiate(
                chosen_figure,
                chosen_figure.transform.position,
                chosen_figure.transform.rotation,
                chosen_figure.transform.parent
            );
        }
        else
        {
            _currentFigure = _nextFigure;
            _currentFigure.transform.position = transform.position;
        }
        _nextFigure = Instantiate(
            next_changed_figure,
            next_field.transform.position,
            next_changed_figure.transform.rotation,
            next_changed_figure.transform.parent);
        _currentFigure.SetActive(true);
        _nextFigure.SetActive(true);
    }
}
