using System;
using System.Xml.Serialization;

namespace nature_net.user_controls
{
    public class KeyAssignments
    {
        private KeyAssignment[][] _theKeyAssignmentArray;

        [XmlArrayItem("ListItem", typeof(KeyAssignment))]
        [XmlArray("Assignments")]
        public KeyAssignment[][] Assignments
        {
            get
            {
                if (_theKeyAssignmentArray == null)
                {
                    _theKeyAssignmentArray = new KeyAssignment[5][] {
                        new KeyAssignment[14] {VK_OEM_3, VK_1, VK_2, VK_3, VK_4, VK_5, VK_6, VK_7, VK_8, VK_9, VK_0, VK_OEM_MINUS, VK_OEM_PLUS, VK_BS},
                        new KeyAssignment[14] {VK_HT, VK_Q, VK_W, VK_E, VK_R, VK_T, VK_Y, VK_U, VK_I, VK_O, VK_P, VK_OEM_4, VK_OEM_6, VK_OEM_5},
                        new KeyAssignment[14] {VK_CAPS, VK_A, VK_S, VK_D, VK_F, VK_G, VK_H, VK_J, VK_K, VK_L, VK_OEM_1, VK_OEM_7, VK_LF, null},
                        new KeyAssignment[14] {VK_SHIFT, VK_Z, VK_X, VK_C, VK_V, VK_B, VK_N, VK_M, VK_OEM_COMMA, VK_OEM_PERIOD, VK_OEM_2, VK_SHIFT, null, null},
                    new KeyAssignment[14] {VK_CTRL, null, VK_ALT, null, VK_Space, null, VK_ALT, null, VK_CTRL, null, null, null, null, null}};
                }
                return _theKeyAssignmentArray;
            }
        }

        #region 0:  VK_OEM_3
        /// <summary>
        /// Get the KeyAssignment for VK_OEM_3 (0: Grace Accent).
        /// </summary>
        public virtual KeyAssignment VK_OEM_3
        {
            get
            {
                if (_VK_OEM_3 == null)
                {
                    _VK_OEM_3 = new KeyAssignment(0x0060, 0x007E, "Grace Accent", "Tilde", false, true);
                }
                return _VK_OEM_3;
            }
        }
        protected KeyAssignment _VK_OEM_3;
        #endregion

        #region 1:  VK_1
        public virtual KeyAssignment VK_1
        {
            get
            {
                if (_VK_1 == null)
                {
                    _VK_1 = new KeyAssignment(0x0031, 0x0021, "digit ONE", "Exclamation Mark", false, true);
                }
                return _VK_1;
            }
        }
        protected KeyAssignment _VK_1;
        #endregion

        #region 2:  VK_2
        public virtual KeyAssignment VK_2
        {
            get
            {
                if (_VK_2 == null)
                {
                    _VK_2 = new KeyAssignment(0x0032, 0x0040, "digit TWO", "At sign", false, true);
                }
                return _VK_2;
            }
        }
        protected KeyAssignment _VK_2;
        #endregion

        #region 3:  VK_3
        public virtual KeyAssignment VK_3
        {
            get
            {
                if (_VK_3 == null)
                {
                    _VK_3 = new KeyAssignment(0x0033, 0x0023, "digit THREE", "Number sign", false, true);
                }
                return _VK_3;
            }
        }
        protected KeyAssignment _VK_3;
        #endregion

        #region 4:  VK_4
        public virtual KeyAssignment VK_4
        {
            get
            {
                if (_VK_4 == null)
                {
                    _VK_4 = new KeyAssignment(0x0034, 0x0024, "digit FOUR", "Dollar sign", false, true);
                }
                return _VK_4;
            }
        }
        protected KeyAssignment _VK_4;
        #endregion

        #region 5:  VK_5
        public virtual KeyAssignment VK_5
        {
            get
            {
                if (_VK_5 == null)
                {
                    _VK_5 = new KeyAssignment(0x0035, 0x0025, "digit FIVE", "Percent sign", false, true);
                }
                return _VK_5;
            }
        }
        protected KeyAssignment _VK_5;
        #endregion

