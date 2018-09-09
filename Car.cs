using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ここにInputクラスとCarMovementクラスを呼び出して
//FixedUpdateを書いていく
public class Car : MonoBehaviour {

	[SerializeField]
	public float acceleration,brake,angle;
	public int maxDistance,turnDis;
	float speed;
	//mode0 前進　mode1 右折　mode2　左折
	int mode=0;

	Vector3 heading;
	Vector3 dirF=new Vector3(0,0,2f);
	Vector3 dirFR=new Vector3(1f,0,1f);
	Vector3 dirFL=new Vector3(-1f,0,1f);
	Vector3 dirR=new Vector3(2f,0,0);
	Vector3 dirL=new Vector3(-2f,0,0);
	Vector3 dirBR=new Vector3(1f,0,-1f);
	Vector3 dirBL=new Vector3(-1f,0,-1f);
	Quaternion rotation;

	CarMovement carMovement;
	InputForCar inputForCar;

	RaycastHit hit;


	void Start () {
		//carMovement=GetComponent<CarMovement>();
		//carMovement.startup();
		inputForCar=GetComponent<InputForCar>();
		rotation=new Quaternion(0,0,0,0);

		}

	void FixedUpdate () {
		if(inputForCar.wFlag)	{
			goForward(acceleration);
		}else if(inputForCar.sFlag){
			doBrake();
		}else{
			goForward(0);
		}

		if(inputForCar.dFlag){
			turnRight(angle);
		}else if(inputForCar.aFlag){
			turnLeft(angle);
		}

		if(mode==0)
		{
			if((!isHit(dirFR,turnDis))&&isHit(dirFL,turnDis)) {
				mode=1;
			}else if((isHit(dirFR,turnDis))&&!isHit(dirFL,turnDis)) {
				mode=2;
			}

			if(isHit(dirR).distance-isHit(dirL).distance>5) turnRight(0.3f);
			else if(isHit(dirR).distance-isHit(dirL).distance<-5) turnLeft(0.3f);
			else {
				if(isHit(dirFR).distance-isHit(dirBR).distance<-0.1f) turnLeft(0.1f);
				else if(isHit(dirFL).distance-isHit(dirBL).distance<-0.1f) turnRight(0.1f);

			}

			if(isHit(dirF,maxDistance))	doBrake();
			else goForward(acceleration);

		}else if(mode==1)
		{
			if( (isHit(dirFR,turnDis)&&isHit(dirFL,turnDis)) )
			{
				mode=0;
			}
			turnRight(angle);
			if(isHit(dirF,maxDistance))	doBrake();
			else goForward(acceleration);

		}else if(mode==2)
		{
			if( (isHit(dirFR,turnDis)&&isHit(dirFL,turnDis)) )
			{
				mode=0;
			}
			turnLeft(angle);
			if(isHit(dirF,maxDistance))	doBrake();
			else goForward(acceleration);
		}


	}

	bool isHit(Vector3 vector,int dis)
	{
		Vector3 dir=transform.TransformDirection(vector);

		Debug.DrawRay(this.transform.position,dir*dis*speed,Color.green);
		if(Physics.Raycast(this.transform.position,dir,out hit,dis*speed)){
			return true;
		}
		return false;
	}
	RaycastHit isHit(Vector3 vector)
	{
		float width=100f;
		Vector3 dir=transform.TransformDirection(vector);

		Debug.DrawRay(this.transform.position,dir*width,Color.green);
		if(Physics.Raycast(this.transform.position,dir,out hit,width)){

		}
		return hit;
	}


//速度に加速度を加算し、回転をなくす
	public void goForward(float acceleration)
	{
		speed+=acceleration;
		rotation=Quaternion.Euler(0,0,0);

		heading = this.transform.forward;
		heading.Normalize();
		this.transform.position+=heading*speed;

	}
	public void doBrake()
	{
		if(speed-brake<0) speed=0;

		speed-=brake;
		rotation=Quaternion.Euler(0,0,0);

	}

	public void turnRight(float angle)
	{
		rotation=Quaternion.Euler(0,angle,0);
		this.transform.rotation*=rotation;
	}

	public void turnLeft(float angle)
	{
		rotation=Quaternion.Euler(0,-angle,0);
		this.transform.rotation*=rotation;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="wall")
		Destroy(gameObject);

	}

}
