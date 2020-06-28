using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Numerics;

namespace GameEstate.Toy.Renderer
{
    public class Camera
    {
        const float CAMERASPEED = 300f; // Per second
        const float FOV = OpenTK.MathHelper.PiOver4;

        public Vector3 Location { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public float Scale { get; private set; } = 1.0f;

        Matrix4x4 ProjectionMatrix;
        public Matrix4x4 CameraViewMatrix { get; private set; }
        public Matrix4x4 ViewProjectionMatrix { get; private set; }
        public Frustum ViewFrustum { get; } = new Frustum();

        // Set from outside this class by forms code
        public bool MouseOverRenderArea { get; set; }

        Vector2 WindowSize;
        float AspectRatio;

        bool MouseDragging;

        Vector2 MouseDelta;
        Vector2 MousePreviousPosition;

        KeyboardState KeyboardState;

        public Camera()
        {
            Location = new Vector3(1);
            LookAt(new Vector3(0));
        }

        void RecalculateMatrices()
        {
            CameraViewMatrix = Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateLookAt(Location, Location + GetForwardVector(), Vector3.UnitZ);
            ViewProjectionMatrix = CameraViewMatrix * ProjectionMatrix;
            ViewFrustum.Update(ViewProjectionMatrix);
        }

        // Calculate forward vector from pitch and yaw
        Vector3 GetForwardVector() =>
           new Vector3((float)(Math.Cos(Yaw) * Math.Cos(Pitch)), (float)(Math.Sin(Yaw) * Math.Cos(Pitch)), (float)Math.Sin(Pitch));

        Vector3 GetRightVector() =>
            new Vector3((float)Math.Cos(Yaw - OpenTK.MathHelper.PiOver2), (float)Math.Sin(Yaw - OpenTK.MathHelper.PiOver2), 0);

        public void SetViewportSize(int viewportWidth, int viewportHeight)
        {
            // Store window size and aspect ratio
            AspectRatio = viewportWidth / (float)viewportHeight;
            WindowSize = new Vector2(viewportWidth, viewportHeight);

            // Calculate projection matrix
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FOV, AspectRatio, 1.0f, 40000.0f);

            RecalculateMatrices();

            // setup viewport
            GL.Viewport(0, 0, viewportWidth, viewportHeight);
        }

        public void CopyFrom(Camera fromOther)
        {
            AspectRatio = fromOther.AspectRatio;
            WindowSize = fromOther.WindowSize;
            Location = fromOther.Location;
            Pitch = fromOther.Pitch;
            Yaw = fromOther.Yaw;
            ProjectionMatrix = fromOther.ProjectionMatrix;
            CameraViewMatrix = fromOther.CameraViewMatrix;
            ViewProjectionMatrix = fromOther.ViewProjectionMatrix;
            ViewFrustum.Update(ViewProjectionMatrix);
        }

        public void SetLocation(Vector3 location)
        {
            Location = location;
            RecalculateMatrices();
        }

        public void SetLocationPitchYaw(Vector3 location, float pitch, float yaw)
        {
            Location = location;
            Pitch = pitch;
            Yaw = yaw;
            RecalculateMatrices();
        }

        public void LookAt(Vector3 target)
        {
            var dir = Vector3.Normalize(target - Location);
            Yaw = (float)Math.Atan2(dir.Y, dir.X);
            Pitch = (float)Math.Asin(dir.Z);

            ClampRotation();
            RecalculateMatrices();
        }

        public void SetFromTransformMatrix(Matrix4x4 matrix)
        {
            Location = matrix.Translation;

            // Extract view direction from view matrix and use it to calculate pitch and yaw
            var dir = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Yaw = (float)Math.Atan2(dir.Y, dir.X);
            Pitch = (float)Math.Asin(dir.Z);

            RecalculateMatrices();
        }

        public void SetScale(float scale)
        {
            Scale = scale;
            RecalculateMatrices();
        }

        public void Tick(float deltaTime)
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

        // Prevent camera from going upside-down
        void ClampRotation()
        {
            if (Pitch >= OpenTK.MathHelper.PiOver2) Pitch = OpenTK.MathHelper.PiOver2 - 0.001f;
            else if (Pitch <= -OpenTK.MathHelper.PiOver2) Pitch = -OpenTK.MathHelper.PiOver2 + 0.001f;
        }
    }
}
