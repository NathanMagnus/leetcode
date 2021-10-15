using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    // Time: 
    public class Problem0126_WordLadderII
    {
        [Test]
        [TestCase("hit", "cog", "hot,dot,dog,lot,log,cog", "hit,hot,dot,dog,cog-hit,hot,lot,log,cog")]
        [TestCase("hit", "cog", "hot,dot,dog,lot,log", "")]
        [TestCase("a", "c", "a,b,c", "a,c")]
        [TestCase("qa", "sq", "si,go,se,cm,so,ph,mt,db,mb,sb,kr,ln,tm,le,av,sm,ar,ci,ca,br,ti,ba,to,ra,fa,yo,ow,sn,ya,cr,po,fe,ho,ma,re,or,rn,au,ur,rh,sr,tc,lt,lo,as,fr,nb,yb,if,pb,ge,th,pm,rb,sh,co,ga,li,ha,hz,no,bi,di,hi,qa,pi,os,uh,wm,an,me,mo,na,la,st,er,sc,ne,mn,mi,am,ex,pt,io,be,fm,ta,tb,ni,mr,pa,he,lr,sq,ye", "")]
        public void Test(string beginWord, string endWord, string wordListString, string expectedString)
        {
            var wordList = wordListString.ToStringArray().ToList();
            var expected = expectedString.ToStringArray('-').Select(list => list.ToStringArray().Where(x => !String.IsNullOrEmpty(x)).ToList()).Where(x => x.Any()).ToList();

            var sut = new Problem0126_WordLadderII();
            var result = sut.FindLadders(beginWord, endWord, wordList).Where(x => x.Any());

            var v = result.Select(x => String.Join(",", x));

            Assert.True(Helpers.AreEquivalent(expected, result));
        }

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            if (!wordList.Contains(endWord))
                return new List<IList<string>>();

            // case where begin is end
            if (beginWord == endWord)
                return new List<IList<string>>()
                {
                    new List<string>()
                    {
                        beginWord
                    }
                };

            var root = BuildTree(beginWord, endWord, wordList);

            var ladders = GetLadders(root, beginWord);
            var list = RemoveDuplicates(ladders);
            // construct the ladders
            return list.Select(x => (IList<string>)x.Select(y => y.Word).ToList()).ToList();
        }

        private IEnumerable<List<TreeNode>> RemoveDuplicates(IEnumerable<List<TreeNode>> ladders)
        {
            return ladders;
        }

        private IEnumerable<List<TreeNode>> GetLadders(TreeNode endNode, string beginWord)
        {
            var alreadyReturned = new HashSet<TreeNode>();
            var results = new List<List<TreeNode>>();
            var queue = new List<(int Depth, List<TreeNode> Nodes)>();
            queue.Add((1, new List<TreeNode>() { endNode }));

            var selectedMin = int.MaxValue;

            while (queue.Count > 0)
            {
                var currentTuple = queue[0];
                queue.RemoveAt(0);

                if (currentTuple.Depth > selectedMin)
                {
                    continue;
                }

                if (currentTuple.Nodes.Last().Word == beginWord)
                {
                    selectedMin = currentTuple.Depth;

                    alreadyReturned.Add(currentTuple.Nodes.Last());
                    currentTuple.Nodes.Reverse();
                    yield return currentTuple.Nodes;

                    continue;
                }


                queue.AddRange(currentTuple
                    .Nodes
                    .Last()
                    .Parents
                    .Select(n =>
                    {
                        var newList = currentTuple.Nodes.ToList();
                        newList.Add(n);
                        return (currentTuple.Depth + 1, newList);
                    }));
            }
        }

        private static bool HasOnlyOneDifference(string currentWord, string word)
        {
            var count = 0;
            for (var i = 0; i < currentWord.Length; i++)
            {
                if (currentWord[i] != word[i])
                    count++;
                if (count > 1)
                    return false;
            }
            return true;
        }

        private static TreeNode BuildTree(string beginWord, string endWord, IList<string> wordList)
        {
            wordList.Insert(0, beginWord);
            var dictionary = wordList.Distinct().ToDictionary(k => k, v => new TreeNode() { Word = v });

            for (var i = wordList.Count - 1; i >= 0; i--)
            {
                var currentWord = wordList[i];
                for (var j = i - 1; j >= 0; j--)
                {
                    if (HasOnlyOneDifference(currentWord, wordList[j]))
                        dictionary[currentWord].Parents.Add(dictionary[wordList[j]]);
                }
            }
            return dictionary[endWord];
        }

        //private static TreeNode[] BuildTree(string beginWord, string endWord, IList<string> wordList)
        //{
        //    var oneOff = new List<TreeNode>();
        //    var queue = new List<TreeNode>
        //    {
        //        new TreeNode() { Word = beginWord, WordList = wordList.ToArray() }
        //    };
        //    var leafNodes = Array.Empty<TreeNode>();

        //    while (queue.Count > 0)
        //    {
        //        var currentNode = queue.First();
        //        queue.RemoveAt(0);

        //        if (currentNode.Word == endWord)
        //        {
        //            oneOff.Add(currentNode);
        //            continue;
        //        }

        //        leafNodes = currentNode
        //            .WordList
        //            .Where(word => HasOnlyOneDifference(currentNode.Word, word))
        //            .Select(word => new TreeNode() { Word = word, Parent = currentNode, WordList = currentNode.WordList.Except(new[]{ word }).ToArray() })
        //            .ToArray();

        //        //var oneOffs = leafNodes.Where(node => HasOnlyOneDifference(node.Word, endWord)).ToArray();
        //        //if(oneOffs.Any())
        //        //    return oneOffs;

        //        queue.AddRange(leafNodes);
        //        //foreach (var node in leafNodes)
        //        //{
        //        //    wordList.Remove(node.Word);
        //        //}
        //    }

        //    return oneOff.ToArray();
        //}

        private class TreeNode
        {
            public string Word { get; set; }
            public IList<TreeNode> Parents { get; set; } = new List<TreeNode>();

            public override string ToString()
                => Word;
        }
    }
}