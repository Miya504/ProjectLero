using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public int Clear_score;	// 最大のスコア
	public int Max_hate;	// 最大のヘイト
	public int Min_hate;	// 最少のヘイト
	public float hate_time = 0.5f; // ヘイトの間隔(0.5秒)
	public bool clear_flag;
	//public int RunHate;	// 走り時のヘイト倍数
	private int hate;
	private int score;
	private int score_plus;
	private int hate_plus;
	private int hate_minus;
//------------------------------------------------
	void Start() {
		hate = 0;
		score = 0;
		hate_plus = 0;
		hate_minus = 0;
		score_plus = 0;
	}

	// Update is called once per frame
	void Update() {
		// ヘイトがマックス超えるとゲームオーバー
		if (hate > Max_hate) {
			Application.LoadLevel(1);
		}

		//// クリアスコアを超えるとクリア
		//if (Clear_score < score) { 
		//	if (clear_flag)
		//		Application.LoadLevel(2);
		//}
		Debug_draw();
	}

	//--------------------------------------
	//	名前	:	Hate_calk_plus
	//	処理	:	ヘイト加算
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Hate_calk_plus() {
		this.hate += hate_plus;
	}
	public void Hate_calk_plus(int hate) {
		this.hate += hate;
	}
	//--------------------------------------
	//	名前	:	Hate_calk_minus
	//	処理	:	ヘイト減産
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Hate_calk_minus() {
		if (hate > Min_hate)
			this.hate -= hate_minus;
		else
			this.hate = Min_hate;
	}
	//--------------------------------------
	//	名前	:	Score_calk
	//	処理	:	スコア加算
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Score_calk() {
		this.score += score_plus;
	}
	//--------------------------------------
	//	名前	:	Score_minus
	//	処理	:	スコア減算
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Score_minus(int minus) {
		this.score -= minus;
	}


	//--------------------------------------
	//	名前	:	Game_End
	//	処理	:	ゲーム終了判定
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Game_End() {
		// クリアスコアを超えるとクリア
		if (Clear_score < score) {
			Application.LoadLevel(2);
		} else { 
			Application.LoadLevel(1);
		}
	}
//================== Get関数 ======================================//
	//--------------------------------------
	//	名前	:	Get_Hate_Time
	//	処理	:	ヘイト時間取得
	//	戻り値	:	秒数
	//	引数	:	N/A
	//--------------------------------------
	public float Get_Hate_Time() {
		return this.hate_time;
	}

//================== Set関数 ======================================//
	//--------------------------------------
	//	名前	:	Set_Hate_Plus
	//	処理	:	ヘイト加算値取得
	//	戻り値	:	N/A
	//	引数	:	加算値
	//--------------------------------------
	public void Set_Hate_Plus(int plus) {
		this.hate_plus = plus;
	}

	//--------------------------------------
	//	名前	:	Set_Hate_Minus
	//	処理	:	ヘイト減産値取得
	//	戻り値	:	N/A
	//	引数	:	減産値
	//--------------------------------------
	public void Set_Hate_Minus(int minus) {
		this.hate_minus = minus;
	}

	//--------------------------------------
	//	名前	:	Set_Score_Plus
	//	処理	:	スコア値取得
	//	戻り値	:	N/A
	//	引数	:	スコア
	//--------------------------------------
	public void Set_Score_Plus(int plus) {
		this.score_plus = plus;
	}
//=================== デバッグ ====================================//
	//--------------------------------------
	//	名前	:	Debug_draw
	//	処理	:	デバッグ表示
	//	戻り値	:	N/A
	//	引数	:	N/A
	//--------------------------------------
	public void Debug_draw() {
		GameObject.Find("Hate").GetComponent<GUIText>().text = "" + this.hate;
		GameObject.Find("Score").GetComponent<GUIText>().text = "" + this.score;
	}
}
