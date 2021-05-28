using RadioCarSimulator.Car;

namespace RadioCarSimulator.Command
{
    internal class TurnRightCommand : ICommand
    {
        private RadioControlledCar _car;

        public TurnRightCommand(RadioControlledCar car)
        {
            _car = car;
        }

        public void Invoke()
        {
            _car.TurnRight();
        }
    }
}
