using UnityEngine;
using System.Collections;
using Uniduino;

public class CharacterScript : MonoBehaviour {
	public float thrust;
	public Arduino arduino;
	public int tiltSensor = 2;
	public int tiltValue;
	public GameObject candyPrefab;
	public Rigidbody rb;
	public static int totalScore=0;
	public AudioClip impact;
	AudioSource audio;
	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
		StartCoroutine (SpawnTimer ());
		audio = GetComponent<AudioSource>();
	}
	void ConfigurePins()
	{
		arduino.pinMode (tiltSensor, PinMode.INPUT);
		arduino.reportDigital((byte)(tiltSensor/8), 1);
		//arduino.pinMode (pressureSensor, PinMode.ANALOG);
		//arduino.reportAnalog (pin, 1);

	}
	// Update is called once per frame
	void Update () {
	

		tiltValue = arduino.digitalRead (tiltSensor);
		Debug.Log (tiltValue);
		if (tiltValue==1) {
			transform.Translate (Vector3.left * 5.0f * Time.deltaTime);
		} else if(tiltValue==0) 
		{
			transform.Translate (Vector3.right * 5.0f * Time.deltaTime);

		}

	}
	void SpawnCandies()
	{
		float randomX = Random.Range (-5.0f, 5.0f);
		Instantiate (candyPrefab, new Vector3(randomX, 7.0f, 0.0f), Quaternion.identity);
		StartCoroutine (SpawnTimer ());
	}
	IEnumerator SpawnTimer()
	{
		float randomTime = Random.Range(0,3);
		yield return new WaitForSeconds (randomTime);
		//StartCoroutine (SpawnCandies ());
		SpawnCandies ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "candy") {
			audio.PlayOneShot (impact, 1.0f);
		}
	}

}
