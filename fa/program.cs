using System;
using System.Collections.Generic;

namespace fans
{
    public class State
    {
        public string Name { get; }
        public Dictionary<char, State> Transitions { get; }
        public bool IsAcceptState { get; }

        public State(string name, bool isAcceptState)
        {
            Name = name;
            IsAcceptState = isAcceptState;
            Transitions = new Dictionary<char, State>();
        }

        public void AddTransition(char input, State newState)
        {
            Transitions[input] = newState;
        }
    }

    public class FiniteAutomaton
    {
        private readonly State _initialState;

        public FiniteAutomaton(State initialState)
        {
            _initialState = initialState;
        }

        public bool? Run(IEnumerable<char> input)
        {
            State currentState = _initialState;
            foreach (char symbol in input)
            {
                if (!currentState.Transitions.TryGetValue(symbol, out currentState))
                {
                    return null;
                }
            }
            return currentState.IsAcceptState;
        }
    }

    class Program
    {
        static void SetupTransitionsForFA1(State a, State b, State c, State d, State e)
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
        }

        static void SetupTransitionsForFA2(State a, State b, State c, State d)
        {
            a.AddTransition('0', c);
            a.AddTransition('1', b);
            b.AddTransition('0', d);
            b.AddTransition('1', a);
            c.AddTransition('0', a);
            c.AddTransition('1', d);
            d.AddTransition('0', b);
            d.AddTransition('1', c);
        }

        static void SetupTransitionsForFA3(State a, State b, State c)
        {
            a.AddTransition('0', a);
            a.AddTransition('1', b);
            b.AddTransition('0', a);
            b.AddTransition('1', c);
            c.AddTransition('0', c);
            c.AddTransition('1', c);
        }

        static void Main(string[] args)
        {
            string input = "0101";

            // FA1
            State aFA1 = new State("a", false);
            State bFA1 = new State("b", false);
            State cFA1 = new State("c", true);
            State dFA1 = new State("d", false);
            State eFA1 = new State("e", false);
            SetupTransitionsForFA1(aFA1, bFA1, cFA1, dFA1, eFA1);
            FiniteAutomaton fa1 = new FiniteAutomaton(aFA1);
            bool? resultFA1 = fa1.Run(input);
            Console.WriteLine($"Result FA1: {resultFA1}");

            // FA2
            State aFA2 = new State("a", false);
            State bFA2 = new State("b", false);
            State cFA2 = new State("c", false);
            State dFA2 = new State("d", true);
            SetupTransitionsForFA2(aFA2, bFA2, cFA2, dFA2);
            FiniteAutomaton fa2 = new FiniteAutomaton(aFA2);
            bool? resultFA2 = fa2.Run(input);
            Console.WriteLine($"Result FA2: {resultFA2}");

            // FA3
            State aFA3 = new State("a", false);
            State bFA3 = new State("b", false);
            State cFA3 = new State("c", true);
            SetupTransitionsForFA3(aFA3, bFA3, cFA3);
            FiniteAutomaton fa3 = new FiniteAutomaton(aFA3);
            bool? resultFA3 = fa3.Run(input);
            Console.WriteLine($"Result FA3: {resultFA3}");
        }
    }
}
Этот код завершает рефакторинг исходного кода, предоставляя чёткую и краткую структуру для создания конечных автоматов и их использования. Каждый из конечных автоматов может быть запущен с любой входной по
