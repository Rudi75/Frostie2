using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour 
{
	public Transform Player;
	public Vector2 Margin;
	public Vector2 Smoothing;

	public BoxCollider2D Bounds;

	private Vector3 minPosition, maxPosition;

	public bool IsFollowing = true;

	public void Start()
	{
		minPosition = Bounds.bounds.min;
		maxPosition = Bounds.bounds.max;

        var x = Player.transform.position.x;
        var y = Player.transform.position.y;
        var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, minPosition.x + cameraHalfWidth, maxPosition.x - cameraHalfWidth);
        y = Mathf.Clamp(y, minPosition.y + GetComponent<Camera>().orthographicSize, maxPosition.y - GetComponent<Camera>().orthographicSize);
        GetComponent<Camera>().transform.position = new Vector3(x, y, transform.position.z);
	}

	public void Update()
	{
		var x = GetComponent<Camera>().transform.position.x;
		var y = GetComponent<Camera>().transform.position.y;
       
        if (IsFollowing && Player != null)
		{
			if (Mathf.Abs (x - Player.position.x) > Margin.x)
				x = Mathf.Lerp (x, Player.position.x, Smoothing.x * Time.deltaTime);

			if (Mathf.Abs (y - Player.position.y) > Margin.y)
				y = Mathf.Lerp (y, Player.position.y, Smoothing.y * Time.deltaTime);
		}

		var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

		x = Mathf.Clamp(x,minPosition.x +cameraHalfWidth, maxPosition.x - cameraHalfWidth);
		y = Mathf.Clamp(y,minPosition.y +GetComponent<Camera>().orthographicSize, maxPosition.y - GetComponent<Camera>().orthographicSize);
		GetComponent<Camera>().transform.position = new Vector3 (x, y, transform.position.z);
	}



}
