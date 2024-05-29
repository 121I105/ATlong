using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class Result_to_csv : MonoBehaviour
{
    // CSVに書き出すデータを保持する変数
    string DPI;
    string hit_time;
    int hit = 0;
    string sum, sum1, hf, top1;
    float time = 0f;
    int i = 0, j = 1;
    int hitf_cnt = 1;
    
    // タイマーのテキストUI要素
    public Text sec;

    void Update()
    {
        // 残り時間を計算し、UIに表示
        time += Time.deltaTime;
        i = 5 - (int)time;
        sec.text = "リスタートまで残り " + i.ToString() + " 秒";

        // 残り時間が0以下になったらCSVファイルに結果を書き出す
        if (i <= 0)
        {
            OP_csv();
        }
    }

    // CSVファイルに書き出す処理
    public void OP_csv()
    {
        // 各種データを取得
        hit = Destroy_obj.hit_count;
        hit_time = String.Join(",", Create_Target.T_array);
        DPI = Info.DPI;
        sum = hit_time + "," + DPI;
        hf = String.Join(",", hit_flag.h_flag);
        
        // 結果を書き出すCSVファイルのパスとファイル名
        string fileName = "sato_result";
        FileInfo fi;
        fi = new FileInfo(Application.persistentDataPath + "/" + fileName + ".csv");

        // 結果の書き出し
        using (StreamWriter sw = fi.AppendText())
        {
            sw.WriteLine(hf);
            sw.WriteLine(sum);
        }

        // マウス座標を書き出すCSVファイルのパスとファイル名
        string fileName1 = "sato_mouse";
        FileInfo fi1;
        fi1 = new FileInfo(Application.persistentDataPath + "/" + fileName1 + ".csv");

        // マウス座標の書き出し
        using (StreamWriter sw1 = fi1.AppendText())
        {
            top1 = String.Join(",", "num", "x", "y");
            sw1.WriteLine(top1);
            for (int n = 0; n <= FPS.CameraController.ms; n++)
            {
                sum1 = String.Join(",", j, FPS.CameraController.xpd[n], FPS.CameraController.ypd[n]);
                sw1.WriteLine(sum1);
                j++;
                if (FPS.CameraController.hitflame[hitf_cnt] <= n) {
                    j = 1;
                    hitf_cnt++;
                }
            }
        }

        // ログを出力し、変数をリセットしてシーンを遷移
        Debug.Log("出力した");
        Destroy_obj.hit_count = 0;
        Shooting.Click_cnt = 0;
        FPS.CameraController.ms = 0;
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
