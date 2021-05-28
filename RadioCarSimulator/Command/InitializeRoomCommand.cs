namespace RadioCarSimulator.Command
{
    internal class InitializeRoomCommand : ICommand
    {
        private Room _room;
        private int _width;
        private int _length;

        public InitializeRoomCommand(Room room, int width, int length)
        {
            _room = room;
            _width = width;
            _length = length;
        }

        public void Invoke()
        {
            _room.Length = _length;
            _room.Width = _width;
        }
    }
}
