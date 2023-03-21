using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
	// needed for spawning
	[SerializeField]
	GameObject prefabBos;

	// spawn control
	const float MinSpawnDelay = 1;
	const float MaxSpawnDelay = 2;

	// spawn location support
	const int SpawnBorderSize = 100;
	int minSpawnX;
	int maxSpawnX;
	int minSpawnY;
	int maxSpawnY;

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
	{
		// save spawn boundaries for efficiency
		minSpawnX = SpawnBorderSize;
		maxSpawnX = Screen.width - SpawnBorderSize;
		minSpawnY = SpawnBorderSize;
		maxSpawnY = Screen.height - SpawnBorderSize;

		// create and start timer
		
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
	}

	/// <summary>
	/// Spawns a new teddy bear at a random location
	/// </summary>
	void SpawnBoss()
	{
		// generate random location and create new teddy bear
		Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX),
			Random.Range(minSpawnY, maxSpawnY),
			-Camera.main.transform.position.z);
		Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
		GameObject boss = Instantiate(prefabBos) as GameObject;
		boss.transform.position = worldLocation;
	}
}
