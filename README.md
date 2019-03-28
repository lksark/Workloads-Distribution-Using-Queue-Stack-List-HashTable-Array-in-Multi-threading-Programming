# Workloads-Distribution-Using-Queue-Stack-List-HashTable-Array-in-Multi-threading-Programming
Workloads Distribution Using Queue, Stack, List, Hash Table, Array in Multi-threading Programming
Introduction
Nowadays computers are all multi-cores. Therefore, we can use Queue, Stack, Priority Queue, List to distribute workloads in multithreading programming.

If the workloads can be completed within a finite timeframe and number of workloads are much bigger than the computer’s available threads, we can tag these workloads and place the tags into Queue, Stack, Priority Queue, Linked List. Subsequently these workloads / processes can then be distributed accordingly among all the available threads to execute.

To tag the workloads, we can use numbering or other form of unique, not repetitive ID.

 

Hash Table and Array
Both Hash Table (equivalent to Visual Studio’s Dictionary) and Array can be used to save workloads result if we want to trace the results are coming from which workloads.

Hash Table has memory allocation advantage over Array. Before using Array, we need to declare Array. When declaring an Array, program will reserve a continuous chunk of memory block. In Hash Table, single node size memory is reserved when new node created; each node memory location are scattered around and are not cluster together.

Array allows concurrent multithreads data read / write, because the required memory block are reserved in advance. Hash Table can also allow concurrent multithreads data read / write, provided Hash Table memory allocation are created before the data editing.

If memory allocation is done upon adding a new node into Hash Table, the thread needs to lock the Hash Table, preventing other threads from read / write the Hash Table; after the thread added the node into Hash Table, the thread unlocks the Hash Table, allowing other threads to write data into the Hash Table.

Hash Table and Array are not suitable to store workload tags, because the tags should be removed out upon the threads are processing the workloads. Furthermore, new workload tags are being added into the queue / stack / list at any time.

 

Queue and Stack
If the workloads need to be processed sequentially, we can use queue and stack to save the workloads tag.

Queue is First In First Out (FIFO) manner; Stack is Last In Fist Out (LIFO) manner.

 

Priority Queue
When the workloads have priority, highest priority workloads must be attended first. We can use Priority Queue (command in Visual Studio is ‘SortedList’).

Visual Studio ‘SortedList’ class represents a collection of key-and-value pairs that are sorted by the keys and are accessible by key and by index. Here the keys indicate individual workloads’ priority number, value is the tag number.

 

 

Linked List
When we want to process the workloads in random manner, we can use Linked List to save the workloads tag.

 

Example

This program print-out numbers 1 to 100000 individually without repeating in random manner using multi-threading programming.

Version ‘1’ in c# Console App: Example_v1.cs.txt

Version ‘2’ New single number (100001~200000) is added into the List every millisecond while threads are printing the numbers out.

In c# Console App:  Example_v2.cs.txt

Version ‘3’: Added Array and Hash Table into the code. Randomly generate some data and save into Array / Hash Table.  Example_v3.cs.txt

 

 

References
·       Book ‘Visual C# 2005 How to Program’ by Deitel
