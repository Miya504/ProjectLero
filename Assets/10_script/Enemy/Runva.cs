using UnityEngine;
using System.Collections;

public class Runva : MonoBehaviour {
	private Vector2 vec;
	public Vector2 speed;
	private int hit_count;
	private int hit_num;

	private Manager manager;

    //アニメーション
    private Animator _animator;

    void Start() {
		Initialize();

        //アニメーターコンポーネント呼び出し
        _animator = GetComponent<Animator>();

        //モーション判定用のパラメータ   
        _animator.SetFloat("Direction_X", vec.x);
        _animator.SetFloat("Direction_Y", vec.y);

    }

	void Update() {

		if (hit_count == 0){
			hit_num = Random.Range(3, 6);
		}
		Vector2 pos = transform.position;
		transform.position = pos + vec;
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Wall") {
			Wall_Hit(obj);	// 壁のあたり処理
		} else if (obj.gameObject.name == "ke") {
			Destroy(obj.gameObject.transform.parent.gameObject);
			manager.Score_minus(100);
		} else if (obj.gameObject.name == "Player") {
			//GameObject copied = Object.Instantiate(original) as GameObject;
		}
	}

	void OnCollisionEnter2D(Collision2D obj) {
		vec *= -1.0f;
	}
//-------------	ユーザー関数 -------------------- //
	// 初期化処理
	private void Initialize() {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		vec = speed;
		hit_count = 0;
		hit_num = 0;
	}

	//--------------------------------------------
	//	名前	: Runva_Hit(GameObject[])
	//	処理	: 一番近い毛の要素番号を探す
	//	引数	: 存在しているkeオブジェクト
	//	戻り値	: 要素番号
	//------------------------------------------
	private int Runva_Hit( GameObject[] obj) {
		// 初期要素番号[0]
		int count = 0;
		for (int i = 1; i < obj.Length; i++) {
			// 一番近い毛の判定処理
			if (System.Math.Abs(obj[i].transform.position.y - transform.position.y) +2.0f<
				System.Math.Abs(obj[count].transform.position.y - transform.position.y) &&
				System.Math.Abs(obj[i].transform.position.x - transform.position.x) + 2.0f <
				System.Math.Abs(obj[count].transform.position.x - transform.position.x)) {
				count = i;
			}
		}
		return count;
	}

	//--------------------------------------------
	//	名前	: Wall_Hit(GameObject[])
	//	処理	: 壁のあたり判定
	//	引数	: 存在しているkeオブジェクト
	//	戻り値	: N/A
	//------------------------------------------
	private void Wall_Hit(Collider2D obj) {
		hit_count++;
        // WALL SIDE(L.R) is HIT ?
        if (obj.gameObject.name == "Wall_R" || obj.gameObject.name == "Wall_L") {
			// 横速度反転
			vec.x *= -1.0f;
			Debug.Log(hit_count);
            if (hit_count == hit_num) {
				try {
					GameObject[] kes = GameObject.FindGameObjectsWithTag("ke");

					// 毛がルンバ上下どっちにあるか判定
					if (kes[Runva_Hit(kes)].transform.position.y < transform.position.y) {
						vec.y = -speed.y;   // 下がる
                    }
                    else {
						vec.y = speed.y;	// 上がる
					}
				} catch { }
				hit_count = 0;
			}
		} else {// WALL SIDE(Top.Down) is HIT ?
			vec.y *= -1.0f;
			Debug.Log(hit_count);
			if (hit_count == hit_num) {
				try {
					GameObject[] kes = GameObject.FindGameObjectsWithTag("ke");

					// 毛がルンバ左右どっちにあるか判定
					if (kes[Runva_Hit(kes)].transform.position.x < transform.position.x) {
						vec.x = -speed.x;	// 左
					} else {
						vec.x = speed.x;	// 右
					}
				} catch { }
				hit_count = 0;
			}
		}

        //モーション判定用のパラメータ   
        _animator.SetFloat("Direction_X", vec.x);
        _animator.SetFloat("Direction_Y", vec.y);

    }
}
