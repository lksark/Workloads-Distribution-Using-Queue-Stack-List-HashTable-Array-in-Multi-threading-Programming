# Workloads-Distribution-Using-Queue-Stack-List-HashTable-Array-in-Multi-threading-Programming
Workloads Distribution Using Queue, Stack, List, Hash Table, Array in Multi-threading Programming

## Introduction

Nowadays computers are all multi-cores. Therefore, we can use Queue, Stack, Priority Queue, List to distribute workloads in multithreading programming.

If the workloads can be completed within a finite timeframe and number of workloads are much bigger than the computer’s available threads, we can tag these workloads and place the tags into Queue, Stack, Priority Queue, Linked List. Subsequently these workloads / processes can then be distributed accordingly among all the available threads to execute.

To tag the workloads, we can use numbering or other form of unique, not repetitive ID.

 

## Hash Table and Array
Both Hash Table (equivalent to Visual Studio’s Dictionary) and Array can be used to save workloads result if we want to trace the results are coming from which workloads.

Hash Table has memory allocation advantage over Array. Before using Array, we need to declare Array. When declaring an Array, program will reserve a continuous chunk of memory block. In Hash Table, single node size memory is reserved when new node created; each node memory location are scattered around and are not cluster together.

Array allows concurrent multithreads data read / write, because the required memory block are reserved in advance. Hash Table can also allow concurrent multithreads data read / write, provided Hash Table memory allocation are created before the data editing.

If memory allocation is done upon adding a new node into Hash Table, the thread needs to lock the Hash Table, preventing other threads from read / write the Hash Table; after the thread added the node into Hash Table, the thread unlocks the Hash Table, allowing other threads to write data into the Hash Table.

Hash Table and Array are not suitable to store workload tags, because the tags should be removed out upon the threads are processing the workloads. Furthermore, new workload tags are being added into the queue / stack / list at any time.

 

## Queue and Stack
If the workloads need to be processed sequentially, we can use queue and stack to save the workloads tag.

Queue is First In First Out (FIFO) manner; Stack is Last In Fist Out (LIFO) manner.

 

## Priority Queue
When the workloads have priority, highest priority workloads must be attended first. We can use Priority Queue (command in Visual Studio is ‘SortedList’).

Visual Studio ‘SortedList’ class represents a collection of key-and-value pairs that are sorted by the keys and are accessible by key and by index. Here the keys indicate individual workloads’ priority number, value is the tag number.

 

 

## Linked List
When we want to process the workloads in random manner, we can use Linked List to save the workloads tag.

 

### Example

This program print-out numbers 1 to 100000 individually without repeating in random manner using multi-threading programming.

Version ‘1’ in c# Console App: Example_v1.cs.txt

Version ‘2’ New single number (100001~200000) is added into the List every millisecond while threads are printing the numbers out.

In c# Console App:  Example_v2.cs.txt

Version ‘3’: Added Array and Hash Table into the code. Randomly generate some data and save into Array / Hash Table.  Example_v3.cs.txt

Version ‘4’: Using C# lock statement. The lock statement acquires the mutual-exclusion lock for a given object, executes a statement block, and then releases the lock. Other threads will have to wait until the lock is released. The statement method same to coding that using Monitor Class above.  Example_v4.cs.txt

Version ‘5’: Using Monitor Class. The ‘try-finally’ block is used so that the ‘isLocked’ variable will always set to false when the try/catch block leaves the execution, regardless the try block terminates normally or terminates due to an exception. Hence allows other threads to read / edit task queue afterwards.  Example_v5.cs.txt

Deadlock / Livelock may happen if statement block is depending on external members.

 
### Example 2

In previous example, each subprocess threads will exit individually when found the task queue / stack / list is empty. Main process thread will end when all the sub-process threads are ended.

In some scenarios we want the main program to continue running even when task queue / stack / list is empty, waiting for new tasks to be added into the task queue and letting subprocess threads execute the tasks.

In this example, the subprocess threads and main program will not exit when the task queue / stack / list is empty. Instead, worker threads will enter ‘WaitSleepJoin’ state until new workloads tag numbers are added into the task queue / stack / list. Subprocess worker threads will become Running state when obtained the lock.

Producer threads will input a number of workloads tag numbers into the task queue / stack / list. Then sleep for 5 seconds. Wake-up, input another number of workloads tag numbers into the task queue / stack / list. Therefore, certain time within these 5 seconds interval, the task queue / stack / list is empty.

Version ‘1’: Although main program will not exit when task queue / stack / list is empty. But this program also unable to exit by itself, except force termination ctrl-c.

c# Console App:  multithreaded_message_queue1.cs.txt

Version ‘2’: amended coding. press ‘q’ button to one-by-one end all the subprocess worker threads and then the main program thread.

c# Console App: multithreaded_message_queue2.cs.txt 




## References
·       Book ‘Visual C# 2005 How to Program’ by Deitel