        #region 6:  VK_6
        public virtual KeyAssignment VK_6
        {
            get
            {
                if (_VK_6 == null)
                {
                    _VK_6 = new KeyAssignment(0x0036, 0x005E, "digit SIX", "Circumflex accent", false, true);
                }
                return _VK_6;
            }
        }
        protected KeyAssignment _VK_6;
        #endregion

        #region 7:  VK_7
        public virtual KeyAssignment VK_7
        {
            get
            {
                if (_VK_7 == null)
                {
                    _VK_7 = new KeyAssignment(0x0037, 0x0026, "digit SEVEN", "Ampersand", false, true);
                }
                return _VK_7;
            }
        }
        protected KeyAssignment _VK_7;
        #endregion

        #region 8:  VK_8
        public virtual KeyAssignment VK_8
        {
            get
            {
                if (_VK_8 == null)
                {
                    _VK_8 = new KeyAssignment(0x0038, 0x002A, "digit EIGHT", "Asterisk", false, true);
                }
                return _VK_8;
            }
        }
        protected KeyAssignment _VK_8;
        #endregion

        #region 9:  VK_9
        public virtual KeyAssignment VK_9
        {
            get
            {
                if (_VK_9 == null)
                {
                    _VK_9 = new KeyAssignment(0x0039, 0x0028, "digit NINE", "Left Parenthesis", false, true);
                }
                return _VK_9;
            }
        }
        protected KeyAssignment _VK_9;
        #endregion

        #region 10: VK_0
        public virtual KeyAssignment VK_0
        {
            get
            {
                if (_VK_0 == null)
                {
                    _VK_0 = new KeyAssignment(0x0030, 0x0029, "digit ZERO", "Right Parenthesis", false, true);
                }
                return _VK_0;
            }
        }
        protected KeyAssignment _VK_0;
        #endregion

        #region 11: VK_OEM_MINUS
        /// <summary>
        /// 11  Hyphen
        /// </summary>
        public virtual KeyAssignment VK_OEM_MINUS
        {
            get
            {
                if (_VK_OEM_MINUS == null)
                {
                    _VK_OEM_MINUS = new KeyAssignment(0x002D, 0x005F, "Hyphen,Minus", "Underscore", false, true);
                }
                return _VK_OEM_MINUS;
            }
        }
        protected KeyAssignment _VK_OEM_MINUS;
        #endregion

        #region 12: VK_OEM_PLUS
        public virtual KeyAssignment VK_OEM_PLUS
        {
            get
            {
                if (_VK_OEM_PLUS == null)
                {
                    _VK_OEM_PLUS = new KeyAssignment(0x03D, 0x002B, "Equals sign", "Plus sign", false, true);
                }
                return _VK_OEM_PLUS;
            }
        }
        protected KeyAssignment _VK_OEM_PLUS;
        #endregion

        #region 13:  VK_Q
        public virtual KeyAssignment VK_Q
        {
            get
            {
                if (_VK_Q == null)
                {
                    _VK_Q = new KeyAssignment(0x0071, 0x0051);
                }
                return _VK_Q;
            }
        }
        protected KeyAssignment _VK_Q;
        #endregion

        #region 14: VK_W
        public virtual KeyAssignment VK_W
        {
            get
            {
                if (_VK_W == null)
                {
                    _VK_W = new KeyAssignment(0x0077, 0x0057);
                }
                return _VK_W;
            }
        }
        protected KeyAssignment _VK_W;
        #endregion

        #region 15: VK_E
        /// <summary>
        /// 15
        /// </summary>
        public virtual KeyAssignment VK_E
        {
            get
            {
                if (_VK_E == null)
                {
                    _VK_E = new KeyAssignment(0x0065, 0x0045);
                }
                return _VK_E;
            }
        }
        protected KeyAssignment _VK_E;
        #endregion

