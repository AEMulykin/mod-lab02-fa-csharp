using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;

namespace fans
{
    public class State
    {
        public string Name { get; set; }
        public Dictionary<char, State> Transitions { get; private set; } = new Dictionary<char, State>();
        public bool IsAcceptState { get; set; }

        public void AddTransition(char input, State state)
        {
            Transitions[input] = state;
        }
    }

    public class FiniteAutomaton
    {
        private readonly State _initialState;

        public FiniteAutomaton(State initialState)
        {
            _initialState = initialState;
        }

        public void InitializeTransitions(Action<State> transitionsInitializer)
        {
            transitionsInitializer(_initialState);
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = _initialState;
            foreach (var c in s)
            {
                if (!current.Transitions.TryGetValue(c, out current))
                    return null;
            }
            return current.IsAcceptState;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var s = "0101";
            
            var fa1 = CreateFA1();
            var result1 = fa1.Run(s);
            Console.WriteLine($"Result for FA1: {result1}");

            var fa2 = CreateFA2();
            var result2 = fa2.Run(s);
            Console.WriteLine($"Result for FA2: {result2}");

            var fa3 = CreateFA3();
            var result3 = fa3.Run(s);
            Console.WriteLine($"Result for FA3: {result3}");
        }

        static FiniteAutomaton CreateFA1()
        {
            var a = new State { Name = "a", IsAcceptState = false };
            var b = new State { Name = "b", IsAcceptState = false };
            var c = new State { Name = "c", IsAcceptState = true };
            var d = new State { Name = "d", IsAcceptState = false };
            var e = new State { Name = "e", IsAcceptState = false };

            var fa1 = new FiniteAutomaton(a);
            fa1.InitializeTransitions((initialState) =>
            {
                a.AddTransition('0', b);
                a.AddTransition('1', e);
                b.AddTransition('0', d);
                b.AddTransition('1', c);
                c.AddTransition('0', d);
                c.AddTransition('1', c);
                d.AddTransition('0', d);
                d.AddTransition('1', d);
                e.AddTransition('0', c);
                e.AddTransition('1', e);
            });

            return fa1;
        }

        static FiniteAutomaton CreateFA2()
        {
            var a = new State { Name = "a", IsAcceptState = false };
            var b = new State { Name = "b", IsAcceptState = false };
            var c = new State { Name = "c", IsAcceptState = false };
            var d = new State { Name = "d", IsAcceptState = true };

            var fa2 = new FiniteAutomaton(a);
            fa2.InitializeTransitions((initialState) =>
            {
                a.AddTransition('0', c);
                a.AddTransition('1', b);
                b.AddTransition('0', d);
                b.AddTransition('1', a);
                c.AddTransition('0', a);
                c.AddTransition('1', d);
                d.AddTransition('0', b);
                d.AddTransition('1', c);
            });

            return fa2;
        }

        static FiniteAutomaton CreateFA3()
        {
            var a = new State { Name = "a", IsAcceptState = false };
            var b = new State { Name = "b", IsAcceptState = false };
            var c = new State { Name = "c", IsAcceptState = true };

            var fa3 = new FiniteAutomaton(a);
            fa3.InitializeTransitions((initialState) =>
            {
                a.AddTransition('0', a);
                a.AddTransition('1', b);
                b.AddTransition('0', a);
                b.AddTransition('1', c);
                c.AddTransition('0', c);
                c.AddTransition('1', c);
            });

            return fa3;
        }
    }
}
