using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseMode_Manager : MonoBehaviour {





	//-------------------------READ ME FIRST!!-------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------THIS SCRIPT IS NOT EXPLAINED AND IS NOT A PART OF THE TUTORIAL----------------
	//----------------------------------JUST MY PRIVATE SCRIPT---------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------




	private GameObject SelectedPoint;

	private bool AddRigidbody;
    private bool AddCollider;

	public GameObject MainObjectToEdit;

	public GameObject UI_InEdit;
	public GameObject UI_InTest;
	public Text Adder;

	public GameObject MySmoothMovingGM;

	private Vector3 MyFirstPos;
	private Quaternion MyFirstRot;

	void Start () {

		MyFirstPos = MainObjectToEdit.transform.position;
		MyFirstRot = MainObjectToEdit.transform.rotation;
	
	}

	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();

		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(ray,out hit,Mathf.Infinity))
			{
				if(hit.collider.tag == "Point")
				{
					SelectedPoint = hit.transform.gameObject;
				}
			}
		}

		if(SelectedPoint != null)
		{
			if(Input.GetMouseButton(0))
			{
				Vector3 m = new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"),0);
				SelectedPoint.transform.TransformDirection(Camera.main.transform.position);
				SelectedPoint.transform.Translate(m * 6 * Time.deltaTime,Camera.main.transform);
				SelectedPoint.transform.GetComponent<Renderer>().material.color = Color.red;
			}
			else
			{
				SelectedPoint.transform.GetComponent<Renderer>().material.color = Color.white;
				SelectedPoint = null;
			}
		}

	
	
			if(MainObjectToEdit.GetComponent<Rigidbody>())
			{
				Vector3 torquer = new Vector3(Input.GetAxis("Vertical"),0,-Input.GetAxis("Horizontal"));
				MainObjectToEdit.transform.GetComponent<Rigidbody>().AddTorque(torquer * 1500,ForceMode.Acceleration);
			}





		MySmoothMovingGM.transform.LookAt( GameObject.Find ("MiddleObjToLook").transform.position);

		if(Input.GetMouseButton(1))
		{
		float RotatX = Input.GetAxis ("Mouse X");
		float RotatY = Input.GetAxis ("Mouse Y");
		
		Vector3 DirectionMove = new Vector3 (-RotatX, -RotatY, 0);
		DirectionMove = MySmoothMovingGM.transform.TransformDirection (DirectionMove);
		MySmoothMovingGM.transform.position += DirectionMove * 0.2F;

		float Distance = Vector3.Distance (MySmoothMovingGM.transform.position, GameObject.Find ("MiddleObjToLook").transform.position);
		if(Distance>=9)
		{
			MySmoothMovingGM.transform.position+=MySmoothMovingGM.transform.forward * 1;
		}
		}
		transform.position = Vector3.Lerp (transform.position, MySmoothMovingGM.transform.position, 0.02F);
		transform.rotation = Quaternion.Lerp (transform.rotation, MySmoothMovingGM.transform.rotation, 0.02F);
	}

	public void Button_AddRigidbody()
	{
		AddRigidbody = true;
		Adder.text += "Added RigidBody!" + System.Environment.NewLine;
	}
	public void Button_AddCollider()
	{
		AddCollider = true;
		Adder.text += "Added Collider!" + System.Environment.NewLine;
	}
	public void Button_ClearComponents()
	{
		if(MainObjectToEdit.GetComponent<Rigidbody>())
		{
			Destroy(this.MainObjectToEdit.GetComponent<Rigidbody>());
		}
		if(MainObjectToEdit.GetComponent<Collider>())
		{
			Destroy(this.MainObjectToEdit.GetComponent<Collider>());
		}

		Adder.text = "";

		AddRigidbody = false;
		AddCollider = false;
	}

	public void Button_TEST()
	{
		if(AddCollider)
		{
			if(!MainObjectToEdit.GetComponent<MeshCollider>())
			{
			MainObjectToEdit.AddComponent<MeshCollider>();
			}
			if(MainObjectToEdit.GetComponent<MeshCollider>())
			{
				MainObjectToEdit.gameObject.GetComponent<MeshCollider> ().convex = true;
			}
		}
		if(AddRigidbody)
		{
			if(!MainObjectToEdit.GetComponent<Rigidbody>())
			{
				MainObjectToEdit.AddComponent<Rigidbody>();
			}
			if(MainObjectToEdit.GetComponent<Rigidbody>())
			{
				MainObjectToEdit.GetComponent<Rigidbody>().useGravity = true;
				MainObjectToEdit.GetComponent<Rigidbody>().isKinematic = false;
			}
		}

		foreach(GameObject OnePoint in GameObject.FindGameObjectsWithTag("Point"))
		{
			OnePoint.GetComponent<MeshRenderer>().enabled = false;
			OnePoint.GetComponent<Collider>().enabled = false;
		}

		UI_InEdit.SetActive (false);
		UI_InTest.SetActive (true);
	}
	public void Button_EDIT()
	{
	
			if(MainObjectToEdit.GetComponent<MeshCollider>())
			{
				MainObjectToEdit.gameObject.GetComponent<MeshCollider> ().convex = true;
			}

	        if(MainObjectToEdit.GetComponent<Rigidbody>())
			{
				MainObjectToEdit.GetComponent<Rigidbody>().useGravity = false;
				MainObjectToEdit.GetComponent<Rigidbody>().isKinematic = true;
			}

		foreach(GameObject OnePoint in GameObject.FindGameObjectsWithTag("Point"))
		{
			OnePoint.GetComponent<MeshRenderer>().enabled = true;
			OnePoint.GetComponent<Collider>().enabled = true;
		}


		MainObjectToEdit.transform.position = MyFirstPos;
		MainObjectToEdit.transform.rotation = MyFirstRot;

		UI_InEdit.SetActive (true);
		UI_InTest.SetActive (false);
	}
}
