using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    #region Variables - Camera Movement
    /*
    #region MouseLook Transplant Test
    public RotationalAxis axis = RotationalAxis.MouseX;
    #endregion
    */
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
        /*
        #region MouseLook Transplant Test
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        #endregion
        */
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
        /*
        #region MouseLook Transplant Test
        if (axis == RotationalAxis.MouseXandY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * xSensitivity;
            y += Input.GetAxis("Mouse Y") * ySensitivity;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            transform.localEulerAngles = new Vector3(-y, rotationX, 0);
        }

        else if (axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * xSensitivity, 0);
        }

        else
        {
            y += Input.GetAxis("Mouse Y") * ySensitivity;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            transform.localEulerAngles = new Vector3(-y, 0, 0);
        }
        #endregion
        */
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360;
        if (angle > 360f)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    /*
    #region MouseLook Transplant Test
    public enum RotationalAxis
    {
        MouseXandY,
        MouseX,
        MouseY
    }
    #endregion
    */
}
