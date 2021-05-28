using RadioCarSimulator.Car;

namespace RadioCarSimulator.Command
{
    internal class MoveForwardCommand : ICommand
    {
        private Room _room;
        private RadioControlledCar _car;

        public MoveForwardCommand(Room room, RadioControlledCar car)
        {
            _room = room;
            _car = car;
        }

        public void Invoke()
        {
            _car.MoveForward();
            _room.AssertIsInBounds(_car);
        }
    }
}
