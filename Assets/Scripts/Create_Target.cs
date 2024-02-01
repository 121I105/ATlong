using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create_Target : MonoBehaviour
{
    // ターゲットの生成が可能かどうかを示すフラグ
    public static bool Create = true;
    // ターゲットのプレハブ
    public GameObject Target;
    // ターゲットのインスタンス
    public static GameObject obj;
    // ターゲット生成の状態を示すフラグ
    public static bool Stt = false;
    // 経過時間
    float elapsedTime;
    // 生成されたターゲットの数
    public static int Create_count = 0;
    // ターゲットを生成するかどうかのフラグ
    bool dainyuu = false;
    // ターゲットの生成時間の配列
    public static float[] T_array = new float[10];

    void Start()
    {
        // コルーチンを開始して3秒後に生成フラグを立てる
        StartCoroutine("Coroutine");
    }

    // 3秒後に生成フラグを立てるコルーチン
    private IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(3.0f);
        Stt = true;
        yield break;
    }
    
    void Update()
    {
        // ターゲットを生成する条件を満たしている場合
        if (Create == true && Stt == true && Create_count <= 9)
        {
            // ターゲットの生成が完了している場合
            if (dainyuu == true)
            {
                // ターゲット生成にかかった時間を記録し、初期化
                T_array[Create_count] = elapsedTime;
                Debug.Log(T_array[Create_count]);
                elapsedTime = 0.0f;
                Create_count++;
            }
            // ターゲットを生成する
            if (Create_count <= 9) 
            {
                var Tgt_x = Random.Range(-8.0f, 8.0f);
                var Tgt_y = Random.Range(1.5f, 8.0f);
                obj = Instantiate(Target, new Vector3(Tgt_x, Tgt_y, 11.15f), Quaternion.identity);
                Create = false;
            }
        }
        // ターゲットが生成中の場合
        else if(Stt == true && Create_count <= 9)
        {
            // 経過時間を更新
            elapsedTime += Time.deltaTime;
            dainyuu = true;
        }
        // ターゲット生成が完了した場合
        if(Create_count >= 10)
        {
            // ターゲット生成の状態をリセットし、ゲーム終了シーンに遷移
            Stt = false;
            Create_count = 0;
            SceneManager.LoadScene("End");
        }
    }
}
