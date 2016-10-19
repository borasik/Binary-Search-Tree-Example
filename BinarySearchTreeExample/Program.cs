using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeExample
{
    class Program
    {
        public static List<int> arr = new List<int>() { 1, 20, 3, 0, -5, 1000, 400, -30, 0, -30, 1 };
        static void Main(string[] args)
        {
            BinaryTree binaryTree = new BinaryTree();           
            binaryTree.BuildTree(arr);
            binaryTree.SearchTree(binaryTree.RootNode, 1000);
            //binaryTree.PrintTree(binaryTree.RootNode, arr);
            //binaryTree.FindMin(binaryTree.RootNode);
            binaryTree.DeleteNode(arr, 3);
            binaryTree.DeleteNode(arr, 1000);
            Console.Read();
        }
    }
    public class BinaryTree
    {
        public Node RootNode;
        private List<int> _arr;
        private void CleanTree()
        {
            RootNode = null;
            _arr = null;
        }

        public void BuildTree(List<int> arr)            
        {
            CleanTree();
            _arr = arr;
            foreach (var a in _arr)
            {
                AddNode(RootNode, a);
            }
        }
        public void AddNode(Node node, int input)
        {
            if (RootNode == null)
            {
                RootNode = new Node(input, null);
            }
            else
            {
                if (input < node.Value && node.LeftNode == null)
                {
                    node.LeftNode = new Node(input, node);
                    return;
                }

                if (input > node.Value && node.RightNode == null)
                {
                    node.RightNode = new Node(input, node);
                    return;
                }

                if (input < node.Value)
                {
                    AddNode(node.LeftNode, input);
                }

                if (input > node.Value)
                {
                    AddNode(node.RightNode, input);
                }

                if (input == node.Value)
                {
                    node.DuplicatesCount++;
                    return;
                }
            }
        }

        public Node SearchTree(Node node, int valueToFind)
        {
            Node searchedNode = null;
            if (node == null)
            {
                Console.WriteLine("Value not in the tree");
                return null;
            }            

            if (node.Value == valueToFind)
            {
                Console.WriteLine("Value Found: " + node.Value);
                return node;
            }

            if (node.Value > valueToFind)
            {
                searchedNode = SearchTree(node.LeftNode, valueToFind);
            }
            else if (node.Value <= valueToFind)
            {
                searchedNode= SearchTree(node.RightNode, valueToFind);
            }

            return searchedNode;
        }        

        public Node FindMin(Node node)
        {
            if (node == null)
            {
                Console.WriteLine("Tree is Empty");
                return null;
            }

            if(node.LeftNode == null)
            {               
                return node;
            }

            return FindMin(node.LeftNode);
        }

        public void PrintTree(Node node, List<int> arr)
        {
            if (node == null) return;            

            var minNode = FindMin(node);

            Console.WriteLine(minNode.Value);

            arr.Remove(minNode.Value);

            BuildTree(arr);

            PrintTree(RootNode, arr);
        }

        public void DeleteNode(List<int> arr, int deleteValue)
        {           
            var nodeToDelete = SearchTree(RootNode, deleteValue);           
            if (nodeToDelete.Value < nodeToDelete.ParentNode.Value)
            {
                if (nodeToDelete.LeftNode != null)
                {
                    nodeToDelete.ParentNode.LeftNode = nodeToDelete.LeftNode;
                    nodeToDelete.LeftNode.RightNode = nodeToDelete.RightNode;
                    PrintTreeRecursive(RootNode);
                    return;
                }

                if (nodeToDelete.RightNode != null)
                {
                    nodeToDelete.ParentNode.LeftNode = nodeToDelete.RightNode;
                    nodeToDelete.RightNode.LeftNode = nodeToDelete.LeftNode;
                    PrintTreeRecursive(RootNode);
                    return;
                }

                nodeToDelete.ParentNode.LeftNode = null;
                PrintTreeRecursive(RootNode);
                return;
            }
            if (nodeToDelete.Value > nodeToDelete.ParentNode.Value)
            {
                if (nodeToDelete.LeftNode != null)
                {
                    nodeToDelete.ParentNode.RightNode = nodeToDelete.LeftNode;
                    nodeToDelete.LeftNode.RightNode = nodeToDelete.RightNode;
                    PrintTreeRecursive(RootNode);
                    return;
                }

                if (nodeToDelete.RightNode != null)
                {
                    nodeToDelete.ParentNode.RightNode = nodeToDelete.RightNode;
                    nodeToDelete.RightNode.LeftNode = nodeToDelete.RightNode;
                    PrintTreeRecursive(RootNode);                   
                }

                nodeToDelete.ParentNode.RightNode = null;
                PrintTreeRecursive(RootNode);
                return;
            }            
        }

        public void PrintTreeRecursive(Node node)
        {
            if (RootNode == null)
            {
                Console.Write("Root is NULL");
            }
            if (node == null)
            {
                return;
            }
            Console.Write(node.Value + " ");
            PrintTreeRecursive(node.LeftNode);
            PrintTreeRecursive(node.RightNode);
        }
    }


    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public int Value { get; set; }
        public int DuplicatesCount { get; set; }
        public Node ParentNode { get; set; }
        public Node(int data, Node parentNode)
        {
            Value = data;
            DuplicatesCount = 1;
            ParentNode = parentNode;
        }
    }
}
