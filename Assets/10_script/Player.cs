using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// 移動スピード
	public float speed = 0.0000005f;
	
	// PlayerBulletプレハブ
	public GameObject ke;
	Manager manager;
	public Vector2 mouse_pos;// マウスの座標
	//衝突相手の座標
	public Vector2 hit_pos;
	//public Vector2 direction;
	//public Vector2 vec;

	private float timer = 0.0f;
	private float hate_timer = 0.0f;
	private float hate_timer_minus = 0.0f;
	public float set_timer;
	//点滅の間隔を保存する変数
	private float interval_time = 0.0f;
	//行動停止
	private float stop_timer = 0.0f;
	//吹き飛びセット時間
	public float set_back_timer;
	//行動停止時間
	public float set_stop_timer;
	//無敵時間
	public float set_invincible_timer;
	private bool timer_flg = false;
	private bool run;
	//トゲにあたった時のフラグ
	private bool h_flag = false;
	//ヒットフラグ発動フラグ
	private bool h_flag2 = false;
	//無敵時間発動フラグ
	public bool inv_flag = false;

    //アニメーション
    private Animator _animator;

    void Start() {
		manager	= GameObject.Find("Manager").GetComponent<Manager>();
		// スコアのインスタンス
		Score script = GameObject.Find("Score GUI").GetComponent<Score>();
		run = false;
        //direction = new Vector2(0, 0);
        //vec = new Vector2(0, 0);

        //アニメーターコンポーネント呼び出し
        _animator = GetComponent<Animator>();

        //モーション判定用のパラメータ   
        _animator.SetFloat("Player_X", 0);
        _animator.SetFloat("Player_Y", 0);

    }
	// Startメソッドをコルーチンとして呼び出す
	//IEnumerator Start ()
	//{
	//	// スコアのインスタンス
	//	Score script = GameObject.Find("Score GUI").GetComponent<Score>();
	//	while (true) {
	//		// 弾をプレイヤーと同じ位置/角度で作成
	//		Instantiate (ke, transform.position, transform.rotation);

	//		yield return new WaitForSeconds (2f);
	//	}
	//}
	
	void Update ()
	{
		float time = Time.deltaTime;
		hate_timer_minus += time;
        if (timer_flg) { // クリックされてる間
			timer += time;
			hate_timer += time;
			if (timer > set_timer) {
				// 処理
				//if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
					GameObject ChildObj = Instantiate(ke, transform.position, Quaternion.identity) as GameObject;
					// 親オブジェクト設定
					ChildObj.transform.parent = GameObject.Find("KE").transform;

				//}
				timer = 0;
			}
		}else if (hate_timer_minus > manager.Get_Hate_Time()) {
				manager.Hate_calk_minus();	// ヘイトをマイナス
				hate_timer_minus = 0.0f;
		}
		// 右・左
		//float x = Input.GetAxisRaw ("Horizontal");
	
		// 上・下
		//float y = Input.GetAxisRaw ("Vertical");



		//トラップにあたっていないとき
		if (h_flag == false)
		{
			//移動処理
			move();
			if (Input.GetMouseButtonDown(0))
			{
				//Debug.Log("マウス座標" + Input.mousePosition);
			}
		//トラップにあたったとき
		}else{
			timer_flg = false;
			//ノックバック
			if (stop_timer < set_back_timer)
			{
				if (h_flag2 == true) {
					//ノックバック処理関数
					HitBack();
					h_flag2 = false;
				}	
				stop_timer += time;
				GameObject.Find("test5").GetComponent<GUIText>().text = "ヒットバック";
			//停止時間	
			}else if(stop_timer <  set_stop_timer){
				//移動停止
				GameObject.Find("test5").GetComponent<GUIText>().text = "停止時間";
				StopMove();
				stop_timer += time;
			//無敵時間
			}else if(stop_timer < set_invincible_timer){
				//無敵時間の移動
				move();
				if (Input.GetMouseButtonDown(0)) {
					//Debug.Log("マウス座標" + Input.mousePosition);
				}
				
				stop_timer += time;
				GameObject.Find("test5").GetComponent<GUIText>().text = "無敵時間";
				
				
			//無敵終了
			}else{
				h_flag = false;
				inv_flag = false;
				stop_timer = 0.0f;
				GameObject.Find("test5").GetComponent<GUIText>().text = "通常";
				gameObject.GetComponent<Renderer>().enabled = true;
			}
		}

		//無敵時間の点滅処理
		SwitchSprite();


        // 移動する向きを求める
        //Vector2 direction = new Vector2 (x, y).normalized;

        // 移動する向きとスピードを代入する
        //rigidbody2D.velocity = direction * speed;

    }

	// 当たり判定中
	void OnTriggerStay2D (Collider2D collider) {
		// 床のあたり判定処理
		if(collider.gameObject.tag == "floor"){
			//if (rigidbody2D.velocity.x != 0) {
				if (hate_timer > manager.Get_Hate_Time()) {
					manager.Hate_calk_plus();
					hate_timer = 0.0f;
				}
			//} else { 
			//	if (hate_timer > manager.Get_Hate_Time()) {
			//		manager.Hate_calk_minus();
			//		hate_timer = 0.0f;
			//	}
			//}

		// 壁のあたり判定処理
		} else if (collider.gameObject.tag == "Wall") {
			//if (collider.gameObject.name == "Wall_R") {
			//	Vector2 pos = transform.position;
			//	transform.position = new Vector2(pos.x - 0.5f, pos.y);

			//} else if (collider.gameObject.name == "Wall_L"){
			//	Vector2 pos = transform.position;
			//	transform.position = new Vector2(pos.x + 0.5f, pos.y);
			
			//} else if (collider.gameObject.name == "Wall_U"){
			//	Vector2 pos = transform.position;
			//	transform.position = new Vector2(pos.x, pos.y - 0.5f);

			//} else if (collider.gameObject.name == "Wall_D") { 
			//	Vector2 pos = transform.position;
			//	transform.position = new Vector2(pos.x, pos.y + 0.5f);			
			//}

		//トラップの当たり判定処理
		} else if(collider.gameObject.tag == "Trap"){
			// スコアのインスタンス
			Trap trap = GameObject.Find("Trap").GetComponent<Trap>();

			//トラップにあたったフラグ
			if (trap.flag == true)
			{
				if (h_flag == false) {
					h_flag2 = true;
				}
				h_flag = true;
				hit_pos = collider.gameObject.transform.position;
			}
		}

	}
