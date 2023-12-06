namespace DesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ObserverPattern
            var subject = new Subject();
            var observer = new Observer_1(subject);
            subject.AddObserver(observer);
            subject.Notify();
            #endregion
        }
    }

    #region ObserverPattern
    public interface IObserveble
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify();
        int GetState();
    }
    public class Subject : IObserveble//Something that is being observed by many Observers
    {
        List<IObserver> _observers = new();
        public void AddObserver(IObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IObserver observer) => _observers.Remove(observer);
        public void Notify() => _observers.ForEach(_ => _.Update());
        public int GetState() => 1;
    }
    public interface IObserver { void Update(); }
    public class Observer_1 : IObserver//Something that is observing changes of the state of the Subject
    {
        IObserveble _observeble;
        public Observer_1(IObserveble observeble) => _observeble = observeble;
        public void Update() => _observeble.GetState();//Do something locally in this method with data returned by GetState() of the Subject
    }
    #endregion
}