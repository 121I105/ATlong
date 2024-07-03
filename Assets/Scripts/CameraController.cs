using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class CameraController : MonoBehaviour
    {
        private Quaternion initialRotation;
        [Range(0.1f, 20f)]
        public float lookSensitivity = 5f;
        [Range(0.0f, 1f)]
        public float lookSmooth = 0.0f;
        public Vector2 MinMaxAngle = new Vector2(-65, 65);

        private float yRot;
        private float xRot;
        private float currentYRot;
        private float currentXRot;
        private float yRotVelocity;
        private float xRotVelocity;

        public static float Aim;
        public static float[] xpd = new float[6000];
        public static float[] ypd = new float[6000];
        public static int ms = 0;
        public static int[] hitflame = new int[11];
        int hitflame_cnt = 0;
        float xp = 0, temx = 0;
        float yp = 0, temy = 0;

        [SerializeField] bool m_cursor;

        void Start()
        {
            lookSensitivity = Aim;
            Cursor.visible = false;
            Cursor.visible = m_cursor;

            if (m_cursor)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            initialRotation = transform.rotation;
        }

        void Update()
        {
            yRot += Input.GetAxis("Mouse X") * lookSensitivity;
            xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;

            if (Create_Target.Create == false)
            {
                xp = Input.GetAxis("Mouse X");
                yp = Input.GetAxis("Mouse Y");
                xpd[ms] = xp + temx;
                temx = xpd[ms];
                ypd[ms] = yp + temy;
                temy = ypd[ms];
                ms++;
            }
            else if (Create_Target.Stt == true)
            {
                hitflame[hitflame_cnt] = ms;
                hitflame_cnt++;
            }

            xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);
            currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

            transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
        }

        public void ResetCameraOrientation()
        {
            if (initialRotation != null)
            {
                transform.rotation = initialRotation;
                Debug.Log("Camera rotation reset called.");
            }
            else
            {
                Debug.Log("FPS Camera not found!");
            }
        }

        public void StartCameraReset()
        {
            transform.rotation = initialRotation;
            Debug.Log("Camera rotation reset called.");
        }
    }
}
