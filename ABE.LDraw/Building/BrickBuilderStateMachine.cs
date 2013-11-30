using ABE.LDraw.File;

namespace ABE.LDraw.Building
{
    public sealed class BrickBuilderStateMachine
    {
        private IBrickPlacer _brickPlacer;
        private int _posCounter;
        private int _counter;
        private BrickColor? _currentColor;

        public BrickBuilderStateMachine(IBrickPlacer brickPlacer)
        {
            _brickPlacer = brickPlacer;
        }

        private void PlaceBrick()
        {
            if (_counter > 0 && _counter >= MinLength && _currentColor.HasValue)
            {
                _brickPlacer.Place(_posCounter - _counter, _counter, _currentColor.Value);
            }
            _counter = 0;
        }

        public void EndPlacement()
        {
            PlaceBrick();
        }

        public void PushStud(BrickColor? brickColor)
        {
            if (brickColor != _currentColor)
            {
                PlaceBrick();
                _currentColor = brickColor;
            }

            _counter++;
            _posCounter++;
            if (_counter >= MaxLength)
            {
                PlaceBrick();
            }
        }

        public int MaxLength { get; set; }

        public int MinLength { get; set; }
    }
}
