using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_coll : MonoBehaviour
{
    // 他のスクリプトから呼び出すための公開メソッド
    public void HandleCollisionWithTarget()
    {
        Debug.Log("wall_coll");
        Destroy(Create_Target.obj.gameObject);
        Create_Target.Create = true;
        //hit_flag.h_flag[Create_Target.Create_count] = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            HandleCollisionWithTarget();
        }
    }
}
