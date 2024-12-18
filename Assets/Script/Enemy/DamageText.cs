using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
	float lifeTime = 1.5f;
	float speed = 50f;
	float spawnRangeX = 50f;
	float spawnRangeY = 50f;
	TextMeshProUGUI damageText;

	void Awake()
	{
		damageText = GetComponent<TextMeshProUGUI>();
	}
	public void SetDamage(float damage)
	{
		damageText.text = damage.ToString();

		float randomX = Random.Range(-spawnRangeX, spawnRangeX);
		float randomY = Random.Range(-spawnRangeY, spawnRangeY);

		transform.position = new Vector3(randomX, randomY, 0);

	}

	// Update is called once per frame
	void Update()
	{
		transform.position += new Vector3(Random.Range(-0.5f, 0.5f), 1f, 0f) * speed * Time.deltaTime;

		lifeTime -= Time.deltaTime;

		if (lifeTime < 0) { Destroy(gameObject); }
	}
}
