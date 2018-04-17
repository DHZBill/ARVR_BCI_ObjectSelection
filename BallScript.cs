using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour {
    public GameObject left;
    public GameObject right;
    Material leftMaterial;
    Material rightMaterial;
    //public int flickeringFrequency;
    bool flick = false;
    float timePassed;
    public Color normalColor = new Color(1, 1, 1, 1);
    public Color flickColor = new Color(0, 0, 0, 1);
    public float leftFlickSpeed;
    public float rightFlickSpeed;
    public Effect Flick = new Effect();
    bool stoppedFlickingColourChanged = false;
    public float finishFlicking = 20f;
    public bool showIndicator = false;
    public GameObject indicator;
    int indicatorOnLeft;
    Transform indicatorTransform;

    public System.Random rand = new System.Random();
    // Use this for initialization
    void Start () {
        leftMaterial = left.GetComponent<Renderer>().material;
        rightMaterial = right.GetComponent<Renderer>().material;
        indicatorTransform = indicator.transform;
        //indicatorPosition = indicator.GetComponent<Transform>(); 
        //timePassed = 0;
        StartCoroutine(TimeManager());
    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (timePassed > finishFlicking) {
            flick = false;
        }

        if (flick/* && timePassed > flickeringFrequency*/)
        {
            stoppedFlickingColourChanged = false;
            //timePassed = 0;
            leftMaterial.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * leftFlickSpeed, Flick.flickColorDuration)));
            rightMaterial.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * rightFlickSpeed, Flick.flickColorDuration)));
        }
        else if(!stoppedFlickingColourChanged){
            stoppedFlickingColourChanged = true;
            leftMaterial.color = normalColor;
            rightMaterial.color = normalColor;
        }

        //if (showIndicator)
        ///{
        //    if(indicatorOnLeft == 1)
        //        indicatorTransform.position = new Vector3(-1, 0.5f, 2);
        //    else
        //        indicatorTransform.position = new Vector3(1, 0.5f, 2);
        //}
        //if (showIndicator)
        //{
        //    if (leftIndicator)
         //       indicatorPosition.position.Set(1, 0.5f, 2);
           // else
             //   indicatorPosition.position.Set(-1, 0.5f, 2);
            //indicator.GetComponent<Renderer>().enabled = true;
        //}
        //else
          //  indicator.GetComponent<Renderer>().enabled = false;

	}   

    // Couroutine
    private IEnumerator TimeManager()
    {
        while (timePassed < finishFlicking)
        {
            showIndicator = true;
            indicatorOnLeft = rand.Next(0, 2);
            print(indicatorOnLeft);
            if (indicatorOnLeft == 1)
                indicatorTransform.position = new Vector3(-1, 0.6f, 2);
            else
                indicatorTransform.position = new Vector3(1, 0.6f, 2);
            indicator.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(3f);
            showIndicator = false;
            indicator.GetComponent<Renderer>().enabled = false;
            int flickSpeedRandomIndex = rand.Next(0, 2);
            if(flickSpeedRandomIndex == 0)
            {
                leftFlickSpeed = Flick.lowFrequency;
                rightFlickSpeed = Flick.highFrequency;
            }
            else
            {
                leftFlickSpeed = Flick.highFrequency;
                rightFlickSpeed = Flick.lowFrequency;
            }

            flick = true;
            yield return new WaitForSeconds(10f);
            flick = false;
        }
    }

    //Effect class
    [Serializable]
    public class Effect
    {
        //Parameters to setup
        public float lowFrequency = 7.5f;
        public float highFrequency = 15;
        public float normalColorDuration = 1;
        public float flickColorDuration = 1;
    }
}
