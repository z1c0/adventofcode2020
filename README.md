# Advent of Code 2020

https://adventofcode.com/2020

[Day 10](https://adventofcode.com/2020/day/10) was really hard for me.
I found a very cumbersome solution with exponential runtime.
I head to cheat a little bit and check for an answer on the internet and found
this impressive solution on  [YouTube](https://www.youtube.com/watch?v=cE88K2kFZn0).
The answer was to use recursion and dynamic programming (memoization) to get the
complexity down to O(n*n).

The second part of [Day 13](https://adventofcode.com/2020) was also too tough for me.
I found a solution for the simple examples, but for the puzzle input the runtime
was too bad I my program would not finish.
After researching the issue on the internet, I found solutions mentioning the
[Chinese Remainder Theorem](https://en.wikipedia.org/wiki/Chinese_remainder_theorem)
which is probably the correct number theory approach. I did not bother to understand
that in detail yet to be honest since I also found this very compact and elagant
[solution](https://github.com/Chrinkus/advent-of-code-2020/blob/main/src/day13.cpp)
that I "borrowed" for my solution then.
