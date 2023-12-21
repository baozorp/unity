using UnityEngine;
public partial class FigureScript : MonoBehaviour
{
    void Hold()
    {
        if (_holdFigure == null)
        {
            _nextFigure.transform.position = holdField.position;
            _holdFigure = _nextFigure;
            _holdFigureNumber = _nextFigureNumber;
            _nextFigureNumber = Random.Range(0, figures.Count - 1);
            GameObject nextChangedFigure = figures[_nextFigureNumber];
            _nextFigure = Instantiate(
                nextChangedFigure,
                nextField.transform.position,
                nextChangedFigure.transform.rotation,
                nextChangedFigure.transform.parent);
            _nextFigure.SetActive(true);
        }
        else
        {
            // Меняем фигуры местами
            (_holdFigureNumber, _nextFigureNumber) = (_nextFigureNumber, _holdFigureNumber);
            (_holdFigure.transform.position, _nextFigure.transform.position) = (nextField.position, holdField.position);
            (_holdFigure, _nextFigure) = (_nextFigure, _holdFigure);
        }


    }
}