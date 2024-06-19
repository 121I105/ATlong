using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_coll : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            // "wall_coll"というログを出力
            Debug.Log("wall_coll");

            // Create_Targetオブジェクトを破壊する
            Destroy(Create_Target.obj.gameObject);

            // 新しいターゲットを生成するためのフラグを立てる
            Create_Target.Create = true;

            // 衝突したオブジェクトが壁に衝突したことを記録する
            hit_flag.h_flag[Create_Target.Create_count] = 0;
        }
    }
}
