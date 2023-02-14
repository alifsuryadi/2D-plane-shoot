using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject[] _enemyPrefab;
	public float[] _spawnChance;
	public float[] _spawnChanceEnd;

	public float spawnRapidStart = 1.0f;
	public float spawnRapidMax = 1.0f;
	float spawnRapidTime = 0.0f;

	void OnEnable() {
		spawnRapidTime = Time.time;
	}

	void Update() {
		float spawnRapid = Mathf.Max(spawnRapidMax, spawnRapidStart - Time.timeSinceLevelLoad / 15);
		HandleSpawnChance();

		if(GameManager.isPlay) {
			if(Time.time > spawnRapidTime + spawnRapid) {
				spawnRapidTime = Time.time;

				int randomSpawnChance = Random.Range(0, 100);
				int randomId = RandomId(randomSpawnChance);
				GameObject enemy = Instantiate(_enemyPrefab[randomId], transform.position, transform.rotation) as GameObject;
				enemy.transform.SetParent(transform);

				int randomPos = Random.Range(-2, 3);
				enemy.transform.localPosition = new Vector2(enemy.transform.localPosition.x + randomPos, enemy.transform.localPosition.y);
				enemy.GetComponent<Enemy>().hSpeed = (randomPos > 0.0f) ? -enemy.GetComponent<Enemy>().hSpeed : enemy.GetComponent<Enemy>().hSpeed;
			}
		}
	}

	private void HandleSpawnChance() {
		if(_spawnChanceEnd.Length > 0) {
			for(int i = 0; i < _spawnChance.Length; i++) {
				_spawnChance[i] = Mathf.MoveTowards(_spawnChance[i], _spawnChanceEnd[i], Mathf.Abs(_spawnChance[i] - _spawnChanceEnd[i]) / 240.0f * Time.deltaTime);
			}
		}
	}

	private int RandomId(int randomChance) {
		float sumBetween = 0;
		for(int i = 0; i < _spawnChance.Length; i++) {
			float between2 = sumBetween + _spawnChance[i];
			
			if(randomChance >= sumBetween && randomChance < between2) return i;
			else sumBetween = between2;
		}
		return -1;
	}
}
