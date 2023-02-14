using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	[Range(0.1f, 1.0f)] public float speed = 1.0f;

	public float minXPlayer = -3.0f;
	public float maxXPlayer = 3.0f;

	public GameObject _bulletPrefab;
	public GameObject _explosionPrefab;
	
	void FixedUpdate() {
		if(GameManager.isPlay) {
			transform.Translate(Vector2.right * (Input.acceleration.x + Input.GetAxis("Horizontal")) * speed);

			float playerX = Mathf.Clamp(transform.localPosition.x, minXPlayer, maxXPlayer);
			transform.localPosition = new Vector3(playerX, transform.localPosition.y);

			if(Input.GetMouseButtonDown(0)) {
				CreateBullet();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Bullet") || (other.gameObject.CompareTag("Enemy"))) {
			GameManager.isPlay = false;
			Instantiate(_explosionPrefab, other.contacts[0].point, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	private void CreateBullet() {
		Instantiate(_bulletPrefab, transform.position + transform.up, transform.rotation);
	}
}
