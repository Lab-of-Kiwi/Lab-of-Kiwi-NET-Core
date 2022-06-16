using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabOfKiwi.Cmd;

public readonly struct TableBorder
{
    private static readonly Dictionary<uint, char[]> Cache;

    private readonly char[] _chars;

    public TableBorder()
    {
        _chars = Cache[0U];
    }

    public TableBorder(BorderType outer = BorderType.None, BorderType inner = BorderType.None)
    {
        uint value = outer.CombineIndexes(inner);
        _chars = Cache[value];
    }

    public char OuterHorizontal => _chars[0];

    public char OuterVertical => _chars[1];

    public char OuterTopLeftCorner => _chars[2];

    public char OuterTopRightCorner => _chars[3];

    public char OuterBottomLeftCorner => _chars[4];

    public char OuterBottomRightCorner => _chars[5];

    public char OuterLeftInterset => _chars[6];

    public char OuterRightInterset => _chars[7];

    public char OuterTopIntersect => _chars[8];

    public char OuterBottomIntersect => _chars[9];

    public char InnerIntersect => _chars[10];

    public char InnerHorizontal => _chars[11];

    public char InnerVertical => _chars[12];

    static TableBorder()
    {
        var types = Enum.GetValues<BorderType>();

        Cache = new(types.Length * types.Length);

        foreach (var type1 in types)
        {
            foreach(var type2 in types)
            {
                uint index = type1.CombineIndexes(type2);

                char[] chars = new char[13];

                if (type1 == BorderType.None)
                {
                    Array.Fill(chars, ' ');

                    if (type2 != BorderType.None)
                    {
                        var def2 = type2.GetDefinition()!.Value.AllChars;
                        chars[10] = def2[10];
                        chars[11] = def2[0];
                        chars[12] = def2[1];
                    }
                }
                else if (type2 == BorderType.None)
                {
                    var def1 = type1.GetDefinition()!.Value.AllChars;
                    Array.Copy(def1, chars, def1.Length);
                    chars[6] = chars[7] = def1[1];
                    chars[8] = chars[9] = def1[0];
                    chars[10] = chars[11] = chars[12] = ' ';
                }
                else if (type1 == type2)
                {
                    var def1 = type1.GetDefinition()!.Value.AllChars;
                    Array.Copy(def1, chars, def1.Length);
                    chars[11] = chars[0];
                    chars[12] = chars[1];
                }
                else
                {
                    var def1 = type1.GetDefinition()!.Value.AllChars;
                    var def2 = type2.GetDefinition()!.Value.AllChars;
                    Array.Copy(def1, chars, 6);
                    chars[10] = def2[10];
                    chars[11] = def2[0];
                    chars[12] = def2[1];

                    if (type1 == BorderType.DoubleSolid)
                    {
                        chars[6] = '\u255F';
                        chars[7] = '\u2562';
                        chars[8] = '\u2564';
                        chars[9] = '\u2567';
                    }
                    else if (type2 == BorderType.DoubleSolid)
                    {
                        chars[6] = '\u255E';
                        chars[7] = '\u2561';
                        chars[8] = '\u2565';
                        chars[9] = '\u2568';
                    }
                    else if (type1.IsLight() && type2.IsHeavy())
                    {
                        chars[6] = '\u251D';
                        chars[7] = '\u2525';
                        chars[8] = '\u2530';
                        chars[9] = '\u2538';
                    }
                    else if (type1.IsHeavy() && type2.IsLight())
                    {
                        chars[6] = '\u2520';
                        chars[7] = '\u252B';
                        chars[8] = '\u252F';
                        chars[9] = '\u2537';
                    }
                    else
                    {
                        Debug.Assert(type1.IsLight() == type2.IsLight());
                        Debug.Assert(type1.IsHeavy() == type2.IsHeavy());
                        Array.Copy(def1, 6, chars, 6, 4);
                    }
                }

                Cache.Add(index, chars);
            }
        }
    }

}
