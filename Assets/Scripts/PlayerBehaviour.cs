using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    public float m_MoveSpeed = 0.1f;
    public float m_CamSpeedX = 3.0f;
    public float m_CamSpeedY = 3.0f;
    
    private GameObject m_PlayerCam;
    private GameObject m_MainCam;
    private Quaternion m_CameraRotation;
    private Quaternion m_PlayerRotation;
    //private Animator m_Animator;

    private float m_inputX = 0;
    private float m_inputZ = 0;

    private bool isCursorlock = true;
    private float cameraRangeMinX = -90.0f;
    private float cameraRangeMaxX = 90.0f;
    
// Update is called once per frame

    private void Start()
    {
        m_MainCam = GameObject.Find("InfectionSimulator/Main Camera");
        m_PlayerCam = GameObject.Find("Player/PlayerCamera");

        m_PlayerRotation = transform.rotation;
        m_CameraRotation = m_PlayerCam.transform.rotation;
    }

    private void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * m_CamSpeedY;
        float yRot = Input.GetAxis("Mouse Y") * m_CamSpeedX;

        m_CameraRotation *= Quaternion.Euler(-yRot,0,0);
        m_PlayerRotation *= Quaternion.Euler(0,xRot,0);

        m_CameraRotation = ClampRotation(m_CameraRotation);

        m_PlayerCam.transform.localRotation = m_CameraRotation;
        transform.localRotation = m_PlayerRotation;

        if (Input.GetKey("right shift"))
        {
            //メインカメラをアクティブに設定
            m_MainCam.SetActive(true);
            m_PlayerCam.SetActive(false);
        }
        else
        {
            //サブカメラをアクティブに設定
            m_MainCam.SetActive(false);
            m_PlayerCam.SetActive(true);
        }

        //操作中はマウスポインターを消す
        UpdateCursorLock();


    }
    private void FixedUpdate()
    {
        m_inputX = Input.GetAxis("Horizontal") * m_MoveSpeed;
        m_inputZ = Input.GetAxis("Vertical") * m_MoveSpeed;

        Vector3 cameraForward = Vector3.Scale(m_PlayerCam.transform.forward,new Vector3(1,0,1));
        Vector3 moveForward = cameraForward * m_inputZ + m_PlayerCam.transform.right * m_inputX;

        transform.position += moveForward;

        //if(Mathf.Abs(m_inputX) > 0 || Mathf.Abs(m_inputZ) > 0)
        //{
        //    if (!m_Animator.GetBool("Run"))
        //    {
        //        m_Animator.SetBool("Run", true);
        //    }
        //}
        //else if (m_Animator.GetBool("Run"))
        //{
        //    m_Animator.SetBool("Run", false);
        //}
    }

    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorlock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            isCursorlock = true;
        }

        if (isCursorlock)
        {
          Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!isCursorlock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public Quaternion ClampRotation(Quaternion quaternion)
    {
        quaternion.x /= quaternion.w;
        quaternion.y /= quaternion.w;
        quaternion.z /= quaternion.w;
        quaternion.w = 1;

        float angleX = Mathf.Atan(quaternion.x) * Mathf.Rad2Deg * 2.0f;

        angleX = Mathf.Clamp(angleX,cameraRangeMinX,cameraRangeMaxX);

        quaternion.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return quaternion;
    }
}
