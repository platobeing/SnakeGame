/******************************************************************************
 * Filename:     SnakePart.cs
 * Author:       platobeing<platobeing(c)gmail.com>
 * Created Date: 2014-06-15
 * Description:  This file defines a class "SnakePart" which builds a snake.
 * ****************************************************************************
 * Winter is coming.
 * ***************************************************************************/

namespace Snake
{
    public class SnakePart
    {
        /// <summary>
        /// X-coordinate
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y-coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-cooridnate</param>
        public SnakePart(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
