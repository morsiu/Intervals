// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using Walrus.Ranges.Text;

namespace Walrus.Ranges
{
    public static class RangePairTestCases
    {
        public class TestCase
        {
            public IRange<int> A { get; set; }

            public IRange<int> B { get; set; }

            public bool? AEqualsB { get; set; }

            public bool? AIntersectsWithB { get; set; }

            public bool? ACoversB { get; set; }

            public bool? BCoversA { get; set; }

            public IRange<int> ABIntersection { get; set; }

            public IRange<int> ABSpan { get; set; }
        }

        public static IEnumerable<TestCase> AllRangePairs
        {
            get
            {
                return Concat(
                    AllNonNullRangePairs,
                    NullRangePairs);
            }
        }

        public static IEnumerable<TestCase> AllNonNullRangePairs
        {
            get
            {
                return Concat(
                    EmptyRangePairs,
                    ClosedRangePairs,
                    ClosedAndOpenRangePairs,
                    ClosedAndLeftOpenPairs,
                    ClosedAndRightOpenRangePairs,
                    OpenRangePairs,
                    OpenAndClosedRangePairs,
                    OpenAndLeftOpenRangePairs,
                    OpenAndRightOpenRangePairs,
                    LeftOpenRangePairs,
                    LeftOpenAndClosedRangePairs,
                    LeftOpenAndOpenRangePairs,
                    LeftOpenAndRightOpenRangePairs,
                    RightOpenRangePairs,
                    RightOpenAndClosedRangePairs,
                    RightOpenAndOpenRangePairs,
                    RightOpenAndLeftOpenRangePairs
                );
            }
        }

