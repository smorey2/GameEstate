using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WpfTest.Components
{
    public class FpsComponent : WpfDrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        int _frames;
        int _liveFrames;
        TimeSpan _timeElapsed;

        public FpsComponent(WpfGame game) : base(game) { }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("DefaultFont");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            _timeElapsed += gameTime.ElapsedGameTime;
            if (_timeElapsed >= TimeSpan.FromSeconds(1))
            {
                _timeElapsed -= TimeSpan.FromSeconds(1);
                _frames = _liveFrames;
                _liveFrames = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _liveFrames++;
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, $"FPS: {_frames}", new Vector2(5), Color.White);
            _spriteBatch.End();
        }
    }
}