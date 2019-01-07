﻿//-----------------------------------------------------------------------
// <copyright file="VertexConnectionInfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRA.ClientLibrary.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Definition for VertexConnectionInfo
    /// </summary>
    public struct VertexConnectionInfo
    {
        public VertexConnectionInfo(
            string fromVertex,
            string fromEndpoint,
            string toVertex,
            string toEndpoint)
        {
            FromVertex = fromVertex;
            FromEndpoint = fromEndpoint;
            ToVertex = toVertex;
            ToEndpoint = toEndpoint;
        }

        public string FromVertex { get; }

        public string FromEndpoint { get; }

        public string ToVertex { get; }

        public string ToEndpoint { get; }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "FromVertex '{0}', FromEndpoint '{1}', ToVertex '{2}', ToEndpoint '{3}'",
                FromVertex,
                FromEndpoint,
                ToVertex,
                ToEndpoint);
        }

        public override bool Equals(object obj)
        {
            ConnectionTable other = obj as ConnectionTable;
            return other != null
                && this.FromEndpoint == other.FromEndpoint
                && this.FromVertex == other.FromVertex
                && this.ToEndpoint == other.ToEndpoint
                && this.ToVertex == other.ToVertex;
        }

        public override int GetHashCode()
        {
            return 
                this.FromVertex.GetHashCode()
                ^ (this.FromEndpoint.GetHashCode() << 1)
                ^ (this.ToVertex.GetHashCode()) << 2
                ^ (this.ToEndpoint.GetHashCode() << 3);
        }

        public static bool operator ==(VertexConnectionInfo left, VertexConnectionInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(VertexConnectionInfo left, VertexConnectionInfo right)
        {
            return !(left == right);
        }
    }
}
