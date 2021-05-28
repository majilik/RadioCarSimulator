using RadioCarSimulator.Car;

namespace RadioCarSimulator.Command
{
    internal class MoveBackwardCommand : ICommand
    {
        private Room _room;
        private RadioControlledCar _car;

        public MoveBackwardCommand(Room room, RadioControlledCar car)
        {
            _room = room;
            _car = car;
        }

        public void Invoke()
        {
            _car.MoveBackward();
            _room.AssertIsInBounds(_car);
        }
    }
}
