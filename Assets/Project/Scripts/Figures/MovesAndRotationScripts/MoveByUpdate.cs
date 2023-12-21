using UnityEngine;
public partial class FigureScript : MonoBehaviour
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
            HorizontalMove("back");
        else if (Input.GetKeyDown(KeyCode.S))
            HorizontalMove("forward");
        else if (Input.GetKeyDown(KeyCode.D))
            HorizontalMove("left");
        else if (Input.GetKeyDown(KeyCode.A))
            HorizontalMove("right");
    }

    void HandleInputForNegativeX()
    {
        if (Input.GetKeyDown(KeyCode.W))
            HorizontalMove("forward");
        else if (Input.GetKeyDown(KeyCode.S))
            HorizontalMove("back");
        else if (Input.GetKeyDown(KeyCode.D))
            HorizontalMove("right");
        else if (Input.GetKeyDown(KeyCode.A))
            HorizontalMove("left");
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