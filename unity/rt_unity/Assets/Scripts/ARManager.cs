using GoogleARCore;
using UnityEngine;

// Based on HelloARController from Google ARCore Examples Unity SDK
namespace UnityTemplateProjects
{
#if UNITY_EDITOR
	// Set up touch input propagation while using Instant Preview in the editor.
	using Input = InstantPreviewInput;

#endif

	public interface IARManager : IManager
	{
	}

	public class ARManager : MonoBehaviour, IARManager
	{
		/// <summary>
		/// The first-person camera being used to render the passthrough camera image (i.e. AR
		/// background).
		/// </summary>
		public Camera FirstPersonCamera;

		/// <summary>
		/// True if the app is in the process of quitting due to an ARCore connection error,
		/// otherwise false.
		/// </summary>
		private bool m_IsQuitting = false;

		public void Awake()
		{
			// Enable ARCore to target 60fps camera capture frame rate on supported devices.
			// Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
			Application.targetFrameRate = 60;
		}

		public void Update()
		{
			if (State != ManagerState.Active)
			{
				return;
			}

			_UpdateApplicationLifecycle();
			
			
			// If the player has not touched the screen, we are done with this update.
			Touch touch;
			if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
			{
				return;
			}

			// Raycast against the location the player touched to search for planes.
			TrackableHit hit;
			TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
			                                  TrackableHitFlags.FeaturePointWithSurfaceNormal;

			if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
			{
				
				Debug.Log("Unity RayCast");
				// Use hit pose and camera pose to check if hittest is from the
				// back of the plane, if it is, no need to create the anchor.
				if ((hit.Trackable is DetectedPlane) &&
				    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
					    hit.Pose.rotation * Vector3.up) < 0)
				{
					Debug.Log("Hit at back of the current DetectedPlane");
				}
				else
				{
					Debug.Log("Setting anchor point for visualization");

					var anchor = hit.Trackable.CreateAnchor(hit.Pose);
					Manager.ItemManager.AnchorPoint = anchor.transform.position;
				}
			}
		}

		/// <summary>
		/// Check and update the application lifecycle.
		/// </summary>
		private void _UpdateApplicationLifecycle()
		{
			// Exit the app when the 'back' button is pressed.
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}

			// Only allow the screen to sleep when not tracking.
			if (Session.Status != SessionStatus.Tracking)
			{
				Screen.sleepTimeout = SleepTimeout.SystemSetting;
			}
			else
			{
				Screen.sleepTimeout = SleepTimeout.NeverSleep;
			}

			if (m_IsQuitting)
			{
				return;
			}

			// Quit if ARCore was unable to connect and give Unity some time for the toast to
			// appear.
			if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
			{
				_ShowAndroidToastMessage("Camera permission is needed to run this application.");
				m_IsQuitting = true;
				Invoke("_DoQuit", 0.5f);
			}
			else if (Session.Status.IsError())
			{
				_ShowAndroidToastMessage(
					"ARCore encountered a problem connecting.  Please start the app again.");
				m_IsQuitting = true;
				Invoke("_DoQuit", 0.5f);
			}
		}

		/// <summary>
		/// Actually quit the application.
		/// </summary>
		private void _DoQuit()
		{
			Application.Quit();
		}

		/// <summary>
		/// Show an Android toast message.
		/// </summary>
		/// <param name="message">Message string to show in the toast.</param>
		private void _ShowAndroidToastMessage(string message)
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject unityActivity =
				unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

			if (unityActivity != null)
			{
				AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
				unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
				{
					AndroidJavaObject toastObject =
						toastClass.CallStatic<AndroidJavaObject>(
							"makeText", unityActivity, message, 0);
					toastObject.Call("show");
				}));
			}
		}

		public ManagerState State { get; private set; }
		public void Startup()
		{
			State = ManagerState.Active;
		}
	}
}