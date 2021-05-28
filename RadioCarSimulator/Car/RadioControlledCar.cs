using RadioCarSimulator.Entities;

namespace RadioCarSimulator.Car
{
    internal abstract class RadioControlledCar
    {
        internal Orientation Heading { get; set; }
        internal int X { get; set; }
        internal int Y { get; set; }

        internal abstract int GetSpeedInMeters();

        internal void MoveForward()
        {
            Move(GetSpeedInMeters());
        }

        internal void MoveBackward()
        {
            Move(-1 * GetSpeedInMeters());
        }

        private void Move(int meters)
        {
            switch (Heading)
            {
                case Orientation.North:
                    X += meters;
                    break;
                case Orientation.South:
                    X -= meters;
                    break;
                case Orientation.East:
                    Y += meters;
                    break;
                case Orientation.West:
                    Y -= meters;
                    break;
            }
        }

        internal void TurnLeft()
        {
            Turn(clockwise: false);
        }

        internal void TurnRight()
        {
            Turn(clockwise: true);
        }

        private void Turn(bool clockwise)
        {
            Heading = Heading switch
            {
                Orientation.North => clockwise ? Orientation.East : Orientation.West,
                Orientation.East => clockwise ? Orientation.South : Orientation.North,
                Orientation.South => clockwise ? Orientation.West : Orientation.East,
                Orientation.West => clockwise ? Orientation.North : Orientation.South,
                _ => throw new System.NotImplementedException()
            };
        }

        public override string ToString()
        {
            return $"The car is located at coordinates ({X}, {Y}) heading {Heading}";
        }
    }
}