//-------------------------------
//	名前	: move()
//	処理	: 移動処理
//	引数	: N/A
//	戻り値	: N/A
//-------------------------------
	void move() {
        Vector2 vec = new Vector2(0, 0);
		Vector2 direction = new Vector2(0, 0);

        if (Input.GetMouseButtonDown(0)) {		// 左クリックが押されたときの処理
			mouse_pos = (Vector2)Input.mousePosition;// マウスの座標取得
			GameObject.Find("test2").GetComponent<GUIText>().text = "マウス開始店" + mouse_pos;
		}else if (Input.GetMouseButton(0)) {	// 左クリックされているときの処理

			direction = ((Vector2)Input.mousePosition - mouse_pos) * 0.1f;
			GameObject.Find("test3").GetComponent<GUIText>().text = "マウス現在点" + Input.mousePosition;
			GameObject.Find("test3").GetComponent<GUIText>().text = "差" + direction;

            // x+y=3 以下の時歩く処理
            run = false;
            if (System.Math.Abs(direction.x) >= 1.0f || System.Math.Abs(direction.y) >= 1.0f)
            {//ちんげふらしが移動中の処理
                _animator.SetBool("PLAYER_MOVE", true);

                if (System.Math.Abs(direction.x) > 1.0f)
                {
                    if (System.Math.Abs(direction.x) <= 9.0f)
                    {
                        vec.x = direction.x / System.Math.Abs(direction.x) * 2;
                        set_timer = 1.0f;
                    }
                    else
                    {
                        vec.x = direction.x / System.Math.Abs(direction.x) * 7.0f;
                        set_timer = 0.25f;
                        run = true;
                    }
                }
                if (System.Math.Abs(direction.y) > 1.0f)
                {
                    if (System.Math.Abs(direction.y) <= 9.0f)
                    {
                        vec.y = direction.y / System.Math.Abs(direction.y) * 2.0f;
                        set_timer = 1.0f;
                    }
                    else
                    {
                        vec.y = direction.y / System.Math.Abs(direction.y) * 7.0f;
                        set_timer = 0.25f;
                        run = true;
                    }
                }

                //アニメーション（ブレンドツリー）用パラメータ   
                if (System.Math.Abs(direction.x) <= System.Math.Abs(direction.y))//縦方向の移動量が大きいとき
                {
                    if (vec.y < 0)
                    {
                        _animator.SetFloat("Player_X", 0.0f);
                        _animator.SetFloat("Player_Y", -1.0f);//下向き
                    }
                    else
                    {
                        _animator.SetFloat("Player_X", 0.0f);
                        _animator.SetFloat("Player_Y", 1.0f);//上向き
                    }
                }
                else
                {
                    if (vec.x < 0)
                    {
                        _animator.SetFloat("Player_X", -1.0f);//左向き
                        _animator.SetFloat("Player_Y", 0.0f);
                    }
                    else
                    {
                        _animator.SetFloat("Player_X", 1.0f);//右向き
                        _animator.SetFloat("Player_Y", 0.0f);
                    }
                }
            }

			//}else if (/*System.Math.Abs(direction.x) <= 10.0f | System.Math.Abs(direction.y) <= 10.0f |*/
			//	System.Math.Abs(direction.x) + System.Math.Abs(direction.y) <= 10.0f) {
			//	if (System.Math.Abs(direction.x) > 3.0f)
			//		vec.x = direction.x / System.Math.Abs(direction.x) *2;
			//	if (System.Math.Abs(direction.y) > 3.0f)
			//		vec.y = direction.y / System.Math.Abs(direction.y) *2;
			//} 

			//Debug.Log("移動速度"+direction);
			GameObject.Find("test").GetComponent<GUIText>().text = "速度"+ vec;
            timer_flg = true;

        } else if (Input.GetMouseButtonUp(0)) {//キーを離したとき
			timer_flg = false;
            _animator.SetBool("PLAYER_MOVE", false);//停止中のアニメーションに遷移

        } else { //キーが押されていないとき
			hate_timer = 0.0f;
        }


        //歩行アニメーションのスピード調整
        if (run == false)
        {
            _animator.speed = 0.5f;
        }
        else
        {
            _animator.speed = 1.0f;
        }

        GetComponent<Rigidbody2D>().velocity = vec;
		//Debug.Log(rigidbody2D.velocity);

	}

