using System.Collections.Generic;
using UnityEngine;

public partial class FigureScript : MonoBehaviour
{


    // Задаем размеры поля
    // private int _minZ = -10;
    // private int _minY = -20;
    // private int _maxX = 1;
    private int iters = 0;

    // Задаем фигуры
    public GameObject[] figures = new GameObject[7];

    private GameObject _currentFigure;
    private GameObject _nextFigure;
    private int nextFigureNumber = -1;

    public Transform nextField;
    public Transform holdField;
    private Dictionary<string, GameObject> positionsDict;


    void Start()
    {
        int countOfFigures = figures.Length;
        if (countOfFigures == 0)
        {
            Debug.Log("List of figures is empty!");
            return;
        }
        // Сохраняем первоначальное положение фигур
        positionsDict = new();
        ChoseNewFigures();
        StartCoroutine(MoveDownCoroutine());
    }
    void Update()
    {
    }


}
