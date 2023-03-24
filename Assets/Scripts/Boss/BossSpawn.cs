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
	[SerializeField]
	Transform player;
	// spawn location support
	const int SpawnBorderSize = 100;
	int minSpawnX = -20;
	int maxSpawnX = 20;
	int minSpawnY = -20;
	int maxSpawnY = 20;
	StageTime stageTime;
	bool spawnBoss = false;
	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
	{
		// save spawn boundaries for efficiency
		minSpawnX = SpawnBorderSize;
		maxSpawnX = Screen.width - SpawnBorderSize + 20;
		minSpawnY = SpawnBorderSize;
		maxSpawnY = Screen.height - SpawnBorderSize + 20;
		stageTime = FindObjectOfType<StageTime>();
		// create and start timer
		
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		if(stageTime.time >= 240f && !spawnBoss)
		{
            Notification notification = GameManager.Instance.notificationPanel.GetComponent<Notification>();
            StartCoroutine(notification.ShowMessage("BOSS!!", 1f));
            SpawnBoss();
			spawnBoss = true;
		}
	}

	/// <summary>
	/// Spawns a new teddy bear at a random location
	/// </summary>
	void SpawnBoss()
	{
		// generate random location and create new teddy bear
		Vector3 location = new Vector3(player.position.x + Random.Range(minSpawnX, maxSpawnX),
            player.position.y + Random.Range(minSpawnY, maxSpawnY),
			-Camera.main.transform.position.z);
		Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
		GameObject boss = Instantiate(prefabBos) as GameObject;
		boss.transform.position = worldLocation;
	}
}
