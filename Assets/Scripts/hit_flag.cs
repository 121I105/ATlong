using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_flag : MonoBehaviour
{
    // ターゲットがヒットされたかどうかを記録するフラグの配列
    public static int[] h_flag = new int[12];

    void Start()
    {
        // hit_flag配列を0で初期化
        for (int i = 0; i < h_flag.Length; i++)
        {
            h_flag[i] = 0;
        }
    }

    void Update()
    {
    }
}
