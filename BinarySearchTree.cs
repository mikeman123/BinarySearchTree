/**********************************************************************
 * Project:     Binary Search Tree
 * File:        Binary Search Tree Class
 * Language:    C#
 * 
 * Description: This class implements a simple binary search tree.
 *              
 * College:     Husson University
 * Course:      IT 325
 * 
 * Edit History
 * Ver  Who Date        Notes
 * ---  ------------    -----------------------------------------------
 * 0.1  MJD 02/24/24    - initial writing
 * 0.2  MJD 02/25/24    - finished code for Add routine
 *                      - added inorder traversal of the tree
 * 0.3  MJD 02/26/24    - finished code for searchWord routine
 *                      - finished code for process file routine
 *                      - Added a clearTree routine 
 **********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace BinarySearchTreeProject
{
    internal class BinarySearchTree
    {
        #region enum
        public enum Traversal : Byte
        {
            InOrder
        }
        #endregion enum
        #region data
        #endregion data

        #region properties
        public Node Root { get; set; } // root of the BST
        public int Count { get; set; }
        #endregion properties

        #region constructor
        public BinarySearchTree()
        {
            Root = null;
            Count = 0;
        }
        #endregion constructor

        #region events
        #endregion events

        #region methods
        /// <summary>
        ///  Adds a word to the binary search tree.
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(string word)
        {
            Root = Add(Root, word);
        }

        /// <summary>
        /// This method clears the binary search tree
        /// </summary>
        public void ClearTree()
        {
            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Inserts a node with the given key into the binary search tree.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns>The root of the updated subtree</returns>
        private Node Add(Node root, string key)
        {
            // If root is null, create a new node 
            if (root == null)
            {
                Count++;
                return new Node(key);
            }

            int comparison = string.Compare(key, root.Key);
            if (comparison < 0)
                root.LeftChild = Add(root.LeftChild, key);
            else if (comparison > 0)
                root.RightChild = Add(root.RightChild, key);
            else
                root.Count++; // Increment count for existing word

            return root;
        }

        /// <summary>
        /// Searches for a word in the binary search tree and returns its count if found.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="count"></param>
        /// <returns>True if the word is found, otherwise false.</returns>
        public bool SearchWord(string word, out int count)
        {
            count = 0;
            Node current = Root;
            while (current != null)
            {
                int comparison = string.Compare(word, current.Key);
                if (comparison == 0)
                {
                    count = current.Count;
                    return true;
                }
                else if (comparison < 0)

                    current = current.LeftChild;
                else
                    current = current.RightChild;
            }
            return false;
        }

        /// <summary>
        /// Processes a text file, adding each word to the binary search tree.
        /// </summary>
        /// <param name="filePath"></param>
        public void ProcessTextFile(string filePath)
        {
            // Read the file and process each line
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    AddWord(word);
                }
            }
        }

        /// <summary>
        /// this method will search the binary search tree using a in order traversal
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> InOrderTraversal()
        {
            foreach (Node node in InOrderTraversal(this.Root))
            {
                yield return node;
            }
        }

        private IEnumerable<Node> InOrderTraversal(Node parent)
        {
            if (parent != null)
            {
                // handle the left subtree 
                foreach (Node node in InOrderTraversal(parent.LeftChild))
                {
                    yield return node;
                }
                //handle the parent
                yield return parent;
                //handle the right subtree
                foreach (Node node in InOrderTraversal(parent.RightChild))
                {
                    yield return node;
                }

            }
        } 
        #endregion methods
    }
}

