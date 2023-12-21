using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
public partial class FigureScript : MonoBehaviour
{


    // Задаем размеры поля
    private readonly float _minZ = -10;
    private readonly float _maxZ = 0;
    private readonly float _minY = -19;
    private readonly float _minX = -0.25f;
    private readonly float _maxX = 1.25f;
    private readonly float _defaultSpeed = 0.4f;
    private float _currentSpeed = 0.1f;

    public GameObject pause_button;
    // Задаем фигуры
    public List<GameObject> figures;

    private GameObject _currentFigure;
    private GameObject _nextFigure;
    private GameObject _holdFigure;
    public GameObject scoreField;
    private int _nextFigureNumber = -1;
    private int _holdFigureNumber = -1;

    public Transform nextField;
    public Transform holdField;
    private Dictionary<string, GameObject> positionsDict;
    private bool _isPause = false;
    private int _score = 0;

    void Start()
    {
        int countOfFigures = figures.Count;
        if (countOfFigures == 0)
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
        if (_isPause)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateByZ();
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

    public void Pause()
    {
        _isPause = !_isPause;
    }

    IEnumerator GameOver()
    {
        _isPause = true;
        GameObject gameOverField = Camera.main.transform.GetChild(0).gameObject;
        for (int i = 0; i < gameOverField.transform.childCount; i++)
        {
            gameOverField.SetActive(true);
            if (gameOverField.transform.GetChild(i).name == "Text")
            {
                TextMeshPro textField = gameOverField.transform.GetChild(i).GetChild(0).GetComponent<TextMeshPro>();
                string savedText = textField.text;
                for (int j = 5; j > 0; j--)
                {
                    textField.text = savedText + "\n" +
                     "Игра возобновится через \n" + j.ToString();
                    yield return new WaitForSeconds(1f);
                }
                textField.text = savedText;
                gameOverField.SetActive(false);
            }
        }
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
        _isPause = false;
        Start();

    }
}