        #region 16: VK_R
        /// <summary>
        /// 16
        /// </summary>
        public virtual KeyAssignment VK_R
        {
            get
            {
                if (_VK_R == null)
                {
                    _VK_R = new KeyAssignment(0x0072, 0x0052);
                }
                return _VK_R;
            }
        }
        protected KeyAssignment _VK_R;
        #endregion

        #region 17: VK_T
        /// <summary>
        /// 17
        /// </summary>
        public virtual KeyAssignment VK_T
        {
            get
            {
                if (_VK_T == null)
                {
                    _VK_T = new KeyAssignment(0x0074, 0x0054);
                }
                return _VK_T;
            }
        }
        protected KeyAssignment _VK_T;
        #endregion

        #region 18: VK_Y
        /// <summary>
        /// 18
        /// </summary>
        public virtual KeyAssignment VK_Y
        {
            get
            {
                if (_VK_Y == null)
                {
                    _VK_Y = new KeyAssignment(0x0079, 0x0059);
                }
                return _VK_Y;
            }
        }
        protected KeyAssignment _VK_Y;
        #endregion

        #region 19:  VK_U
        /// <summary>
        /// 19
        /// </summary>
        public virtual KeyAssignment VK_U
        {
            get
            {
                if (_VK_U == null)
                {
                    _VK_U = new KeyAssignment(0x0075, 0x0055);
                }
                return _VK_U;
            }
        }
        protected KeyAssignment _VK_U;
        #endregion

        #region 20: VK_I
        public virtual KeyAssignment VK_I
        {
            get
            {
                if (_VK_I == null)
                {
                    _VK_I = new KeyAssignment(0x0069, 0x0049);
                }
                return _VK_I;
            }
        }
        protected KeyAssignment _VK_I;
        #endregion

        #region 21: VK_O
        /// <summary>
        /// 21
        /// </summary>
        public virtual KeyAssignment VK_O
        {
            get
            {
                if (_VK_O == null)
                {
                    _VK_O = new KeyAssignment(0x006F, 0x004F);
                }
                return _VK_O;
            }
        }
        protected KeyAssignment _VK_O;
        #endregion

        #region 22: VK_P
        /// <summary>
        /// 22
        /// </summary>
        public virtual KeyAssignment VK_P
        {
            get
            {
                if (_VK_P == null)
                {
                    _VK_P = new KeyAssignment(0x0070, 0x0050);
                }
                return _VK_P;
            }
        }
        protected KeyAssignment _VK_P;
        #endregion

        #region 23: VK_OEM_4
        /// <summary>
        /// 23  Left brackets
        /// </summary>
        public virtual KeyAssignment VK_OEM_4
        {
            get
            {
                if (_VK_OEM_4 == null)
                {
                    _VK_OEM_4 = new KeyAssignment(0x005B, 0x007B, "Left Square Bracket", "Left Curly Bracket", false, true);
                }
                return _VK_OEM_4;
            }
        }
        protected KeyAssignment _VK_OEM_4;
        #endregion

        #region 24: VK_OEM_6
        /// <summary>
        /// 24  Right brackets
        /// </summary>
        public virtual KeyAssignment VK_OEM_6
        {
            get
            {
                if (_VK_OEM_6 == null)
                {
                    _VK_OEM_6 = new KeyAssignment(0x005D, 0x007D, "Right Square Bracket", "Right Curly Bracket", false, true);
                }
                return _VK_OEM_6;
            }
        }
        protected KeyAssignment _VK_OEM_6;
        #endregion

        #region 25: VK_OEM_5
        /// <summary>
        /// 25
        /// </summary>
        public virtual KeyAssignment VK_OEM_5
        {
            get
            {
                if (_VK_OEM_5 == null)
                {
                    _VK_OEM_5 = new KeyAssignment(0x005C, 0x007C, "Reverse Solidus", "Vertical Line", false, true);
                }
                return _VK_OEM_5;
            }
        }
        protected KeyAssignment _VK_OEM_5;
        #endregion

