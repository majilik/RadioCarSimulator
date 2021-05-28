using RadioCarSimulator.Car;
using RadioCarSimulator.Entities;

namespace RadioCarSimulator.Command
{
    internal class InitializeStartPositionCommand : ICommand
    {
        private Room _room;
        private RadioControlledCar _car;
        private int _x;
        private int _y;
        private Orientation _heading;

        public InitializeStartPositionCommand(Room room, RadioControlledCar car, int x, int y, Orientation heading)
        {
            _room = room;
            _car = car;
            _x = x;
            _y = y;
            _heading = heading;
        }

        public void Invoke()
        {
            _car.X = _x;
            _car.Y = _y;
            _car.Heading = _heading;
            _room.AssertIsInBounds(_car);
        }
    }
}
