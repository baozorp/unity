using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FigureScript : MonoBehaviour
{


    // Задаем размеры поля
    private int _minZ = -10;
    private int _minY = -20;
    private int _maxX = 1;

    // Задаем фигуры
    public List<GameObject> figures;
    private GameObject _currentFigure;
    private GameObject _nextFigure;

    public Transform next_field;
    public Transform hold_field;

    void Start()
    {
        // Устаналиваем координаты объектов в нулевую точку генератора
        for (int i = 0; i < figures.Count; i++)
        {
            GameObject figure = figures[i];
            figure.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z
            );
            Debug.Log(transform.position);
        }
        var current_number = new System.Random().Next(0, figures.Count);
        var next_number = new System.Random().Next(0, figures.Count);
        GameObject chosen_figure = figures[current_number];
        GameObject next_changed_figure = figures[next_number];
        _currentFigure = Instantiate(
            chosen_figure,
            chosen_figure.transform.position,
            chosen_figure.transform.rotation,
            chosen_figure.transform.parent
            );
        _nextFigure = Instantiate(
            next_changed_figure,
            next_field.transform.position,
            next_changed_figure.transform.rotation,
            next_changed_figure.transform.parent);
        _currentFigure.SetActive(true);
        _nextFigure.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // for (int i = 0; i < objects.Count; i++)
        // {
        //     if (i == 0)
        //     {
        //         Vector3 position = objects[i].transform.localPosition;
        //         objects[i].transform.localPosition = new Vector3(position.x + Time.deltaTime, position.y, position.z);
        //         Debug.Log(objects[i].transform.transform.localPosition);
        //     }
        // }
    }
}
