using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour {

    // 位置座標
    private Vector3 mousePosition;
    private int i = 0;//deb

   private void ToMain()
    {
        //ボタンがクリックされたときにシーン移動
        SceneManager.LoadScene("Option");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Vector3でマウス位置座標を取得する（マネージャースクリプトに処理を移すか、要検討）
        mousePosition = Input.mousePosition;

        //このオブジェクトの座標とサイズを取得（マネージャースクリプトに処理を移すか、要検討）
        var sr = this.GetComponent<RectTransform>();
        var width = sr.sizeDelta.x;
        var high = sr.sizeDelta.y;
        var pos = transform.position;

        //マウスの座標が｢ふらす｣ボタンの上にあるとき
        if (mousePosition.x > pos.x - width / 2 & mousePosition.x < pos.x + width / 2 & mousePosition.y > pos.y - high / 2 & mousePosition.y < pos.y + high / 2)
        {
            //選択カーソルを表示
            GameObject.Find("SelectOptionButton").GetComponent<Image>().enabled = true;
        }
        else
        {
            //普段は非表示
            GameObject.Find("SelectOptionButton").GetComponent<Image>().enabled = false;
        }
    }
}
