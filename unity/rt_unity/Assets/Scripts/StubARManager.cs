using UnityEngine;

namespace UnityTemplateProjects
{
	public class StubARManager : MonoBehaviour, IARManager
	{
		public ManagerState State { get; private set; }
		public void Startup()
		{
			State = ManagerState.Active;
		}
	}
}