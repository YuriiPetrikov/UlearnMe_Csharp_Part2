using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using static Clones.Clone;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private List<Clone> clones;

        public CloneVersionSystem()
        {
            clones = new List<Clone> { new Clone() };
        }

        public string Execute(string query)
        {
            var oneQuery = query.Split();
            var cloneNumber = Int32.Parse(oneQuery[1]) - 1;

            switch (oneQuery[0])
            {
                case "learn":
                    clones.ElementAt(cloneNumber).Learn(oneQuery[2]);
                    return null;
                case "rollback":
                    clones.ElementAt(cloneNumber).Rollback();
                    return null;
                case "relearn":
                    clones.ElementAt(cloneNumber).Relearn();
                    return null;
                case "check":
                    return clones.ElementAt(cloneNumber).Check();
                case "clone":
                    clones.Add(new Clone(clones.ElementAt(cloneNumber)));
                    return null;
            }

            return null;
        }
    }


    public class Clone
    {
        LinkedStack<string> programAssimilate;
        LinkedStack<string> programCancel;

        public Clone()
        {
            programAssimilate = new LinkedStack<string>();
            programCancel = new LinkedStack<string>();
        }

        public Clone(Clone oldClone)
        {
            programAssimilate = new LinkedStack<string>(oldClone.programAssimilate);
            programCancel = new LinkedStack<string>(oldClone.programCancel);
        }

        public void Learn(string pi)
        {
            programAssimilate.Push(pi);
            programCancel.Clear();
        }

        public void Rollback()
        {
            programCancel.Push(programAssimilate.Pop());
        }

        public void Relearn()
        {
            programAssimilate.Push(programCancel.Pop());
        }

        public string Check()
        {
            return programAssimilate.IsEmpty() ? "basic" : programAssimilate.Peek();
        }
    }

    public class LinkedStack<T>
    {
        StackItem<T> head;
       
        public LinkedStack() { }
        public LinkedStack(LinkedStack<T> stack)
        {
            head = stack.head;
        }

        public void Push(T value)
        {
            var item = new StackItem<T> { Value = value, Previous = head };
            head = item;
        }

        public T Peek()
        {
            return head.Value;
        }

        public T Pop()
        {
            var value = head.Value;
            head = head.Previous;
        
            return value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void Clear()
        {
            head = null;
        }
    }

    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Previous { get; set; }
    }
}
