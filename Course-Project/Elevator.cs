using System;
using System.Collections.Generic;
using System.Threading;

namespace Course_Project
{
    public class Elevator
    {
        private int _currentFloor;
        private Agent _currentAgen;
        private int _nextFloor;


        public void ActivateElevator(Base area51)
        {
            Console.WriteLine("Welcome to area51");
            
            new Thread(() =>
            {
                area51.ActivateBase();

            }).Start();

            new Thread(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    switch (_currentFloor)
                    {
                        case 0:
                            takeAgentOnBoard(area51.ListForAgentsWaitingToEnterBase);
                            Console.WriteLine("Taking Agent " + _currentAgen.Name + " from waiting queue.");
                            break;
                        case 1:
                            takeAgentOnBoard(area51.ListForGroundFloorElevator);
                            Console.WriteLine("Taking Agent " + _currentAgen.Name + " from floor: 1");
                            break;
                        case 2:
                            takeAgentOnBoard(area51.ListForSecretFloorElevator);
                            Console.WriteLine("Taking Agent " + _currentAgen.Name + " from floor: 2");
                            break;
                        case 3:
                            takeAgentOnBoard(area51.ListForT1FloorElevator);
                            Console.WriteLine("Taking Agent " + _currentAgen.Name + " from floor: 3");
                            break;
                        case 4:
                            takeAgentOnBoard(area51.ListForT2FloorElevator);
                            Console.WriteLine("Taking Agent " + _currentAgen.Name + " from floor: 4");
                            break;
                    }
                    _nextFloor = GetRandomFloor();
                    MoveElevator(_nextFloor);
                    SendAgentToFloor(area51);
                    if (i < 2)
                    {
                        MoveElevator(0);
                    }
                    else
                    {
                        MoveElevator(waitForAgentToCallElevator(area51));
                    }

                    if (area51.CompletedTasks >= 20)
                    {
                        Console.WriteLine("All the work is done, exiting program");
                        Environment.Exit(1);
                    }
                }
            }).Start();
        }


        private int waitForAgentToCallElevator(Base area51)
        {
            if (area51.QueueForElevatorCommands.Count < 1)
            {
                Thread.Sleep(2000);
                waitForAgentToCallElevator(area51);
            }
            return area51.QueueForElevatorCommands.Dequeue();
        }
        private void takeAgentOnBoard(List<Agent> listOfAgents)
        {
            _currentAgen = listOfAgents[0];
            listOfAgents.Remove(_currentAgen);
        }
        
        private void MoveElevator(int nextFloor)
        {
            if (_currentFloor > nextFloor)
            {
                Console.WriteLine("=============================");
                Console.WriteLine("Going down");
                for (int i = _currentFloor; i > nextFloor; i--)
                {
                    Console.WriteLine("Elevator is on floor: " + i);
                    Thread.Sleep(1000);
                }
                _currentFloor = nextFloor;
                Console.WriteLine("Arrived at floor: " + _currentFloor);
                Console.WriteLine("=============================");
            }
            else if(_currentFloor < nextFloor)
            {
                Console.WriteLine("=============================");
                Console.WriteLine("Going up");
                for (int i = _currentFloor; i < nextFloor; i++)
                {
                    Console.WriteLine("Elevator is on floor: " + i);
                    Thread.Sleep(1000);
                }
                _currentFloor = nextFloor;
                Console.WriteLine("Elevator arrived at floor: " + _currentFloor);
                Console.WriteLine("=============================");
            }
            else if(_currentFloor == nextFloor)
            {
                Console.WriteLine("Elevator is already on this floor");
            }
        }
        
        private bool GetValidationForSecurityLevelOfAgent()
        {
            var result = false;
            if (_currentAgen.SecurityLevel == 1 && _currentFloor == 1)
            {
                result = true;
            }
            else if (_currentAgen.SecurityLevel == 2 && (_currentFloor == 1 || _currentFloor == 2))
            {
                result = true;
            }
            else if (_currentAgen.SecurityLevel == 3)
            {
                result = true;
            }

            return result;
        }
        private void SendAgentToFloor(Base area51)
        {
            if (GetValidationForSecurityLevelOfAgent())
            {
                if (_currentFloor == 1) area51.ListForGroundFloor.Add(_currentAgen);
                if (_currentFloor == 2) area51.ListForSecretFloor.Add(_currentAgen); 
                if (_currentFloor == 3) area51.ListForT1Floor.Add(_currentAgen); 
                if (_currentFloor == 4) area51.ListForT2Floor.Add(_currentAgen);
            }
            else
            {
                Console.WriteLine("You dont have permission to be here, enter another floor");
                _nextFloor = GetRandomFloor();
                MoveElevator(_nextFloor);
                SendAgentToFloor(area51);
            }
        }

        private int GetRandomFloor()
        {
            var random = new Random();
            var result = random.Next(1,5);
            Console.WriteLine("Selected floor: " + result);
            return result;
        }
    }
}