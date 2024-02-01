using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    // インスペクター上で設定可能なドロップダウンUI要素
    [SerializeField] private Dropdown DPI_dropdown;
    
    // DPIの設定値を保持する静的変数
    public static string DPI;

    // Updateメソッドはフレームごとに呼び出される
    void Update()
    {
        // DPIのドロップダウン選択に応じて処理を分岐
        switch (DPI_dropdown.value)
        {
            case 0: // DPIが1.5:1の場合（20cm）
                DPI = "1.5:1";
                FPS.CameraController.Aim = 1.7f; // カメラの視点を設定
                break;

            case 1: // DPIが3:1の場合（10cm）
                DPI = "3:1";
                FPS.CameraController.Aim = 0.9f;
                break;

            case 2: // DPIが6:1の場合（5cm）
                DPI = "6:1";
                FPS.CameraController.Aim = 0.4f;
                break;
        }
    }

    // UI要素がクリックされた時に呼び出されるメソッド
    public void onClick()
    {
        // DPIの設定値をログに出力
        Debug.Log(DPI);
        
        // "Play"シーンに遷移する
        SceneManager.LoadScene("Play");
    }
}
