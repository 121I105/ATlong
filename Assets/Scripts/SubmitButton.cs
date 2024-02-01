using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SubmitButton : MonoBehaviour
{
    // トグルグループへの参照
    public ToggleGroup toggleGroup;

    void Start()
    {
        // ゲームの品質設定とフレームレートを調整
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    
    void Update()
    {

    }

    // ボタンがクリックされたときの処理
    public void onClick()
    {
        // アクティブなトグルからラベルを取得
        string selectedLabel = toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;

        // 取得したラベルをログに出力
        Debug.Log("selected " + selectedLabel);
    }
}
