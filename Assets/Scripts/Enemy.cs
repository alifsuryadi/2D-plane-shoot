using UnityEngine;
//using GooglePlayGames;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int enemyHP = 1;
	[Range(-5.0f, 5.0f)] public float hSpeed = 1.0f;
	[Range(0.0f, 3.0f)]	public float vSpeed = 1.0f;

	public bool aiCanShoot = false;
	public GameObject _bulletPrefab;
	public GameObject _explosionPrefab;
	[Range(0.1f, 2.0f)] public float rapid = 1.0f;
	float rapidTime = 0.0f;

	public int score = 1;

	void Update() {
		if(GameManager.isPlay) {
			transform.localPosition = new Vector2(transform.localPosition.x + Mathf.Sin(Time.time) * hSpeed * Time.deltaTime, transform.localPosition.y + vSpeed * Time.deltaTime);

			if(aiCanShoot) {
				if(Time.time > rapidTime + rapid) {
					rapidTime = Time.time;
					CreateBullet();
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		enemyHP = Mathf.Max(0, enemyHP - 1);
		if(other.gameObject.CompareTag("Bullet")) {
			Instantiate(_explosionPrefab, other.contacts[0].point, Quaternion.identity);
			GameManager.score += score;
		}

		if(enemyHP <= 0 || other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
		}
	}

	private void CreateBullet() {
		Instantiate(_bulletPrefab, transform.position + transform.up, transform.rotation);
	}
}