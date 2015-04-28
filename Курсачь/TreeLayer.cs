using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Курсачь
{
    internal class TreeLayer
    {
        private Tree root = new Tree();
        private List<WordStruct> foundWordList = new List<WordStruct>();

        private struct WordStruct
        {
            public float rating;
            public string word;
        }

        public List<string> getFoundWord(int n = 10)
        {
            foundWordList.Sort((a, b) => b.rating.CompareTo(a.rating));
            if (foundWordList.Count > n)
            {
                foundWordList.RemoveRange(n, foundWordList.Count - n);
            }

            List<string> wordList = new List<string>();
            foreach (var item in foundWordList)
            {
                wordList.Add(item.word);
            }
            return wordList;
        }

        public void searchWord(string word, int maxDist = 3)
        {
            foundWordList.Clear();
            string foundWord = "";
            searchWordTree(root, word, 0, foundWord, 0, maxDist);
            if (foundWordList.Count < 20)
            {
                foundWordList.Clear();
                foundWord = "";
                searchWordTree(root, word, 0, foundWord, 0, maxDist+12);
            }
        }

        private void searchWordTree(Tree node, string seekWord, int seekIndex, string foundWord, int dist, int maxDist)
        {
            if (seekIndex < seekWord.Length)
            {
                char i = seekWord[seekIndex++];
                if (node.child.ContainsKey(i))
                {
                    foundWord += i;
                    searchWordTree(node.child[i], seekWord, seekIndex, foundWord, dist, maxDist);
                }
            }
            else
            {
                if(dist++ > maxDist) return;
                if (node.rating > 0)
                {
                    WordStruct temp = new WordStruct();
                    temp.word = new string(foundWord.ToCharArray());
                    temp.rating = (float) node.rating/(float) dist;

                    foundWordList.Add(temp);
                }

                foreach (var item in node.child)
                {
                    foundWord += item.Key;
                    searchWordTree(item.Value, seekWord, seekIndex, foundWord, dist, maxDist);
                    foundWord = foundWord.Remove(foundWord.Length - 1);
                }
            }
        }

        public void addWord(string word, int rating = 1)
        {            
            addWordTree(root, word, 0, rating);
        }

        private void addWordTree(Tree node, string addedWord, int addedIndex, int rating)
        {
            char i = addedWord[addedIndex];
            if (!node.child.ContainsKey(i))
            {
                node.child.Add(i, new Tree());
            }
            node = node.child[i];

            if (++addedIndex >= addedWord.Length)
            {
                node.rating += rating;
                return;
            }
            addWordTree(node, addedWord, addedIndex, rating);
        }

        private List<short> convertWordToList(string word)
        {
            List<short> resWord = new List<short>(word.Length);
            foreach (char item in word.ToLower())
            {
                if (item <= 'z')
                    resWord.Add((short) (item - 'a'));
                else
                    resWord.Add((short) Math.Min(32, (item - 'а')));
            }
            return resWord;
        }

        private string convertListToWord(List<short> arg)
        {
            string word = "";
            foreach (short item in arg)
            {
                if (item == 32)
                    word += 'ё';
                else
                    word += (char) (item + 'а');
            }
            return word;
        }
    }

    internal class Tree
    {
        public Dictionary<char,Tree> child = new Dictionary<char, Tree>();
        public int rating=0;
    }
}
