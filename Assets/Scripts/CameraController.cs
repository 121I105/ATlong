using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class CameraController : MonoBehaviour
    {
        // 視点の感度
        [Range(0.1f, 20f)]
        public float lookSensitivity = 5f;

        // 視点の滑らかさ
        [Range(0.0f, 1f)]
        public float lookSmooth = 0.0f;

        // 視角の最小と最大値
        public Vector2 MinMaxAngle = new Vector2(-65, 65);

        private float yRot;
        private float xRot;

        private float currentYRot;
        private float currentXRot;

        private float yRotVelocity;
        private float xRotVelocity;

        // エイムのための変数
        public static float Aim;

        // エイムに関連する変数
        public static float[] xpd = new float[6000];
        public static float[] ypd = new float[6000];
        public static int ms = 0;
        public static int[] hitflame = new int[11];
        int hitflame_cnt=0;
        float xp = 0, temx = 0;
        float yp = 0, temy = 0;

        // カーソルの表示を制御する変数
        [SerializeField] bool m_cursor;
        void Start()
        {
            // エイムの感度を設定
            lookSensitivity = Aim;

            // カーソルの表示を制御
            Cursor.visible = false;

            Cursor.visible = m_cursor;

            // カーソルのロック状態を設定
            if (m_cursor)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            // VSyncを無効化し、フレームレートを60に設定
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        void Update()
        {
            // マウスの移動によって視点を操作
            yRot += Input.GetAxis("Mouse X") * lookSensitivity;
            xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;

            // ターゲットを作成していない場合の処理
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
            // ターゲットを作成した場合の処理
            else if(Create_Target.Stt==true)
            {
                hitflame[hitflame_cnt] = ms;
                hitflame_cnt++;
            }

            // 視角の制限
            xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);

            // 視角の滑らかな移動
            currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

            // カメラの回転を更新
            transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
        }
    }
}