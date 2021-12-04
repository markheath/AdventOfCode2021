# Advent of Code 2021

This project contains solutions to the [Advent of Code](https://adventofcode.com/) puzzles for 2021 in C#.

Not sure I'm going to have as much time to devote to this year's puzzles, but this gives me a bit of an excuse to use some of the capabilities of .NET 6 and VS2022.

Some notes

- **Day 1** As usual MoreLINQ turned out to be useful, with the `Pairwise` and `Window` functions idea for this particular problem
- **Day 2** The MoreLINQ `Scan` method is really helpful here, and also a good example of where a C# record is much more readable than using tuples and just as terse
- **Day 3** I made a meal of this one due to a silly error. Also it was the type of problem that could be solved functionally but hurt my brain to do so, so expanded out into more procedural code. Needs tidying up
- **Day 4** A good example of when creating a class can make code much more readable - I made one to represent a bingo grid.