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
        [TestCase("qa", "sq", "si,go,se,cm,so,ph,mt,db,mb,sb,kr,ln,tm,le,av,sm,ar,ci,ca,br,ti,ba,to,ra,fa,yo,ow,sn,ya,cr,po,fe,ho,ma,re,or,rn,au,ur,rh,sr,tc,lt,lo,as,fr,nb,yb,if,pb,ge,th,pm,rb,sh,co,ga,li,ha,hz,no,bi,di,hi,qa,pi,os,uh,wm,an,me,mo,na,la,st,er,sc,ne,mn,mi,am,ex,pt,io,be,fm,ta,tb,ni,mr,pa,he,lr,sq,ye", "")]
        public void Test(string beginWord, string endWord, string wordListString, string expectedString)
        {
            var wordList = wordListString.ToStringArray().ToList();
            var expected = expectedString.ToStringArray('-').Select(list => list.ToStringArray().Where(x => !String.IsNullOrEmpty(x)).ToList()).Where(x => x.Any()).ToList();

            var sut = new Problem0126_WordLadderII();
            var result = sut.FindLadders(beginWord, endWord, wordList);

            var v = result.Select(x => String.Join(",", x));

            Helpers.AssertAreEquivalent(expected, result);
        }

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            var possibleLists = new List<IList<string>>();

            var leafNodes = BuildTree(beginWord, endWord, wordList);

            foreach (var leaf in leafNodes)
            {
                var list = new List<string>();
                var currentNode = leaf;
                while (currentNode != null)
                {
                    list.Insert(0, currentNode.Word);
                    currentNode = currentNode.Parent;
                }
                possibleLists.Add(list);
            }
            var min = possibleLists.Any() ? possibleLists.Min(list => list.Count) : 0;

            return possibleLists.Where(list => list.Count == min).ToList();
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

        private static TreeNode[] BuildTree(string beginWord, string endWord, IList<string> wordList)
        {
            var oneOff = new List<TreeNode>();
            var queue = new List<TreeNode>
            {
                new TreeNode() { Word = beginWord, WordList = wordList.ToArray() }
            };
            var leafNodes = Array.Empty<TreeNode>();

            while (queue.Count > 0)
            {
                var currentNode = queue.First();
                queue.RemoveAt(0);

                if (currentNode.Word == endWord)
                {
                    oneOff.Add(currentNode);
                    continue;
                }

                leafNodes = currentNode
                    .WordList
                    .Where(word => HasOnlyOneDifference(currentNode.Word, word))
                    .Select(word => new TreeNode() { Word = word, Parent = currentNode, WordList = currentNode.WordList.Except(new[]{ word }).ToArray() })
                    .ToArray();

                //var oneOffs = leafNodes.Where(node => HasOnlyOneDifference(node.Word, endWord)).ToArray();
                //if(oneOffs.Any())
                //    return oneOffs;

                queue.AddRange(leafNodes);
                //foreach (var node in leafNodes)
                //{
                //    wordList.Remove(node.Word);
                //}
            }

            return oneOff.ToArray();
        }

        private class TreeNode
        {
            public string Word { get; set; }
            public TreeNode Parent { get; set; }
            public string[] WordList { get; set; } = Array.Empty<string>();

            public override string ToString()
                => Word;
        }
    }
}