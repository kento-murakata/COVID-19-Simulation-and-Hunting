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

    private float m_inputX = 0;
    private float m_inputZ = 0;
    
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

        m_PlayerCam.transform.localRotation = m_CameraRotation;
        transform.localRotation = m_PlayerRotation;

        if (Input.GetKey("space"))
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
        
    }
    private void FixedUpdate()
    {
        m_inputX = Input.GetAxis("Horizontal") * m_MoveSpeed;
        m_inputZ = Input.GetAxis("Vertical") * m_MoveSpeed;

        Vector3 cameraForward = Vector3.Scale(m_PlayerCam.transform.forward,new Vector3(1,0,1));
        Vector3 moveForward = cameraForward * m_inputZ + m_PlayerCam.transform.right * m_inputX;

       transform.position += moveForward;
    }

}