//-------------------------------
//	名前	: HitBack()
//	処理	: トラップとの衝突時のノックバック処理
//	引数	: N/A
//	戻り値	: N/A
//-------------------------------
	void HitBack(){
		Vector2 vec = new Vector2(0, 0);
		//反発係数
		float i = 15.0f;

		Vector2 pos = hit_pos - (Vector2)transform.position;
		if (pos.x > 0) {
			vec.x = -1 * i;
		}
		else{
			vec.x = i;
		}

		if (pos.y > 0) {
			vec.y = -1 * i;
		}
		else{
			vec.y = 1 * i;
		}

		GetComponent<Rigidbody2D>().velocity = vec;
	}

//-------------------------------
//	名前	: StopMove()
//	処理	: 移動の停止
//	引数	: N/A
//	戻り値	: N/A
//-------------------------------
	void StopMove() {
		Vector2 vec = new Vector2(0, 0);

		GetComponent<Rigidbody2D>().velocity = vec;
	}

//-------------------------------
//	名前	: SwitchSprite()
//	処理	: 点滅処理
//	引数	: N/A
//	戻り値	: N/A
//-------------------------------
	void SwitchSprite() {
		if (h_flag == true) {
			interval_time += Time.deltaTime;
			if (interval_time >= 0.1f) {
				interval_time = 0.0f;
				gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
			}
		}
	}

//-------------------------------
//	名前	: isRun()
//	処理	: 入っているかチェック
//	引数	: N/A
//	戻り値	: true / false
//-------------------------------
	public bool isRun() {
		return this.run;
	}
}