using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    public static class Input
    {
        private static Hashtable _keys = new Hashtable();

        public static void ChangeState(Keys key, bool state)
        {
            _keys[key] = state;
        }
        
        /// <summary>
        /// Returns whether or not "key" is being pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Press(Keys key)
        {
            if (_keys[key] == null)
            {
                _keys[key] = false;
            }

            return (bool)_keys[key];
        }
    }
}
