namespace UnityTemplateProjects
{
    // Singleton mgr pattern from Unity in Action (Chp8 - https://www.manning.com/books/unity-in-action-second-edition)
    public interface IManager
    {
        ManagerState State { get; }
        void Startup();
    }

    public enum ManagerState
    {
        InActive,
        Active
    }
}