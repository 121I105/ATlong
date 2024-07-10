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

        public bool isReset = false;  // フラグをpublicに変更
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

            // ms の値が範囲内に収まるかどうかを確認する
            if (ms < xpd.Length && ms < ypd.Length)
            {
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
                    if (hitflame_cnt < hitflame.Length)
                    {
                        hitflame[hitflame_cnt] = ms; // hitflame の配列も範囲内に収まっていることを確認する必要があります
                        hitflame_cnt++;
                    }
                }
            }

            xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);
            currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

            transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);

            // マウス左クリックでカメラをリセット
            if (Input.GetMouseButtonDown(0))
            {
                ResetCameraOrientation();
                isReset = true;

                if (resetCoroutine != null)
                {
                    StopCoroutine(resetCoroutine); // 既存のコルーチンを停止
                }

                resetCoroutine = StartCoroutine(ResetCameraAfterDelay(3.0f)); // 新しいコルーチンを開始
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
    }
}
