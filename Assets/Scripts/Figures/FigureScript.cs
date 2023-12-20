using System.Collections.Generic;
using UnityEngine;

public partial class FigureScript : MonoBehaviour
{


    // Задаем размеры поля
    private float _minZ = -5;
    private float _maxZ = 4;
    private float _minY = -19;
    private float _minX = 0;
    private float _maxX = 1;
    private float iters = 0;

    // Задаем фигуры
    public List<GameObject> figures;

    private GameObject _currentFigure;
    private GameObject _nextFigure;
    private int _nextFigureNumber = -1;

    public Transform nextField;
    public Transform holdField;
    private Dictionary<string, GameObject> positionsDict;


    void Start()
    {
        int countOfFigures = figures.Count;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateByZ();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateByZ();
        }
    }


}