        #region 26: VK_A
        /// <summary>
        /// 26
        /// </summary>
        public virtual KeyAssignment VK_A
        {
            get
            {
                if (_VK_A == null)
                {
                    _VK_A = new KeyAssignment(0x0061, 0x0041);
                }
                return _VK_A;
            }
        }
        protected KeyAssignment _VK_A;
        #endregion

        #region 27: VK_S
        /// <summary>
        /// 27
        /// </summary>
        public virtual KeyAssignment VK_S
        {
            get
            {
                if (_VK_S == null)
                {
                    _VK_S = new KeyAssignment(0x0073, 0x0053);
                }
                return _VK_S;
            }
        }
        protected KeyAssignment _VK_S;
        #endregion

        #region 28: VK_D
        /// <summary>
        /// 28
        /// </summary>
        public virtual KeyAssignment VK_D
        {
            get
            {
                if (_VK_D == null)
                {
                    _VK_D = new KeyAssignment(0x0064, 0x0044);
                }
                return _VK_D;
            }
        }
        protected KeyAssignment _VK_D;
        #endregion

        #region 29: VK_F
        /// <summary>
        /// 29
        /// </summary>
        public virtual KeyAssignment VK_F
        {
            get
            {
                if (_VK_F == null)
                {
                    _VK_F = new KeyAssignment(0x0066, 0x0046);
                }
                return _VK_F;
            }
        }
        protected KeyAssignment _VK_F;
        #endregion

        #region 30: VK_G
        /// <summary>
        /// 30
        /// </summary>
        public virtual KeyAssignment VK_G
        {
            get
            {
                if (_VK_G == null)
                {
                    _VK_G = new KeyAssignment(0x0067, 0x0047);
                }
                return _VK_G;
            }
        }
        protected KeyAssignment _VK_G;
        #endregion

        #region 31: VK_H
        /// <summary>
        /// 31
        /// </summary>
        public virtual KeyAssignment VK_H
        {
            get
            {
                if (_VK_H == null)
                {
                    _VK_H = new KeyAssignment(0x0068, 0x0048);
                }
                return _VK_H;
            }
        }
        protected KeyAssignment _VK_H;
        #endregion

        #region 32: VK_J
        /// <summary>
        /// 32
        /// </summary>
        public virtual KeyAssignment VK_J
        {
            get
            {
                if (_VK_J == null)
                {
                    _VK_J = new KeyAssignment(0x006A, 0x004A);
                }
                return _VK_J;
            }
        }
        protected KeyAssignment _VK_J;
        #endregion

        #region 33: VK_K
        /// <summary>
        /// 33
        /// </summary>
        public virtual KeyAssignment VK_K
        {
            get
            {
                if (_VK_K == null)
                {
                    _VK_K = new KeyAssignment(0x006B, 0x004B);
                }
                return _VK_K;
            }
        }
        protected KeyAssignment _VK_K;
        #endregion

        #region 34: VK_L
        /// <summary>
        /// 34
        /// </summary>
        public virtual KeyAssignment VK_L
        {
            get
            {
                if (_VK_L == null)
                {
                    _VK_L = new KeyAssignment(0x006C, 0x004C);
                }
                return _VK_L;
            }
        }
        protected KeyAssignment _VK_L;
        #endregion

        #region 35: VK_OEM_1
        /// <summary>
        /// 35
        /// </summary>
        public virtual KeyAssignment VK_OEM_1
        {
            get
            {
                if (_VK_OEM_1 == null)
                {
                    _VK_OEM_1 = new KeyAssignment(0x003B, 0x003A, "Semicolon", "Colon", false, true);
                }
                return _VK_OEM_1;
            }
        }
        protected KeyAssignment _VK_OEM_1;
        #endregion

