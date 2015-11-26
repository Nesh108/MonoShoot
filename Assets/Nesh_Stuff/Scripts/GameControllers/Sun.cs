using UnityEngine;
using System.Collections;

[AddComponentMenu("Day Night Cycle/Sun")]
public class Sun : MonoBehaviour
{
	public float maxLightBrightness;
	public float minLightBrightness;

	public float maxFlareBrightness;
	public float minFlareBrightness;

	public bool givesLight = false;

	void Start (){
		if(gameObject.GetComponent<Light>() != null)
			givesLight = true;

	}
}
