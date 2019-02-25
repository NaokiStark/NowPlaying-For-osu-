using Fabi.EzFramework.Framework.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Animations
{
    public class AnimationObject : UIObjectBase
    {
        Dictionary<AnimationAction, KeyValuePair<AnimationType, Action>> animationEvents = new Dictionary<AnimationAction, KeyValuePair<AnimationType, Action>>();

        Dictionary<AnimationAction, KeyValuePair<AnimationType, int>> animationElapsed = new Dictionary<AnimationAction, KeyValuePair<AnimationType, int>>();

        Dictionary<AnimationAction, KeyValuePair<AnimationType, int>> animationDuration = new Dictionary<AnimationAction, KeyValuePair<AnimationType, int>>();


        Vector2 initialPosition, toPosition = Vector2.Zero;

        /// <summary>
        /// FadeIn Transition
        /// </summary>
        /// <param name="animationType">Animation type</param>
        /// <param name="duration">Animation duration in milliseconds</param>
        /// <param name="end">Function is invoked when animation finishes</param>
        public void FadeIn(AnimationType animationType, int duration, Action end = null)
        {
            _addOrUpdate(animationElapsed, AnimationAction.FadeIn, new KeyValuePair<AnimationType, int>(animationType, 0));
            _addOrUpdate(animationEvents, AnimationAction.FadeIn, new KeyValuePair<AnimationType, Action>(animationType, end));
            _addOrUpdate(animationDuration, AnimationAction.FadeIn, new KeyValuePair<AnimationType, int>(animationType, duration));
        }

        /// <summary>
        /// FadeOut Transition
        /// </summary>
        /// <param name="animationType">Animation type</param>
        /// <param name="duration">Animation duration in milliseconds</param>
        /// <param name="end">Function is invoked when animation finishes</param>
        public void FadeOut(AnimationType animationType, int duration, Action end = null)
        {
            _addOrUpdate(animationElapsed, AnimationAction.FadeOut, new KeyValuePair<AnimationType, int>(animationType, 0));
            _addOrUpdate(animationEvents, AnimationAction.FadeOut, new KeyValuePair<AnimationType, Action>(animationType, end));
            _addOrUpdate(animationDuration, AnimationAction.FadeOut, new KeyValuePair<AnimationType, int>(animationType, duration));
        }

        /// <summary>
        /// Movement Transition
        /// </summary>
        /// <param name="animationType">Animation type</param>
        /// <param name="_toPosition">Start position</param>
        /// <param name="duration">Animation duration in milliseconds</param>
        /// <param name="end">Function is invoked when animation finishes</param>
        public void MoveTo(AnimationType animationType, Vector2 _toPosition, int duration, Action end = null)
        {
            initialPosition = Position;
            toPosition = _toPosition;
            _addOrUpdate(animationElapsed, AnimationAction.MoveTo, new KeyValuePair<AnimationType, int>(animationType, 0));
            _addOrUpdate(animationEvents, AnimationAction.MoveTo, new KeyValuePair<AnimationType, Action>(animationType, end));
            _addOrUpdate(animationDuration, AnimationAction.MoveTo, new KeyValuePair<AnimationType, int>(animationType, duration));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateAnimations(gameTime);
        }

        private void _addOrUpdate(Dictionary<AnimationAction, KeyValuePair<AnimationType, Action>> dic, AnimationAction key, KeyValuePair<AnimationType, Action> value)
        {
            KeyValuePair<AnimationType, Action> val;
            if (dic.TryGetValue(key, out val))
            {
                // yay, value exists!
                dic[key] = value;
            }
            else
            {
                // darn, lets add the value
                dic.Add(key, value);
            }
        }

        private void _addOrUpdate(Dictionary<AnimationAction, KeyValuePair<AnimationType, int>> dic, AnimationAction key, KeyValuePair<AnimationType, int> value)
        {
            KeyValuePair<AnimationType, int> val;
            if (dic.TryGetValue(key, out val))
            {
                // yay, value exists!
                dic[key] = value;
            }
            else
            {
                // darn, lets add the value
                dic.Add(key, value);
            }
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            for (int a = 0; a < animationElapsed.Count; a++)
            {
                // Get animationType

                AnimationType aniType = animationElapsed.ElementAt(a).Value.Key;

                // Get Elapsed time
                int aniElapsed = animationElapsed.ElementAt(a).Value.Value;

                // Set elapsed to Dictionary
                animationElapsed[animationElapsed.ElementAt(a).Key]
                    = new KeyValuePair<AnimationType, int>(aniType, aniElapsed + gameTime.ElapsedGameTime.Milliseconds);

                // Make animation
                issueAnimation(animationElapsed.ElementAt(a));
            }
        }

        private void issueAnimation(KeyValuePair<AnimationAction, KeyValuePair<AnimationType, int>> animation)
        {
            AnimationAction animationAction = animation.Key;
            AnimationType animationType = animation.Value.Key;
            int elapsed = animation.Value.Value;

            switch (animationAction)
            {
                case AnimationAction.FadeIn:
                    __makeFadeIn(animationType, elapsed);
                    break;
                case AnimationAction.FadeOut:
                    __makeFadeOut(animationType, elapsed);
                    break;
                case AnimationAction.MoveTo:
                    __makeMove(animationType, elapsed);
                    break;
            }
        }

        private void __makeFadeIn(AnimationType type, int elapsed)
        {
            switch (type)
            {
                case AnimationType.Linear:
                    Opacity = linearIn(elapsed, animationDuration[AnimationAction.FadeIn].Value);
                    break;
                case AnimationType.Ease:
                    Opacity = bezierBlend(linearIn(elapsed, animationDuration[AnimationAction.FadeIn].Value));
                    break;
                case AnimationType.Bounce:

                    float tIn = linearIn(elapsed, animationDuration[AnimationAction.FadeIn].Value);
                    Opacity = bounceIn(tIn);
                    break;
            }

            if (Opacity == 1f)
            {
                animationEvents[AnimationAction.FadeIn].Value?.Invoke();
                animationEvents.Remove(AnimationAction.FadeIn);
                animationElapsed.Remove(AnimationAction.FadeIn);
                animationDuration.Remove(AnimationAction.FadeIn);
            }
        }

        private void __makeFadeOut(AnimationType type, int elapsed)
        {

            switch (type)
            {
                case AnimationType.Linear:
                    Opacity = Math.Max(linearOut(elapsed, animationDuration[AnimationAction.FadeOut].Value), 0f);
                    break;
                case AnimationType.Ease:
                    float bBlnd = bezierBlend(linearOut(elapsed, animationDuration[AnimationAction.FadeOut].Value));
                    Opacity = Math.Max(bBlnd, 0f);
                    break;
                case AnimationType.Bounce:
                    float tOut = Math.Max(linearOut(elapsed, animationDuration[AnimationAction.FadeOut].Value), 0f);
                    Opacity = bounceOut(tOut);
                    break;
            }

            if (Opacity == 0f)
            {
                animationEvents[AnimationAction.FadeOut].Value?.Invoke();
                animationEvents.Remove(AnimationAction.FadeOut);
                animationElapsed.Remove(AnimationAction.FadeOut);
                animationDuration.Remove(AnimationAction.FadeOut);
            }
        }

        private void __makeMove(AnimationType type, int elapsed)
        {
            float initialPosX = initialPosition.X;
            float initialPosY = initialPosition.Y;
            float destPosX = toPosition.X;
            float destPosY = toPosition.Y;

            int duration = animationDuration[AnimationAction.MoveTo].Value;

            float scalePosX = (initialPosX > destPosX) ? linearOut(elapsed, duration) : linearIn(elapsed, duration);
            float scalePosY = (initialPosY > destPosY) ? linearOut(elapsed, duration) : linearIn(elapsed, duration);

            switch (type)
            {
                case AnimationType.Ease:
                    scalePosX = bezierBlend(scalePosX);
                    scalePosY = bezierBlend(scalePosY);
                    break;
                case AnimationType.Bounce:
                    scalePosX = bounceIn(scalePosX);
                    scalePosY = bounceIn(scalePosY);
                    break;
            }

            float actualX = (initialPosX - destPosX) * scalePosX;
            float actualY = (initialPosY - destPosY) * scalePosY;


            if (initialPosX < destPosX)
            {
                actualX = (destPosX - initialPosX) * scalePosX + initialPosX;
            }
            else if (initialPosX == destPosX)
            {
                actualX = initialPosX;
            }
            else
            {
                actualX = (destPosX + initialPosX) * scalePosX + initialPosX;
            }

            if (initialPosY < destPosY)
            {
                actualY = (destPosY - initialPosY) * scalePosY + initialPosY;
            }
            else if (initialPosY == destPosY)
            {
                actualY = initialPosY;
            }
            else
            {
                actualY = (destPosY + initialPosY) * scalePosY + initialPosY;
            }

            Position = new Vector2(actualX, actualY);
            if (elapsed >= duration)
            {
                animationEvents[AnimationAction.MoveTo].Value?.Invoke();
                animationEvents.Remove(AnimationAction.MoveTo);
                animationElapsed.Remove(AnimationAction.MoveTo);
                animationDuration.Remove(AnimationAction.MoveTo);
            }
        }

        private float linearIn(float time, float duration)
        {
            return Math.Min(time / (duration / 100f) / 100f, 1f);
        }

        private float linearOut(int time, int duration)
        {
            return Math.Max((duration - time) / (duration / 100f) / 100f, 0f);
        }

        private float bezierBlend(float t)
        {
            return (float)Math.Pow(t, 2f) * (3.0f - 2.0f * t);
        }

        private float bounceIn(float t)
        {
            return 1 - bounceOut(1 - t);
        }

        private float bounceOut(float t)
        {
            return (t = +t) < b1 ? b0 * t * t : t < b3 ? b0 * (t -= b2) * t + b4 : t < b6 ? b0 * (t -= b5) * t + b7 : b0 * (t -= b8) * t + b9;
        }

        float b1 = 4f / 11f,
            b2 = 6f / 11f,
            b3 = 8f / 11f,
            b4 = 3f / 4f,
            b5 = 9 / 11f,
            b6 = 10f / 11f,
            b7 = 15f / 16f,
            b8 = 21f / 22f,
            b9 = 63f / 64f;

        float b0 = 1f / (4f / 11f) / (4f / 11f);

        public AnimationObject(string Name) : base(Name)
        {
        }
    }
}
