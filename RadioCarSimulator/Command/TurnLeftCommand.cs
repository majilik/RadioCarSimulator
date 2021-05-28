using RadioCarSimulator.Car;

namespace RadioCarSimulator.Command
{
    internal class TurnLeftCommand : ICommand
    {
        private RadioControlledCar _car;

        public TurnLeftCommand(RadioControlledCar car)
        {
            _car = car;
        }

        public void Invoke()
        {
            _car.TurnLeft();
        }
    }
}
