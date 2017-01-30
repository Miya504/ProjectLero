
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // 位置座標
    private Vector3 mousePosition;
    private int i = 0;//deb

    void Start()
    {
    }

    public void ToMain()
    {
        //ボタンがクリックされたときにシーン移動
        SceneManager.LoadScene("prote1");
    }

    void Update()
    {
        // Vector3でマウス位置座標を取得する→マネージャーに移行
        mousePosition = Input.mousePosition;

        //このオブジェクトの座標とサイズを取得
        var sr = this.GetComponent<RectTransform>();
        var width = sr.sizeDelta.x;
        var high = sr.sizeDelta.y;
        var pos = transform.position;

        //取得座標表示(デバッグ用)
        GameObject.Find("MousePositonXdeb").GetComponent<GUIText>().text = "X:" + mousePosition.x.ToString();
        GameObject.Find("MousePositonYdeb").GetComponent<GUIText>().text = "Y:" + mousePosition.y.ToString();
        GameObject.Find("PositonX").GetComponent<GUIText>().text = "X:" + pos.x.ToString();
        GameObject.Find("PositonY").GetComponent<GUIText>().text = "Y:" + pos.y.ToString();
        GameObject.Find("Countdeb").GetComponent<GUIText>().text = "count:" + i;
        i++;
        //取得座標表示(デバッグ用)

                
        //マウスの座標が｢ふらす｣ボタンの上にあるとき
        if (mousePosition.x > pos.x-width/2 & mousePosition.x < pos.x+width/2 & mousePosition.y > pos.y-high/2 & mousePosition.y < pos.y+high/2)
        {
            //選択カーソルを表示
            GameObject.Find("SelectStartButton").GetComponent<Image>().enabled = true;
        }
        else
        {
            //普段は非表示
            GameObject.Find("SelectStartButton").GetComponent<Image>().enabled = false;
        }
        GameObject.Find("Countdeb").GetComponent<GUIText>().text = "count:" + i;
    }
}
