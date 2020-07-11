using OpenTK.Input;
using System;
using System.Numerics;

namespace GameEstate.Graphics
{
    public class DebugCamera : Camera
    {
        public bool MouseOverRenderArea { get; set; } // Set from outside this class by forms code

        bool MouseDragging;

        Vector2 MouseDelta;
        Vector2 MousePreviousPosition;

        KeyboardState KeyboardState;

        public override void Tick(float deltaTime)
        {
            if (!MouseOverRenderArea)
                return;

            // Use the keyboard state to update position
            HandleKeyboardInput(deltaTime);

            // Full width of the screen is a 1 PI (180deg)
            Yaw -= (float)Math.PI * MouseDelta.X / WindowSize.X;
            Pitch -= ((float)Math.PI / AspectRatio) * MouseDelta.Y / WindowSize.Y;

            ClampRotation();

            RecalculateMatrices();
        }

        public void HandleInput(MouseState mouseState, KeyboardState keyboardState)
        {
            KeyboardState = keyboardState;

            if (MouseOverRenderArea && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!MouseDragging)
                {
                    MouseDragging = true;
                    MousePreviousPosition = new Vector2(mouseState.X, mouseState.Y);
                }

                var mouseNewCoords = new Vector2(mouseState.X, mouseState.Y);

                MouseDelta.X = mouseNewCoords.X - MousePreviousPosition.X;
                MouseDelta.Y = mouseNewCoords.Y - MousePreviousPosition.Y;

                MousePreviousPosition = mouseNewCoords;
            }

            if (!MouseOverRenderArea || mouseState.LeftButton == ButtonState.Released)
            {
                MouseDragging = false;
                MouseDelta = default;
            }
        }

        void HandleKeyboardInput(float deltaTime)
        {
            var speed = CAMERASPEED * deltaTime;

            // Double speed if shift is pressed
            if (KeyboardState.IsKeyDown(Key.ShiftLeft)) speed *= 2;
            else if (KeyboardState.IsKeyDown(Key.F)) speed *= 10;

            if (KeyboardState.IsKeyDown(Key.W)) Location += GetForwardVector() * speed;
            if (KeyboardState.IsKeyDown(Key.S)) Location -= GetForwardVector() * speed;
            if (KeyboardState.IsKeyDown(Key.D)) Location += GetRightVector() * speed;
            if (KeyboardState.IsKeyDown(Key.A)) Location -= GetRightVector() * speed;
            if (KeyboardState.IsKeyDown(Key.Z)) Location += new Vector3(0, 0, -speed);
            if (KeyboardState.IsKeyDown(Key.Q)) Location += new Vector3(0, 0, speed);
        }
    }
}
