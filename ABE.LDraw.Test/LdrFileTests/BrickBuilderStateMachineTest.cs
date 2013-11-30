using ABE.LDraw.Building;
using ABE.LDraw.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public sealed class BrickBuilderStateMachineTest
    {
        private BrickBuilderStateMachine _stateMachine;
        private Mock<IBrickPlacer> _mockPlacer;

        [TestInitialize]
        public void Setup()
        {
            _mockPlacer = new Mock<IBrickPlacer>();
            _mockPlacer.Setup(e => e.Place(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BrickColor>())).Returns(true);
            _stateMachine = new BrickBuilderStateMachine(_mockPlacer.Object) { MaxLength = 2 };
        }

        private void VerifyBrick(int pos, int len, BrickColor color)
        {
            _mockPlacer.Verify(p => p.Place(It.Is<int>(i => i == pos), It.Is<int>(i => i == len), It.Is<BrickColor>(bc => bc == color)), Times.Once);
        }

        private void VerifyNever()
        {
            _mockPlacer.Verify(p => p.Place(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BrickColor>()), Times.Never);
        }

        [TestMethod]
        public void Initial_state()
        {
            VerifyNever();
        }

        [TestMethod]
        public void PushStud()
        {
            _stateMachine.PushStud(BrickColor.Black);
            VerifyNever();
        }

        [TestMethod]
        public void End_placement()
        {
            _stateMachine.EndPlacement();
            VerifyNever();
        }

        [TestMethod]
        public void PushStud_and_end_placement()
        {
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.EndPlacement();
            VerifyBrick(0, 1, BrickColor.Black);
        }

        [TestMethod]
        public void PushStud_and_end_placement_minimum()
        {
            _stateMachine.MinLength = 2;
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.EndPlacement();
            VerifyNever();
        }

        [TestMethod]
        public void Push_two_and_verify()
        {
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            VerifyBrick(0, 2, BrickColor.Black);
        }

        [TestMethod]
        public void Push_four_and_verify()
        {
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            VerifyBrick(0, 2, BrickColor.Black);
            VerifyBrick(2, 2, BrickColor.Black);
        }

        [TestMethod]
        public void Push_four_after_gap_and_verify()
        {
            _stateMachine.PushStud(null);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            VerifyBrick(1, 2, BrickColor.Black);
            VerifyBrick(3, 2, BrickColor.Black);
        }

        [TestMethod]
        public void Push_four_max_4_after_gap_and_verify()
        {
            _stateMachine.MaxLength = 4;
            _stateMachine.PushStud(null);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            VerifyBrick(1, 4, BrickColor.Black);
        }

        [TestMethod]
        public void Push_four_colours_max_4_after_gap_and_verify()
        {
            _stateMachine.MaxLength = 4;
            _stateMachine.PushStud(null);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Red);
            _stateMachine.PushStud(BrickColor.Red);
            _stateMachine.PushStud(BrickColor.Red);
            _stateMachine.EndPlacement();
            VerifyBrick(1, 2, BrickColor.Black);
            VerifyBrick(3, 3, BrickColor.Red);
        }

        [TestMethod]
        public void Push_four_with_gap_and_verify()
        {
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(null);
            _stateMachine.PushStud(BrickColor.Black);
            _stateMachine.PushStud(BrickColor.Black);
            VerifyBrick(0, 2, BrickColor.Black);
            VerifyBrick(3, 2, BrickColor.Black);
        }

        [TestMethod]
        public void Push_a_bunch_of_nulls()
        {
            for (int i = 0; i < 4; i++)
            {
                _stateMachine.PushStud(null);
            }
            _stateMachine.EndPlacement();
            VerifyNever();
        }

    }
}
