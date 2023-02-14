using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float standardSpeed = 1.0f;

	void Update() {
		if(GameManager.isPlay) {
			transform.Translate(Vector2.up * standardSpeed * Time.deltaTime);
		}
	}

	void LateUpdate() {
		HandleBulletOutsideScreen();
	}

	void OnCollisionEnter2D(Collision2D other) {
		Destroy(gameObject);
	}

	private void HandleBulletOutsideScreen() {
		Vector2 cameraPos = Camera.main.transform.position;
		Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		Vector2 screenSize = 2 * (screen - cameraPos);
		bool horizontalCheck = transform.position.x > screen.x || transform.position.x < screen.x - screenSize.x;
		bool verticalCheck = transform.position.y > screen.y || transform.position.y < screen.y - screenSize.y;
		if(horizontalCheck || verticalCheck) {
			Destroy(this.gameObject);
		}
	}
}
