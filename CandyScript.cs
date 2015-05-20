using UnityEngine;
using System.Collections;

public class CandyScript : MonoBehaviour {
	TextMesh score;

	// Use this for initialization
	void Start () {
		score = GameObject.FindGameObjectWithTag ("score").GetComponent<TextMesh>();
		score.text = "SCORE: " + CharacterScript.totalScore;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{	
		Destroy (this.gameObject);
		if (other.gameObject.tag == "Player") {
			CharacterScript.totalScore+=1;
			score.text = "SCORE: " + CharacterScript.totalScore;

		}
	}
}
