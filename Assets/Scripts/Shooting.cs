using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCamera;
    bool Flag = true;
    public static int Click_cnt = 0;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Flag == true)
            {
                if (Create_Target.Stt == true)
                {
                    Click_cnt++;
                }

                Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Target"))
                    {
                        Debug.Log("Target hit!");
                        Destroy_obj.hit_count++;
                        Destroy(hit.collider.gameObject);
                        Create_Target.Create = true;
                        hit_flag.h_flag[Create_Target.Create_count] = 1;
                    }
                    else if (hit.collider.CompareTag("Wall"))
                    {
                        Debug.Log("Wall hit!");
                        Wall_coll wallColl = hit.collider.GetComponent<Wall_coll>();
                        if (wallColl != null)
                        {
                            wallColl.HandleCollisionWithTarget();
                        }
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
