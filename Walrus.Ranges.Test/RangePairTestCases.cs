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
                For(null).
                And(null).
                AEqualsB();

                yield return
                For(null).
                And("     ").
                ADoesNotEqualB();

                yield return
                For(null).
                And("x===x").
                ADoesNotEqualB();

                yield return
                For(null).
                And("o===o").
                ADoesNotEqualB();

                yield return
                For(null).
                And("o===x").
                ADoesNotEqualB();

                yield return
                For(null).
                And("x===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And(null).
                ADoesNotEqualB();

                yield return
                For("x===x").
                And(null).
                ADoesNotEqualB();

                yield return
                For("o===o").
                And(null).
                ADoesNotEqualB();

                yield return
                For("o===x").
                And(null).
                ADoesNotEqualB();

                yield return
                For("x===o").
                And(null).
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedRangePairs
        {
            get
            {
                yield return
                For("x===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===x            ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("  x===x          ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("    x===x        ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("      x===x      ").
                AEqualsB();

                yield return
                For("        x===x    ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("          x===x  ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("            x===x").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("x===x            ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("  x===x          ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("    x===x        ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("        x===x    ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("          x===x  ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("            x===x").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("x==x    ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("  x==x  ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("    x==x").
                ADoesNotEqualB();

                yield return
                For("x==x    ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("  x==x  ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("    x==x").
                And("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenRangePairs
        {
            get
            {
                yield return
                For("o===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===o            ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("  o===o          ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("    o===o        ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("      o===o      ").
                AEqualsB();

                yield return
                For("        o===o    ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("          o===o  ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("            o===o").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("o===o            ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("  o===o          ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("    o===o        ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("        o===o    ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("          o===o  ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("            o===o").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("o==o    ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("  o==o  ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("    o==o").
                ADoesNotEqualB();

                yield return
                For("o==o    ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("  o==o  ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("    o==o").
                And("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenRangePairs
        {
            get
            {
                yield return
                For("o===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===x            ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("  o===x          ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("    o===x        ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("      o===x      ").
                AEqualsB();

                yield return
                For("        o===x    ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("          o===x  ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("            o===x").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("o===x            ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("  o===x          ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("    o===x        ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("        o===x    ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("          o===x  ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("            o===x").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("o==x    ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("  o==x  ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("    o==x").
                ADoesNotEqualB();

                yield return
                For("o==x    ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("  o==x  ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("    o==x").
                And("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenRangePairs
        {
            get
            {
                yield return
                For("x===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===o            ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("  x===o          ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("    x===o        ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("      x===o      ").
                AEqualsB();

                yield return
                For("        x===o    ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("          x===o  ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("            x===o").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("x===o            ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("  x===o          ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("    x===o        ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("        x===o    ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("          x===o  ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("            x===o").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("x==o    ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("  x==o  ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("    x==o").
                ADoesNotEqualB();

                yield return
                For("x==o    ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("  x==o  ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("    x==o").
                And("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndOpenRangePairs
        {
            get
            {
                yield return
                For("x===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===x            ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("  x===x          ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("    x===x        ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("        x===x    ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("          x===x  ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("            x===x").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("o===o            ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("  o===o          ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("    o===o        ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("        o===o    ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("          o===o  ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("            o===o").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("o==o    ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("  o==o  ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("    o==o").
                ADoesNotEqualB();

                yield return
                For("x==x    ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("  x==x  ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("    x==x").
                And("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndClosedRangePairs
        {
            get
            {
                yield return
                For("o===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===o            ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("  o===o          ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("    o===o        ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("        o===o    ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("          o===o  ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("            o===o").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("x===x            ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("  x===x          ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("    x===x        ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("        x===x    ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("          x===x  ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("            x===x").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("x==x    ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("  x==x  ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("    x==x").
                ADoesNotEqualB();

                yield return
                For("o==o    ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("  o==o  ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("    o==o").
                And("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndClosedRangePairs
        {
            get
            {
                yield return
                For("o===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===x            ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("  o===x          ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("    o===x        ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("        o===x    ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("          o===x  ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("            o===x").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("x===x            ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("  x===x          ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("    x===x        ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("        x===x    ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("          x===x  ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("            x===x").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("x==x    ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("  x==x  ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("    x==x").
                ADoesNotEqualB();

                yield return
                For("o==x    ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("  o==x  ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("    o==x").
                And("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndLeftOpenPairs
        {
            get
            {
                yield return
                For("x===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===x            ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("  x===x          ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("    x===x        ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("        x===x    ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("          x===x  ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("            x===x").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("o===x            ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("  o===x          ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("    o===x        ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("        o===x    ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("          o===x  ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("            o===x").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("o==x    ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("  o==x  ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("    o==x").
                ADoesNotEqualB();

                yield return
                For("x==x    ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("  x==x  ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("    x==x").
                And("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndClosedRangePairs
        {
            get
            {
                yield return
                For("x===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===o            ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("  x===o          ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("    x===o        ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("        x===o    ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("          x===o  ").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("            x===o").
                And("      x===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("x===x            ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("  x===x          ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("    x===x        ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("        x===x    ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("          x===x  ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("            x===x").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("x==x    ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("  x==x  ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("    x==x").
                ADoesNotEqualB();

                yield return
                For("x==o    ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("  x==o  ").
                And("x======x").
                ADoesNotEqualB();

                yield return
                For("    x==o").
                And("x======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> ClosedAndRightOpenRangePairs
        {
            get
            {
                yield return
                For("x===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===x            ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("  x===x          ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("    x===x        ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("        x===x    ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("          x===x  ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("            x===x").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("x===o            ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("  x===o          ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("    x===o        ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("        x===o    ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("          x===o  ").
                ADoesNotEqualB();

                yield return
                For("      x===x      ").
                And("            x===o").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("x==o    ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("  x==o  ").
                ADoesNotEqualB();

                yield return
                For("x======x").
                And("    x==o").
                ADoesNotEqualB();

                yield return
                For("x==x    ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("  x==x  ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("    x==x").
                And("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                For("o===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===o            ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("  o===o          ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("    o===o        ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("        o===o    ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("          o===o  ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("            o===o").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("o===x            ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("  o===x          ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("    o===x        ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("        o===x    ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("          o===x  ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("            o===x").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("o==x    ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("  o==x  ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("    o==x").
                ADoesNotEqualB();

                yield return
                For("o==o    ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("  o==o  ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("    o==o").
                And("o======x").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndOpenRangePairs
        {
            get
            {
                yield return
                For("o===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===x            ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("  o===x          ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("    o===x        ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("        o===x    ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("          o===x  ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("            o===x").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("o===o            ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("  o===o          ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("    o===o        ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("        o===o    ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("          o===o  ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("            o===o").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("o==o    ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("  o==o  ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("    o==o").
                ADoesNotEqualB();

                yield return
                For("o==x    ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("  o==x  ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("    o==x").
                And("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> OpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                For("o===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===o            ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("  o===o          ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("    o===o        ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("        o===o    ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("          o===o  ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("            o===o").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("x===o            ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("  x===o          ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("    x===o        ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("        x===o    ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("          x===o  ").
                ADoesNotEqualB();

                yield return
                For("      o===o      ").
                And("            x===o").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("x==o    ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("  x==o  ").
                ADoesNotEqualB();

                yield return
                For("o======o").
                And("    x==o").
                ADoesNotEqualB();

                yield return
                For("o==o    ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("  o==o  ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("    o==o").
                And("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndOpenRangePairs
        {
            get
            {
                yield return
                For("x===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===o            ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("  x===o          ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("    x===o        ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("        x===o    ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("          x===o  ").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("            x===o").
                And("      o===o      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("o===o            ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("  o===o          ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("    o===o        ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("        o===o    ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("          o===o  ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("            o===o").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("o==o    ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("  o==o  ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("    o==o").
                ADoesNotEqualB();

                yield return
                For("x==o    ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("  x==o  ").
                And("o======o").
                ADoesNotEqualB();

                yield return
                For("    x==o").
                And("o======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> LeftOpenAndRightOpenRangePairs
        {
            get
            {
                yield return
                For("o===x").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("x===o").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("o===x            ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("  o===x          ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("    o===x        ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("        o===x    ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("          o===x  ").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("            o===x").
                And("      x===o      ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("x===o            ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("  x===o          ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("    x===o        ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("        x===o    ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("          x===o  ").
                ADoesNotEqualB();

                yield return
                For("      o===x      ").
                And("            x===o").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("x==o    ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("  x==o  ").
                ADoesNotEqualB();

                yield return
                For("o======x").
                And("    x==o").
                ADoesNotEqualB();

                yield return
                For("o==x    ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("  o==x  ").
                And("x======o").
                ADoesNotEqualB();

                yield return
                For("    o==x").
                And("x======o").
                ADoesNotEqualB();
            }
        }

        public static IEnumerable<TestCase> RightOpenAndLeftOpenRangePairs
        {
            get
            {
                yield return
                For("x===o").
                And("     ").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("o===x").
                ADoesNotEqualB();

                yield return
                For("     ").
                And("     ").
                AEqualsB();

                yield return
                For("x===o            ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("  x===o          ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("    x===o        ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("        x===o    ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("          x===o  ").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("            x===o").
                And("      o===x      ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("o===x            ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("  o===x          ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("    o===x        ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("        o===x    ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("          o===x  ").
                ADoesNotEqualB();

                yield return
                For("      x===o      ").
                And("            o===x").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("o==x    ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("  o==x  ").
                ADoesNotEqualB();

                yield return
                For("x======o").
                And("    o==x").
                ADoesNotEqualB();

                yield return
                For("x==o    ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("  x==o  ").
                And("o======x").
                ADoesNotEqualB();

                yield return
                For("    x==o").
                And("o======x").
                ADoesNotEqualB();
            }
        }

        private static IEnumerable<TestCase> Concat(params IEnumerable<TestCase>[] manyTestCases)
        {
            return manyTestCases.SelectMany(testCases => testCases);
        }

        private static TestCaseBuilder For(string rangeA)
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

            public TestCaseBuilder And(string rangeB)
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