        #region 36: VK_OEM_7
        /// <summary>
        /// 36  Apostrophe/Quotation mark
        /// </summary>
        public virtual KeyAssignment VK_OEM_7
        {
            get
            {
                if (_VK_OEM_7 == null)
                {
                    _VK_OEM_7 = new KeyAssignment(0x0027, 0x0022, "Apostrophe", "Quotation mark", false, true);
                }
                return _VK_OEM_7;
            }
        }
        protected KeyAssignment _VK_OEM_7;
        #endregion

        #region 37: VK_Z
        /// <summary>
        /// 37
        /// </summary>
        public virtual KeyAssignment VK_Z
        {
            get
            {
                if (_VK_Z == null)
                {
                    _VK_Z = new KeyAssignment(0x007A, 0x005A);
                }
                return _VK_Z;
            }
        }
        protected KeyAssignment _VK_Z;
        #endregion

        #region 38: VK_X
        /// <summary>
        /// 38
        /// </summary>
        public virtual KeyAssignment VK_X
        {
            get
            {
                if (_VK_X == null)
                {
                    _VK_X = new KeyAssignment(0x0078, 0x0058);
                }
                return _VK_X;
            }
        }
        protected KeyAssignment _VK_X;
        #endregion

        #region 39: VK_C
        /// <summary>
        /// 39
        /// </summary>
        public virtual KeyAssignment VK_C
        {
            get
            {
                if (_VK_C == null)
                {
                    _VK_C = new KeyAssignment(0x0063, 0x0043);
                }
                return _VK_C;
            }
        }
        protected KeyAssignment _VK_C;
        #endregion

        #region 40: VK_V
        /// <summary>
        /// 40
        /// </summary>
        public virtual KeyAssignment VK_V
        {
            get
            {
                if (_VK_V == null)
                {
                    _VK_V = new KeyAssignment(0x0076, 0x0056);
                }
                return _VK_V;
            }
        }
        protected KeyAssignment _VK_V;
        #endregion

        #region 41: VK_B
        /// <summary>
        /// 41
        /// </summary>
        public virtual KeyAssignment VK_B
        {
            get
            {
                if (_VK_B == null)
                {
                    _VK_B = new KeyAssignment(0x0062, 0x0042);
                }
                return _VK_B;
            }
        }
        protected KeyAssignment _VK_B;
        #endregion

        #region 42: VK_N
        /// <summary>
        /// 42
        /// </summary>
        public virtual KeyAssignment VK_N
        {
            get
            {
                if (_VK_N == null)
                {
                    _VK_N = new KeyAssignment(0x006E, 0x004E);
                }
                return _VK_N;
            }
        }
        protected KeyAssignment _VK_N;
        #endregion

        #region 43: VK_M
        /// <summary>
        /// 43
        /// </summary>
        public virtual KeyAssignment VK_M
        {
            get
            {
                if (_VK_M == null)
                {
                    _VK_M = new KeyAssignment(0x006D, 0x004D);
                }
                return _VK_M;
            }
        }
        protected KeyAssignment _VK_M;
        #endregion

        #region 44: VK_OEM_COMMA
        /// <summary>
        /// 44  Commas / Less-than sign
        /// </summary>
        public virtual KeyAssignment VK_OEM_COMMA
        {
            get
            {
                if (_VK_OEM_COMMA == null)
                {
                    _VK_OEM_COMMA = new KeyAssignment(0x002C, 0x003C, "Comma", "Less-than sign", false, true);
                }
                return _VK_OEM_COMMA;
            }
        }
        protected KeyAssignment _VK_OEM_COMMA;
        #endregion

        #region 45: VK_OEM_PERIOD
        /// <summary>
        /// 45  Period / Greater-than sign
        /// </summary>
        public virtual KeyAssignment VK_OEM_PERIOD
        {
            get
            {
                if (_VK_OEM_PERIOD == null)
                {
                    _VK_OEM_PERIOD = new KeyAssignment(0x002E, 0x003E, "Period", "Greater-than sign", false, true);
                }
                return _VK_OEM_PERIOD;
            }
        }
        protected KeyAssignment _VK_OEM_PERIOD;
        #endregion

