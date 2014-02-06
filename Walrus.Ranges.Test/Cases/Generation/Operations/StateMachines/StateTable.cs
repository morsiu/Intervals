// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Cases.Generation.Operations.StateMachines
{
    internal sealed class StateTable : IReadOnlyCollection<State>
    {
        private static readonly PointTypeMatcher _matcher = new PointTypeMatcher('-', '=', 'x', 'o');
        private readonly HashSet<State> _states = new HashSet<State>();
        private PointType[] _columns;

        public StateTable AssumingHeader(char firstColumn, char secondColumn, char thirdColumn, char fourthColumn)
        {
            _columns = Match(firstColumn, secondColumn, thirdColumn, fourthColumn).ToArray();
            return this;
        }

        public StateTable AppendRow(char row, char firstColumn, char secondColumn, char thirdColumn, char fourthColumn)
        {
            var outputs = Match(firstColumn, secondColumn, thirdColumn, fourthColumn);
            var secondInput = _matcher.Match(row).Value;
            var index = 0;
            foreach (var output in outputs)
            {
                var firstInput = _columns[index];
                _states.Add(new State(firstInput, secondInput, output));
                ++index;
            }
            return this;
        }
    
        public int Count
        {
            get { return _states.Count; }
        }

        public IEnumerator<State> GetEnumerator()
        {
            return _states.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _states.GetEnumerator();
        }

        private static IEnumerable<PointType> Match(params char[] columns)
        {
            var values =
                from column in columns
                let value = _matcher.Match(column).Value
                select value;
            return values;
        }
    }
}
