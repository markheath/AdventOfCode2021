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
- **Day 15** I picked the wrong algorithm today and even with optimizations part 1 was never going to be comepleted in time. I needed a hint if I was to finish in my available time, and the elegant solution involves tracking a grid of the shortest route to every point. Part 2 was thankfully relatively straightforward after that
- **Day 16** I liked today's problem even though it took me a while to code, and I really should have made a custom bitstream which I did as a refactoring later. Would make for a good coding interview question.
- **Day 17** I made a meal of it today and ended up trying to optimise the search space with binary searches. Turns out it didn't need that level of optimization, and a simpler approach helped me chase out the bugs. Took inspiration from another solution for the final form of my answer.
- **Day 18** I spent way too long on the parsing and tree structure, and so realised I was going to run out of time. In the end I decided to port an elegant Python solution to C# which took a while and still left me with quite a lot to do as I needed my own parser and probably needed my own Permutations
- **Day 19** An epic fail for me. While I was pretty confident I knew the algorithm to solve this, I also knew it was going to take me far too long to implement due to my very rusty skills in 3D rotations. I decided it was going to be a case of learning from someone else's solution
- **Day 20** Finally back on track able to solve the problem on my own. A hashset of set pixels was an obvious way to efficiently store infinite images. The really nasty trick on this was realising that out of bounds pixels were flashing on and off, so made a custom infinite grid class to simplify my code. Thankfully Part 1's solution was already optimised well enough for part 2