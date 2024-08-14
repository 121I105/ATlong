using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class hit_flag
{
    public static int[] h_flag = new int[12];

    // 静的コンストラクタ（初期化処理）
    static hit_flag()
    {
        ResetHitFlagArray();
    }

    // 配列をリセットする静的メソッド
    public static void ResetHitFlagArray()
    {
        for (int i = 0; i < h_flag.Length; i++)
        {
            h_flag[i] = 0;
        }
        Debug.Log("hit_flag.h_flag 配列をリセットしました。");
    }
}
