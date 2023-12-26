using UnityEngine;
public partial class Tetris : MonoBehaviour
{
    private float _cameraLastPosition = -1;
    void ProcessingByCameraPosistion()
    {
        float cameraX = Camera.main.transform.position.x;
        Vector3 vector;

        switch (cameraX > 0)
        {
            case true:
                vector = InputManager.HandleInputForPositiveX();
                if (_cameraLastPosition <= 0)
                {
                    RotateFields();
                }
                break;

            case false:
                vector = -InputManager.HandleInputForPositiveX();
                if (_cameraLastPosition >= 0)
                {
                    RotateFields();
                }
                break;
        }
        _currentFigure.HorizontalMove(vector, positionsDict, _minX, _maxX, _minZ, _maxZ);
        _cameraLastPosition = Camera.main.transform.position.x;
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