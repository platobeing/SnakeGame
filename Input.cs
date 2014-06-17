/******************************************************************************
 * Filename:     Input.cs
 * Author:       platobeing<platobeing(c)gmail.com>
 * Created Date: 2014-06-15
 * Description:  This file defines a static class named "Input" which has two
 *               methods: ChangeState and Pressed.
 * ****************************************************************************
 * Winter is coming.
 * ***************************************************************************/

namespace Snake
{
    using System.Collections;
    using System.Windows.Forms;

    public static class Input
    {
        private static Hashtable _keys = new Hashtable();

        /// <summary>
        /// Update the key's state.
        /// </summary>
        /// <param name="key">The key whose state will be changed.</param>
        /// <param name="state">The key's state.</param>
        public static void ChangeState(Keys key, bool state)
        {
            _keys[key] = state;
        }
        
        /// <summary>
        /// Return whether or not "key" is being pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>If the "key" is pressed,it will return true,otherwise return false.</returns>
        public static bool Pressed(Keys key)
        {
            if (_keys[key] == null)
            {
                _keys[key] = false;
            }

            return (bool)_keys[key];
        }
    }
}