        public static IEnumerable<TestCase> NullRangePairs
        {
            get
            {
                yield return
                ForA(null).
                AndB(null).
                AEqualsB();

                yield return
                ForA(null).
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA(null).
                AndB("x===x").
                ADoesNotEqualB();

                yield return
                ForA(null).
                AndB("o===o").
                ADoesNotEqualB();

                yield return
                ForA(null).
                AndB("o===x").
                ADoesNotEqualB();

                yield return
                ForA(null).
                AndB("x===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB(null).
                ADoesNotEqualB();

                yield return
                ForA("x===x").
                AndB(null).
                ADoesNotEqualB();

                yield return
                ForA("o===o").
                AndB(null).
                ADoesNotEqualB();

                yield return
                ForA("o===x").
                AndB(null).
                ADoesNotEqualB();

                yield return
                ForA("x===o").
                AndB(null).
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> EmptyRangePairs
        {
            get
            {
                yield return
                ForA("     ").
                AndB("     ").
                SetN("     ").AsABIntersection().
                SetN("     ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                AEqualsB();

                yield return
                ForA("     ").
                AndB("x===x").
                SetN("     ").AsABIntersection().
                SetN("x===x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===o").
                SetN("     ").AsABIntersection().
                SetN("o===o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===x").
                SetN("     ").AsABIntersection().
                SetN("o===x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===o").
                SetN("     ").AsABIntersection().
                SetN("x===o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x===x").
                AndB("     ").
                SetN("     ").AsABIntersection().
                SetN("x===x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o===o").
                AndB("     ").
                SetN("     ").AsABIntersection().
                SetN("o===o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o===x").
                AndB("     ").
                SetN("     ").AsABIntersection().
                SetN("o===x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x===o").
                AndB("     ").
                SetN("     ").AsABIntersection().
                SetN("x===o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedRangePairs
        {
            get
            {
                yield return
                ForA("x===x            ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      x===x      ").
                SetN("      x          ").AsABIntersection().
                SetN("  x=======x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      x===x      ").
                SetN("      x=x        ").AsABIntersection().
                SetN("    x=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      x===x      ").
                SetN("      x===x      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BCoversA().
                AEqualsB();

                yield return
                ForA("        x===x    ").
                AndB("      x===x      ").
                SetN("        x=x      ").AsABIntersection().
                SetN("      x=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      x===x      ").
                SetN("          x      ").AsABIntersection().
                SetN("      x=======x  ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("x==x    ").
                SetN("x==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  x==x  ").
                SetN("  x==x  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    x==x").
                SetN("    x==x").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("x======x").
                SetN("x==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("x======x").
                SetN("  x==x  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("x======x").
                SetN("    x==x").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o            ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      o===o      ").
                SetN("      o=o        ").AsABIntersection().
                SetN("    o=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      o===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      o===o      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BCoversA().
                AEqualsB();

                yield return
                ForA("        o===o    ").
                AndB("      o===o      ").
                SetN("        o=o      ").AsABIntersection().
                SetN("      o=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("o==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  o==o  ").
                SetN("  o==o  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    o==o").
                SetN("    o==o").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("o======o").
                SetN("o==o    ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("o======o").
                SetN("  o==o  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("o======o").
                SetN("    o==o").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x            ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      o===x      ").
                SetN("      o=x        ").AsABIntersection().
                SetN("    o=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      o===x      ").
                SetN("      o===x      ").AsABIntersection().
                SetN("      o===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BCoversA().
                AEqualsB();

                yield return
                ForA("        o===x    ").
                AndB("      o===x      ").
                SetN("        o=x      ").AsABIntersection().
                SetN("      o=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("o==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  o==x  ").
                SetN("  o==x  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    o==x").
                SetN("    o==x").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("o======x").
                SetN("o==x    ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("o======x").
                SetN("  o==x  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("o======x").
                SetN("    o==x").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o            ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      x===o      ").
                SetN("      x=o        ").AsABIntersection().
                SetN("    x=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      x===o      ").
                SetN("      x===o      ").AsABIntersection().
                SetN("      x===o      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BCoversA().
                AEqualsB();

                yield return
                ForA("        x===o    ").
                AndB("      x===o      ").
                SetN("        x=o      ").AsABIntersection().
                SetN("      x=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("x==o    ").
                SetN("x==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  x==o  ").
                SetN("  x==o  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    x==o").
                SetN("    x==o").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("x======o").
                SetN("x==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("x======o").
                SetN("  x==o  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("x======o").
                SetN("    x==o").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===x            ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      o===o      ").
                SetN("      o=x        ").AsABIntersection().
                SetN("    x=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      o===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      o===o      ").
                SetN("        x=o      ").AsABIntersection().
                SetN("      o=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("o==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  o==o  ").
                SetN("  o==o  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    o==o").
                SetN("    o==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("o======o").
                SetN("o==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("o======o").
                SetN("  x==x  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("o======o").
                SetN("    x==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("o===o            ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      x===x      ").
                SetN("      x=o        ").AsABIntersection().
                SetN("    o=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      x===x      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      x===x      ").
                SetN("        o=x      ").AsABIntersection().
                SetN("      x=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("x==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  x==x  ").
                SetN("  x==x  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    x==x").
                SetN("    x==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("x======x").
                SetN("o==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("x======x").
                SetN("  o==o  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("x======x").
                SetN("    o==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("o===x            ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      x===x      ").
                SetN("      x          ").AsABIntersection().
                SetN("  o=======x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      x===x      ").
                SetN("      x=x        ").AsABIntersection().
                SetN("    o=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      x===x      ").
                SetN("      o===x      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      x===x      ").
                SetN("        o=x      ").AsABIntersection().
                SetN("      x=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("x==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  x==x  ").
                SetN("  x==x  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    x==x").
                SetN("    x==x").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("x======x").
                SetN("o==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("x======x").
                SetN("  o==x  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("x======x").
                SetN("    o==x").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndLeftOpenPairs
        {
            get
            {
                yield return
                ForA("x===x            ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      o===x      ").
                SetN("      o=x        ").AsABIntersection().
                SetN("    x=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      o===x      ").
                SetN("      o===x      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      o===x      ").
                SetN("        x=x      ").AsABIntersection().
                SetN("      o=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      o===x      ").
                SetN("          x      ").AsABIntersection().
                SetN("      o=======x  ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      o===x      ").
                SetN("      o=========x").AsABIntersection().
                SetN("      o=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("o==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  o==x  ").
                SetN("  o==x  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    o==x").
                SetN("    o==x").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("o======x").
                SetN("o==x    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("o======x").
                SetN("  x==x  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("o======x").
                SetN("    x==x").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("x===o            ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      x===x      ").
                SetN("      x=o        ").AsABIntersection().
                SetN("    x=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      x===x      ").
                SetN("      x===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      x===x      ").
                SetN("        x=x      ").AsABIntersection().
                SetN("      x=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      x===x      ").
                SetN("          x      ").AsABIntersection().
                SetN("      x=======o  ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      x===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("x==x    ").
                SetN("x==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  x==x  ").
                SetN("  x==x  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    x==x").
                SetN("    x==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("x======x").
                SetN("x==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("x======x").
                SetN("  x==o  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("x======x").
                SetN("    x==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===x            ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      x===o      ").
                SetN("      x          ").AsABIntersection().
                SetN("  x=======o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      x===o      ").
                SetN("      x=x        ").AsABIntersection().
                SetN("    x=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      x===o      ").
                SetN("      x===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      x===o      ").
                SetN("        x=o      ").AsABIntersection().
                SetN("      x=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("x==o    ").
                SetN("x==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  x==o  ").
                SetN("  x==o  ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    x==o").
                SetN("    x==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("x======o").
                SetN("x==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("x======o").
                SetN("  x==x  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("x======o").
                SetN("    x==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o            ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      o===x      ").
                SetN("      o=o        ").AsABIntersection().
                SetN("    o=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      o===x      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      o===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      o===x      ").
                SetN("        o=x      ").AsABIntersection().
                SetN("      o=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("o==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  o==x  ").
                SetN("  o==x  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    o==x").
                SetN("    o==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("o======x").
                SetN("o==o    ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("o======x").
                SetN("  o==o  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("o======x").
                SetN("    o==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x            ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      o===o      ").
                SetN("      o=x        ").AsABIntersection().
                SetN("    o=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      o===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      o===x      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      o===o      ").
                SetN("        o=o      ").AsABIntersection().
                SetN("      o=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("o==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  o==o  ").
                SetN("  o==o  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    o==o").
                SetN("    o==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("o======o").
                SetN("o==x    ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("o======o").
                SetN("  o==x  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("o======o").
                SetN("    o==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o            ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  o=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      x===o      ").
                SetN("      x=o        ").AsABIntersection().
                SetN("    o=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      x===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      x===o      ").
                SetN("        o=o      ").AsABIntersection().
                SetN("      x=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("x==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  x==o  ").
                SetN("  x==o  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    x==o").
                SetN("    x==o").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("x======o").
                SetN("o==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("x======o").
                SetN("  o==o  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("x======o").
                SetN("    o==o").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o            ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      o===o      ").
                SetN("      o=o        ").AsABIntersection().
                SetN("    x=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      o===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===o      ").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      o===o      ").
                SetN("        x=o      ").AsABIntersection().
                SetN("      o=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=======o  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      o===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("o==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  o==o  ").
                SetN("  o==o  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    o==o").
                SetN("    o==o").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("o======o").
                SetN("o==o    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("o======o").
                SetN("  x==o  ").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("o======o").
                SetN("    x==o").AsABIntersection().
                SetN("o======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x            ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("o=========o      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      x===o      ").
                SetN("      x          ").AsABIntersection().
                SetN("  o=======o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      x===o      ").
                SetN("      x=x        ").AsABIntersection().
                SetN("    o=====o      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      x===o      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      x===o      ").
                SetN("        o=o      ").AsABIntersection().
                SetN("      x=====x    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=======x  ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      x===o      ").
                SetN("                 ").AsABIntersection().
                SetN("      x=========x").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("x==o    ").
                SetN("o==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  x==o  ").
                SetN("  x==o  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    x==o").
                SetN("    x==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("x======o").
                SetN("o==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("x======o").
                SetN("  o==x  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("x======o").
                SetN("    o==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o            ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("x=========x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("  x=======x      ").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      o===x      ").
                SetN("      o=o        ").AsABIntersection().
                SetN("    x=====x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      o===x      ").
                SetN("      o===o      ").AsABIntersection().
                SetN("      x===x      ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      o===x      ").
                SetN("        x=x      ").AsABIntersection().
                SetN("      o=====o    ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      o===x      ").
                SetN("          x      ").AsABIntersection().
                SetN("      o=======o  ").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      o===x      ").
                SetN("                 ").AsABIntersection().
                SetN("      o=========o").AsABSpan().
                ADoesNotIntersectWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("o==x    ").
                SetN("o==x    ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  o==x  ").
                SetN("  o==x  ").AsABIntersection().
                SetN("x======o").AsABSpan().
                AIntersectsWithB().
                ACoversB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    o==x").
                SetN("    o==o").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("o======x").
                SetN("o==o    ").AsABIntersection().
                SetN("x======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BDoesNotCoverA().
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("o======x").
                SetN("  x==o  ").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("o======x").
                SetN("    x==o").AsABIntersection().
                SetN("o======x").AsABSpan().
                AIntersectsWithB().
                ADoesNotCoverB().
                BCoversA().
                ADoesNotEqualB();
            }
        }

        private static IEnumerable<TestCase> Concat(params IEnumerable<TestCase>[] manyTestCases)
        {
            return manyTestCases.SelectMany(testCases => testCases);
        }

        private static TestCaseBuilder ForA(string rangeA)
        {
            return new TestCaseBuilder(rangeA, Parser);
        }

        private static readonly TextRangeParser Parser =
            new TextRangeParser(
                new PointTypeMatcher(' ', '=', 'x', 'o'));

        private class TestCaseBuilder
        {
            private readonly TestCase testCase = new TestCase();
            private readonly TextRangeParser parser;

            public static implicit operator TestCase(TestCaseBuilder builder)
            {
                return builder.testCase;
            }

            public TestCaseBuilder(string rangeA, TextRangeParser parser)
            {
                this.parser = parser;
                testCase.A = Parse(rangeA);
            }

            public TestCaseBuilder AndB(string rangeB)
            {
                testCase.B = Parse(rangeB);
                return this;
            }

            public INRangeTargets SetN(string rangeN)
            {
                var n = Parse(rangeN);
                return new NRangeTargets(n, this);
            }

            public TestCaseBuilder AEqualsB()
            {
                testCase.AEqualsB = true;
                return this;
            }

            public TestCaseBuilder ADoesNotEqualB()
            {
                testCase.AEqualsB = false;
                return this;
            }

            public TestCaseBuilder AIntersectsWithB()
            {
                testCase.AIntersectsWithB = true;
                return this;
            }

            public TestCaseBuilder ADoesNotIntersectWithB()
            {
                testCase.AIntersectsWithB = false;
                return this;
            }

            public TestCaseBuilder ACoversB()
            {
                testCase.ACoversB = true;
                return this;
            }

            public TestCaseBuilder ADoesNotCoverB()
            {
                testCase.ACoversB = false;
                return this;
            }

            public TestCaseBuilder BCoversA()
            {
                testCase.BCoversA = true;
                return this;
            }

            public TestCaseBuilder BDoesNotCoverA()
            {
                testCase.BCoversA = false;
                return this;
            }

            private IRange<int> Parse(string textRange)
            {
                return textRange == null ? null : parser.Parse(textRange);
            }

            public interface INRangeTargets
            {
                TestCaseBuilder AsABIntersection();

                TestCaseBuilder AsABSpan();
            }

            private class NRangeTargets : INRangeTargets
            {
                private readonly TestCaseBuilder builder;
                private readonly IRange<int> rangeN;

                public NRangeTargets(IRange<int> rangeN, TestCaseBuilder builder)
                {
                    this.builder = builder;
                    this.rangeN = rangeN;
                }

                public TestCaseBuilder AsABIntersection()
                {
                    builder.testCase.ABIntersection = rangeN;
                    return builder;
                }

                public TestCaseBuilder AsABSpan()
                {
                    builder.testCase.ABSpan = rangeN;
                    return builder;
                }
            }
        }
    }
}
