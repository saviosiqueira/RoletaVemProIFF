using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Roletop : MonoBehaviour
{
	public List<int> prize;
	public List<AnimationCurve> animationCurves;
	
	private bool roletaGirando;
	private float anguloItem;	
	private int randomTime;
	private int numeroEscolhido;

    private Vector3 mouseDelta = Vector3.zero;
    private Vector3 lastMousePosition = Vector3.zero;


    private void Start()
    {
		roletaGirando = false;
		anguloItem = 360/prize.Count;		
	}
	
	private void Update ()
	{
	    mouseDelta = Input.mousePosition - lastMousePosition;
	    lastMousePosition = Input.mousePosition;

        Debug.Log(mouseDelta.magnitude);
	    
        if (!Input.GetMouseButton(0)) return;
        if (mouseDelta.magnitude <= 50 || roletaGirando) return;
        
	    randomTime = Random.Range(3, 4);
	    Debug.Log((randomTime));
	    numeroEscolhido = Random.Range(0, prize.Count);

        float maxAngle = 380 * randomTime + (numeroEscolhido * anguloItem);
	        
        StartCoroutine(GirarRoleta(2 * randomTime, maxAngle));
	}
	
	private IEnumerator GirarRoleta (float tempo, float maxAngle)
	{
		roletaGirando = true;
		
		float timer = 0.0f;		
		float startAngle = transform.eulerAngles.z;		
		maxAngle = maxAngle - startAngle;
		
		int animationCurveNumber = Random.Range (0, animationCurves.Count);
		Debug.Log ("Animation Curve No. : " + animationCurveNumber);
		
		while (timer < tempo) {
		//calcular rotação
			float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate (timer / tempo) ;
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
			timer += Time.deltaTime;
			yield return 0;
		}
		
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, maxAngle + startAngle);
		roletaGirando = false;
			
		Debug.Log ("Número: " + prize[numeroEscolhido]);
	}
}
