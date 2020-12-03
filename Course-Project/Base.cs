using System;
using System.Collections.Generic;
using System.Threading;

namespace Course_Project
{
    public class Base
    {
        public List<Agent> ListForGroundFloor = new List<Agent>();
        public List<Agent> ListForSecretFloor = new List<Agent>();
        public List<Agent> ListForT1Floor = new List<Agent>();
        public List<Agent> ListForT2Floor = new List<Agent>();
        public List<Agent> ListForGroundFloorElevator = new List<Agent>();
        public List<Agent> ListForSecretFloorElevator = new List<Agent>();
        public List<Agent> ListForT1FloorElevator = new List<Agent>();
        public List<Agent> ListForT2FloorElevator = new List<Agent>();
        public Queue<int> QueueForElevatorCommands = new Queue<int>();
        public List<Agent> ListForAgentsWaitingToEnterBase = new List<Agent>();
        object lockForGround = new object();
        object lockForSecret = new object();
        object lockForT1 = new object();
        object lockForT2 = new object();
        public int CompletedTasks;
        
        public void ActivateBase()
        {
            while (true)
            {
                new Thread(() =>
                {
                    lock (lockForGround)
                    {
                        if (ListForGroundFloor.Count != 0)
                        {
                            var agent = ListForGroundFloor[0];
                            CompletedTasks++;                            
                            Console.WriteLine("---Agent " + agent.Name + " is doing a task on ground floor");
                            QueueForElevatorCommands.Enqueue(1);
                            ListForGroundFloor.Remove(agent);
                            ListForGroundFloorElevator.Add(agent);
                        }
                    }
                    
                    lock (lockForSecret)
                    {
                        if (ListForSecretFloor.Count != 0)
                        {
                            var agent = ListForSecretFloor[0];
                            Console.WriteLine("---Agent " + agent.Name + " is doing a task on secret floor");
                            CompletedTasks++;
                            QueueForElevatorCommands.Enqueue(2);
                            ListForSecretFloor.Remove(agent);
                            ListForSecretFloorElevator.Add(agent);
                        }
                    }
                    
                    lock (lockForT1)
                    {
                        if (ListForT1Floor.Count != 0)
                        {
                            var agent = ListForT1Floor[0];
                            Console.WriteLine("---Agent " + agent.Name + " is doing a task on T1 floor");
                            CompletedTasks++;
                            QueueForElevatorCommands.Enqueue(3);
                            ListForT1Floor.Remove(agent);
                            ListForT1FloorElevator.Add(agent);
                        }
                    }
                    
                    lock (lockForT2)
                    {
                        if (ListForT2Floor.Count != 0)
                        {
                            Agent agent = ListForT2Floor[0];
                            Console.WriteLine("---Agent " + agent.Name + " is doing a task on T2 floor");
                            CompletedTasks++;
                            QueueForElevatorCommands.Enqueue(4);
                            ListForT2Floor.Remove(agent);
                            ListForT2FloorElevator.Add(agent);
                            
                        }
                    }
                }).Start();
            }
        }
    }
}