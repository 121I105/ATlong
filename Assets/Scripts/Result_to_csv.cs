using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Result_to_csv : MonoBehaviour
{
    string DPI;
    string hit_time;
    int hit = 0;
    string sum, sum1, hf, top1;
    float time = 0f;
    int i = 0, j = 1;
    int hitf_cnt = 1;

    public Text sec;

    void Update()
    {
        time += Time.deltaTime;
        i = 5 - (int)time;
        sec.text = "リスタートまで残り " + i.ToString() + " 秒";

        if (i <= 0)
        {
            OP_csv();
        }
    }

    public void OP_csv()
    {
        hit = Destroy_obj.hit_count;
        hit_time = String.Join(",", Create_Target.T_array);
        DPI = Info.DPI;
        sum = hit_time + "," + DPI;
        hf = String.Join(",", hit_flag.h_flag);

        // デスクトップのパスを取得
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        // 結果用CSVファイルの保存先パス
        string resultFilePath = Path.Combine(desktopPath, "sato_result.csv");
        using (StreamWriter sw = new StreamWriter(resultFilePath))
        {
            // Create_Target.Create_count に基づいて書き込む行数を制御
            if (Create_Target.Create_count < hit_flag.h_flag.Length)
            {
                sw.WriteLine(hf);
            }
            else
            {
                Debug.LogError("Index out of bounds: hit_flag.h_flag length exceeded Create_Target.Create_count");
            }

            sw.WriteLine(sum);
        }

        // マウスの移動履歴用CSVファイルの保存先パス
        string mouseFilePath = Path.Combine(desktopPath, "sato_mouse.csv");
        using (StreamWriter sw1 = new StreamWriter(mouseFilePath))
        {
            top1 = String.Join(",", "num", "x", "y");
            sw1.WriteLine(top1);
            for (int n = 0; n <= FPS.CameraController.ms; n++)
            {
                if (j < int.MaxValue)
                {
                    sum1 = String.Join(",", j, FPS.CameraController.xpd[n], FPS.CameraController.ypd[n]);
                    sw1.WriteLine(sum1);
                    j++;
                }
                else
                {
                    Debug.LogWarning("Index j is going out of bounds.");
                }

                // FPS.CameraController.hitflame の長さと hitf_cnt に基づいて条件を調整
                if (hitf_cnt < FPS.CameraController.hitflame.Length && FPS.CameraController.hitflame[hitf_cnt] <= n)
                {
                    j = 1;
                    hitf_cnt++;
                }
            }
        }

        Debug.Log("CSVファイルを出力しました。");
        Destroy_obj.hit_count = 0;
        Shooting.Click_cnt = 0;
        FPS.CameraController.ms = 0;

        // Score.set_cnt が 50 に達しているかどうかを確認してシーン遷移
        if (Score.set_cnt == 50)
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            SceneManager.LoadScene("Play");
        }
    }
}
