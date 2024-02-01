using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // 弾のプレハブ
    public GameObject bulletPrefab;
    // 弾の速度
    public float shotSpeed;
    // 弾の発射間隔
    private float shotInterval;
    // クリックのフラグ
    bool Flag = true;   
    // クリックした回数
    public static int Click_cnt = 0;

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
                // 弾を生成して発射
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                Flag = false;
                Destroy(bullet, 1.0f); // 弾を一定時間後に削除
            }
        }
        else
        {
            Flag = true;
        }
    }
}
