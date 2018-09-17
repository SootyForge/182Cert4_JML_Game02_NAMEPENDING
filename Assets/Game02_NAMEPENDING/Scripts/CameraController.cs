using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    #region Variables - Camera Movement
    public Transform target;

    private float x = 0f;
    private float y = 0f;

    private float yMinLimit = -90f;
    private float yMaxLimit = 90f;

    public float xSensitivity = 120f;
    public float ySensitivity = 120f;

    public bool hideCursor = true;
    #endregion

    #endregion

    // Use this for initialization
    void Start()
    {
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        #region Camera Rotation
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(y, x, 0);
        }
        #endregion
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360;
        if (angle > 360f)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
