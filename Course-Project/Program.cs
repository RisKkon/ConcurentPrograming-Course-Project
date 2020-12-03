
namespace Course_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            var area51 = new Base();
            var agent1 = new Agent("Gosho", 1);
            var agent2 = new Agent("MaikulSKavala", 2);
            var agent3 = new Agent("MishoShamara", 3);
            var elevator = new Elevator();
            area51.ListForAgentsWaitingToEnterBase.Add(agent1);
            area51.ListForAgentsWaitingToEnterBase.Add(agent2);
            area51.ListForAgentsWaitingToEnterBase.Add(agent3);
            elevator.ActivateElevator(area51);
        }
    }
}