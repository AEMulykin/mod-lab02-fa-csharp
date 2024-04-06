using System;
using System.Collections.Generic;

namespace fans
{
    public class State
    {
        public string Name { get; private set; }
        public Dictionary<char, State> Transitions { get; private set; }
        public bool IsAcceptState { get; private set; }

        public State(string name, bool isAcceptState)
        {
            Name = name;
            IsAcceptState = isAcceptState;
            Transitions = new Dictionary<char, State>();
        }

        public void AddTransition(char symbol, State state)
        {
            Transitions[symbol] = state;
        }
    }

    public class FA1
    {
        private State initialState;

        public FA1()
        {
            initialState = new State("a", false);

            var b = new State("b", false);
            var c = new State("c", true);
            var d = new State("d", false);
            var e = new State("e", false);

            initialState.AddTransition('0', b);
            initialState.AddTransition('1', e);
            b.AddTransition('0', d);
            b.AddTransition('1', c);
            c.AddTransition('0', d);
            c.AddTransition('1', c);
            d.AddTransition('0', d);
            d.AddTransition('1', d);
            e.AddTransition('0', c);
            e.AddTransition('1', e);
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = initialState;
            foreach (var c in s)
            {
                if (!current.Transitions.TryGetValue(c, out current))
                    return null;
            }
            return current.IsAcceptState;
        }
    }

    public class FA2
    {
        private State initialState;

        public FA2()
        {
            initialState = new State("a", false);

            var b = new State("b", false);
            var c = new State("c", false);
            var d = new State("d", true);

            initialState.AddTransition('0', c);
            initialState.AddTransition('1', b);
            b.AddTransition('0', d);
            b.AddTransition('1', a);
            c.AddTransition('0', a);
            c.AddTransition('1', d);
            d.AddTransition('0', b);
            d.AddTransition('1', c);
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = initialState;
            foreach (var c in s)
            {
                if (!current.Transitions.TryGetValue(c, out current))
                    return null;
            }
            return current.IsAcceptState;
        }
    }

    public class FA3
    {
        private State initialState;

        public FA3()
        {
            initialState = new State("a", false);

            var b = new State("b", false);
            var c = new State("c", true);

            initialState.AddTransition('0', initialState);
            initialState.AddTransition('1', b);
            b.AddTransition('0', initialState);
            b.AddTransition('1', c);
            c.AddTransition('0', c);
            c.AddTransition('1', c);
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = initialState;
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
            string s = "0101";
            FA1 fa1 = new FA1();
            bool? result1 = fa1.Run(s);
            Console.WriteLine(result1);

            FA2 fa2 = new FA2();
            bool? result2 = fa2.Run(s);
            Console.WriteLine(result2);

            FA3 fa3 = new FA3();
            bool? result3 = fa3.Run(s);
            Console.WriteLine(result3);
        }
    }
}
