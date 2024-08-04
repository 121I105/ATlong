using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Result_to_csv : MonoBehaviour
{
    string DPI;
    string hit_time;
    int hit = 0;
    string sum, sum1, hf, top1;
    float time = 0f;
    int i = 0, j = 1;
    int hitf_cnt = 1;
    int operationCount = 1; // セット内での操作回数
    float startX = 0f; // 操作開始時のX位置
    float startY = 0f; // 操作開始時のY位置

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

        using (StreamWriter sw = new StreamWriter(resultFilePath, true)) // 'true' to append to the file
        {
            sw.WriteLine(hf);
            sw.WriteLine(sum);
        }

        // マウスの移動履歴用CSVファイルの保存先パス
        string mouseFilePath = Path.Combine(desktopPath, "sato_mouse.csv");
        using (StreamWriter sw1 = new StreamWriter(mouseFilePath, true)) // 'true' to append to the file
        {
            // ヘッダー行を追記
            if (FPS.CameraController.ms == 0)
            {
                top1 = String.Join(",", "set_num", "operation_count", "num", "x", "y", "distance_from_start");
                sw1.WriteLine(top1);
            }

            for (int n = 0; n <= FPS.CameraController.ms; n++)
            {
                float xCoord = FPS.CameraController.xpd[n];
                float yCoord = FPS.CameraController.ypd[n];

                if (hitf_cnt < FPS.CameraController.hitflame.Length && FPS.CameraController.hitflame[hitf_cnt] <= n)
                {
                    j = 1;
                    operationCount++;
                    hitf_cnt++;
                    startX = xCoord; // 操作開始時のX位置を更新
                    startY = yCoord; // 操作開始時のY位置を更新
                }

                float distanceFromStart = Mathf.Sqrt(Mathf.Pow(xCoord - startX, 2) + Mathf.Pow(yCoord - startY, 2));

                sum1 = String.Join(",", Score.set_cnt, operationCount, j, xCoord - startX, yCoord - startY, distanceFromStart);
                sw1.WriteLine(sum1);
                j++;
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

        operationCount = 1; // セット終了後に操作回数をリセット
    }
}
