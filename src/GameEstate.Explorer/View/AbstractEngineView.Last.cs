//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Windows;

//namespace GameEstate.Explorer.View
//{
//    // https://www.reddit.com/r/gamedev/comments/eej7zq/tutorial_dotnet_core_30_31_with_monogame/
//    public class AbstractEngineView : WpfGame
//    {
//        public static AbstractEngineView Instance;
//        public static bool UseMSAA = true;
//        WpfGraphicsDeviceService _graphicsDeviceManager;
//        protected SpriteBatch SpriteBatch;
//        WpfKeyboard _keyboard;
//        WpfMouse _mouse;
//        protected MouseState MouseState;
//        protected KeyboardState KeyboardState;
//        protected KeyboardState PrevKeyboardState;
//        protected DateTime LastResizeEvent;

//        //public new Render.Render Render;
//        //public static Camera Camera;

//        protected override void Initialize()
//        {
//            _graphicsDeviceManager = new WpfGraphicsDeviceService(this)
//            {
//                PreferMultiSampling = UseMSAA
//            };
//            SpriteBatch = new SpriteBatch(GraphicsDevice);
//            _keyboard = new WpfKeyboard(this);
//            _mouse = new WpfMouse(this);
//            Instance = this;
//            // must be called after the WpfGraphicsDeviceService instance was created
//            base.Initialize();
//            SizeChanged += new SizeChangedEventHandler(GameView_SizeChanged);
//        }

//        protected override void Update(GameTime time)
//        {
//            // every update we can now query the keyboard & mouse for our WpfGame
//            MouseState = _mouse.GetState();
//            KeyboardState = _keyboard.GetState();

//            if (KeyboardState.IsKeyDown(Keys.C) && !PrevKeyboardState.IsKeyDown(Keys.C))
//                CancelRequested();

//            if (!_graphicsDeviceManager.PreferMultiSampling && UseMSAA && DateTime.Now - LastResizeEvent >= TimeSpan.FromSeconds(1))
//            {
//                _graphicsDeviceManager.PreferMultiSampling = true;
//                _graphicsDeviceManager.ApplyChanges();
//            }

//            PrevKeyboardState = KeyboardState;
//        }

//        protected override void Draw(GameTime time) => GraphicsDevice.Clear(new Color(0, 0, 0));

//        protected virtual void CancelRequested() { }

//        void GameView_SizeChanged(object sender, SizeChangedEventArgs e)
//        {
//            if (_graphicsDeviceManager.PreferMultiSampling)
//            {
//                _graphicsDeviceManager.PreferMultiSampling = false;
//                _graphicsDeviceManager.ApplyChanges();
//                LastResizeEvent = DateTime.Now;
//            }
//        }
//    }
//}
