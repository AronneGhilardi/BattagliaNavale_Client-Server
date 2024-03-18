using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient
{
    public class Ship
    {
        public string shipName { get; set; }
        public int remShips { get; set; }
        public List<ShipButtons> shipPerButton { get; set; }
    }
    public class ShipButtons
    {
        public List<string> buttonNames { get; set; }
    }
}
