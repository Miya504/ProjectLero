using UnityEngine;

public class ke : MonoBehaviour
{
	public int speed = -1;
	//GameObject gb = new GameObject();
	yuka floor;
	Manager manager;
	Player player;

	void Start ()
	{
		floor	= GameObject.Find("yuka").GetComponent<yuka>();
		manager	= GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find("Player").GetComponent<Player>();

		Vector3 pos = transform.position;
		//var y = pos.y;
		//Debug.Log(y);

		rigidbody2D.velocity = transform.up.normalized * speed;
		Invoke("Stop",0.3f);
		Debug.Log(rigidbody2D.velocity.y);
		/*if(y <= -2.0f){
		*	rigidbody2D.velocity = new Vector2(0, 0);
		*}
		*/
	}

	void Stop(){
		rigidbody2D.velocity = new Vector2(0, 0);
		Debug.Log(rigidbody2D.velocity.y);
	}

	//コライダーとぶつかったとき
	void OnTriggerStay2D (Collider2D collider) {
		
		if (rigidbody2D.velocity.y == 0){
			if(collider.gameObject.tag == "floor"){
				// 当たっている床を取得
				yuka now_floor = collider.gameObject.GetComponent<yuka>();
				if (now_floor.hit > 0) {	// 床のhit回数が残っているなら
					manager.Score_calk();
					now_floor.hit--;		// 床のhit回数マイナス
				}
				Destroy(rigidbody2D);
			}
		}
	}	
}
