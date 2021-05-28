using RadioCarSimulator.Command;
using RadioCarSimulator.Entities;
using RadioCarSimulator.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RadioCarSimulator.Car;

namespace RadioCarSimulator
{
    internal class Simulator
    {
        private Action<string> _writeOutput;
        private Func<string> _readInput;

        private ICollection<ICommand> _commands;
        private Room _room;
        private RadioControlledCar _car;

        private readonly Regex _roomSizeCapturePattern = new Regex(@"^(\d*) (\d*)$");
        private readonly Regex _startPositionCapturePattern = new Regex(@"^(\d*) (\d*) ([NSWE])$", RegexOptions.IgnoreCase);
        private readonly Regex _movementCommandsCapturePattern = new Regex(@"([FBLR])", RegexOptions.IgnoreCase);
        private readonly Regex _movementCommandsValidPattern = new Regex(@"[FBLR]", RegexOptions.IgnoreCase);

        /// <summary>
        /// A simulator for a radio controlled car driving around in a room.
        /// </summary>
        /// <param name="writeOutput">Action to handle the simulator output</param>
        /// <param name="readInput">Function used to input into the simulator</param>
        internal Simulator(Action<string> writeOutput, Func<string> readInput)
        {
            if (writeOutput == null)
            {
                throw new ArgumentNullException(nameof(writeOutput));
            }

            if (readInput == null)
            {
                throw new ArgumentNullException(nameof(readInput));
            }

            _writeOutput = writeOutput;
            _readInput = readInput;
            _commands = new List<ICommand>();

            _room = new Room();
            _car = new MonsterTruck();
        }

        internal void Run()
        {
            _writeOutput("Welcome to RadioCarSimulator");
            PromptRoomSize();
            PromptStartPosition();
            PromptMovementCommands();
            CalculateResult();
        }
        
        private void PromptRoomSize()
        {
            Queue<string> captures = PromptAndCaptureResponse(
                "Enter the room size (two integers, separated by space):", 
                _roomSizeCapturePattern);
            
            int width = Convert.ToInt32(captures.Dequeue());
            int length = Convert.ToInt32(captures.Dequeue());

            _commands.Add(new InitializeRoomCommand(_room, width, length));
        }

        private void PromptStartPosition()
        {
            Queue<string> captures = PromptAndCaptureResponse(
                "Enter the start position and heading (two integers and one letter [N, S, W, E], separated by space):",
                _startPositionCapturePattern);

            int x = Convert.ToInt32(captures.Dequeue());
            int y = Convert.ToInt32(captures.Dequeue());
            Orientation heading = OrientationHelper.ParseOrientation(captures.Dequeue());

            _commands.Add(new InitializeStartPositionCommand(_room, _car, x, y, heading));
        }

        private void PromptMovementCommands()
        {
            Queue<string> captures = PromptAndCaptureResponse(
                "Enter a collection of commands ([F, B, L, R], not separated):",
                _movementCommandsCapturePattern,
                _movementCommandsValidPattern);

            while(captures.Any())
            {
                string capture = captures.Dequeue();
                ICommand movementCommand = capture.ToUpper() switch
                {
                    "F" => new MoveForwardCommand(_room, _car) as ICommand,
                    "B" => new MoveBackwardCommand(_room, _car) as ICommand,
                    "L" => new TurnLeftCommand(_car) as ICommand,
                    "R" => new TurnRightCommand(_car) as ICommand,
                    _ => throw new InvalidOperationException($"Unexpected movement value {capture}")
                };

                _commands.Add(movementCommand);
            }
        }

        private void CalculateResult()
        {
            _writeOutput("Simulating...");

            try
            {
                foreach (ICommand command in _commands)
                {
                    command.Invoke();
                }
                _writeOutput($"Simulation done.");
                _writeOutput(_car.ToString());
            }
            catch (OutOfBoundsException oobe)
            {
                _writeOutput($"An error occured: {oobe.Message}");
                return;
            }
            finally
            {
                _commands.Clear();
            }
        }

        /// <summary>
        /// Prompts a question to the user, validates the input and captures it according to regex patterns.
        /// </summary>
        /// <param name="question">Question to prompt the user</param>
        /// <param name="capturePattern">Regex pattern with capture groups</param>
        /// <param name="validationPattern">Regex pattern to validate the entire input, if null uses the capturePattern</param>
        /// <returns>Queue of captured values, where each value matches a capture group in the regex pattern.</returns>
        private Queue<string> PromptAndCaptureResponse(string question, Regex capturePattern, Regex validationPattern = null)
        {
            if (validationPattern == null)
            {
                validationPattern = capturePattern;
            }

            Match match;
            string input;
            do
            {
                _writeOutput(question);
                input = _readInput();
                match = validationPattern.Match(input);
                if (!match.Success)
                {
                    _writeOutput("Error: invalid command given");
                }
            } while (!match.Success);

            IEnumerable<string> captures = capturePattern
                .Matches(input)
                .SelectMany(match => match
                    .Groups
                    .Values
                    // Skip the first occurence, since it contains the whole matched string and not the capture groups.
                    .Skip(1) 
                    .Select(g => g.Value));

            return new Queue<string>(captures);
        }
    }
}
