using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_coll : MonoBehaviour
{
    // 他のオブジェクトがこのトリガーに侵入したときに呼び出される
    void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトが"Shell"タグを持っている場合
        if (other.CompareTag("Shell"))
        {
            // "wall_coll"というログを出力
            Debug.Log("wall_coll");
            
            // 衝突したオブジェクトを破壊する
            Destroy(other.gameObject);
            
            // Create_Targetオブジェクトを破壊する
            Destroy(Create_Target.obj.gameObject);
            
            // 新しいターゲットを生成するためのフラグを立てる
            Create_Target.Create = true;
            
            // 衝突したオブジェクトが壁に衝突したことを記録する
            hit_flag.h_flag[Create_Target.Create_count] = 0;
        }
    }
}
