using UnityEngine;
public partial class Generator : MonoBehaviour
{
    void Hold()
    {
        if (_holdFigure == null)
        {
            _holdFigure = _nextFigure;
            _holdFigure.transform.position = holdField.position;
            _nextFigure = FiguresGenerator.GetInstance().GenerateFigure(transform.gameObject, nextField);
        }
        else
        {
            FiguresGenerator.GetInstance().ReverseHoldAndNextNumbers();
            (_holdFigure.transform.position, _nextFigure.transform.position) = (nextField.position, holdField.position);
            (_holdFigure, _nextFigure) = (_nextFigure, _holdFigure);
        }
    }
}