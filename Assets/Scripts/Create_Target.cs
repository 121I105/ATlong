using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create_Target : MonoBehaviour
{
    public static bool Create = true;
    public GameObject Target;
    public static GameObject obj;
    public static bool Stt = false;
    public static bool TargetVisible = false; // 追加
    float elapsedTime;
    public static int Create_count = 0;
    bool dainyuu = false;
    public static float[] T_array = new float[12];

    private Vector3[] targetPositions = new Vector3[]
    {
        new Vector3(0f, 45f, 50f),
        new Vector3(12.5f, 41.65f, 50f),
        new Vector3(21.65f, 32.5f, 50f),
        new Vector3(25f, 20f, 50f),
        new Vector3(21.65f, 7.5f, 50f),
        new Vector3(12.5f, -1.65f, 50f),
        new Vector3(0f, -5f, 50f),
        new Vector3(-12.5f, -1.65f, 50f),
        new Vector3(-21.65f, 7.5f, 50f),
        new Vector3(-25f, 20f, 50f),
        new Vector3(-21.65f, 32.5f, 50f),
        new Vector3(-12.5f, 41.65f, 50f)
    };

    private List<int> remainingPositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

    void Start()
    {
        StartCoroutine("Coroutine");
        Create_Target.Create = true;
        Debug.Log("Create_Target.Create の値: " + Create_Target.Create);
    }

    private IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Stt = true;
        yield break;
    }

    void Update()
    {
        FPS.CameraController cameraController = FindObjectOfType<FPS.CameraController>();
        if (cameraController != null && cameraController.isReset)
        {
            return; // カメラがリセット状態の場合、ターゲット生成をスキップ
        }

        if (Create == true && Stt == true && Create_count < 12)
        {
            if (dainyuu == true)
            {
                T_array[Create_count] = elapsedTime;
                Debug.Log(T_array[Create_count]);
                elapsedTime = 0.0f;
                Create_count++;
                dainyuu = false;

                if (cameraController != null)
                {
                    cameraController.ResetCameraOrientation();
                }
            }

            if (Create_count < 12)
            {
                if (remainingPositions.Count == 0)
                {
                    remainingPositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                }
                int randomIndex = Random.Range(0, remainingPositions.Count);
                int randomPos = remainingPositions[randomIndex];
                remainingPositions.RemoveAt(randomIndex);

                obj = Instantiate(Target, targetPositions[randomPos], Quaternion.identity);
                TargetVisible = true; // ターゲットが出現したことを示すフラグを設定
            }
            Create = false;
        }
        else if (Stt == true && Create_count < 12)
        {
            elapsedTime += Time.deltaTime;
            dainyuu = true;
        }

        if (Create_count >= 12)
        {
            Stt = false;
            Create_count = 0;
            SceneManager.LoadScene("End");
        }
    }
}
