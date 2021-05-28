using RadioCarSimulator.Car;

namespace RadioCarSimulator
{
    internal class Room
    {
        internal int Width { get; set; }
        internal int Length { get; set; }

        public Room()
        {
        }

        internal void AssertIsInBounds(RadioControlledCar car)
        {
            if (car.X < 0 || car.X >= Width)
                throw new OutOfBoundsException($"Out of bounds, hit the wall at X: {car.X}");
            if (car.Y < 0 || car.Y >= Length)
                throw new OutOfBoundsException($"Out of bounds, hit the wall at Y: {car.Y}");
        }
    }
}
