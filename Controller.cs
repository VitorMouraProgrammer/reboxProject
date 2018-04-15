using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Controller : MonoBehaviour {
	
	public EventTrigger.TriggerEvent OnLeft;
	public EventTrigger.TriggerEvent OnRight;
	public EventTrigger.TriggerEvent OnTop;
	public EventTrigger.TriggerEvent OnBottom;
	public EventTrigger.TriggerEvent OnClick;
	public EventTrigger.TriggerEvent OnDoubleClick;
	public EventTrigger.TriggerEvent OnHold;

	private BaseEventData eventData = new BaseEventData (EventSystem.current);

	private readonly Vector2 mXAxis = new Vector2(1, 0);
	private readonly Vector2 mYAxis = new Vector2(0, 1);

	private float lastClickTime = 0;
	private readonly float catchTime = 0.25F;

	private int mMessageIndex = 0;

	// The angle range for detecting swipe
	private	const float mAngleRange = 30.0F;

	// To recognize as swipe user should at lease swipe for this many pixels
	private	const float mMinSwipeDist = 10.0F;

	// To recognize as a swipe the velocity of the swipe
	// should be at least mMinVelocity
	// Reduce or increase to control the swipe speed
	private	const float mMinVelocity = 200.0F;

	private Vector2 mStartPosition;
	private float mSwipeStartTime;

	// Use this for initialization
	void Start() {}

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			mMessageIndex = 1;
			OnLeft.Invoke (eventData);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			mMessageIndex = 2;
			OnRight.Invoke (eventData);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			mMessageIndex = 3;
			OnTop.Invoke (eventData);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			mMessageIndex = 4;
			OnBottom.Invoke (eventData);
		}
			
		if(Input.GetMouseButtonDown(0)) {
			if (Time.time - lastClickTime < catchTime) {
				mMessageIndex = 5;
			} else {
				mMessageIndex = 6;
			}
			lastClickTime = Time.time;
			// Record start time and position
			mStartPosition = new Vector2 (Input.mousePosition.x,
				Input.mousePosition.y);
			mSwipeStartTime = Time.time;
		}

		// Mouse button up, possible chance for a swipe
		if (Input.GetMouseButtonUp (0)) {
			float deltaTime = Time.time - mSwipeStartTime;

			Vector2 endPosition = new Vector2 (Input.mousePosition.x,
				                      Input.mousePosition.y);
			Vector2 swipeVector = endPosition - mStartPosition;

			float velocity = swipeVector.magnitude / deltaTime;

			if (velocity > mMinVelocity &&
			    swipeVector.magnitude > mMinSwipeDist) {
				// if the swipe has enough velocity and enough distance

				swipeVector.Normalize ();

				float angleOfSwipe = Vector2.Dot (swipeVector, mXAxis);
				angleOfSwipe = Mathf.Acos (angleOfSwipe) * Mathf.Rad2Deg;

				// Detect left and right swipe
				if (angleOfSwipe < mAngleRange) {
					mMessageIndex = 2;
					OnRight.Invoke (eventData);
				} else if ((180.0F - angleOfSwipe) < mAngleRange) {
					mMessageIndex = 1;
					OnLeft.Invoke (eventData);
				} else {
					// Detect top and bottom swipe
					angleOfSwipe = Vector2.Dot (swipeVector, mYAxis);
					angleOfSwipe = Mathf.Acos (angleOfSwipe) * Mathf.Rad2Deg;
					if (angleOfSwipe < mAngleRange) {
						mMessageIndex = 3;
						OnTop.Invoke (eventData);
					} else if ((180.0F - angleOfSwipe) < mAngleRange) {
						mMessageIndex = 4;
						OnBottom.Invoke (eventData);
					} else {
						mMessageIndex = 0;
					}
				}
			} else {
				if (mMessageIndex == 5 && !Input.GetMouseButton (0))
					OnDoubleClick.Invoke (eventData);
				else if (mMessageIndex == 6 && !Input.GetMouseButton (0))
					OnClick.Invoke (eventData);
			}
		}
	}
}