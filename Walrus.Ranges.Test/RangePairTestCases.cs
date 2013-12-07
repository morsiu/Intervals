using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walrus.Ranges.Text;

namespace Walrus.Ranges
{
    public static class RangePairTestCases
    {
        public class TestCase
        {
            public IRange<int> A { get; set; }

            public IRange<int> B { get; set; }

            public bool AEqualsB { get; set; }
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

        public static IEnumerable<TestCase> ClosedRangePairs
        {
            get
            {
                yield return
                ForA("x===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===x            ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      x===x      ").
                AEqualsB();

                yield return
                ForA("        x===x    ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("x===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("  x===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("    x===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("        x===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("          x===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("            x===x").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("x==x    ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  x==x  ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    x==x").
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===o            ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      o===o      ").
                AEqualsB();

                yield return
                ForA("        o===o    ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("o===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("  o===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("    o===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("        o===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("          o===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("            o===o").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("o==o    ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  o==o  ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    o==o").
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===x            ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      o===x      ").
                AEqualsB();

                yield return
                ForA("        o===x    ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("o===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("  o===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("    o===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("        o===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("          o===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("            o===x").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("o==x    ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  o==x  ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    o==x").
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===o            ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      x===o      ").
                AEqualsB();

                yield return
                ForA("        x===o    ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("x===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("  x===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("    x===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("        x===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("          x===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("            x===o").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("x==o    ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  x==o  ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    x==o").
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===x            ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("o===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("  o===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("    o===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("        o===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("          o===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("            o===o").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("o==o    ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  o==o  ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    o==o").
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("o===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===o            ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("x===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("  x===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("    x===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("        x===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("          x===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("            x===x").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("x==x    ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  x==x  ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    x==x").
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("o===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===x            ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("x===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("  x===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("    x===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("        x===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("          x===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("            x===x").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("x==x    ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  x==x  ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    x==x").
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndLeftOpenPairs
        {
            get
            {
                yield return
                ForA("x===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===x            ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("o===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("  o===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("    o===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("        o===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("          o===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("            o===x").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("o==x    ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  o==x  ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    o==x").
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndClosedRangePairs
        {
            get
            {
                yield return
                ForA("x===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===o            ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      x===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("x===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("  x===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("    x===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("        x===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("          x===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("            x===x").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("x==x    ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  x==x  ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    x==x").
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("x======x").
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===x            ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===x          ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===x        ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===x    ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===x  ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===x").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("x===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("  x===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("    x===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("        x===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("          x===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===x      ").
                AndB("            x===o").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("x==o    ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("  x==o  ").
                ADoesNotEqualB();

                yield return
                ForA("x======x").
                AndB("    x==o").
                ADoesNotEqualB();

                yield return
                ForA("x==x    ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("  x==x  ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("    x==x").
                AndB("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===o            ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("o===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("  o===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("    o===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("        o===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("          o===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("            o===x").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("o==x    ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  o==x  ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    o==x").
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===x            ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("o===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("  o===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("    o===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("        o===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("          o===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("            o===o").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("o==o    ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  o==o  ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    o==o").
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===o            ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===o          ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===o        ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===o    ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===o  ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===o").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("x===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("  x===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("    x===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("        x===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("          x===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===o      ").
                AndB("            x===o").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("x==o    ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("  x==o  ").
                ADoesNotEqualB();

                yield return
                ForA("o======o").
                AndB("    x==o").
                ADoesNotEqualB();

                yield return
                ForA("o==o    ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("  o==o  ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("    o==o").
                AndB("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===o            ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      o===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("o===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("  o===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("    o===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("        o===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("          o===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("            o===o").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("o==o    ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  o==o  ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    o==o").
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("o======o").
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                ForA("o===x").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("x===o").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("o===x            ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("  o===x          ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("    o===x        ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("        o===x    ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("          o===x  ").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("            o===x").
                AndB("      x===o      ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("x===o            ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("  x===o          ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("    x===o        ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("        x===o    ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("          x===o  ").
                ADoesNotEqualB();

                yield return
                ForA("      o===x      ").
                AndB("            x===o").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("x==o    ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("  x==o  ").
                ADoesNotEqualB();

                yield return
                ForA("o======x").
                AndB("    x==o").
                ADoesNotEqualB();

                yield return
                ForA("o==x    ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("  o==x  ").
                AndB("x======o").
                ADoesNotEqualB();

                yield return
                ForA("    o==x").
                AndB("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                ForA("x===o").
                AndB("     ").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("o===x").
                ADoesNotEqualB();

                yield return
                ForA("     ").
                AndB("     ").
                AEqualsB();

                yield return
                ForA("x===o            ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("  x===o          ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("    x===o        ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("        x===o    ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("          x===o  ").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("            x===o").
                AndB("      o===x      ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("o===x            ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("  o===x          ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("    o===x        ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("        o===x    ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("          o===x  ").
                ADoesNotEqualB();

                yield return
                ForA("      x===o      ").
                AndB("            o===x").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("o==x    ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("  o==x  ").
                ADoesNotEqualB();

                yield return
                ForA("x======o").
                AndB("    o==x").
                ADoesNotEqualB();

                yield return
                ForA("x==o    ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("  x==o  ").
                AndB("o======x").
                ADoesNotEqualB();

                yield return
                ForA("    x==o").
                AndB("o======x").
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

            private IRange<int> Parse(string textRange)
            {
                return textRange == null ? null : parser.Parse(textRange);
            }
        }
    }
}