        #region 46: VK_OEM_2
        /// <summary>
        /// 46  Solidus
        /// </summary>
        public virtual KeyAssignment VK_OEM_2
        {
            get
            {
                if (_VK_OEM_2 == null)
                {
                    _VK_OEM_2 = new KeyAssignment(0x002F, 0x003F, "Solidus", "Question Mark", false, true);
                }
                return _VK_OEM_2;
            }
        }
        protected KeyAssignment _VK_OEM_2;
        #endregion

        #region 47: VK_Space
        /// <summary>
        /// 47  Space
        /// </summary>
        public virtual KeyAssignment VK_Space
        {
            get
            {
                if (_VK_Space == null)
                {
                    _VK_Space = new KeyAssignment(0x0020, "Space", false);
                }
                return _VK_Space;
            }
        }
        protected KeyAssignment _VK_Space;
        #endregion

        #region 48: VK_BS
        /// <summary>
        /// 48  Backspace
        /// </summary>
        public virtual KeyAssignment VK_BS
        {
            get
            {
                if (_VK_BS == null)
                {
                    _VK_BS = new KeyAssignment(0x0008, "BackSpace", false);
                }
                return _VK_BS;
            }
        }
        protected KeyAssignment _VK_BS;
        #endregion

        #region 49: VK_HT
        /// <summary>
        /// 49  Horizontal Tab
        /// </summary>
        public virtual KeyAssignment VK_HT
        {
            get
            {
                if (_VK_HT == null)
                {
                    _VK_HT = new KeyAssignment(0x0009, "Horizontal Tab", false);
                }
                return _VK_HT;
            }
        }
        protected KeyAssignment _VK_HT;
        #endregion

        #region 50: VK_LF
        /// <summary>
        /// 50  Line Feed
        /// </summary>
        public virtual KeyAssignment VK_LF
        {
            get
            {
                if (_VK_LF == null)
                {
                    _VK_LF = new KeyAssignment(0x000A, "Line Feed", false);
                }
                return _VK_LF;
            }
        }
        protected KeyAssignment _VK_LF;
        #endregion

        #region 51: VK_ALT
        /// <summary>
        /// 51  ALT
        /// </summary>
        public virtual KeyAssignment VK_ALT
        {
            get
            {
                if (_VK_ALT == null)
                {
                    _VK_ALT = new KeyAssignment(true, false, false, false);
                }
                return _VK_ALT;
            }
        }
        protected KeyAssignment _VK_ALT;
        #endregion

        #region 52: VK_CTRL
        /// <summary>
        /// 52  Control
        /// </summary>
        public virtual KeyAssignment VK_CTRL
        {
            get
            {
                if (_VK_CTRL == null)
                {
                    _VK_CTRL = new KeyAssignment(false, true, false, false);
                }
                return _VK_CTRL;
            }
        }
        protected KeyAssignment _VK_CTRL;
        #endregion

        #region 53: VK_CAPS
        /// <summary>
        /// 53  CAPS Lock
        /// </summary>
        public virtual KeyAssignment VK_CAPS
        {
            get
            {
                if (_VK_CAPS == null)
                {
                    _VK_CAPS = new KeyAssignment(false, false, false, true);
                }
                return _VK_CAPS;
            }
        }
        protected KeyAssignment _VK_CAPS;
        #endregion

        #region 54: VK_SHIFT
        /// <summary>
        /// 54  SHIFT
        /// </summary>
        public virtual KeyAssignment VK_SHIFT
        {
            get
            {
                if (_VK_SHIFT == null)
                {
                    _VK_SHIFT = new KeyAssignment(false, false, true, false);
                }
                return _VK_SHIFT;
            }
        }
        protected KeyAssignment _VK_SHIFT;
        #endregion
    }
}
