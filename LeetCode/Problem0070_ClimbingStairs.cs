using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Easy
    public class Problem0070_ClimbingStairs
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 5)]
        [TestCase(5, 8)]
        public void Test(int steps, int expected)
        {
            var sut = new Problem0070_ClimbingStairs();
            var result = sut.ClimbStairs(steps);
            Assert.AreEqual(expected, result);
        }

        public int ClimbStairs(int steps)
        {
            var stepCounts = new int[steps];
            if (steps > 0)
                stepCounts[0] = 1;
            if (steps > 1)
                stepCounts[1] = 2;
            for (var i = 2; i < steps; i++)
            {
                stepCounts[i] = stepCounts[i - 1] + stepCounts[i - 2];
            }
            return stepCounts.Last();
        }

        //private readonly IDictionary<int, Step> _stepDictionary = new Dictionary<int, Step>();

        //public int ClimbStairs(int stairs)
        //{
        //    for (var i = 1; i <= stairs; i++)
        //    {
        //        if(!_stepDictionary.ContainsKey(i))
        //        {
        //            var previous = i > 1 ? _stepDictionary[i - 1] : null;
        //            var previous2 = i > 2 ? _stepDictionary[i - 2] : null;
        //            var step = Step.Create(i, previous, previous2);
        //            _stepDictionary.Add(i, step);
        //        }
        //    }

        //    return _stepDictionary[stairs].Options.Length;
        //}

        //private class Step
        //{
        //    public int Steps { get; }
        //    public string[] Options { get; }

        //    private Step(int steps, string[] options)
        //    {
        //        Steps = steps;
        //        Options = options;
        //    }

        //    public static Step Create(int steps, Step? previous, Step? previous2)
        //    {
        //        var newSteps = new List<string>();

        //        newSteps.AddRange(GetNewSteps(previous, ToAdd.One));
        //        if (steps >= 2)
        //            newSteps.AddRange(GetNewSteps(previous2, ToAdd.Two));

        //        return new Step(steps, newSteps.Distinct().ToArray());
        //    }

        //    private static IEnumerable<string> GetNewSteps(Step previousStep, ToAdd toAdd)
        //    {
        //        var valuesToAdd = toAdd == ToAdd.One
        //            ? new[] { "1" }
        //            : new[] { "2" };

        //        if (previousStep == null)
        //            previousStep = new Step(0, new[] { "" });

        //        foreach (var option in previousStep.Options)
        //        {
        //            foreach (var valueToAdd in valuesToAdd)
        //            {
        //                //yield return $"{valueToAdd}{option}";
        //                yield return $"{option}{valueToAdd}";
        //            }
        //        }
        //    }
        //}

        //private enum ToAdd
        //{
        //    One,
        //    Two
        //}

        // 4 = 1-1-1-1, 1-1-2, 1-2-1, 2-1-1, 2-2
        // 5 = 1-1-1-1-1, 1-1-1-2, 1-1-2-1, 1-2-1-1, 2-1-1-1, 1-2-2, 2-1-2, 2-2-1
        // 6 = 1-1-1-1-1-1, 1-1-1-1-2, 1-1-1-2-1, 1-1-2-1-1, 1-2-1-1-1, 2-1-1-1-1, 1-1-2-2, 1-2-1-2, 2-1-1-2, 1-2-2-1, 2-1-2-1, 2-2-1-1, 2-2-2
        // all of previous 1 with 1 added, all of previous 2 with 2 added
    }
}