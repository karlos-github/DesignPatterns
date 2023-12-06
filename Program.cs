using System.ComponentModel;

namespace DesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ObserverPattern
            var subject = new ObserverPattern.Subject();
            var observer = new ObserverPattern.Observer_1(subject);
            subject.AddObserver(observer);
            subject.Notify();
            #endregion

            #region DecoratorPattern
            Decorator_Pattern.IComponent component = new Decorator_Pattern.Component_1();//expresso
            Decorator_Pattern.IComponent component_decorated_1 = new Decorator_Pattern.ConcreteDecorator_1(component);//expresso + sugar
            Decorator_Pattern.IComponent component_decorated_2 = new Decorator_Pattern.ConcreteDecorator_2(component_decorated_1);//expresso + sugar + soya milk
            Console.WriteLine($"{component_decorated_2.Method()}");//final price is 3 + 4 + 10
            #endregion
        }
    }

    #region ObserverPattern
    namespace ObserverPattern
    {
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
    }

    #endregion

    #region DecoratorPattern
    namespace Decorator_Pattern
    {
        public interface IComponent { int Method(); }//ICoffe component, Method() == GetCost() .... cost of the coffee
        public class Component_1 : IComponent//ConcreteComponent as expresso coffee
        {
            public int Method() => 3;//Expresso coffe costs 3
        }
        public class Component_2 : IComponent//ConcreteComponent as latte coffee
        {
            public int Method() => 300;//Latte  costs 300
        }
        public abstract class Decorator : IComponent//Abstract Condiment, must inherit from IComponent
        {
            IComponent _component;//must have IComponent
            public Decorator(IComponent component) => _component = component;
            public virtual int Method() => _component.Method();//must be override
        }
        public class ConcreteDecorator_1 : Decorator//Concrete condiment like sugar
        {
            public ConcreteDecorator_1(IComponent component) : base(component) { }
            public override int Method() => base.Method() + 4;//Increses the cost of mixture component + this concrete decorator (ex. .. latte + sugar)
        }
        public class ConcreteDecorator_2 : Decorator//Other condiment like soya milk
        {
            public ConcreteDecorator_2(IComponent component) : base(component) { }
            public override int Method() => base.Method() + 10;//Increses the cost of mixture
        }
    }

    #endregion
}