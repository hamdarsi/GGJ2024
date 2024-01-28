using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject chickenTemplate;
	float timer;

	List<Chicken> chickens = new List<Chicken>();
	public int maxChickens = 2;

	public float spawnRadius = 7f;

	void Start()
	{
        ResetTimer();
    }

	void Update()
	{
		if (chickenTemplate == null) return;

        timer -= Time.deltaTime;
		if(timer <= 0f)
		{
			int nChickens = 0;
			foreach(Chicken ch in chickens)
			{
				if(ch != null && !ch.IsOwned())
				{
					nChickens++;
				}
			}
			if(nChickens < maxChickens)
			{
                GameObject newChicken = GameObject.Instantiate(chickenTemplate, GetRandomPos(), GetRandomRot());
                chickens.Add(newChicken.GetComponent<Chicken>());
                ResetTimer();
            }
			else
			{
				ResetTimer();
            }
		}
	}

	void ResetTimer()
	{
		timer = Random.Range(2.5f, 5f);
	}

	Vector3 GetRandomPos()
	{
        Vector2 v = spawnRadius * Random.insideUnitCircle;
        return new Vector3(v.x, 0f, v.y);
	}

	Quaternion GetRandomRot()
	{
		return Quaternion.Euler(0f, 180f, 0f);
	}
}