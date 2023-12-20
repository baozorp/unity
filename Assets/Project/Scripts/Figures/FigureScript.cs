using System.Collections.Generic;
using UnityEngine;

public partial class GeneratorScript : MonoBehaviour
{


    // Задаем размеры поля
    private readonly float _minZ = -10;
    private readonly float _maxZ = 0;
    private readonly float _minY = -19;
    private readonly float _minX = -0.25f;
    private readonly float _maxX = 1.25f;

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
            HorizontalMove("forward");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            HorizontalMove("back");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HorizontalMove("right");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            HorizontalMove("left");
        }
    }

    void GameOver()
    {
        Destroy(_currentFigure);
        Destroy(_nextFigure);
        int count = transform.childCount;
        for (int childOfField = 0; childOfField < count; childOfField++)
        {
            var childToDel = transform.GetChild(childOfField).gameObject;
            var childToDelName = childToDel.name;
            if (childToDelName.Contains("Clone"))
            {
                Destroy(childToDel);
            }
        }
        return;
    }


}
