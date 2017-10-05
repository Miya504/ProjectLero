using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public float trap_time;
	private float timer = 0.0f;
	public bool flag = false;
	public bool hit_flag = false;
	yuka floor;
	Player player;
	// Use this for initialization
	void Start () {
		floor = GetComponent<yuka>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// トゲのアニメーション
	void Update () {
		timer += Time.deltaTime;
		if (timer > trap_time) {
			flag = !flag;
			timer = 0.0f;
		}
		if (flag == true) {
			transform.Find("toge").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("toge");
		} else { 
			transform.Find("toge").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("togeana");
		}

	}

    //プレイヤーと接触時
	void OnTriggerStay2D(Collider2D collider) {
		//Player player = GameObject.Find("Player").GetComponent<Player>();
		if (collider.gameObject.tag == "Player"){
			if (flag == true  && player.inv_flag == false) {
				//ヒット時にヘイトが上昇
				floor.manager.Hate_calk_plus(50);
				player.inv_flag = true;
				Debug.Log("hit");
			}	
		}
	}
}
