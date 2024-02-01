using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // スコアを表示するテキストオブジェクトへの参照
    public Text t_cnt;
    // ターゲットへのヒット時間の合計
    public float sum = 0;
    // 平均ヒット時間
    public float ans = 0;
    // ゲームのセット回数
    public static int set_cnt;

    void Start()
    {
        // ゲームのセット回数をインクリメントして表示
        set_cnt++;
        Debug.Log(set_cnt);
        t_cnt.text = set_cnt + " 回目終了";
        
        // セット回数が45以上ならば45回目の終了として表示
        if(set_cnt >= 45)
        {
            t_cnt.text = "45 回目終了";
        }
    }
}
