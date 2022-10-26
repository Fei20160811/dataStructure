using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dataStructure.Controllers
{
    [Route("api/[controller]")]
    public class BinaryTreeController : Controller
    {
        string preOrderStr = "";
        string inOrderStr = "";
        string postOrderStr = "";
        string levelOrderStr = "";

        [HttpGet]
        public IActionResult Get()
        {
            return Json("Welcome to the BST World");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] QueryCondition queryCondition)
        {
            BinarySearchTree tree = new BinarySearchTree();
            string[] strArr = queryCondition.queryStr.Split(' ');

            foreach (string value in strArr)
            {
                tree.create(int.Parse(value));
            }

            TreeResult result = new TreeResult();
            result.preOrderResultStr = PreOrder(tree.root);
            result.inOrderResultStr = InOrder(tree.root);
            result.postOrderResultStr = PostOrder(tree.root);
            result.levelOrderResultStr = LevelOrder(tree.root);

            return Json(result);
        }

        //InOrder DLR
        public string PreOrder(Node root)
        {
            preOrderStr += ((preOrderStr != "") ? " " : "") + root.info.ToString();

            if (root.left != null)
            {
                PreOrder(root.left);
            }

            if (root.right != null)
            {
                PreOrder(root.right);
            }

            return preOrderStr;
        }

        //InOrder LDR
        public string InOrder(Node root)
        {
            if (root.left != null)
            {
                InOrder(root.left);
            }

            inOrderStr += ((inOrderStr != "") ? " " : "") + root.info.ToString();

            if (root.right != null)
            {
                InOrder(root.right);
            }

            return inOrderStr;
        }

        //PostOrder LRD
        public string PostOrder(Node root)
        {
            if (root.left != null)
            {
                PostOrder(root.left);
            }

            if (root.right != null)
            {
                PostOrder(root.right);
            }

            postOrderStr += ((postOrderStr != "") ? " " : "") + root.info.ToString();

            return postOrderStr;
        }

        //LevelOrder BSF
        public string LevelOrder(Node root)
        {
            levelOrderStr = root.info.ToString();

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                root = q.Dequeue();
                if (root.left != null)
                {
                    levelOrderStr += " " + root.left.info.ToString();
                    q.Enqueue(root.left);
                }

                if (root.right != null)
                {
                    levelOrderStr += " " + root.right.info.ToString();
                    q.Enqueue(root.right);
                }
            }

            return levelOrderStr;
        }
    }

    public class QueryCondition
    {
        public string queryStr { get; set; }
    }

    public class TreeResult
    {
        public string preOrderResultStr { get; set; }
        public string inOrderResultStr { get; set; }
        public string postOrderResultStr { get; set; }
        public string levelOrderResultStr { get; set; }
    }

    public class Node
    {
        public int info;
        public Node left;
        public Node right;

        public Node(int info)
        {
            this.info = info;
        }
    }

    public class BinarySearchTree
    {
        public Node root;

        public void create(int val)
        {
            if (this.root == null)
            {
                this.root = new Node(val);
            }
            else
            {
                Node current = this.root;
                while (true)
                {
                    if (val < current.info)
                    {
                        if (current.left == null)
                        {
                            current.left = new Node(val);
                            break;
                        }
                        else
                        {
                            current = current.left;
                        }
                    }
                    else if (val > current.info)
                    {
                        if (current.right == null)
                        {
                            current.right = new Node(val);
                            break;
                        }
                        else
                        {
                            current = current.right;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }
    }
}
