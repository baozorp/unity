using UnityEngine;

class InputManager
{
    public static Vector3 HandleInputForPositiveX()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return Vector3.left;
        else if (Input.GetKeyDown(KeyCode.S))
            return Vector3.right;
        else if (Input.GetKeyDown(KeyCode.D))
            return Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.A))
            return Vector3.back;
        return Vector3.zero;
    }
}