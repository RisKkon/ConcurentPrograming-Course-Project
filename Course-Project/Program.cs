
namespace Course_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            var area51 = new Base();
            var agent1 = new Agent("Mike", 1);
            var agent2 = new Agent("John", 2);
            var agent3 = new Agent("Monika", 3);
            var elevator = new Elevator();
            area51.ListForAgentsWaitingToEnterBase.Add(agent1);
            area51.ListForAgentsWaitingToEnterBase.Add(agent2);
            area51.ListForAgentsWaitingToEnterBase.Add(agent3);
            elevator.ActivateElevator(area51);
        }
    }
}