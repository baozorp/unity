using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
public partial class Generator : MonoBehaviour
{


    // Задаем размеры поля
    private readonly float _minZ = -5;
    private readonly float _maxZ = 5;
    private readonly float _minY = -19;
    private readonly float _minX = -0.25f;
    private readonly float _maxX = 1.25f;
    private readonly float _defaultSpeed = 0.4f;
    private float _currentSpeed = 0.1f;

    private Figure _currentFigure;
    private Figure _nextFigure;
    private Figure _holdFigure;
    public GameObject scoreField;
    public Transform nextField;
    public Transform holdField;
    private Dictionary<string, GameObject> positionsDict;
    private bool _isPause = false;
    private int _score = 0;

    void Start()
    {
        positionsDict = new();
        (_currentFigure, _nextFigure) = FiguresGenerator.GetInstance().ChoseNewFigures(transform, nextField);
        StartCoroutine(MoveDownCoroutine());
    }
    void Update()
    {
        if (_isPause)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentFigure.RotateByZ(positionsDict, _minZ, _maxZ);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Hold();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _currentSpeed = _defaultSpeed / 5;
        }
        else
        {
            _currentSpeed = _defaultSpeed;
        }
        ProcessingByCameraPosistion();
    }


    bool TestGenerateHasCollision()
    {
        int childCount = _currentFigure.transform.gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _currentFigure.transform.gameObject.transform.GetChild(i);
            // Получаем позицию на поле
            Vector3 currentStep = transform.InverseTransformPoint(child.position);
            if (
                positionsDict.ContainsKey(currentStep.ToString()))
            {
                return true;
            }
        }
        return false;
    }
}
