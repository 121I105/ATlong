using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // カメラの参照
    private Camera mainCamera;
    // クリックのフラグ
    bool Flag = true;   
    // クリックした回数
    public static int Click_cnt = 0;

    void Start()
    {
        // メインカメラの参照を取得
        mainCamera = Camera.main;
    }

    void Update()
    {
        // マウスの左クリックが押されたら
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Flag == true)
            {
                // ターゲットが表示されている状態でのみクリックがカウントされる
                if (Create_Target.Stt == true)
                {
                    Click_cnt++;
                }
                
                // レイキャストを使ってヒット判定
                Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Target"))
                    {
                        // ターゲットにヒット
                        Debug.Log("Target hit!");
                        Destroy_obj.hit_count++;
                        Destroy(hit.collider.gameObject);
                        Create_Target.Create = true;    
                        hit_flag.h_flag[Create_Target.Create_count] = 1;
                    }
                }
                
                Flag = false;
            }
        }
        else
        {
            Flag = true;
        }
    }
}
