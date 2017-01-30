using UnityEngine;
using System.Collections;

public class yuka : MonoBehaviour {
	
	public int point = 100;	// ポイント加算値
	public int hate = 1;	// 歩きヘイト加算値
	public int run_hate = 3;// 走りヘイト加算値
	public int minus_hate = 1;
	public int hit = 10;
	public Manager manager;
	Player player;
	private int hate_score;
	// Use this for initialization
	void Start () {

		Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//コライダーとぶつかったとき
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {

			if (player.isRun()) {	// プレイヤーが走っている
				manager.Set_Hate_Plus(this.run_hate);
			} else {				// プレイヤーが走っていない
				manager.Set_Hate_Plus(this.hate);
			}
			manager.Set_Hate_Minus(this.minus_hate);
		}
		//tagのkeが当たった時
		if(collider.gameObject.tag == "ke"){
			// スコアコンポーネントを取得してポイントを追加
			//FindObjectOfType<Score>().AddPoint(point);
			//if (hit > 0) {
				manager.Set_Score_Plus(point);
				//hit--;
			//}
			
		}

	}

	// 初期化
	void Initialize() {
 
		hate_score = 0;
		// マネージャの宣言
		manager	= GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}
}
