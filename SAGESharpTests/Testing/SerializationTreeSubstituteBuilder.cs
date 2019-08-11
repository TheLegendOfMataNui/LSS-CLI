﻿/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
using NSubstitute;
using SAGESharp.IO;
using System;
using System.Collections.Generic;

namespace SAGESharp.Testing
{
    static class BuilderFor
    {
        public sealed class DataNodeSubstitute<T>
        {
            public List<IEdge> Edges { get; set; } = new List<IEdge>();

            public IDataNode Build(Action<IDataNode> setup)
            {
                IDataNode node = Substitute.For<IDataNode>();

                node.Type.Returns(typeof(T));
                node.Edges.Returns(Edges);

                setup?.Invoke(node);

                return node;
            }
        }

        public sealed class ListNodeSubstitute<T>
        {
            public IDataNode ChildNode { get; set; }

            public IListNode Build(Action<IListNode> setup)
            {
                IListNode node = Substitute.For<IListNode>();

                node.Type.Returns(typeof(IList<T>));
                node.ChildNode.Returns(ChildNode);

                node.GetListCount(Arg.Any<IList<T>>()).Returns(args =>
                {
                    IList<T> list = (IList<T>)args[0];

                    return list.Count;
                });
                node.GetListEntry(Arg.Any<IList<T>>(), Arg.Any<int>()).Returns(args =>
                {
                    IList<T> list = (IList<T>)args[0];
                    int index = (int)args[1];

                    return list[index];
                });

                setup?.Invoke(node);

                return node;
            }
        }

        public sealed class EdgeSubstitute
        {
            public INode ChildNode { get; set; }

            public IEdge Build(Action<IEdge> setup)
            {
                IEdge edge = Substitute.For<IEdge>();

                edge.ChildNode.Returns(ChildNode);

                setup?.Invoke(edge);

                return edge;
            }
        }
    }
}
