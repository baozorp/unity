using UnityEngine;
public partial class Generator : MonoBehaviour
{
    private float _cameraLastPosition = -1;
    void ProcessingByCameraPosistion()
    {
        float cameraX = Camera.main.transform.position.x;


        switch (cameraX > 0)
        {
            case true:
                HandleInputForPositiveX();
                if (_cameraLastPosition <= 0)
                {
                    RotateFields();
                }
                break;

            case false:
                HandleInputForNegativeX();
                if (_cameraLastPosition >= 0)
                {
                    RotateFields();
                }
                break;
        }
        _cameraLastPosition = Camera.main.transform.position.x;
    }

    void HandleInputForPositiveX()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _currentFigure.HorizontalMove(Vector3.left, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.S))
            _currentFigure.HorizontalMove(Vector3.right, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.D))
            _currentFigure.HorizontalMove(Vector3.forward, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.A))
            _currentFigure.HorizontalMove(Vector3.back, positionsDict, _minX, _maxX, _minZ, _maxZ);
    }

    void HandleInputForNegativeX()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _currentFigure.HorizontalMove(Vector3.right, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.S))
            _currentFigure.HorizontalMove(Vector3.left, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.D))
            _currentFigure.HorizontalMove(Vector3.back, positionsDict, _minX, _maxX, _minZ, _maxZ);
        else if (Input.GetKeyDown(KeyCode.A))
            _currentFigure.HorizontalMove(Vector3.forward, positionsDict, _minX, _maxX, _minZ, _maxZ);
    }

    void RotateFields()
    {
        {
            holdField.transform.Rotate(Vector3.up, 180);
            nextField.transform.Rotate(Vector3.up, 180);
            scoreField.transform.Rotate(Vector3.up, 180);
        }
    }
}