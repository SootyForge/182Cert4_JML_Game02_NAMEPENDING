using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Variables - Player Movement
    public Rigidbody playerRigid;

    public float moveSpeed;
    public float jumpHeight;

    public float rayDst = 1f;
    public LayerMask ignoreLayers;

    private bool isGrounded = false;

    public bool rotateToMainCamera = false;
    #endregion
    /*
    #region Variables - Camera Movement
    
    private float x = 0f;
    private float y = 0f;

    private float yMinLimit = -90f;
    private float yMaxLimit = 90f;

    public float xSensitivity = 120f;
    public float ySensitivity = 120f;

    public bool hideCursor = true;
    #endregion
    */
    #endregion

    void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDst);
    }

    // Use this for initialization
//    void Start()
//    {
//        playerRigid = GetComponent<Rigidbody>();
        /*
        transform.SetParent(null);

        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        */
//    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(groundRay, out hit, rayDst, ~ignoreLayers))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 moveDir = new Vector3(inputH, 0f, inputV);

        Vector3 camEuler = Camera.main.transform.eulerAngles;

        if (rotateToMainCamera)
        {
            moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
        }

        Vector3 force = new Vector3(moveDir.x, playerRigid.velocity.y, moveDir.z);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            force.y = jumpHeight;
        }

        playerRigid.velocity = force;

        Quaternion playerRotation = Quaternion.AngleAxis(camEuler.y, Vector3.up);
        transform.rotation = playerRotation;

        if (Input.GetButtonDown("Fire1"))
        {
            ShootRay();
        }
        /*
        #region Camera Rotation

        x += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        transform.rotation = Quaternion.Euler(y, x, 0);

        #endregion
        */
    }

    public void ShootRay()
    {
        Ray playerFire;
        playerFire = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        if (Physics.Raycast(playerFire, out hitInfo, 500f))
        {
            if (hitInfo.collider)
            {
                Debug.Log("Cat.");
            }
        }
    }
    /*
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360;
        if (angle > 360f)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    */
}
