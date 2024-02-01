using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_to_Strat : MonoBehaviour
{
    // ボタンがクリックされたときに呼ばれるメソッド
    public void onClick()
    {
        // Startシーンに遷移
        SceneManager.LoadScene("Start");
        // クリック数をリセット
        Shooting.Click_cnt = 0;
    }
}
