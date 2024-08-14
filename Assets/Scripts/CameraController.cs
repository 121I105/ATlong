using System.Collections;
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

        public bool isReset = false;
        private Coroutine resetCoroutine;

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
            if (isReset)
            {
                return;
            }

            yRot += Input.GetAxis("Mouse X") * lookSensitivity;
            xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;

            if (ms < xpd.Length && ms < ypd.Length)
            {
                if (Create_Target.TargetVisible)
                {
                    xp = Input.GetAxis("Mouse X");
                    yp = Input.GetAxis("Mouse Y");
                    xpd[ms] = xp + temx;
                    temx = xpd[ms];
                    ypd[ms] = yp + temy;
                    temy = ypd[ms];
                    ms++;
                }
            }

            xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);
            currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

            transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);

            if (Input.GetMouseButtonDown(0))
            {
                if (Create_Target.TargetVisible)
                {
                    if (hitflame_cnt < hitflame.Length)
                    {
                        hitflame[hitflame_cnt] = ms;
                        Debug.Log($"Hit recorded: {ms} at index {hitflame_cnt}");
                        hitflame_cnt++;
                    }
                    else
                    {
                        Debug.LogWarning("hitflame配列が一杯です。");
                    }

                    ResetCameraOrientation();
                    isReset = true;

                    if (resetCoroutine != null)
                    {
                        StopCoroutine(resetCoroutine);
                    }

                    resetCoroutine = StartCoroutine(ResetCameraAfterDelay(0.5f));
                    Create_Target.TargetVisible = false;
                }
            }
        }

        private IEnumerator ResetCameraAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isReset = false;
            resetCoroutine = null;
        }

        public void ResetCameraOrientation()
        {
            transform.rotation = initialRotation;
            Debug.Log("Camera rotation reset called.");

            xRot = 0f;
            yRot = 0f;
            currentXRot = 0f;
            currentYRot = 0f;
        }

        public void ResetHitflameArray()
        {
            for (int i = 0; i < hitflame.Length; i++)
            {
                hitflame[i] = 0;
            }
            hitflame_cnt = 0;
            Debug.Log("hitflame array and counter reset.");
        }
    }
}
