using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalGamepad.GUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        UniversalGamepad.BL.XBoxGamepad gamepad;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            gamepad = new BL.XBoxGamepad();
            gamepad.OnXBoxGamepadButtonPressA += Gamepad_OnXBoxGamepadButtonPressA;
            gamepad.OnXBoxGamepadButtonPressB += Gamepad_OnXBoxGamepadButtonPressB;
            gamepad.OnXBoxGamepadButtonPressX += Gamepad_OnXBoxGamepadButtonPressX;
            gamepad.OnXBoxGamepadButtonPressY += Gamepad_OnXBoxGamepadButtonPressY;

            gamepad.OnXBoxGamepadLeftStickMove += Gamepad_OnXBoxGamepadLeftStickMove;
            gamepad.OnXBoxGamepadRightStickMove += Gamepad_OnXBoxGamepadRightStickMove;

            gamepad.OnXBoxGamepadButtonMenu += Gamepad_OnXBoxGamepadButtonMenu;
            gamepad.OnXBoxGamepadButtonView += Gamepad_OnXBoxGamepadButtonView;
        }

        public async void UpdateOutput(string message)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                lblOutput.Text = String.Format("{0}{1}{2}", message, Environment.NewLine, lblOutput.Text);
            }
            );
        }

        private void Gamepad_OnXBoxGamepadButtonView(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
        }

        private void Gamepad_OnXBoxGamepadButtonMenu(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
        }

        private void Gamepad_OnXBoxGamepadRightStickMove(object sender, List<double> e)
        {
            UpdateOutput(e[0].ToString() + ":" + e[1].ToString());
        }

        private void Gamepad_OnXBoxGamepadLeftStickMove(object sender, List<double> e)
        {
            UpdateOutput(e[0].ToString() + ":" + e[1].ToString());
        }

        private void Gamepad_OnXBoxGamepadButtonPressY(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
        }

        private async void Gamepad_OnXBoxGamepadButtonPressX(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
            await gamepad.Vibrate();
        }

        private void Gamepad_OnXBoxGamepadButtonPressB(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
        }

        private void Gamepad_OnXBoxGamepadButtonPressA(object sender, GamepadButtons e)
        {
            UpdateOutput(e.ToString());
        }
        
    }
}
