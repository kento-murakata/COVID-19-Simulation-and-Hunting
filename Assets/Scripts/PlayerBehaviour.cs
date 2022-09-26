using System
    .Collections;
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
    
    private Vector3 m_PlayerMovement;
    private Vector3 m_Position;
    private Vector3 m_Direction;
    private Vector3 m_Distance;
    private bool m_IsMoving;


        // Update is called once per frame

    private void Start()
    {
        m_PlayerMovement = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);

        m_MainCam = GameObject.Find("InfectionSimulator/Main Camera");
        m_PlayerCam = GameObject.Find("Player/PlayerCamera");

        m_PlayerRotation = transform.rotation;
        m_CameraRotation = m_PlayerCam.transform.rotation;
    }

    private void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * m_CamSpeedY;
        float yRot = Input.GetAxis("Mouse Y") * m_CamSpeedX;

        m_CameraRotation *= Quaternion.Euler(
            -yRot,
            0,
            0);

        m_PlayerRotation *= Quaternion.Euler(
           0,
           xRot,
           0);

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
        m_PlayerCam.transform.localRotation = m_CameraRotation;
        transform.localRotation = m_PlayerRotation;
    }
    private void FixedUpdate()
    {
        m_inputX = Input.GetAxisRaw("Horizontal") * m_MoveSpeed;
        m_inputZ = Input.GetAxisRaw("Vertical") * m_MoveSpeed;

        //m_PlayerMovement.Set(
        //    m_inputX,
        //    0,
        //    m_inputZ);

        //transform.position += m_PlayerMovement;

        transform.position += m_PlayerCam.transform.forward.normalized * m_inputZ + m_PlayerCam.transform.right.normalized * m_inputX;
    }

}
