using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float minSpeed = 5;
	public float maxSpeed = 10;
	public GameObject lightningSphereOne;
	public GameObject lightningSphereTwo;
	public AudioClip[] blipAudio;
	private Rigidbody _r;



	public enum BallColor{
		Red,
		Green,
		Blue,
		Yellow
	}

	public BallColor bColor = BallColor.Red;

	// Use this for initialization

	void Awake(){
		_r = rigidbody;
	}

	void Start () {
		MeshRenderer mRendererOne = lightningSphereOne.GetComponent<MeshRenderer>();
		MeshRenderer mRendererTwo = lightningSphereTwo.GetComponent<MeshRenderer>();
		mRendererOne.material = Resources.Load ("Electricity/Materials/Lightning" + bColor.ToString()) as Material;
		mRendererTwo.material = Resources.Load ("Electricity/Materials/Lightning" + bColor.ToString()) as Material;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		if (_r.isKinematic == false) {
			if (_r.velocity.magnitude > maxSpeed) {
				_r.velocity = _r.velocity.normalized * maxSpeed;
			}

			if (_r.velocity.magnitude < minSpeed) {
				_r.velocity = _r.velocity.normalized * minSpeed;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		AudioSource.PlayClipAtPoint (blipAudio[Random.Range(0,blipAudio.Length - 1)],transform.position, 0.25f);

	}

}
