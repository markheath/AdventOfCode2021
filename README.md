# Advent of Code 2021

This project contains solutions to the [Advent of Code](https://adventofcode.com/) puzzles for 2021 in C#.

Not sure I'm going to have as much time to devote to this year's puzzles, but this gives me a bit of an excuse to use some of the capabilities of .NET 6 and VS2022.

Some notes

- **Day 1** As usual MoreLINQ turned out to be useful, with the `Pairwise` and `Window` functions idea for this particular problem
- **Day 2** The MoreLINQ `Scan` method is really helpful here, and also a good example of where a C# record is much more readable than using tuples and just as terse
- **Day 3** I made a meal of this one due to a silly error. Also it was the type of problem that could be solved functionally but hurt my brain to do so, so expanded out into more procedural code. Needs tidying up
- **Day 4** A good example of when creating a class can make code much more readable - I made one to represent a bingo grid. It also proved helpful tracking down a bug as I could override ToString and see what was going on.
- **Day 5** A relatively kind one for which I made myself a generic grid helper class (should have done that yesterday). My diagonal line points algorithm is ugly - needs refactoring!
- **Day 6** I feared this would be a performance optimization that made my head explode, but actually it was a nice use case for memoization
- **Day 7** Part 2 could be optimized further (memoization would probably be sensible) but it didn't run too slow, so was able to get away without it
- **Day 8** Was fairly easy to do part 2 by allowing a cost calculation function to be injected into the part 1 solution
- **Day 9** Was finally the catalyst to improve the grid I made on day 5 and introduce a coordinate class based on one I created last year. A nice recursive solution using hashsets to track the points already found in the basin.
- **Day 10** A relatively kind challenge that could be solved with a stack. My main mistake was not using longs for part 2 so getting integer arithmetic overflow!
- **Day 11** The grid class is proving very helpful, and another opportunity for some recursion today
- **Day 12** A nice opportunity to build a graph and write a recursive depth first algorithm. Could be optimised a bit, but using a string to track route history worked reasonably well.
- **Day 13** Another opportunity to improve my Grid helper a bit and make it resizable and able to print itself out. For part 2, doing OCR or letter recognition would have been nice but time did not permit!
- **Day 14** A bit of a challenge today. Part 1 was nice and easy, but part 2 required a rethink for performance reasons. My code was getting very ugly, but was hugely simplified by creating a C# equivalent of the Python counter class