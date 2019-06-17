using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolManager : MonoBehaviour
{
	public List<GameObject> EnemyPoolList = new List<GameObject>();

	public List<Transform> EnemySpawnPts = new List<Transform>();

	public GameObject EnemyPrefab;

	public int EnemyCount = 12;

	int SpawnPts;

	private IEnumerator coroutine;

	void Start()
	{
		SpawnPts = EnemySpawnPts.Count;

		CreateEnemy(EnemyPrefab);

		coroutine = PlaceEnemy(1.0f);
		StartCoroutine(coroutine);
	}

	void CreateEnemy(GameObject EnemyObj)
	{
		for (int i = 0; i < EnemyCount; i++)
		{
			var GObj = Instantiate(EnemyObj, gameObject.transform.position, Quaternion.identity);
			GObj.SetActive(false);
			EnemyPoolList.Add(GObj);
		}
	}

	private IEnumerator PlaceEnemy(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			int nPos = Random.Range(0, SpawnPts);
			//Debug.Log("No of calls");
			EnemyAt(nPos);
		}
	}

	public void EnemyAt(int Pt)
	{
		///Debug.Log(Pt);
		int FromList = (EnemyPoolList.Count - 1);
		var GameObj = EnemyPoolList[FromList];
		EnemyPoolList.RemoveAt(FromList);
		GameObj.transform.position = new Vector3(EnemySpawnPts[Pt].position.x, EnemySpawnPts[Pt].position.y, EnemySpawnPts[Pt].position.z);

		GameObj.SetActive(true);
		//Debug.LogError("CULPRIT 1111");
	}


	public void AddEnemies(GameObject Enemy)
	{
		//Debug.LogError("CULPRIT 2222");
		EnemyPoolList.Add(Enemy);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene("MainGame_Mobile");
	}
}

