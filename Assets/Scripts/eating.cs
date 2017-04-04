using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class eating : MonoBehaviour {

	public AudioClip swallow;



	void FixedUpdate ()
	{
	}

	void OnTriggerEnter()
	{
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint(swallow, new Vector3(5, 1, 2));

	}

}
