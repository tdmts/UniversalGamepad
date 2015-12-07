using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace UniversalGamepad.BL
{
    public class XBoxGamepad
    {
        private List<Gamepad> _controllers = new List<Gamepad>();
        private bool _running = true;

        Task backgroundWorkTask;

        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressA;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressB;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressX;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressY;

        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressLeft;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressRight;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressUp;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonPressDown;

        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonMenu;
        public event EventHandler<GamepadButtons> OnXBoxGamepadButtonView;

        public event EventHandler<List<Double>> OnXBoxGamepadLeftStickMove;
        public event EventHandler<List<Double>> OnXBoxGamepadRightStickMove;



        public XBoxGamepad()
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            backgroundWorkTask = Task.Run(() => PollGamepad());
        }

        public async Task Vibrate()
        {
            if (_running)
            {
                foreach (Gamepad controller in _controllers)
                {
                    GamepadVibration gv = new GamepadVibration();
                    gv.LeftMotor = 1;
                    gv.RightMotor = 1;
                    controller.Vibration = gv;
                    await Task.Delay(1000);
                    gv.LeftMotor = 0;
                    gv.RightMotor = 0;
                    controller.Vibration = gv;
                }
            }
        }
        
        private void Start()
        {
            _running = true;
        }

        public void Stop()
        {
            _running = false;
        }

        public async Task PollGamepad()
        {
            while (true)
            {
                if (_running)
                {
                    foreach (Gamepad controller in _controllers)
                    {
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.A)
                        {
                            OnXBoxGamepadButtonPressA(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.B)
                        {
                            OnXBoxGamepadButtonPressB(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.X)
                        {
                            OnXBoxGamepadButtonPressX(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.Y)
                        {
                            OnXBoxGamepadButtonPressY(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.DPadLeft)
                        {
                            OnXBoxGamepadButtonPressLeft(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.DPadUp)
                        {
                            OnXBoxGamepadButtonPressUp(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.DPadRight)
                        {
                            OnXBoxGamepadButtonPressRight(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.DPadDown)
                        {
                            OnXBoxGamepadButtonPressDown(controller, controller.GetCurrentReading().Buttons);
                        }

                        if (controller.GetCurrentReading().Buttons == GamepadButtons.Menu)
                        {
                            OnXBoxGamepadButtonMenu(controller, controller.GetCurrentReading().Buttons);
                        }
                        if (controller.GetCurrentReading().Buttons == GamepadButtons.View)
                        {
                            OnXBoxGamepadButtonView(controller, controller.GetCurrentReading().Buttons);
                        }

                        if (controller.GetCurrentReading().LeftThumbstickX >= 0.1
                            || controller.GetCurrentReading().LeftThumbstickX <= -0.1
                            || controller.GetCurrentReading().LeftThumbstickY >= 0.1
                            || controller.GetCurrentReading().LeftThumbstickY <= -0.1)
                        {
                            OnXBoxGamepadLeftStickMove(controller, new List<double>() { controller.GetCurrentReading().LeftThumbstickX, controller.GetCurrentReading().LeftThumbstickY });
                        }
                        if (controller.GetCurrentReading().RightThumbstickX >= 0.1 
                            || controller.GetCurrentReading().RightThumbstickX <= -0.1
                            || controller.GetCurrentReading().RightThumbstickY >= 0.1
                            || controller.GetCurrentReading().RightThumbstickY <= -0.1)
                        {
                            OnXBoxGamepadRightStickMove(controller, new List<double>() { controller.GetCurrentReading().RightThumbstickX, controller.GetCurrentReading().RightThumbstickY });
                        }
                    }
                }
                await Task.Delay(50);
            }
        }

        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            _controllers.Remove(e);
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            _controllers.Add(e);
        }
    }
}
