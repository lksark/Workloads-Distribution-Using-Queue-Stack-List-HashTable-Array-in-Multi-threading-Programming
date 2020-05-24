using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Timers;

namespace MultithreadingWorkloadsDistribution
{
    class Program
    {
        private static class_mulithreading_List mySharedList_multithreads = new class_mulithreading_List();
        private static class_mulithreading_Queue mySharedQueue_multithreads = new class_mulithreading_Queue();
        private static class_mulithreading_Stack mySharedStack_multithreads = new class_mulithreading_Stack();
        private static class_mulithreading_PriorityQueue mySharedPriority_multithreads = new class_mulithreading_PriorityQueue();
        private static System.Timers.Timer aTimer;
        private static int tmpCount = 100000;
        private static int[] myArray;
        private static Dictionary<int, int> myDict = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            //Array memory allocation before any data Read / Write
            myArray = new int[200000];
            /*
            for (int i = 0; i < 200000; i++)
            {
                myArray[i] = 0;
            }
            */

            //Hash Table memory allocation in advance before any data Read / Write
            for (int i = 0; i < 200000; i++)
            {
                myDict.Add(i, 0);
            }

            for (int i = 0; i < 100000; i++)
            {
                mySharedList_multithreads.Add(i);
                //mySharedQueue_multithreads.Enqueue(i);
                //mySharedStack_multithreads.Push(i);
                //mySharedPriority_multithreads.Add(i, i);
            }

            int number_of_Program_Threads = Environment.ProcessorCount - 1;
            class_PrintNumber[] myMessagePrinter_class = new class_PrintNumber[number_of_Program_Threads];
            Thread[] threadsArray = new Thread[number_of_Program_Threads];

            for (int i = 0; i < number_of_Program_Threads; i++)
            {
                myMessagePrinter_class[i] = new class_PrintNumber(i + 1);
                threadsArray[i] = new Thread(new ThreadStart(myMessagePrinter_class[i].Print));
                threadsArray[i].Name = "WorkerID#" + i;
            }

            foreach (Thread t in threadsArray)
                t.Start();

            SetTimer();

            foreach (Thread t in threadsArray)
                t.Join();
            // Join both threads with no timeout
            // Run both until done.
            // threads have finished at this point.

            Console.WriteLine("\nFinished!");

            if (tmpCount >= 200000)
            {
                aTimer.Stop();
                aTimer.Dispose();
            }

            Console.Read();
        }

        private static void SetTimer()
        {
            // Create a timer and set a 0.001 second interval.
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            mySharedList_multithreads.Add(tmpCount++);
            //mySharedQueue_multithreads.Enqueue(tmpCount++);
            //mySharedStack_multithreads.Push(tmpCount++);
            //mySharedPriority_multithreads.Add(tmpCount++, tmpCount++);
            Random random = new Random();
            int tmpIndex = random.Next(0, 199999);
            //Console.WriteLine("myArray[" + tmpIndex + "] = " + myArray[tmpIndex]);
            Console.WriteLine("myDict[" + tmpIndex + "] = " + myDict[tmpIndex]);
        }

        public class class_mulithreading_List
        {
            private List<int> List_multithreads = new List<int>();
            private static Random random = new Random();
            private int TagNumber, ListIndex;
            private bool isLocked;

            public class_mulithreading_List()
            {
                isLocked = false;
            }

            public void Add(int theTagNumber)
            {
                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                List_multithreads.Add(theTagNumber);

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);
            }

            public bool Any()
            {
                bool _Any;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                _Any = List_multithreads.Any();

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                return _Any;
            }

