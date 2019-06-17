using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public PoolManager poolManagerScript;

	float SpeedVal;
	public GameObject Coin;
	public GameObject Meteor;

	public enum Type { coin, meteor};
	public Type ObjectType;

	public float speed = 300f;
	public float X;

	bool OnceBool = false;

	public void OnEnable()
	{
		OnceBool = false;
		poolManagerScript = GameObject.FindObjectOfType<PoolManager>();
		gameObject.GetComponent<Collider>().enabled = true;

		SetType();
	}

	private void OnDisable()
	{
	  Coin.SetActive(false);
	  Meteor.SetActive(false);
    }

	public void SetType()
	{
		int Rand = Random.Range(0, 2);

		if (Rand == 0)
		{
			ObjectType = Type.coin;
			Coin.SetActive(true);
		}
		else
		{
			ObjectType = Type.meteor;
			Meteor.SetActive(true);
		}
	}

	void Update()
	{
		if (ObjectType == Type.meteor)
		{
			if (gameObject.transform.position.y > -1f)
			{

				SpeedVal = 8f * Time.deltaTime;
				gameObject.transform.Translate(Vector3.down * SpeedVal);

			}
			else
			{
				PlaceBack();
			}
		}
		else if(ObjectType == Type.coin)
		{
			if (gameObject.transform.position.y > 0.4f)
			{

				SpeedVal = 4f * Time.deltaTime;
				gameObject.transform.Translate(Vector3.down * SpeedVal);

				X += speed * Time.deltaTime;
				Coin.transform.rotation = Quaternion.Euler(-90f, X, 0);
			}
			else {

				if (OnceBool == false) {
					OnceBool = true;
					Invoke("PlaceBack", 2f);
				}
			}
		}
		
	}

	public void PlaceBack()
	{
		gameObject.SetActive(false);
		poolManagerScript.AddEnemies(this.gameObject);
	}


	/*public void HealthAffected()
	{
		health -= 100f;
		gameObject.SetActive(false);
		GameManager.instance.RefScript.poolManagerScript.AddEnemies(this.gameObject);
	}*/

}
