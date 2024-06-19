using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_obj : MonoBehaviour
{
    // ターゲットにヒットした回数をカウントする変数
    public static int hit_count = 0;

    // 他のColliderとの衝突を検知するメソッド
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            // ヒットカウントを増やす
            hit_count++;
            // 自身のゲームオブジェクトを破壊する
            Destroy(this.gameObject);
            // 新しいターゲットを生成するためのフラグを立てる
            Create_Target.Create = true;
            // ヒットしたターゲットを記録するフラグを立てる
            hit_flag.h_flag[Create_Target.Create_count] = 1;
        }
    }
}
