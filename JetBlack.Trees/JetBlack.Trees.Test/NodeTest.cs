using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;

namespace JetBlack.Trees.Test
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void SmokeTest()
        {
            var tree = new MyClass("root")
            {
                new MyClass("A"),
                new MyClass("B")
                {
                    new MyClass("BA"),
                    new MyClass("BB")
                },
                new MyClass("C")
            };

            Assert.IsTrue(new[] { "root", "A", "B", "BA", "BB", "C" }.SequenceEqual(tree.DepthFirstTraverse(x => x.AsEnumerable()).Select(x => x.Name)), "Depth first traversal or tree");
            Assert.IsTrue(new[] { "root", "A", "B", "C", "BA", "BB" }.SequenceEqual(tree.BreadthFirstTraversal(x => x.AsEnumerable()).Select(x => x.Name)), "Breadth first traversal of tree");

            Assert.IsTrue(new[] { "A", "BA", "BB", "C" }.SequenceEqual(tree.DepthFirstTraverse(x => x.AsEnumerable()).Where(x => !x.AsEnumerable().Any()).Select(x => x.Name)), "Depth first traversal of leaves");
            Assert.IsTrue(new[] { "A", "C", "BA", "BB" }.SequenceEqual(tree.BreadthFirstTraversal(x => x.AsEnumerable()).Where(x => !x.AsEnumerable().Any()).Select(x => x.Name)), "Breadth first traversal of leaves");

            var json = JsonConvert.SerializeObject(tree);
            var roundTrip = JsonConvert.DeserializeObject<MyClass>(json);
            Assert.IsTrue(tree.DepthFirstTraverse(x => x.AsEnumerable()).Select(x => x.Name).SequenceEqual(roundTrip.DepthFirstTraverse(x => x.AsEnumerable()).Select(x => x.Name)), "Round trip serialization");
        }

        public class MyClass : Node<MyClass>
        {
            public MyClass(string name)
            {
                Name = name;
            }

            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name},{base.ToString()}";
            }
        }
    }
}
