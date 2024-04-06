using System;
using System.Collections.Generic;

namespace fans
{
    public class State
    {
        public string Name { get; private set; }
        public Dictionary<char, State> Transitions { get; private set; } = new Dictionary<char, State>();
        public bool IsAcceptState { get; private set; }

        public State(string name, bool isAcceptState)
        {
            Name = name;
            IsAcceptState = isAcceptState;
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

        public bool? Run(string s)
        {
            var current = initialState;
            foreach (var symbol in s)
            {
                if (!current.Transitions.TryGetValue(symbol, out current))
                {
                    return null;
                }
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
            b.AddTransition('1', initialState);
            c.AddTransition('0', initialState);
            c.AddTransition('1', d);
            d.AddTransition('0', b);
            d.AddTransition('1', c);
        }

        public bool? Run(string s)
        {
            var current = initialState;
            foreach (var symbol in s)
            {
                if (!current.Transitions.TryGetValue(symbol, out current))
                {
                    return null;
                }
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

        public bool? Run(string s)
        {
            var current = initialState;
            foreach (var symbol in s)
            {
                if (!current.Transitions.TryGetValue(symbol, out current))
                {
                    return null;
                }
            }
            return current.IsAcceptState;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string testString = "0101";
            FA1 fa1 = new FA1();
            FA2 fa2 = new FA2();
            FA3 fa3 = new FA3();

            bool? resultFA1 = fa1.Run(testString);
            bool? resultFA2 = fa2.Run(testString);
            bool? resultFA3 = fa3.Run(testString);

            Console.WriteLine($"Result for FA1: {resultFA1}");
            Console.WriteLine($"Result for FA2: {resultFA2}");
            Console.WriteLine($"Result for FA3: {resultFA3}");
        }
    }
}