            public Tuple<bool, int> RandomRemove()
            {
                bool Any = false;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                if (List_multithreads.Any())
                {
                    ListIndex = random.Next(0, List_multithreads.Count() - 1);
                    TagNumber = List_multithreads.ElementAtOrDefault(ListIndex);
                    List_multithreads.RemoveAt(ListIndex);
                    Any = true;
                }

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                //if no data from 'List_multithreads', return false. Else return true.
                return new Tuple<bool, int>(Any, TagNumber);
            }
        }

        public class class_mulithreading_Queue
        {
            private Queue<int> Queue_multithreads = new Queue<int>();
            private int TagNumber;
            private bool isLocked;

            public class_mulithreading_Queue()
            {
                isLocked = false;
            }

            public bool Any()
            {
                bool _Any;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                _Any = Queue_multithreads.Any();

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                return _Any;
            }

            public void Enqueue(int theTagNumber)
            {
                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                Queue_multithreads.Enqueue(theTagNumber);

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);
            }

            public Tuple<bool, int> Dequeue()
            {
                bool Any = false;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                if (Queue_multithreads.Any())
                {
                    TagNumber = Queue_multithreads.Dequeue();
                    Any = true;
                }

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                //if no data from 'Queue_multithreads', return false. Else return true.
                return new Tuple<bool, int>(Any, TagNumber);
            }
        }

        public class class_mulithreading_Stack
        {
            private Stack<int> Stack_multithreads = new Stack<int>();
            private int TagNumber;
            private bool isLocked;

            public class_mulithreading_Stack()
            {
                isLocked = false;
            }

            public bool Any()
            {
                bool _Any;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                _Any = Stack_multithreads.Any();

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                return _Any;
            }

            public void Push(int theTagNumber)
            {
                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                Stack_multithreads.Push(theTagNumber);

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);
            }

            public Tuple<bool, int> Pop()
            {
                bool Any = false;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                if (Stack_multithreads.Any())
                {
                    TagNumber = Stack_multithreads.Pop();
                    Any = true;
                }

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                //if no data from 'Stack_multithreads', return false. Else return true.
                return new Tuple<bool, int>(Any, TagNumber);
            }
        }

        public class class_mulithreading_PriorityQueue
        {
            //In 'PriorityQueue_multithreads', key is the Priority Level, value is the Tag Number.
            private SortedList<int, int> PriorityQueue_multithreads = new SortedList<int, int>();
            private int TagNumber;
            private bool isLocked;

            public class_mulithreading_PriorityQueue()
            {
                isLocked = false;
            }

            public bool Any()
            {
                bool _Any;

                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                _Any = PriorityQueue_multithreads.Any();

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);

                return _Any;
            }

            public void Add(int PriorityLevel, int theTagNumber)
            {
                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);
                }
                isLocked = true;

                PriorityQueue_multithreads.Add(PriorityLevel, theTagNumber);

                isLocked = false;
                Monitor.Pulse(this);
                Monitor.Exit(this);
            }

            public Tuple<bool, int> Remove()
            {
                bool Any = false;

                //obtain lock on this object
                Monitor.Enter(this);

                if (isLocked)
                {
                    Monitor.Wait(this);  //enter WaitSleepJoin state
                }
                isLocked = true;

                if (PriorityQueue_multithreads.Any())
                {
                    TagNumber = PriorityQueue_multithreads.ElementAtOrDefault(0).Value;
                    //Alway remove the first node from PriorityQueue, presuming nodes at front have highest priority
                    PriorityQueue_multithreads.RemoveAt(0);
                    Any = true;
                }

                isLocked = false;
                Monitor.Pulse(this);  //tell waiting thread (if there is one) to become ready to execute (Running state)
                Monitor.Exit(this);   //release lock on this object

                //if no data from 'Stack_multithreads', return false. Else return true.
                return new Tuple<bool, int>(Any, TagNumber);
            }
        }

        class class_PrintNumber
        {
            private int threadID;

            public class_PrintNumber(int _threadID)
            {
                threadID = _threadID;
            }

            public void Print()
            {
                Tuple<bool, int> myNumber;

                while (mySharedList_multithreads.Any())
                //while (mySharedQueue_multithreads.Any())
                //while (mySharedStack_multithreads.Any())
                //while (mySharedPriority_multithreads.Any())
                {
                    myNumber = mySharedList_multithreads.RandomRemove();
                    //myNumber = mySharedQueue_multithreads.Dequeue();
                    //myNumber = mySharedStack_multithreads.Pop();
                    //myNumber = mySharedPriority_multithreads.Remove();
                    if (myNumber.Item1)
                    {
                        Random random = new Random();
                        //myArray[myNumber.Item2] = random.Next(1, 10);
                        myDict[myNumber.Item2] = random.Next(1, 10);
                        //Console.WriteLine("threadID: " + threadID + ", myArray[" + myNumber.Item2 + "] = " + myArray[myNumber.Item2]);
                        Console.WriteLine("threadID: " + threadID + ", myDict[" + myNumber.Item2 + "] = " + myDict[myNumber.Item2]);
                    }
                }
            }
        }
    }
}
