using UnityEngine;
//----------------------------
//  スコア用クラス
//
//                  ver.1.00
//-----------------------------
public class Score : MonoBehaviour {
//================================================
//  メンバ
//================================================
	// スコアを表示するGUIText
	public GUIText scoreGUIText;

	private int score;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreGUIText.text = score.ToString ();
	}
//---------------- セット関数 ----------------------------------------
	// スコアの値セット
	// 引数   : 更新スコア
	// 戻り値 : N /A
	//public void setScore(int get_point) {
	//	this.score += get_point;
	//}

	public void AddPoint(int point)
	{
		//スコア加算
		score = score + point;
	}

}
