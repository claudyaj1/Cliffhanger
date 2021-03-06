using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Claudy.Input;

namespace Cliffhanger
{
    public class Menu : Microsoft.Xna.Framework.GameComponent
    {
        public readonly Color colorSelectYES = Color.Red,
            colorSelectNO = Color.SlateGray,
            colorTitle = Color.Gold;
        Vector2 playMenuItemPos, helpMenuItempPos, exitMenuItemPos, titleMenuItemPos;
        private ClaudyInput input;

        enum MenuState
        {
            TopMost,
            Help,
            InGame,
            Exit
        }

        /// <summary>
        /// Finite State Machine of the menu.
        /// </summary>
        MenuState currentMenuState;

        enum MenuChoice // THIS ENUMERATION MUST BE IN ORDER.
        {
            Play,
            Help,
            Exit // THIS MUST BE LAST.
        }

        MenuChoice currentlySelectedMenuChoice;

        Texture2D helpScreenTexture;
        Texture2D menuScreenTexture;

        SpriteFont tahoma, consolas;

        public Menu(CliffhangerGame game)
            : base(game)
        {
            currentMenuState = MenuState.TopMost;
            currentlySelectedMenuChoice = MenuChoice.Help;

            playMenuItemPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 6.0f,
                Game.GraphicsDevice.Viewport.Height / 2.0f - Game.GraphicsDevice.Viewport.Height * 0.2f);
            helpMenuItempPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 6.0f,
                Game.GraphicsDevice.Viewport.Height / 2.0f - Game.GraphicsDevice.Viewport.Height * 0.1f);
            exitMenuItemPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 6.0f, 
                Game.GraphicsDevice.Viewport.Height / 2.0f + Game.GraphicsDevice.Viewport.Height * 0.0f);
            titleMenuItemPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - Game.GraphicsDevice.Viewport.Width * 0.1f,
                Game.GraphicsDevice.Viewport.Height * 0.05f);

            helpScreenTexture = Game.Content.Load<Texture2D>("helpScreenTexture");
            menuScreenTexture = Game.Content.Load<Texture2D>("menuScreenTexture");
            tahoma = Game.Content.Load<SpriteFont>("Tahoma");
            consolas = Game.Content.Load<SpriteFont>("consolas");

            input = game.input;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        public void Update(GameTime gameTime, CliffhangerGame game)
        {
            #region Main Menu
            switch (currentMenuState)
            {
                case MenuState.TopMost:
                    for (int pi = 1; pi <= 2; pi++)
                    {
                        if (input.GamepadByID[pi].IsConnected)
                        {
                            if ((input.GamepadByID[pi].DPad.Up == ButtonState.Pressed && input.PreviousGamepadByID[pi].DPad.Up == ButtonState.Released) ||
                                (input.GamepadByID[pi].ThumbSticks.Left.Y > 0.5f && input.PreviousGamepadByID[pi].ThumbSticks.Left.Y <= 0.5f) ||
                                (input.GamepadByID[pi].ThumbSticks.Right.Y > 0.5f && input.PreviousGamepadByID[pi].ThumbSticks.Right.Y <= 0.5f) ||
                                input.isFirstPress(Keys.Up))
                            {
                                if (currentlySelectedMenuChoice != MenuChoice.Play)
                                    currentlySelectedMenuChoice--;
                            }
                        }
                        if (input.GamepadByID[pi].IsConnected)
                        {
                            if (input.GamepadByID[pi].DPad.Down == ButtonState.Pressed && input.PreviousGamepadByID[pi].DPad.Down == ButtonState.Released ||
                                (input.GamepadByID[pi].ThumbSticks.Left.Y < -0.5f && input.PreviousGamepadByID[pi].ThumbSticks.Left.Y >= -0.5f) ||
                                (input.GamepadByID[pi].ThumbSticks.Right.Y < -0.5f && input.PreviousGamepadByID[pi].ThumbSticks.Right.Y >= -0.5f) ||
                                input.isFirstPress(Keys.Down))
                            {
                                if (currentlySelectedMenuChoice != MenuChoice.Exit)
                                    currentlySelectedMenuChoice++;
                            }
                        }
                        if ((input.isFirstPress(Buttons.A, PlayerIndex.One) || input.isFirstPress(Buttons.A, PlayerIndex.Two)))
                        {
                            switch (currentlySelectedMenuChoice)
                            {
                                case MenuChoice.Play:
                                    currentMenuState = MenuState.InGame;
                                    game.currentGameState = CliffhangerGame.LevelStateFSM.Level1;
                                    break;
                                case MenuChoice.Help:
                                    currentMenuState = MenuState.Help;
                                    break;
                                case MenuChoice.Exit:
                                    currentMenuState = MenuState.Exit;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    break;
                case MenuState.Help:
                    for (int pi = 1; pi <= 4; pi++)
                    {
                        if (input.GamepadByID[pi].IsConnected)
                        {
                            if (input.isFirstPress(Buttons.B, pi))
                            {
                                game.currentGameState = CliffhangerGame.LevelStateFSM.AlphaMenu;
                                currentMenuState = MenuState.TopMost;
                                currentlySelectedMenuChoice = MenuChoice.Play;
                            }
                        }
                    }
                    break;
                case MenuState.Exit:
                    game.Exit();
                    break;
                default:
                    break;
            }
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch passed MUST have its .Begin() call first.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Assumes SpriteBatch has begun already.
            switch (currentMenuState)
            {
                case MenuState.TopMost:
                    spriteBatch.Draw(menuScreenTexture, Game.GraphicsDevice.Viewport.Bounds, Color.White);
                    spriteBatch.DrawString(tahoma, "Cliffhanger", titleMenuItemPos, colorTitle);
                    switch (currentlySelectedMenuChoice)
                    {
                        case MenuChoice.Play:
                            spriteBatch.DrawString(tahoma, "Play", playMenuItemPos, colorSelectYES);
                            spriteBatch.DrawString(tahoma, "Help", helpMenuItempPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Exit", exitMenuItemPos, colorSelectNO);
                            break;
                        case MenuChoice.Help:
                            spriteBatch.DrawString(tahoma, "Play", playMenuItemPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Help", helpMenuItempPos, colorSelectYES);
                            spriteBatch.DrawString(tahoma, "Exit", exitMenuItemPos, colorSelectNO);
                            break;
                        case MenuChoice.Exit:
                            spriteBatch.DrawString(tahoma, "Play", playMenuItemPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Help", helpMenuItempPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Exit", exitMenuItemPos, colorSelectYES);
                            break;
                        default:
                            spriteBatch.DrawString(tahoma, "Play", playMenuItemPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Help", helpMenuItempPos, colorSelectNO);
                            spriteBatch.DrawString(tahoma, "Exit", exitMenuItemPos, colorSelectNO);
                            break;
                    }
                    break;
                case MenuState.Help:
                    //TODO: Draw 1920x1080 texture which explains how to play the game.
                    spriteBatch.Draw(helpScreenTexture, Game.GraphicsDevice.Viewport.Bounds, Color.White);
                    break;
                case MenuState.Exit:
                    break;
                default:
                    break;
            }

        }
    }
}
