using UnityEngine;
using System.Collections;

public class TimerTest : MonoBehaviour {

	private float startTime = 99.0f; // 残り時間
	private Sprite[] sprite_L;	// 左側緑表示
	private Sprite[] sprite_L2;	// 左側赤表示

	private Sprite[] sprite_R;	// 右側緑表示
	private Sprite[] sprite_R2;	// 右側赤表示

	// Use this for initialization
	void Start () {
		sprite_L = Resources.LoadAll<Sprite>("timer_l");
		sprite_L2 = Resources.LoadAll<Sprite>("timer_l2");

		sprite_R = Resources.LoadAll<Sprite>("timer_r");
		sprite_R2 = Resources.LoadAll<Sprite>("timer_r2");
	}
	
	// Update is called once per frame
	void Update () {
		startTime -= Time.deltaTime;	// タイムを減産

		if ((int)startTime >= 20) {	// 緑画像表示
			GameObject.Find("timer_L").GetComponent<SpriteRenderer>().sprite = sprite_L[(int)startTime / 10];
			GameObject.Find("timer_R").GetComponent<SpriteRenderer>().sprite = sprite_R[(int)startTime % 10];
		} else {	// 赤画像表示
			GameObject.Find("timer_L").GetComponent<SpriteRenderer>().sprite = sprite_L2[(int)startTime / 10];
			GameObject.Find("timer_R").GetComponent<SpriteRenderer>().sprite = sprite_R2[(int)startTime % 10];

		}
		if (startTime < 0) {	// タイマが０ならシーン切り替え（GameOverS）{
			Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
			manager.Game_End();
		}
		if (Input.GetKey(KeyCode.F)) {
			startTime -= 1.0f;

		}
		//foreach (Sprite s in sprite_L) {
		//	Debug.Log(s);
		//}
	}
}
