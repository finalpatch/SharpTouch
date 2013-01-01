using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpTouch
{
    public enum GestureAction
    {
        AeroPeek,
        Flip2D,
        Flip3D,
        MinimizeOthers,
        ShowDesktop,
        DockLeft,
        DockRight,
        Maximize,
        Minimize,
    };

    public class Gesture
    {
        public GestureAction ActionCode { get; private set; }
        public string Descroption {get; private set; }
        public Keys[] KeySeq { get; private set; }

        Gesture(GestureAction act, string desc, Keys[] keys)
        {
            ActionCode = act;
            Descroption = desc;
            KeySeq = keys;
        }

        public override string ToString()
        {
            return Descroption;
        }

        public static readonly Gesture[] AllGestures = new Gesture[]{
            new Gesture(GestureAction.AeroPeek, "Aero Peek", new Keys[] { Keys.LWin, Keys.Space }),
            new Gesture(GestureAction.Flip2D, "Flip2D", new Keys[] { Keys.ControlKey, Keys.Menu, Keys.Tab }),
            new Gesture(GestureAction.Flip3D, "Flip3D", new Keys[] { Keys.LWin, Keys.ControlKey, Keys.Tab }),
            new Gesture(GestureAction.MinimizeOthers, "Minimize Other Windows", new Keys[] { Keys.LWin, Keys.Home }),
            new Gesture(GestureAction.ShowDesktop, "Show Desktop", new Keys[] { Keys.LWin, Keys.D }),
            new Gesture(GestureAction.DockLeft, "Dock Window to Left", new Keys[] { Keys.LWin, Keys.Left }),
            new Gesture(GestureAction.DockRight, "Dock Window to Right", new Keys[] { Keys.LWin, Keys.Right }),
            new Gesture(GestureAction.Maximize, "Maximize Window", new Keys[] { Keys.LWin, Keys.Up }),
            new Gesture(GestureAction.Minimize, "Minimize Window", new Keys[] { Keys.LWin, Keys.Down })
        };
    }
}
