using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public const int REVIVE_MAXIMUM = 5;

	public static bool isPlay = false;
	public static int reviveLeft = 5;

	public Text _scoreText;
	public static int score = 0;

	public RectTransform _gameplayUI;
	public RectTransform _gameOverUI;
	public Text _lastScoreText;
	float bossDelayTime = 0.0f;
	float gameOverTime = 0.0f;

	void Start() {
		isPlay = true;

		_gameplayUI.gameObject.SetActive(isPlay);
		_gameOverUI.gameObject.SetActive(!isPlay);
	}

	void Update() {
		if(isPlay) {
			_scoreText.text = score.ToString();
		}
		else {
			if(Time.time >= gameOverTime + 2.0f) {
				_gameplayUI.gameObject.SetActive(isPlay);
				_gameOverUI.gameObject.SetActive(!isPlay);

				_lastScoreText.text = GameManager.score.ToString();
			}
		}
	}

	public void ChangeSceneButton(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
