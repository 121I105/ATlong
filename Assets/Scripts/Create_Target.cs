using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create_Target : MonoBehaviour
{
    // ターゲットを生成するかどうかを制御するフラグ
    public static bool Create = true;
    // ターゲットのプレハブ
    public GameObject Target;
    // 生成されたターゲットのインスタンス
    public static GameObject obj;
    // タイマーのステータスを示すフラグ
    public static bool Stt = false;
    // 経過時間を保持する変数
    float elapsedTime;
    // 生成したターゲットの数
    public static int Create_count = 0;
    // ターゲットの位置を重複させないためのフラグ
    bool dainyuu = false;
    // ターゲットの位置配列
    public static float[] T_array = new float[16];

    // ターゲットの初期位置
    private Vector3[] targetPositions = new Vector3[]
    {
        new Vector3(0, 20, 50),          // 0
        new Vector3(0, 40, 50),          // 1
        new Vector3(21.21f, 41.21f, 50), // 2
        new Vector3(30, 20, 50),         // 3
        new Vector3(21.21f, -1.21f, 50), // 4
        new Vector3(0, 0, 50),           // 5
        new Vector3(-21.21f, -1.21f, 50), // 6
        new Vector3(-30, 20, 50),        // 7
        new Vector3(-21.21f, 41.21f, 50) // 8
    };

    // 未使用のターゲット位置リスト
    private List<int> remainingPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    // 前回のランダムな位置
    private int lastRandomPosition = -1;

    void Start()
    {
        // コルーチンを開始し、1秒後にステータスを有効にする
        StartCoroutine("Coroutine");
        Create_Target.Create = true;
        Debug.Log("Create_Target.Create の値: " + Create_Target.Create);
    }

    private IEnumerator Coroutine()
    {
        // 1秒待機してからステータスを有効にする
        yield return new WaitForSeconds(1.0f);
        Stt = true;
        yield break;
    }

    void Update()
    {
        // ターゲット生成可能かつステータスが有効であり、生成上限に達していない場合
        if (Create == true && Stt == true && Create_count <= 15)
        {
            // 既にターゲットが生成されている場合
            if (dainyuu == true)
            {
                // 経過時間を配列に格納し、リセットする
                T_array[Create_count] = elapsedTime;
                Debug.Log(T_array[Create_count]);
                elapsedTime = 0.0f;
                Create_count++;
                dainyuu = false;
            }

            // まだターゲットを生成できる場合
            if (Create_count <= 15)
            {
                // ターゲットの生成位置を決定
                if (Create_count % 2 == 0)
                {
                    obj = Instantiate(Target, targetPositions[0], Quaternion.identity);
                }
                else
                {
                    // 未使用の位置からランダムに選択し、その位置にターゲットを生成
                    if (remainingPositions.Count == 0)
                    {
                        remainingPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
                    }
                    int randomIndex = Random.Range(0, remainingPositions.Count);
                    int randomPos = remainingPositions[randomIndex];
                    remainingPositions.RemoveAt(randomIndex);

                    obj = Instantiate(Target, targetPositions[randomPos], Quaternion.identity);
                    lastRandomPosition = randomPos;
                }
                Create = false;
            }
        }
        // ステータスが有効であり、生成上限に達していない場合
        else if (Stt == true && Create_count <= 15)
        {
            // 経過時間を更新し、フラグをセットする
            elapsedTime += Time.deltaTime;
            dainyuu = true;
        }

        // 生成上限に達した場合
        if (Create_count >= 16)
        {
            // ステータスを無効にし、生成数をリセットし、シーンを切り替える
            Stt = false;
            Create_count = 0;
            SceneManager.LoadScene("End");
        }
    }
}
