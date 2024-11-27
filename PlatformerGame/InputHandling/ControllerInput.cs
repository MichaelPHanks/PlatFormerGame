using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.InputHandling
{
    public class ControllerInput : IInputDevice
    {
        /// <summary>
        /// Registers a callback-based command
        /// </summary>
        public void registerCommand(Buttons button, bool buttonPressOnly, IInputDevice.CommandDelegate callback)
        {
            //
            // If already registered, remove it!
            if (m_commandEntries.ContainsKey(button))
            {
                m_commandEntries.Remove(button);
            }
            m_commandEntries.Add(button, new ButtonCommandEntry(button, buttonPressOnly, callback));
        }
        

        public void registerCommand(GamePadThumbSticks stick, bool singleStickOnly, IInputDevice.CommandDelegate callback)
        {
            if (m_stickCommands.ContainsKey(stick))
            {
                m_stickCommands.Remove(stick);
            }
            m_stickCommands.Add(stick, new StickCommandEntry(stick, singleStickOnly, callback));
        }
        /// <summary>
        /// Track all registered commands in this dictionary
        /// </summary>
        private Dictionary<Buttons, ButtonCommandEntry> m_commandEntries = new Dictionary<Buttons, ButtonCommandEntry>();

        private Dictionary<GamePadThumbSticks, StickCommandEntry> m_stickCommands = new Dictionary<GamePadThumbSticks, StickCommandEntry>();

        /// <summary>
        /// Used to keep track of the details associated with a command
        /// </summary>
        private struct ButtonCommandEntry
        {
            public ButtonCommandEntry(Buttons button, bool buttonPressOnly, IInputDevice.CommandDelegate callback)
            {
                this.button = button;
                this.buttonPressOnly = buttonPressOnly;
                this.callback = callback;
            }

            public Buttons button;
            public bool buttonPressOnly;
            public IInputDevice.CommandDelegate callback;
        }


        private struct StickCommandEntry
        {
            public StickCommandEntry(GamePadThumbSticks button, bool singleStickOnly, IInputDevice.CommandDelegate callback)
            {
                this.button = button;
                this.singleStickOnly = singleStickOnly;
                this.callback = callback;
            }

            public GamePadThumbSticks button;
            public bool singleStickOnly;
            public IInputDevice.CommandDelegate callback;
        }
        /// <summary>
        /// Goes through all the registered commands and invokes the callbacks if they
        /// are active.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            foreach (ButtonCommandEntry entry in this.m_commandEntries.Values)
            {
                if (entry.buttonPressOnly && buttonPressed(entry.button))
                {
                    entry.callback(gameTime);
                }
                else if (!entry.buttonPressOnly && state.IsButtonDown(entry.button))
                {
                    entry.callback(gameTime);
                }
            }

            foreach (StickCommandEntry entry in this.m_stickCommands.Values)
            {
                if (entry.singleStickOnly && stickMoved(entry.button))
                {
                    entry.callback(gameTime);
                }
                else if (!entry.singleStickOnly && state.ThumbSticks.Left.Y >= 0.25)
                {
                    entry.callback(gameTime);
                }
            }


            //
            // Move the current state to the previous state for the next time around
            m_statePrevious = state;


        }

        private GamePadState m_statePrevious;

        private bool stickMoved(GamePadThumbSticks stick)
        {
            return GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y >= 0.25 && !(m_statePrevious.ThumbSticks.Left.Y >= 0.25);
        }
        /// <summary>
        /// Checks to see if a key was newly pressed
        /// </summary>
        private bool buttonPressed(Buttons button)
        {
            return GamePad.GetState(PlayerIndex.One).IsButtonDown(button) && !m_statePrevious.IsButtonDown(button);
        }
    }
}
