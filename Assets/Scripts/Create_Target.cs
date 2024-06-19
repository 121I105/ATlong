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
    float elapsedTime;
    public static int Create_count = 0;
    bool dainyuu = false;
    public static float[] T_array = new float[12]; // ターゲット数を12に変更

    private Vector3[] targetPositions = new Vector3[]
    {
        new Vector3(0f, 45f, 50f), //1
        new Vector3(12.5f, 41.65f, 50f), //2
        new Vector3(21.65f, 32.5f, 50f), //3
        new Vector3(25f, 20f, 50f), //4
        new Vector3(21.65f, 7.49f, 50f), //5
        new Vector3(12.5f, -1.65f, 50f), //6
        new Vector3(0f, -5f, 50f), //7
        new Vector3(-12.5f, -1.65f, 50f), //8
        new Vector3(-21.65f, 7.49f, 50f), //9
        new Vector3(-25f, 20f, 50f), //10
        new Vector3(-21.65f, 32.5f, 50f), //11
        new Vector3(-12.5f, 41.65f, 50f) //12
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
        yield return new WaitForSeconds(1.0f);
        Stt = true;
        yield break;
    }

    void Update()
    {
        if (Create == true && Stt == true && Create_count < 12) // カウント条件を12に変更
        {
            if (dainyuu == true)
            {
                T_array[Create_count] = elapsedTime;
                Debug.Log(T_array[Create_count]);
                elapsedTime = 0.0f;
                Create_count++;
                dainyuu = false;
            }

            if (Create_count < 12) // カウント条件を12に変更
            {
                if (remainingPositions.Count == 0)
                {
                    remainingPositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                }
                int randomIndex = Random.Range(0, remainingPositions.Count);
                int randomPos = remainingPositions[randomIndex];
                remainingPositions.RemoveAt(randomIndex);

                obj = Instantiate(Target, targetPositions[randomPos], Quaternion.identity);
            }
            Create = false;
        }
        else if (Stt == true && Create_count < 12) // カウント条件を12に変更
        {
            elapsedTime += Time.deltaTime;
            dainyuu = true;
        }

        if (Create_count >= 12) // カウント条件を12に変更
        {
            Stt = false;
            Create_count = 0;
            SceneManager.LoadScene("End");
        }
    }
}
